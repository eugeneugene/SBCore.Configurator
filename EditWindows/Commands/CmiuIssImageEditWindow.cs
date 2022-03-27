using SBShared.Types.Data;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;

namespace SBCore.Configurator.EditWindows
{
    internal partial class CmiuIssImageEditWindow
    {
        private void PreviewCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (sender is DataGrid grid && e.Command == DataGrid.DeleteCommand)
            {
                if (grid.SelectedItems.Count > 0)
                {
                    var selectedItems = grid.SelectedItems.Cast<CmiuIssArchiveImageData>().ToArray();
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
                if (grid.CurrentItem is CmiuIssArchiveImageData)
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
            observableCollection.Add(new CmiuIssArchiveImageData());

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
                var selectedItems = new List<CmiuIssArchiveImageData> { };
                var grid = window.CmiuImageGrid;
                grid.CancelEdit();
                if (grid.SelectedItems.Count > 0)
                    selectedItems.AddRange(grid.SelectedItems.Cast<CmiuIssArchiveImageData>());
                else
                {
                    if (grid.CurrentItem is CmiuIssArchiveImageData item)
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
    }
}
