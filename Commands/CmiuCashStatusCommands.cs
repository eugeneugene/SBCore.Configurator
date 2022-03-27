using SBCore.Configurator.EditWindows;
using System.Windows.Input;

namespace SBCore.Configurator.Commands
{
    public static class CmiuCashStatusCommands
    {
        public static RoutedCommand AddRowCommand { get; } = new RoutedCommand("AddRow", typeof(CmiuCashStatusEditWindow));
        public static RoutedCommand RemoveRowCommand { get; } = new RoutedCommand("RemoveRow", typeof(CmiuCashStatusEditWindow));
    }
}
