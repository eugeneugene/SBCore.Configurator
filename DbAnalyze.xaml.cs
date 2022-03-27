using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Hosting;
using SBCore.Configurator.Data;
using SBCore.Configurator.Models;
using SBCore.Configurator.TextResources;
using SBShared.Types;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace SBCore.Configurator
{
    internal partial class DbAnalyze : Window, INotifyPropertyChanged
    {
        private readonly ObservableCollection<TableStatus> _dbStatus = new();

        private readonly Connection _connection;
        private readonly IServiceProvider _serviceProvider;
        private readonly IHostApplicationLifetime _applicationLifetime;

        public event PropertyChangedEventHandler PropertyChanged;

        private readonly List<SqliteIndex> sqliteIndices = new();

        public string DbName { get; set; }
        public string OpenMode { get; set; }

        private bool _canread;
        private bool _canwrite;

        public bool CanRead
        {
            get => _canread;
            private set
            {
                _canread = value;
                OnPropertyChanged(nameof(CanRead));
            }
        }
        public bool CanWrite
        {
            get => _canwrite;
            private set
            {
                _canwrite = value;
                OnPropertyChanged(nameof(CanWrite));
            }
        }

        public DbAnalyze(Connection connection, IServiceProvider serviceProvider, IHostApplicationLifetime applicationLifetime)
        {
            InitializeComponent();
            _connection = connection;
            _serviceProvider = serviceProvider;
            _applicationLifetime = applicationLifetime;
            _applicationLifetime.ApplicationStopping.Register(() => Close());

            CollectionViewSource itemCollectionViewSource;
            itemCollectionViewSource = (CollectionViewSource)FindResource("ItemCollectionViewSource");
            itemCollectionViewSource.Source = _dbStatus;

            DataContext = this;
        }

        private async void WindowLoaded(object sender, RoutedEventArgs e)
        {
            await LoadDataAsync(_applicationLifetime.ApplicationStopping);
        }

        private void WindowClosed(object sender, EventArgs e)
        {
            if (_connection.SqliteConnection.State == ConnectionState.Open)
                _connection.SqliteConnection.Close();
        }

        private async Task LoadDataAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (string.IsNullOrWhiteSpace(DbName))
            {
                StatusBlock.Text = Russian.DatabaseNoName;
                StatusBlock.ToolTip = string.Empty;
                return;
            }

            SqliteOpenMode sqliteOpenMode;
            try
            {
                var connectionStringBuilder = (SqliteConnectionStringBuilder)SqliteFactory.Instance.CreateConnectionStringBuilder();
                connectionStringBuilder.DataSource = DbName;
                if (Enum.TryParse(OpenMode, out sqliteOpenMode))
                    connectionStringBuilder.Mode = sqliteOpenMode;

                _connection.SqliteConnection.ConnectionString = connectionStringBuilder.ToString();
                await _connection.SqliteConnection.OpenAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                StatusBlock.Text = ex.Message;
                StatusBlock.ToolTip = ex.ToString();
                return;
            }

            if (_connection.SqliteConnection.State == ConnectionState.Open)
            {
                CanRead = true;
                if (sqliteOpenMode == SqliteOpenMode.ReadWrite || sqliteOpenMode == SqliteOpenMode.ReadWriteCreate)
                    CanWrite = true;

                if (!await LoadIndicesAsync(cancellationToken))
                {
                    StatusBlock.Text = Russian.IndicesReadError;
                    StatusBlock.ToolTip = string.Empty;
                    return;
                }

                StatusBlock.Text = Russian.DatabaseOk;
                StatusBlock.ToolTip = string.Empty;

                foreach (var table in StaticResources.SBCoreTableMap.Keys)
                {
                    if (!await TableExistsAsync(table, cancellationToken))
                        _dbStatus.Add(new TableStatus(tableName: table, status: TableStatusReason.MissingTable, comment: Russian.TableNotFound, writable: CanWrite, userFilled: StaticResources.SBCoreTableMap[table].UserFilled));
                    else
                    {
                        var scheme = await GetTableSchemeAsync(table, cancellationToken);
                        if (scheme is null)
                            _dbStatus.Add(new TableStatus(tableName: table, status: TableStatusReason.Other, comment: Russian.TableNoScheme, writable: CanWrite, userFilled: StaticResources.SBCoreTableMap[table].UserFilled));
                        else
                        {
                            TableStatus status = await CheckTableSchemeAsync(table, scheme, cancellationToken);
                            if (status.Status == TableStatusReason.Ok)
                            {
                                if (StaticResources.SBCoreIndexMap.ContainsKey(table))
                                {
                                    var tableIndices = StaticResources.SBCoreIndexMap[table];
                                    foreach (var value in tableIndices)
                                    {
                                        var sql = sqliteIndices.SingleOrDefault(item => item.Name == value.Item1);
                                        if (sql is null)
                                        {
                                            status.Status |= TableStatusReason.MissingIndex;
                                            status.Comment = Russian.InvalidTableStructure;
                                            status.Details.Add(string.Format(Russian.MissingIndexA1, value.Item1));
                                        }
                                        else
                                        {
                                            if (!CompareSQL(sql.Sql, value.Item2))
                                            {
                                                status.Status |= TableStatusReason.InvalidIndex;
                                                status.Comment = Russian.InvalidTableStructure;
                                                status.Details.Add(string.Format(Russian.InvalidIndexA1, value.Item1));
                                            }
                                        }
                                    }
                                }
                            }

                            _dbStatus.Add(status);
                        }
                    }
                }
            }
            else
            {
                StatusBlock.Text = Russian.DatabaseNotOpen;
                StatusBlock.ToolTip = string.Empty;
            }

            return;
        }

        private static bool CompareSQL(string str1, string str2)
        {
            return RemoveSpaces(str1) == RemoveSpaces(str2);
        }

        private static string RemoveSpaces(string str)
        {
            return new string(str.Where(c => !char.IsWhiteSpace(c)).Select(c => char.ToUpperInvariant(c)).ToArray());
        }

        private async Task<bool> TableExistsAsync(string tableName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (string.IsNullOrWhiteSpace(tableName) || _connection.SqliteConnection.State != ConnectionState.Open)
                return false;

            using var command = new SqliteCommand(SQLite.SqlTableExist, _connection.SqliteConnection);
            command.Parameters.Add(new SqliteParameter("tablename", SqliteType.Text) { Value = tableName, });
            var reader = await command.ExecuteScalarAsync(cancellationToken);
            return reader is not null && reader.GetType() == typeof(long) && (long)reader > 0L;
        }

        private async Task<DataTable> GetTableSchemeAsync(string tableName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (string.IsNullOrWhiteSpace(tableName) || _connection.SqliteConnection.State != ConnectionState.Open)
                return null;

            using var command = new SqliteCommand(string.Format(CultureInfo.InvariantCulture, SQLite.SqlTableScheme, tableName), _connection.SqliteConnection);
            var reader = await command.ExecuteReaderAsync(CommandBehavior.SchemaOnly, cancellationToken);
            if (reader is null)
                return null;

            return reader.GetSchemaTable();
        }

        private async Task<long> GetTableRecordCountAsync(string tableName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (string.IsNullOrWhiteSpace(tableName) || _connection.SqliteConnection.State != ConnectionState.Open)
                return -1;

            using var command = new SqliteCommand(string.Format(CultureInfo.InvariantCulture, SQLite.SqlRecordCount, tableName), _connection.SqliteConnection);
            var reader = await command.ExecuteScalarAsync(cancellationToken);
            if (reader is null)
                return -1;

            return Convert.ToInt64(reader);
        }

        private async Task<TableStatus> CheckTableSchemeAsync(string tableName, DataTable scheme, CancellationToken cancellationToken)
        {
            var status = new TableStatus(
              tableName: tableName,
              status: TableStatusReason.Ok,
              comment: string.Empty,
              writable: CanWrite,
              userFilled: StaticResources.SBCoreTableMap[tableName].UserFilled);

            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                if (string.IsNullOrWhiteSpace(tableName) || scheme is null)
                {
                    status.Status = TableStatusReason.Other;
                    status.Comment = Russian.InternalError;
                    return status;
                }
                if (!StaticResources.SBCoreTableMap.TryGetValue(tableName, out var d1))
                {
                    status.Status = TableStatusReason.MissingTable;
                    status.Comment = Russian.TableNotFound;
                    return status;
                }

                List<string> Columns = new();
                foreach (DataRow row in scheme.Rows)
                    Columns.Add(row.Field<string>("ColumnName"));

                var missingColumns = d1.TableMap.Keys.Except(Columns);
                if (missingColumns.Any())
                {
                    status.Comment = Russian.InvalidTableStructure;
                    status.Status |= TableStatusReason.MissingColumn;
                    foreach (var text in missingColumns.Select(item => Russian.MissingColumnA1 + item))
                        status.Details.Add(text);
                }

                List<string> redundantColumns = new();

                foreach (DataRow row in scheme.Rows)
                {
                    var columnName = row.Field<string>("ColumnName");
                    if (!d1.TableMap.TryGetValue(columnName, out var tuples))
                        redundantColumns.Add(columnName);
                    else
                    {
                        foreach (var tuple in tuples)
                        {
                            var obj = row[tuple.Item1];
                            if (!Equals(obj, tuple.Item2))
                            {
                                status.Comment = Russian.InvalidTableStructure;
                                status.Status |= TableStatusReason.InvalidScheme;
                                status.Details.Add(string.Format(Russian.InvalidColumnStructureA4, columnName, tuple.Item1, obj, tuple.Item2));
                            }
                        }
                    }
                }

                if (redundantColumns.Any())
                {
                    status.Comment = Russian.InvalidTableStructure;
                    status.Status |= TableStatusReason.Other;
                    foreach (var text in redundantColumns.Select(item => string.Format(Russian.RedundantColumnA1, item)))
                        status.Details.Add(text);
                    var Comment = Russian.RedundantColumnA1 + string.Join(", ", redundantColumns);
                }

                var rows = await GetTableRecordCountAsync(tableName, cancellationToken);
                status.Comment = Rows(rows);
            }
            catch (Exception ex)
            {
                status.Status |= TableStatusReason.Other;
                status.Comment = ex.Message;
                status.Details.Add(ex.Message);
            }
            return status;
        }

        private async Task<bool> LoadIndicesAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (_connection.SqliteConnection.State != ConnectionState.Open)
                return false;

            sqliteIndices.Clear();

            using var command = new SqliteCommand(SQLite.SqlSelectIndices, _connection.SqliteConnection);
            var reader = await command.ExecuteReaderAsync(cancellationToken);

            if (reader is null)
                return false;

            while (await reader.ReadAsync(cancellationToken))
            {
                if (!await reader.IsDBNullAsync("sql", cancellationToken))
                {
                    SqliteIndex sqliteIndex = new()
                    {
                        Name = reader.GetString("name"),
                        Table = reader.GetString("tbl_name"),
                        Sql = reader.GetString("sql"),
                    };
                    Debug.WriteLine(sqliteIndex.ToString());
                    sqliteIndices.Add(sqliteIndex);
                }
            }

            return true;
        }

        private void CloseCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }

        private void OnPropertyChanged([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        private static string Rows(long rows)
        {
            if (rows < 0)
                return Russian.DbException;
            if (rows >= 11 && rows <= 14)
                return string.Format(Russian.Rows0A1, rows);
            if (rows % 10 == 1)
                return string.Format(Russian.Rows1A1, rows);
            if (rows % 10 == 2)
                return string.Format(Russian.Rows2A1, rows);
            if (rows % 10 == 3)
                return string.Format(Russian.Rows2A1, rows);
            if (rows % 10 == 4)
                return string.Format(Russian.Rows2A1, rows);
            return string.Format(Russian.Rows0A1, rows);
        }
    }
}
