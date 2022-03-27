using SBCore.Configurator.EditWindows;
using System.Windows.Input;

namespace SBCore.Configurator.Commands
{
    public static class AdminEditCommands
    {
        public static RoutedCommand AddRowCommand { get; } = new RoutedCommand("AddRow", typeof(AuthEditWindow));
        public static RoutedCommand RemoveRowCommand { get; } = new RoutedCommand("RemoveRow", typeof(AuthEditWindow));
        public static RoutedCommand AuthHelperCommand { get; } = new RoutedCommand("AuthHelper", typeof(AuthEditWindow));
    }
}
