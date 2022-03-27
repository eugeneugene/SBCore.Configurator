using SBCore.Configurator.EditWindows;
using System.Windows.Input;

namespace SBCore.Configurator.Commands
{
    public static class CmiuEventCommands
    {
        public static RoutedCommand AddRowCommand { get; } = new RoutedCommand("AddRow", typeof(CmiuEventEditWindow));
        public static RoutedCommand RemoveRowCommand { get; } = new RoutedCommand("RemoveRow", typeof(CmiuEventEditWindow));
    }
}
