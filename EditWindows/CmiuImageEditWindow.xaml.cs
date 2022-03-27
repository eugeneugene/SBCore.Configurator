using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Win32;
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
    /// Interaction logic for EditWindow.xaml
    /// </summary>
    internal partial class CmiuImageEditWindow : Window, IDisposable
    {
        private readonly ILogger _logger;
        private readonly OpenFileDialog _openFileDialog = new();
        private readonly IServiceProvider _serviceProvider;
        private readonly IHostApplicationLifetime _applicationLifetime;

        private readonly DataContext _context;
        private readonly ObservableCollection<CmiuImageData> observableCollection;

        private bool _justCreatedNewItem;
        private bool disposed;

        private CmiuImageData ClickedItem { get; set; }

        public int LinesChanged { get; private set; }

        public CmiuImageEditWindow(ILogger<CmiuImageEditWindow> logger, Connection connection, IServiceProvider serviceProvider, IHostApplicationLifetime applicationLifetime)
        {
            InitializeComponent();

            _logger = logger;
            _serviceProvider = serviceProvider;
            _applicationLifetime = applicationLifetime;
            _applicationLifetime.ApplicationStopping.Register(() => Close());

            DbContextOptionsBuilder contextOptionsBuilder = new();
            contextOptionsBuilder.UseSqlite(connection.SqliteConnection);
            _context = new DataContext(contextOptionsBuilder.Options);

            CollectionViewSource itemCollectionViewSource = (CollectionViewSource)FindResource("CmiuImageDataSource");
            _context.CmiuImageDataSet.Load();
            observableCollection = _context.CmiuImageDataSet.Local.ToObservableCollection();
            itemCollectionViewSource.Source = observableCollection;

            _openFileDialog.Filter = "Image Files (*.bmp;*.jpg;*.png;*.gif)|*.bmp;*.jpg;*.png;*.gif|All Files (*.*)|*.*";
            _openFileDialog.CheckFileExists = false;
            _openFileDialog.CheckPathExists = true;
            _openFileDialog.Multiselect = false;
            _openFileDialog.Title = Russian.OpenFileDialogTitle;

            _logger.LogDebug("Создание экземпляра {Name}", GetType().Name);
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
                if (e.OriginalSource is TextBlock textBlock && textBlock.DataContext is CmiuImageData cmiuImageData1)
                    ClickedItem = cmiuImageData1;
                if (e.OriginalSource is Border border && border.DataContext is CmiuImageData cmiuImageData2)
                    ClickedItem = cmiuImageData2;

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

        private void Message(string msg)
        {
            _logger.LogInformation("{msg}", msg);
            StatusBlock.Dispatcher.Invoke(() =>
            {
                StatusBlock.Text = msg;
            });
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
