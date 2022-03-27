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
    /// Interaction logic for EditWindow.xaml
    /// </summary>
    internal partial class EventCameraEditWindow : Window, IDisposable
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IHostApplicationLifetime _applicationLifetime;

        private readonly DataContext _context;
        private readonly ObservableCollection<EventCameraData<CmiuDeviceData>> EventCameraCollection;

        private bool _justCreatedNewItem;
        private bool disposed;

        private EventCameraData<CmiuDeviceData> ClickedItem { get; set; }

        public int LinesChanged { get; private set; }

        public EventCameraEditWindow(Connection connection, IServiceProvider serviceProvider, IHostApplicationLifetime applicationLifetime)
        {
            InitializeComponent();

            _serviceProvider = serviceProvider;
            _applicationLifetime = applicationLifetime;
            _applicationLifetime.ApplicationStopping.Register(() => Close());

            DbContextOptionsBuilder contextOptionsBuilder = new();
            contextOptionsBuilder.UseSqlite(connection.SqliteConnection);
            _context = new DataContext(contextOptionsBuilder.Options);

            CollectionViewSource EventCameraDataSource = (CollectionViewSource)FindResource("EventCameraDataSource");
            _context.EventCameraDataSet.Load();
            EventCameraCollection = _context.EventCameraDataSet.Local.ToObservableCollection();
            EventCameraDataSource.Source = EventCameraCollection;

            var deviceCmiuCameraDataSource = (CollectionViewSource)FindResource("DeviceCmiuCameraDataSource");
            _context.DeviceCmiuCameraDataSet.Load();
            deviceCmiuCameraDataSource.Source = _context.DeviceCmiuCameraDataSet.Local.ToObservableCollection();
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
                if (e.OriginalSource is TextBlock textBlock && textBlock.DataContext is EventCameraData<CmiuDeviceData> eventCameraData1)
                    ClickedItem = eventCameraData1;
                if (e.OriginalSource is Border border && border.DataContext is EventCameraData<CmiuDeviceData> eventCameraData2)
                    ClickedItem = eventCameraData2;

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
