using SBCore.Configurator.EditWindows;
using System.Windows.Input;

namespace SBCore.Configurator.Commands
{
    public static class EventCameraCommands
    {
        public static RoutedCommand AddRowCommand { get; } = new RoutedCommand("AddRow", typeof(EventCameraEditWindow));
        public static RoutedCommand RemoveRowCommand { get; } = new RoutedCommand("RemoveRow", typeof(EventCameraEditWindow));
        public static RoutedCommand DeviceCmiuCameraCommand { get; } = new RoutedCommand("DeviceCmiuCamera", typeof(EventCameraEditWindow));
    }
}
