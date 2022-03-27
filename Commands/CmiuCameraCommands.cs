using SBCore.Configurator.EditWindows;
using System.Windows.Input;

namespace SBCore.Configurator.Commands
{
    public static class CmiuCameraCommands
    {
        public static RoutedCommand AddRowCommand { get; } = new RoutedCommand("AddRow", typeof(CmiuCameraEditWindow));
        public static RoutedCommand RemoveRowCommand { get; } = new RoutedCommand("RemoveRow", typeof(CmiuCameraEditWindow));
        public static RoutedCommand PingCommand { get; } = new RoutedCommand("Ping", typeof(CmiuCameraEditWindow));
        public static RoutedCommand PictureCommand { get; } = new RoutedCommand("Picture", typeof(CmiuCameraEditWindow));
        public static RoutedCommand VideoCommand { get; } = new RoutedCommand("Video", typeof(CmiuCameraEditWindow));
        public static RoutedCommand AuthHelperCommand { get; } = new RoutedCommand("AuthHelper", typeof(CmiuCameraEditWindow));
    }
}
