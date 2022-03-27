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
    internal partial class LprIssEventEditWindow
    {
        private void PreviewCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (sender is DataGrid grid && e.Command == DataGrid.DeleteCommand)
            {
                if (grid.SelectedItems.Count > 0)
                {
                    var selectedItems = grid.SelectedItems.Cast<LprIssEventData<CmiuDeviceData>>().ToArray();
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
            if (sender is LprIssEventEditWindow)
            {
                e.CanExecute = true;
                e.Handled = true;
            }
        }

        private void RemoveRowCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (sender is LprIssEventEditWindow window)
            {
                var grid = window.LprIssEventMapGrid;
                if (grid.CurrentItem is LprIssEventData<CmiuDeviceData>)
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
            observableCollection.Add(new LprIssEventData<CmiuDeviceData>());

            if (sender is LprIssEventEditWindow window)
            {
                var grid = window.LprIssEventMapGrid;
                grid.UnselectAllCells();
                grid.CommitEdit();
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
            if (sender is LprIssEventEditWindow window)
            {
                var selectedItems = new List<LprIssEventData<CmiuDeviceData>> { };
                var grid = window.LprIssEventMapGrid;
                grid.CancelEdit();
                if (grid.SelectedItems.Count > 0)
                    selectedItems.AddRange(grid.SelectedItems.Cast<LprIssEventData<CmiuDeviceData>>());
                else
                {
                    if (grid.CurrentItem is LprIssEventData<CmiuDeviceData> item)
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

        private void DeviceCmiuCameraCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            using var window = _serviceProvider.GetRequiredService<DeviceCmiuCameraEditWindow>();
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
            var deviceCmiuCameraDataSource = (CollectionViewSource)FindResource("DeviceCmiuCameraDataSource");
            _context.DeviceCmiuCameraDataSet.Load();
            deviceCmiuCameraDataSource.Source = _context.DeviceCmiuCameraDataSet.Local.ToObservableCollection();
        }
    }
}
