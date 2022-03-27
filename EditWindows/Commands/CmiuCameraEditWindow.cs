using CameraShared.Types;
using Microsoft.Extensions.DependencyInjection;
using SBCore.Configurator.TextResources;
using SBShared.Types.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using WpfNotification;

namespace SBCore.Configurator.EditWindows
{
    internal partial class CmiuCameraEditWindow
    {
        private void PreviewCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (sender is DataGrid grid && e.Command == DataGrid.DeleteCommand)
            {
                if (grid.SelectedItems.Count > 0)
                {
                    var selectedItems = grid.SelectedItems.Cast<CameraData<CmiuDeviceData>>().ToArray();
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
            if (sender is CmiuCameraEditWindow)
            {
                e.CanExecute = true;
                e.Handled = true;
            }
        }

        private void RemoveRowCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (sender is CmiuCameraEditWindow window)
            {
                var grid = window.CameraMapGrid;
                if (grid.CurrentItem is CameraData<CmiuDeviceData>)
                    e.CanExecute = ClickedItem != null;
                e.Handled = true;
            }
        }

        private void PingCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (sender is CmiuCameraEditWindow window)
            {
                var grid = window.CameraMapGrid;
                if (grid.CurrentItem is CameraData<CmiuDeviceData> item)
                {
                    if (item.CameraUri is not null)
                        e.CanExecute = ClickedItem != null;
                }

                e.Handled = true;
            }
        }

        private void PictureCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (sender is CmiuCameraEditWindow window)
            {
                var grid = window.CameraMapGrid;
                if (grid.CurrentItem is CameraData<CmiuDeviceData> item)
                    e.CanExecute = item.CameraUri is not null && (item.Cameratype == CmiuCameraType.ONVIF || item.Cameratype == CmiuCameraType.Axis);
                e.Handled = true;
            }
        }

        private void VideoCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (sender is CmiuCameraEditWindow window)
            {
                var grid = window.CameraMapGrid;
                if (grid.CurrentItem is CameraData<CmiuDeviceData> item)
                    e.CanExecute = item.CameraUri is not null && item.Cameratype == CmiuCameraType.ONVIF;
                e.Handled = true;
            }
        }

        private void AuthHelperCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (sender is CmiuCameraEditWindow window)
            {
                var grid = window.CameraMapGrid;
                if (grid.CurrentItem is CameraData<CmiuDeviceData>)
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
            observableCollection.Add(new CameraData<CmiuDeviceData>());

            if (sender is CmiuCameraEditWindow window)
            {
                var grid = window.CameraMapGrid;
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
            if (sender is CmiuCameraEditWindow window)
            {
                var selectedItems = new List<CameraData<CmiuDeviceData>> { };
                var grid = window.CameraMapGrid;
                grid.CancelEdit();
                if (grid.SelectedItems.Count > 0)
                    selectedItems.AddRange(grid.SelectedItems.Cast<CameraData<CmiuDeviceData>>());
                else
                {
                    if (grid.CurrentItem is CameraData<CmiuDeviceData> item)
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

        private async void PingExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                string CurrentCellDeviceIP = null;
                if (sender is CmiuCameraEditWindow window)
                {
                    var grid = window.CameraMapGrid;
                    if (grid.CurrentItem is CameraData<CmiuDeviceData> item)
                    {
                        if (item.CameraUri is not null)
                            CurrentCellDeviceIP = item.CameraUri.Host;
                    }
                }

                if (CurrentCellDeviceIP == null)
                {
                    Dispatcher.Invoke(() =>
                    {
                        StatusBlock.Text = Russian.NoIP;
                        StatusBlock.ToolTip = string.Empty;
                        PopupWindow popup = new(Russian.Ping, Russian.NoIP, NotificationType.Error)
                        {
                            Owner = this
                        };
                        popup.Show();
                    });
                    e.Handled = true;
                    return;
                }

                using Ping pingSender = new();
                PingOptions options = new()
                {
                    DontFragment = true
                };
                byte[] buffer = new byte[32];
                int timeout = 120;
                PingReply reply = await pingSender.SendPingAsync(CurrentCellDeviceIP, timeout, buffer, options);
                if (reply.Status == IPStatus.Success)
                {
                    Dispatcher.Invoke(() =>
                    {
                        StatusBlock.Text = string.Format(CultureInfo.CurrentCulture, Russian.PingSuccess2A2, CurrentCellDeviceIP, reply.RoundtripTime);
                        StatusBlock.ToolTip = string.Empty;
                        var popup = new PopupWindow(Russian.Ping, string.Format(CultureInfo.CurrentCulture, Russian.PingSuccess1A2, CurrentCellDeviceIP, reply.RoundtripTime), NotificationType.Notification)
                        {
                            Owner = this
                        };
                        popup.Show();
                    });
                }
                else
                {
                    Dispatcher.Invoke(() =>
                    {
                        StatusBlock.Text = string.Format(CultureInfo.CurrentCulture, Russian.PingFail2A2, CurrentCellDeviceIP, reply.Status);
                        StatusBlock.ToolTip = string.Empty;
                        var popup = new PopupWindow(Russian.Ping,
                        string.Format(CultureInfo.CurrentCulture, Russian.PingFail1A2, CurrentCellDeviceIP, reply.Status), NotificationType.Warning)
                        {
                            Owner = this
                        };
                        popup.Show();
                    });
                }
            }
            catch (Exception ex)
            {
                Dispatcher.Invoke(() =>
                {
                    StringBuilder sb = new();
                    sb.AppendLine(Russian.ExceptionA + ex.Message);
                    while (ex.InnerException != null)
                    {
                        ex = ex.InnerException;
                        sb.AppendLine(Russian.InnerExceptionA + ex.Message);
                    }

                    StatusBlock.Text = string.Concat(Russian.ExceptionA, ex.Message);
                    StatusBlock.ToolTip = sb.ToString();
                    var popup = new PopupWindow(Russian.PingException, ex.Message, NotificationType.Error)
                    {
                        Owner = this
                    };
                    popup.Show();
                });
            }

            e.Handled = true;
        }

        private void PictureExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (sender is CmiuCameraEditWindow window)
            {
                var grid = window.CameraMapGrid;
                if (grid.CurrentItem is CameraData<CmiuDeviceData> item)
                {
                    var picturePreviewWindow = _serviceProvider.GetRequiredService<PicturePreviewWindow>();
                    picturePreviewWindow.CmiuCameraData = item;
                    picturePreviewWindow.Owner = this;
                    picturePreviewWindow.ShowDialog();
                }
            }

            e.Handled = true;
        }

        private void VideoExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (sender is CmiuCameraEditWindow window)
            {
                var grid = window.CameraMapGrid;
                if (grid.CurrentItem is CameraData<CmiuDeviceData> item)
                {
                    var videoPreviewWindow = _serviceProvider.GetRequiredService<VideoPreviewWindow>();
                    videoPreviewWindow.CameraItem = item;
                    videoPreviewWindow.Owner = this;
                    videoPreviewWindow.ShowDialog();
                }
            }

            e.Handled = true;
        }

        private void AuthHelperExecuted(object sender, ExecutedRoutedEventArgs e)
        {

            if (sender is CmiuCameraEditWindow window)
            {
                var grid = window.CameraMapGrid;
                var item = grid.CurrentItem as CameraData<CmiuDeviceData>;

                if (item is not null)
                {
                    grid.CommitEdit(DataGridEditingUnit.Row, true);

                    var authWindow = _serviceProvider.GetRequiredService<AuthEditWindow>();
                    authWindow.Owner = this;
                    authWindow.Model.Auth = item.Auth;
                    authWindow.Model.GenerateSalt = true;
                    authWindow.ShowDialog();

                    if (authWindow.Model.Auth is not null)
                    {
                        item.Auth = authWindow.Model.Auth;
                        CollectionViewSource.GetDefaultView(grid.ItemsSource).Refresh();
                    }
                }
            }

            e.Handled = true;
        }
    }
}
