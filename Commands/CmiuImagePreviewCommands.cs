using SBCore.Configurator.EditWindows;
using System.Windows.Input;

namespace SBCore.Configurator.Commands
{
    public static class CmiuImagePreviewCommands
    {
        public static RoutedCommand FullScreenPreviewCommand { get; } = new RoutedCommand("FullScreenPreview", typeof(CmiuImagePreviewWindow));
    }
}
