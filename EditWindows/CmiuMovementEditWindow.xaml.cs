using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using SBCore.Configurator.Data;
using SBCore.Configurator.TextResources;
using SBShared.Types.Data;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using WpfNotification;

namespace SBCore.Configurator.EditWindows
{
    /// <summary>
    /// Interaction logic for CmiuMovementsEditWindow.xaml
    /// </summary>
    internal partial class CmiuMovementEditWindow : Window, IDisposable
    {
        private readonly IHostApplicationLifetime _applicationLifetime;

        private readonly DataContext _context;
        private readonly ObservableCollection<CmiuMovementData> observableCollection;

        private bool _justCreatedNewItem;
        private bool disposed;

        private CmiuMovementData ClickedItem { get; set; }

        public int LinesChanged { get; private set; }

        public CmiuMovementEditWindow(Connection connection, IHostApplicationLifetime applicationLifetime)
        {
            InitializeComponent();
            _applicationLifetime = applicationLifetime;
            _applicationLifetime.ApplicationStopping.Register(() => Close());

            DbContextOptionsBuilder contextOptionsBuilder = new();
            contextOptionsBuilder.UseSqlite(connection.SqliteConnection);
            _context = new DataContext(contextOptionsBuilder.Options);

            CollectionViewSource itemCollectionViewSource = (CollectionViewSource)FindResource("CmiuMovementDataSource");
            _context.CmiuMovementDataSet.Load();
            observableCollection = _context.CmiuMovementDataSet.Local.ToObservableCollection();
            itemCollectionViewSource.Source = observableCollection;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                _context?.Dispose();
            }

            disposed = true;
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is DataGrid)
            {
                ClickedItem = null;
                if (e.OriginalSource is TextBlock textBlock && textBlock.DataContext is CmiuMovementData cmiuMovementData1)
                    ClickedItem = cmiuMovementData1;
                if (e.OriginalSource is Border border && border.DataContext is CmiuMovementData cmiuMovementData2)
                    ClickedItem = cmiuMovementData2;

                Debug.WriteLine("clickedItem: " + ClickedItem?.ToString() ?? "null");
                e.Handled = true;
            }
        }

        private void OnClosing(object sender, CancelEventArgs e)
        {
            try
            {
                LinesChanged = _context.SaveChanges();
            }
            catch (Exception ex)
            {
                var sb = new StringBuilder();
                sb.AppendLine(Russian.ExceptionA + ex.Message);
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                    sb.AppendLine(Russian.InnerExceptionA + ex.Message);
                }

                var popup = new PopupWindow(Russian.DbException, sb.ToString(), NotificationType.Error);
                popup.Show();
                Debug.WriteLine(ex.ToString());
            }
        }

        private void GridBeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            if (sender is DataGrid grid)
            {
                if (_justCreatedNewItem)
                {
                    grid.CommitEdit(DataGridEditingUnit.Row, true);
                    _justCreatedNewItem = false;
                }
            }
        }

        private void GridInitializingNewItem(object sender, InitializingNewItemEventArgs e)
        {
            _justCreatedNewItem = true;
        }
    }
}
