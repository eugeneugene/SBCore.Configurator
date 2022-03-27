using SBCore.Configurator.EditWindows;
using System.Windows.Input;

namespace SBCore.Configurator.Commands
{
    public static class LprIssEventCommands
    {
        public static RoutedCommand AddRowCommand { get; } = new RoutedCommand("AddRow", typeof(LprIssEventEditWindow));
        public static RoutedCommand RemoveRowCommand { get; } = new RoutedCommand("RemoveRow", typeof(LprIssEventEditWindow));
        public static RoutedCommand DeviceCmiuCameraCommand { get; } = new RoutedCommand("DeviceCmiuCamera", typeof(LprIssEventEditWindow));
    }
}
