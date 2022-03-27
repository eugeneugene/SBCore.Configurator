using Microsoft.Extensions.DependencyInjection;
using SBShared.Types.Data;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace SBCore.Configurator.EditWindows
{
    internal partial class AdminEditWindow
    {
        private void PreviewCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (sender is DataGrid grid && e.Command == DataGrid.DeleteCommand)
            {
                if (grid.SelectedItems.Count > 0)
                {
                    var selectedItems = grid.SelectedItems.Cast<AdminData>().ToArray();
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
            if (sender is AdminEditWindow)
            {
                e.CanExecute = true;
                e.Handled = true;
            }
        }

        private void RemoveRowCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (sender is AdminEditWindow window)
            {
                var grid = window.AdminEditGrid;
                if (grid.CurrentItem is AdminData)
                    e.CanExecute = ClickedItem != null;
                e.Handled = true;
            }
        }

        private void AuthHelperCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (sender is AdminEditWindow window)
            {
                var grid = window.AdminEditGrid;
                if (grid.CurrentItem is AdminData)
                    e.CanExecute = ClickedItem != null;
                e.Handled = true;
            }
        }

        private void AddRowExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            observableCollection.Add(new AdminData());

            if (sender is AdminEditWindow window)
            {
                var grid = window.AdminEditGrid;
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
            if (sender is AdminEditWindow window)
            {
                var selectedItems = new List<AdminData> { };
                var grid = window.AdminEditGrid;
                grid.CancelEdit();
                if (grid.SelectedItems.Count > 0)
                    selectedItems.AddRange(grid.SelectedItems.Cast<AdminData>());
                else
                {
                    if (grid.CurrentItem is AdminData item)
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

        private void CloseCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }

        private void AuthHelperExecuted(object sender, ExecutedRoutedEventArgs e)
        {

            if (sender is AdminEditWindow window)
            {
                var grid = window.AdminEditGrid;
                var item = grid.CurrentItem as AdminData;

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