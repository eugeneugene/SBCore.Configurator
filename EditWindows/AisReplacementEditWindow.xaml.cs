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
    /// Interaction logic for AisReplacementEditWindow.xaml
    /// </summary>
    internal partial class AisReplacementEditWindow : Window,IDisposable
    {
        private readonly IHostApplicationLifetime _applicationLifetime;

        private readonly DataContext _context;
        private readonly ObservableCollection<AisReplacementData> observableCollection;

        private bool _justCreatedNewItem;
        private bool disposed;

        private AisReplacementData ClickedItem { get; set; }

        public int LinesChanged { get; private set; }
        
        public AisReplacementEditWindow(Connection connection, IHostApplicationLifetime applicationLifetime)
        {
            InitializeComponent();
            _applicationLifetime = applicationLifetime;
            _applicationLifetime.ApplicationStopping.Register(() => Close());

            DbContextOptionsBuilder contextOptionsBuilder = new();
            contextOptionsBuilder.UseSqlite(connection.SqliteConnection);
            _context = new DataContext(contextOptionsBuilder.Options);

            CollectionViewSource itemCollectionViewSource = (CollectionViewSource)FindResource("AisReplacementDataSource");
            _context.AisReplacementDataSet.Load();
            observableCollection = _context.AisReplacementDataSet.Local.ToObservableCollection();
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

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is DataGrid)
            {
                ClickedItem = null;
                if (e.OriginalSource is TextBlock textBlock && textBlock.DataContext is AisReplacementData AisReplacementData1)
                    ClickedItem = AisReplacementData1;
                if (e.OriginalSource is Border border && border.DataContext is AisReplacementData AisReplacementData2)
                    ClickedItem = AisReplacementData2;

                Debug.WriteLine("clickedItem: " + ClickedItem?.ToString() ?? "null");
                e.Handled = true;
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
