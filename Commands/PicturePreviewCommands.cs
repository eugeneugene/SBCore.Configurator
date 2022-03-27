using SBCore.Configurator.EditWindows;
using System.Windows.Input;

namespace SBCore.Configurator.Commands
{
    public static class PicturePreviewCommands
    {
        public static RoutedCommand FullScreenPreviewCommand { get; } = new RoutedCommand("FullScreenPreview", typeof(PicturePreviewWindow));
        public static RoutedCommand ImageUpdateCommand { get; } = new RoutedCommand("ImageUpdate", typeof(PicturePreviewWindow));
    }
}
