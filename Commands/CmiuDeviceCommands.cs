using SBCore.Configurator.EditWindows;
using System.Windows.Input;

namespace SBCore.Configurator.Commands
{
    public static class CmiuDeviceCommands
    {
        public static RoutedCommand AddRowCommand { get; } = new RoutedCommand("AddRow", typeof(CmiuDeviceEditWindow));
        public static RoutedCommand RemoveRowCommand { get; } = new RoutedCommand("RemoveRow", typeof(CmiuDeviceEditWindow));
        public static RoutedCommand PingCommand { get; } = new RoutedCommand("Ping", typeof(CmiuDeviceEditWindow));
        public static RoutedCommand VersionCommand { get; } = new RoutedCommand("Version", typeof(CmiuDeviceEditWindow));
        public static RoutedCommand CashCommand { get; } = new RoutedCommand("Cash", typeof(CmiuDeviceEditWindow));
        public static RoutedCommand RebootCommand { get; } = new RoutedCommand("Reboot", typeof(CmiuDeviceEditWindow));
        public static RoutedCommand AuthHelperCommand { get; } = new RoutedCommand("AuthHelper", typeof(CmiuDeviceEditWindow));
    }
}
