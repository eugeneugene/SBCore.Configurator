using SBCore.Configurator.EditWindows;
using System.Windows.Input;

namespace SBCore.Configurator.Commands
{
    public static class CmiuImageCommands
    {
        public static RoutedCommand AddRowCommand { get; } = new RoutedCommand("AddRow", typeof(CmiuImageEditWindow));
        public static RoutedCommand RemoveRowCommand { get; } = new RoutedCommand("RemoveRow", typeof(CmiuImageEditWindow));
        public static RoutedCommand LoadImageCommand { get; } = new RoutedCommand("LoadImage", typeof(CmiuImageEditWindow));
        public static RoutedCommand ViewImageCommand { get; } = new RoutedCommand("ViewImage", typeof(CmiuImageEditWindow));
        public static RoutedCommand ViewImageDoubleClickCommand { get; } = new RoutedCommand("ViewImageDoubleClick", typeof(CmiuImageEditWindow));
    }
}
