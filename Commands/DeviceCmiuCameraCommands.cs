using SBCore.Configurator.EditWindows;
using System.Windows.Input;

namespace SBCore.Configurator.Commands
{
    public static class DeviceCmiuCameraCommands
    {
        public static RoutedCommand AddRowCommand { get; } = new RoutedCommand("AddRow", typeof(DeviceCmiuCameraEditWindow));
        public static RoutedCommand RemoveRowCommand { get; } = new RoutedCommand("RemoveRow", typeof(DeviceCmiuCameraEditWindow));
        public static RoutedCommand CameraCommand { get; } = new RoutedCommand("Camera", typeof(DeviceCmiuCameraEditWindow));
        public static RoutedCommand DeviceCommand { get; } = new RoutedCommand("Device", typeof(DeviceCmiuCameraEditWindow));
    }
}
