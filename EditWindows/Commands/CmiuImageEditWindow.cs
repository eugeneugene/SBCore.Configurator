using CameraShared;
using Microsoft.Extensions.DependencyInjection;
using SBCore.Configurator.TextResources;
using SBShared.Types.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace SBCore.Configurator.EditWindows
{
    internal partial class CmiuImageEditWindow
    {
        private void PreviewCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (sender is DataGrid grid && e.Command == DataGrid.DeleteCommand)
            {
                if (grid.SelectedItems.Count > 0)
                {
                    var selectedItems = grid.SelectedItems.Cast<CmiuImageData>().ToArray();
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
            if (sender is CmiuImageEditWindow)
            {
                e.CanExecute = true;
                e.Handled = true;
            }
        }

        private void RemoveRowCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (sender is CmiuImageEditWindow window)
            {
                var grid = window.CmiuImageGrid;
                if (grid.CurrentItem is CmiuImageData)
                    e.CanExecute = ClickedItem != null;
                e.Handled = true;
            }
        }

        private void LoadImageCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (sender is CmiuImageEditWindow window)
            {
                var grid = window.CmiuImageGrid;
                if (grid.CurrentItem is CmiuImageData)
                    e.CanExecute = ClickedItem != null;
                e.Handled = true;
            }
        }

        private void ViewImageCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (sender is CmiuImageEditWindow window)
            {
                var grid = window.CmiuImageGrid;
                if (grid.CurrentItem is CmiuImageData)
                    e.CanExecute = ClickedItem != null && ClickedItem.Data != null && ClickedItem.Data.Any();
                e.Handled = true;
            }
        }

        private void ViewImageDoubleClickCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (sender is CmiuImageEditWindow window)
            {
                var grid = window.CmiuImageGrid;
                if (grid.CurrentItem is CmiuImageData)
                    e.CanExecute = ClickedItem != null && ClickedItem.Data != null && ClickedItem.Data.Any();
                e.Handled = true;
            }
        }

        private void CloseCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }

        private void AddRowExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            observableCollection.Add(new CmiuImageData());

            if (sender is CmiuImageEditWindow && e.Source is DataGrid grid)
            {
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
            if (sender is CmiuImageEditWindow window)
            {
                var selectedItems = new List<CmiuImageData> { };
                var grid = window.CmiuImageGrid;
                grid.CancelEdit();
                if (grid.SelectedItems.Count > 0)
                    selectedItems.AddRange(grid.SelectedItems.Cast<CmiuImageData>());
                else
                {
                    if (grid.CurrentItem is CmiuImageData item)
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

        private void LoadImageExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                if (sender is CmiuImageEditWindow && e.Source is DataGrid grid)
                {
                    if (grid.CurrentItem is CmiuImageData item)
                    {
                        bool? result = _openFileDialog.ShowDialog();
                        if (result == true)
                        {
                            var image = new Bitmap(_openFileDialog.FileName);
                            item.Data = image.ToByteArray(imageFormat: null);
                            grid.CommitEdit(DataGridEditingUnit.Row, true);
                            CollectionViewSource.GetDefaultView(CmiuImageGrid.ItemsSource).Refresh();
                            Message(Russian.Success);
                        }
                        else
                            Message(Russian.Cancel);
                    }
                }
            }
            catch (Exception ex)
            {
                Message(Russian.ExceptionA + ex.Message);
            }

            e.Handled = true;
        }

        private void ViewImageExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (sender is CmiuImageEditWindow && e.Source is DataGrid grid)
            {
                if (grid.CurrentItem is CmiuImageData item && item.Data != null)
                    ShowImagePreviewWindows(item.Data);
            }

            e.Handled = true;
        }

        private void ViewImageDoubleClickCanExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (sender is CmiuImageEditWindow && e.Source is DataGrid grid && e.OriginalSource is DataGridCell cell)
            {
                if (grid.CurrentItem is CmiuImageData item && item.Data != null && cell.Column.Header.ToString() == "Image")
                    ShowImagePreviewWindows(item.Data);
            }

            e.Handled = true;
        }

        private void ShowImagePreviewWindows(byte[] data)
        {
            var imagePreviewWindow = _serviceProvider.GetRequiredService<CmiuImagePreviewWindow>();
            using var ms = new MemoryStream(data);
            imagePreviewWindow.Image = System.Drawing.Image.FromStream(ms);
            imagePreviewWindow.Owner = this;
            imagePreviewWindow.ShowDialog();
        }
    }
}
