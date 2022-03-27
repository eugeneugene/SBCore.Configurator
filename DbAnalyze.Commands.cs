using Microsoft.Data.Sqlite;
using Microsoft.Extensions.DependencyInjection;
using SBCore.Configurator.Code;
using SBCore.Configurator.EditWindows;
using SBCore.Configurator.Models;
using SBCore.Configurator.TextResources;
using SBShared.Types;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace SBCore.Configurator
{
    internal partial class DbAnalyze
    {
        private void PurgeTablesCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _dbStatus.All(item => item.Status == TableStatusReason.Ok);
            e.Handled = true;
        }

        private async void PurgeTablesExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (!CanRead || !CanWrite)
                return;

            for (int index = 0; index < _dbStatus.Count; index++)
            {
                var item = _dbStatus[index];
                if (item.Status == TableStatusReason.Ok)
                {
                    try
                    {
                        using var command = new SqliteCommand(string.Format(CultureInfo.InvariantCulture, SQLite.SqlTruncateTable, item.TableName), _connection.SqliteConnection);
                        await command.ExecuteNonQueryAsync(_applicationLifetime.ApplicationStopping);
                        item.Comment = Russian.RecordsRemoved;
                        item.Details.Clear();
                        _dbStatus[index] = item;
                    }
                    catch (Exception ex)
                    {
                        Dispatcher.Invoke(() => StatusBlock.Text = string.Format(Russian.SqliteTableExceptionA2, item.TableName, ex.Message));
                        Dispatcher.Invoke(() => StatusBlock.ToolTip = ex.ToString());
                        return;
                    }
                }
            }

            Dispatcher.Invoke(() => StatusBlock.Text = Russian.Success);
            Dispatcher.Invoke(() => StatusBlock.ToolTip = string.Empty);
        }

        private void RecreateBrokenTablesCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = CanWrite;
            e.Handled = true;
        }

        private async void RecreateBrokenTablesExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (!CanRead || !CanWrite)
                return;

            for (int index = 0; index < _dbStatus.Count; index++)
            {
                var item = _dbStatus[index];
                if (item.Status != TableStatusReason.Ok)
                {
                    try
                    {
                        string createCmd = StaticResources.CreateScript(item.TableName);

                        if ((item.Status & TableStatusReason.MissingTable) == 0)
                        {
                            using var command1 = new SqliteCommand(string.Format(CultureInfo.InvariantCulture, SQLite.SqlDropTable, item.TableName), _connection.SqliteConnection);
                            await command1.ExecuteNonQueryAsync(_applicationLifetime.ApplicationStopping);
                        }

                        using var command2 = new SqliteCommand(createCmd, _connection.SqliteConnection);
                        await command2.ExecuteNonQueryAsync(_applicationLifetime.ApplicationStopping);

                        foreach (var table in StaticResources.SBCoreIndexMap.Keys.Where(key => key == item.TableName))
                        {
                            foreach (var name_sql in StaticResources.SBCoreIndexMap[table])
                            {
                                using var command3 = new SqliteCommand(name_sql.Item2, _connection.SqliteConnection);
                                await command3.ExecuteNonQueryAsync(_applicationLifetime.ApplicationStopping);
                            }
                        }

                        item.Comment = Russian.TableRecreated;
                        item.Details.Clear();
                        item.Status = TableStatusReason.Ok;
                        _dbStatus[index] = item;
                    }
                    catch (Exception ex)
                    {
                        Dispatcher.Invoke(() => StatusBlock.Text = string.Format(Russian.SqliteTableExceptionA2, item.TableName, ex.Message));
                        Dispatcher.Invoke(() => StatusBlock.ToolTip = ex.ToString());
                        return;
                    }
                }
            }

            Dispatcher.Invoke(() => StatusBlock.Text = Russian.Success);
            Dispatcher.Invoke(() => StatusBlock.ToolTip = string.Empty);
        }

        private void RecreateTablesCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = CanWrite;
            e.Handled = true;
        }

        private async void RecreateTablesExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (!CanRead || !CanWrite)
                return;

            for (int index = 0; index < _dbStatus.Count; index++)
            {
                var item = _dbStatus[index];

                try
                {
                    string createCmd = StaticResources.CreateScript(item.TableName);

                    if ((item.Status & TableStatusReason.MissingTable) == 0)
                    {
                        using var command1 = new SqliteCommand(string.Format(CultureInfo.InvariantCulture, SQLite.SqlDropTable, item.TableName), _connection.SqliteConnection);
                        await command1.ExecuteNonQueryAsync(_applicationLifetime.ApplicationStopping);
                    }

                    using var command2 = new SqliteCommand(createCmd, _connection.SqliteConnection);
                    await command2.ExecuteNonQueryAsync(_applicationLifetime.ApplicationStopping);

                    foreach (var table in StaticResources.SBCoreIndexMap.Keys.Where(key => key == item.TableName))
                    {
                        foreach (var name_sql in StaticResources.SBCoreIndexMap[table])
                        {
                            using var command3 = new SqliteCommand(name_sql.Item2, _connection.SqliteConnection);
                            await command3.ExecuteNonQueryAsync(_applicationLifetime.ApplicationStopping);
                        }
                    }

                    item.Comment = Russian.TableRecreated;
                    item.Details.Clear();
                    item.Status = TableStatusReason.Ok;
                    _dbStatus[index] = item;
                }
                catch (Exception ex)
                {
                    Dispatcher.Invoke(() => StatusBlock.Text = string.Format(Russian.SqliteTableExceptionA2, item.TableName, ex.Message));
                    Dispatcher.Invoke(() => StatusBlock.ToolTip = ex.ToString());
                    return;
                }
            }

            Dispatcher.Invoke(() => StatusBlock.Text = Russian.Success);
            Dispatcher.Invoke(() => StatusBlock.ToolTip = string.Empty);
        }

        private void EditTableCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (sender is DbAnalyze analyze)
            {
                var value = analyze.dbStatusGrid.SelectedItem;
                if (value is TableStatus status)
                    e.CanExecute = status.Writable && status.Status == TableStatusReason.Ok;
                e.Handled = true;
            }
        }

        private async void EditTableExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (!CanRead || !CanWrite)
                return;

            if (sender is DbAnalyze analyze)
            {
                var value = analyze.dbStatusGrid.SelectedItem;
                if (value is TableStatus status)
                {
                    Debug.WriteLine($"Selected: {status.TableName}");
                    var cursor = new Stack<Cursor>();
                    cursor.Push(Cursor);
                    Cursor = Cursors.Wait;
                    var index = _dbStatus.IndexOf(status);
                    long rows = 0L;

                    switch (status.TableName)
                    {
                        case StaticResources.ADMIN:
                            using (var window = _serviceProvider.GetRequiredService<AdminEditWindow>())
                            {
                                window.ShowDialog();
                                StatusBlock.Text = RowsChangedFormat.Format(window.LinesChanged);
                                StatusBlock.ToolTip = string.Empty;
                                rows = await GetTableRecordCountAsync(status.TableName, _applicationLifetime.ApplicationStopping);
                            }
                            break;
                        case StaticResources.AIS_LOG:
                            using (var window = _serviceProvider.GetRequiredService<AisLogEditWindow>())
                            {
                                window.ShowDialog();
                                StatusBlock.Text = RowsChangedFormat.Format(window.LinesChanged);
                                StatusBlock.ToolTip = string.Empty;
                                rows = await GetTableRecordCountAsync(status.TableName, _applicationLifetime.ApplicationStopping);
                            }
                            break;
                        case StaticResources.PARKING_MAP:
                            using (var window = _serviceProvider.GetRequiredService<ParkingEditWindow>())
                            {
                                window.ShowDialog();
                                StatusBlock.Text = RowsChangedFormat.Format(window.LinesChanged);
                                StatusBlock.ToolTip = string.Empty;
                                rows = await GetTableRecordCountAsync(status.TableName, _applicationLifetime.ApplicationStopping);
                            }
                            break;
                        case StaticResources.AIS_REPLACEMENT:
                            using (var window = _serviceProvider.GetRequiredService<AisReplacementEditWindow>())
                            {
                                window.ShowDialog();
                                StatusBlock.Text = RowsChangedFormat.Format(window.LinesChanged);
                                StatusBlock.ToolTip = string.Empty;
                                rows = await GetTableRecordCountAsync(status.TableName, _applicationLifetime.ApplicationStopping);
                            }
                            break;
                        case StaticResources.CAMERA_MAP:
                            using (var window = _serviceProvider.GetRequiredService<CmiuCameraEditWindow>())
                            {
                                window.ShowDialog();
                                StatusBlock.Text = RowsChangedFormat.Format(window.LinesChanged);
                                StatusBlock.ToolTip = string.Empty;
                                rows = await GetTableRecordCountAsync(status.TableName, _applicationLifetime.ApplicationStopping);
                            }
                            break;
                        case StaticResources.CMIUGW_AISSESSION_DELIVERY_STATUS:
                            using (var window = _serviceProvider.GetRequiredService<AisSessionDeliveryStatusEditWindow>())
                            {
                                window.ShowDialog();
                                StatusBlock.Text = RowsChangedFormat.Format(window.LinesChanged);
                                StatusBlock.ToolTip = string.Empty;
                                rows = await GetTableRecordCountAsync(status.TableName, _applicationLifetime.ApplicationStopping);
                            }
                            break;
                        case StaticResources.CMIUGW_CASHSTATUS:
                            using (var window = _serviceProvider.GetRequiredService<CmiuCashStatusEditWindow>())
                            {
                                window.ShowDialog();
                                StatusBlock.Text = RowsChangedFormat.Format(window.LinesChanged);
                                StatusBlock.ToolTip = string.Empty;
                                rows = await GetTableRecordCountAsync(status.TableName, _applicationLifetime.ApplicationStopping);
                            }
                            break;
                        case StaticResources.CMIUGW_CONFIG:
                            using (var window = _serviceProvider.GetRequiredService<ConfigEditWindow>())
                            {
                                window.ShowDialog();
                                StatusBlock.Text = RowsChangedFormat.Format(window.LinesChanged);
                                StatusBlock.ToolTip = string.Empty;
                                rows = await GetTableRecordCountAsync(status.TableName, _applicationLifetime.ApplicationStopping);
                            }
                            break;
                        case StaticResources.CMIUGW_DEVICE_MAP:
                            using (var window = _serviceProvider.GetRequiredService<CmiuDeviceEditWindow>())
                            {
                                window.ShowDialog();
                                StatusBlock.Text = RowsChangedFormat.Format(window.LinesChanged);
                                StatusBlock.ToolTip = string.Empty;
                                rows = await GetTableRecordCountAsync(status.TableName, _applicationLifetime.ApplicationStopping);
                            }
                            break;
                        case StaticResources.CMIUGW_EVENTS:
                            using (var window = _serviceProvider.GetRequiredService<CmiuEventEditWindow>())
                            {
                                window.ShowDialog();
                                StatusBlock.Text = RowsChangedFormat.Format(window.LinesChanged);
                                StatusBlock.ToolTip = string.Empty;
                                rows = await GetTableRecordCountAsync(status.TableName, _applicationLifetime.ApplicationStopping);
                            }
                            break;
                        case StaticResources.CMIUGW_IMAGES:
                            using (var window = _serviceProvider.GetRequiredService<CmiuImageEditWindow>())
                            {
                                window.ShowDialog();
                                StatusBlock.Text = RowsChangedFormat.Format(window.LinesChanged);
                                StatusBlock.ToolTip = string.Empty;
                                rows = await GetTableRecordCountAsync(status.TableName, _applicationLifetime.ApplicationStopping);
                            }
                            break;
                        case StaticResources.CMIUGW_ISSIMAGES:
                            using (var window = _serviceProvider.GetRequiredService<CmiuIssImageEditWindow>())
                            {
                                window.ShowDialog();
                                StatusBlock.Text = RowsChangedFormat.Format(window.LinesChanged);
                                StatusBlock.ToolTip = string.Empty;
                                rows = await GetTableRecordCountAsync(status.TableName, _applicationLifetime.ApplicationStopping);
                            }
                            break;
                        case StaticResources.CMIUGW_LEVELS:
                            using (var window = _serviceProvider.GetRequiredService<CmiuLevelsEditWindow>())
                            {
                                window.ShowDialog();
                                StatusBlock.Text = RowsChangedFormat.Format(window.LinesChanged);
                                StatusBlock.ToolTip = string.Empty;
                                rows = await GetTableRecordCountAsync(status.TableName, _applicationLifetime.ApplicationStopping);
                            }
                            break;
                        case StaticResources.CMIUGW_LPR:
                            using (var window = _serviceProvider.GetRequiredService<CmiuLprEditWindow>())
                            {
                                window.ShowDialog();
                                StatusBlock.Text = RowsChangedFormat.Format(window.LinesChanged);
                                StatusBlock.ToolTip = string.Empty;
                                rows = await GetTableRecordCountAsync(status.TableName, _applicationLifetime.ApplicationStopping);
                            }
                            break;
                        case StaticResources.CMIUGW_MOVEMENTS:
                            using (var window = _serviceProvider.GetRequiredService<CmiuMovementEditWindow>())
                            {
                                window.ShowDialog();
                                StatusBlock.Text = RowsChangedFormat.Format(window.LinesChanged);
                                StatusBlock.ToolTip = string.Empty;
                                rows = await GetTableRecordCountAsync(status.TableName, _applicationLifetime.ApplicationStopping);
                            }
                            break;
                        case StaticResources.CMIUGW_PARKING_LEVEL_MAP:
                            using (var window = _serviceProvider.GetRequiredService<CmiuParkingLevelEditWindow>())
                            {
                                window.ShowDialog();
                                StatusBlock.Text = RowsChangedFormat.Format(window.LinesChanged);
                                StatusBlock.ToolTip = string.Empty;
                                rows = await GetTableRecordCountAsync(status.TableName, _applicationLifetime.ApplicationStopping);
                            }
                            break;
                        case StaticResources.CMIUGW_PAYMENTS:
                            using (var window = _serviceProvider.GetRequiredService<CmiuPaymentEditWindow>())
                            {
                                window.ShowDialog();
                                StatusBlock.Text = RowsChangedFormat.Format(window.LinesChanged);
                                StatusBlock.ToolTip = string.Empty;
                                rows = await GetTableRecordCountAsync(status.TableName, _applicationLifetime.ApplicationStopping);
                            }
                            break;
                        case StaticResources.CMIUGW_PLACES:
                            using (var window = _serviceProvider.GetRequiredService<CmiuPlacesEditWindow>())
                            {
                                window.ShowDialog();
                                StatusBlock.Text = RowsChangedFormat.Format(window.LinesChanged);
                                StatusBlock.ToolTip = string.Empty;
                                rows = await GetTableRecordCountAsync(status.TableName, _applicationLifetime.ApplicationStopping);
                            }
                            break;
                        case StaticResources.CMIUGW_PLACES_URLS_MAP:
                            using (var window = _serviceProvider.GetRequiredService<CmiuPlacesUrlEditWindow>())
                            {
                                window.ShowDialog();
                                StatusBlock.Text = RowsChangedFormat.Format(window.LinesChanged);
                                StatusBlock.ToolTip = string.Empty;
                                rows = await GetTableRecordCountAsync(status.TableName, _applicationLifetime.ApplicationStopping);
                            }
                            break;
                        case StaticResources.DEVICE_CMIU_CAMERA_MAP:
                            using (var window = _serviceProvider.GetRequiredService<DeviceCmiuCameraEditWindow>())
                            {
                                window.ShowDialog();
                                StatusBlock.Text = RowsChangedFormat.Format(window.LinesChanged);
                                StatusBlock.ToolTip = string.Empty;
                                rows = await GetTableRecordCountAsync(status.TableName, _applicationLifetime.ApplicationStopping);
                            }
                            break;
                        case StaticResources.EVENT_CAMERA_MAP:
                            using (var window = _serviceProvider.GetRequiredService<EventCameraEditWindow>())
                            {
                                window.ShowDialog();
                                StatusBlock.Text = RowsChangedFormat.Format(window.LinesChanged);
                                StatusBlock.ToolTip = string.Empty;
                                rows = await GetTableRecordCountAsync(status.TableName, _applicationLifetime.ApplicationStopping);
                            }
                            break;
                        case StaticResources.EXEMPTION_JOBS:
                            using (var window = _serviceProvider.GetRequiredService<ExemptionJobsEditWindow>())
                            {
                                window.ShowDialog();
                                StatusBlock.Text = RowsChangedFormat.Format(window.LinesChanged);
                                StatusBlock.ToolTip = string.Empty;
                                rows = await GetTableRecordCountAsync(status.TableName, _applicationLifetime.ApplicationStopping);
                            }
                            break;
                        case StaticResources.EXEMPTION_LOG:
                            using (var window = _serviceProvider.GetRequiredService<ExemptionLogEditWindow>())
                            {
                                window.ShowDialog();
                                StatusBlock.Text = RowsChangedFormat.Format(window.LinesChanged);
                                StatusBlock.ToolTip = string.Empty;
                                rows = await GetTableRecordCountAsync(status.TableName, _applicationLifetime.ApplicationStopping);
                            }
                            break;
                        case StaticResources.EXEMPTION_PARKING:
                            using (var window = _serviceProvider.GetRequiredService<ExemptionParkingEditWindow>())
                            {
                                window.ShowDialog();
                                StatusBlock.Text = RowsChangedFormat.Format(window.LinesChanged);
                                StatusBlock.ToolTip = string.Empty;
                                rows = await GetTableRecordCountAsync(status.TableName, _applicationLifetime.ApplicationStopping);
                            }
                            break;
                        case StaticResources.EXEMPTION_PROVIDERS:
                            using (var window = _serviceProvider.GetRequiredService<ExemptionProvidersEditWindow>())
                            {
                                window.ShowDialog();
                                StatusBlock.Text = RowsChangedFormat.Format(window.LinesChanged);
                                StatusBlock.ToolTip = string.Empty;
                                rows = await GetTableRecordCountAsync(status.TableName, _applicationLifetime.ApplicationStopping);
                            }
                            break;
                        case StaticResources.LPR_ISS_EVENT_MAP:
                            using (var window = _serviceProvider.GetRequiredService<LprIssEventEditWindow>())
                            {
                                window.ShowDialog();
                                StatusBlock.Text = RowsChangedFormat.Format(window.LinesChanged);
                                StatusBlock.ToolTip = string.Empty;
                                rows = await GetTableRecordCountAsync(status.TableName, _applicationLifetime.ApplicationStopping);
                            }
                            break;
                        default:
                            Debug.WriteLine(Russian.InternalError);
                            StatusBlock.Text = Russian.InternalError;
                            StatusBlock.ToolTip = string.Empty;
                            rows = -1L;
                            break;
                    }

                    if (index >= 0)
                    {
                        if (rows >= 0L)
                            _dbStatus[index].Comment = Rows(rows);
                        else
                            _dbStatus[index].Comment = Russian.InternalError;
                        _dbStatus[index].Details.Clear();
                    }

                    Cursor = cursor.Pop();
                }

                e.Handled = true;
            }
        }

        private void RecreateTableCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (sender is DbAnalyze analyze)
            {
                var value = analyze.dbStatusGrid.SelectedItem;
                if (value is TableStatus status)
                    e.CanExecute = status.Writable;
                e.Handled = true;
            }
        }

        private async void RecreateTableExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (!CanRead || !CanWrite)
                return;

            if (sender is DbAnalyze analyze)
            {
                var value = analyze.dbStatusGrid.SelectedItem;
                if (value is TableStatus status)
                    await RecreateTableAsync(status, _applicationLifetime.ApplicationStopping);
                e.Handled = true;
            }
        }

        private void PurgeTableCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (sender is DbAnalyze analyze)
            {
                var value = analyze.dbStatusGrid.SelectedItem;
                if (value is TableStatus status)
                    e.CanExecute = status.Writable && status.Status == TableStatusReason.Ok;
                e.Handled = true;
            }
        }

        private async void PurgeTableExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (!CanRead || !CanWrite)
                return;

            if (sender is DbAnalyze analyze)
            {
                var value = analyze.dbStatusGrid.SelectedItem;
                if (value is TableStatus status)
                    await TruncateTableAsync(status, _applicationLifetime.ApplicationStopping);
                e.Handled = true;
            }
        }

        private async Task TruncateTableAsync(TableStatus status, CancellationToken cancellationToken)
        {
            if (status is null)
                return;
            int index = _dbStatus.IndexOf(status);
            if (index < 0)
                return;

            if (status.Status == TableStatusReason.Ok)
            {
                try
                {
                    using var command = new SqliteCommand(string.Format(CultureInfo.InvariantCulture, SQLite.SqlTruncateTable, status.TableName), _connection.SqliteConnection);
                    await command.ExecuteNonQueryAsync(cancellationToken);
                    _dbStatus[index].Comment = Russian.RecordsRemoved;
                    _dbStatus[index].Details.Clear();
                }
                catch (Exception ex)
                {
                    Dispatcher.Invoke(() => StatusBlock.Text = string.Format(Russian.SqliteTableExceptionA2, status.TableName, ex.Message));
                    Dispatcher.Invoke(() => StatusBlock.ToolTip = ex.ToString());
                    return;
                }
            }

            Dispatcher.Invoke(() => StatusBlock.Text = Russian.Success);
            Dispatcher.Invoke(() => StatusBlock.ToolTip = string.Empty);
        }

        private async Task RecreateTableAsync(TableStatus status, CancellationToken cancellationToken)
        {
            if (status is null)
                return;
            int index = _dbStatus.IndexOf(status);
            if (index < 0)
                return;

            try
            {
                string createCmd = StaticResources.CreateScript(status.TableName);

                if ((status.Status & TableStatusReason.MissingTable) == 0)
                {
                    using var command1 = new SqliteCommand(string.Format(CultureInfo.InvariantCulture, SQLite.SqlDropTable, status.TableName), _connection.SqliteConnection);
                    await command1.ExecuteNonQueryAsync(cancellationToken);
                }

                using var command2 = new SqliteCommand(createCmd, _connection.SqliteConnection);
                await command2.ExecuteNonQueryAsync(cancellationToken);

                foreach (var pair in StaticResources.SBCoreIndexMap.Where(item1 => item1.Key == status.TableName))
                {
                    foreach (var name_sql in pair.Value)
                    {
                        using var command3 = new SqliteCommand(name_sql.Item2, _connection.SqliteConnection);
                        await command3.ExecuteNonQueryAsync(cancellationToken);
                    }
                }

                _dbStatus[index].Comment = Russian.TableRecreated;
                _dbStatus[index].Details.Clear();
                _dbStatus[index].Status = TableStatusReason.Ok;
            }
            catch (Exception ex)
            {
                Dispatcher.Invoke(() => StatusBlock.Text = string.Format(Russian.SqliteTableExceptionA2, status.TableName, ex.Message));
                Dispatcher.Invoke(() => StatusBlock.ToolTip = ex.ToString());
                return;
            }

            Dispatcher.Invoke(() => StatusBlock.Text = Russian.Success);
            Dispatcher.Invoke(() => StatusBlock.ToolTip = string.Empty);
        }

        private void StatusDetailsCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (e is null || e.Parameter is null)
                return;

            e.Handled = true;
            var tableStatus = _dbStatus.SingleOrDefault(item=>item.TableName == e.Parameter.ToString());
            if (tableStatus is not null)
                e.CanExecute= tableStatus.Details.Count > 0;
        }

        private void StatusDetailsExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            Debug.WriteLine("Executed");
        }
    }
}
