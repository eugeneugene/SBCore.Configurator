using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SBCore.Configurator.Code;
using SBCore.Configurator.TextResources;
using SBShared.Types.Data;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using WpfNotification;

namespace SBCore.Configurator.EditWindows
{
    internal partial class DeviceCmiuCameraEditWindow
    {
        private void PreviewCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (sender is DataGrid grid && e.Command == DataGrid.DeleteCommand)
            {
                if (grid.SelectedItems.Count > 0)
                {
                    var selectedItems = grid.SelectedItems.Cast<DeviceCmiuCameraData<CmiuDeviceData>>().ToArray();
                    for (int i = 0; i < selectedItems.Length; i++)
                    {
                        var selectedItem = selectedItems[i];
                        Debug.WriteLine("Item is being removed: {0}", selectedItem);
                        var removeItem = observableCollection.FirstOrDefault(item => item.Id == selectedItem.Id);
                        bool removed = false;
                        if (removeItem != null)
                            removed = observableCollection.Remove(removeItem);
                        if (removed)
                            Debug.WriteLine("Successfully removed");
                        else
                            Debug.WriteLine("Not removed");
                    }
                }

                e.Handled = true;
            }
        }

        private void AddRowCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (sender is DeviceCmiuCameraEditWindow)
            {
                e.CanExecute = true;
                e.Handled = true;
            }
        }

        private void RemoveRowCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (sender is DeviceCmiuCameraEditWindow window)
            {
                var grid = window.CmiuLoopCameraGrid;
                if (grid.CurrentItem is DeviceCmiuCameraData<CmiuDeviceData>)
                    e.CanExecute = ClickedItem != null;
                e.Handled = true;
            }
        }

        private void CloseCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }

        private void AddRowExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            observableCollection.Add(new DeviceCmiuCameraData<CmiuDeviceData>());

            if (sender is DeviceCmiuCameraEditWindow window)
            {
                var grid = window.CmiuLoopCameraGrid;
                grid.UnselectAllCells();
                var last = observableCollection.Last();
                grid.CurrentCell = new DataGridCellInfo(last, grid.Columns[0]);
                grid.SelectedCells.Add(grid.CurrentCell);
                grid.SelectedItem = last;
                grid.Focus();
            }

            e.Handled = true;
        }

        private void RemoveRowExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (sender is DeviceCmiuCameraEditWindow window)
            {
                var selectedItems = new List<DeviceCmiuCameraData<CmiuDeviceData>> { };
                var grid = window.CmiuLoopCameraGrid;
                grid.CancelEdit();
                if (grid.SelectedItems.Count > 0)
                    selectedItems.AddRange(grid.SelectedItems.Cast<DeviceCmiuCameraData<CmiuDeviceData>>());
                else
                {
                    if (grid.CurrentItem is DeviceCmiuCameraData<CmiuDeviceData> item)
                        selectedItems.Add(item);
                }

                foreach (var selectedItem in selectedItems)
                {
                    Debug.WriteLine("Item is being removed: {0}", selectedItem);

                    var removeItem = observableCollection.FirstOrDefault(item => item.Id == selectedItem.Id);
                    bool removed = false;
                    if (removeItem != null)
                        removed = observableCollection.Remove(removeItem);
                    if (removed)
                        Debug.WriteLine("Successfully removed");
                    else
                        Debug.WriteLine("Not removed");
                }

                e.Handled = true;
            }
        }

        private void CameraCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            using var window = _serviceProvider.GetRequiredService<CmiuCameraEditWindow>();
            window.ShowDialog();
            if (window.LinesChanged > 0)
            {
                var Text = RowsChangedFormat.Format(window.LinesChanged);
                var popup = new PopupWindow(Russian.SuccessfulUpdate, Text, NotificationType.Notification)
                {
                    Owner = this
                };
                popup.Show();

                var cameraDataSource = (CollectionViewSource)FindResource("CameraDataSource");
                _context.CameraDataSet.Load();
                cameraDataSource.Source = _context.CameraDataSet.Local.ToObservableCollection();
            }
        }

        private void DeviceCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            using var window = _serviceProvider.GetRequiredService<CmiuDeviceEditWindow>();
            window.ShowDialog();
            if (window.LinesChanged > 0)
            {
                var Text = RowsChangedFormat.Format(window.LinesChanged);
                var popup = new PopupWindow(Russian.SuccessfulUpdate, Text, NotificationType.Notification)
                {
                    Owner = this
                };
                popup.Show();
            }
            var deviceDataSource = (CollectionViewSource)FindResource("DeviceDataSource");
            _context.CmiuDeviceDataSet.Load();
            deviceDataSource.Source = _context.CmiuDeviceDataSet.Local.ToObservableCollection();
        }
    }
}
