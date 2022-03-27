using SBCore.Configurator.EditWindows;
using System.Windows.Input;

namespace SBCore.Configurator.Commands
{
    public static class AisSessionDeliveryStatusCommand
    {
        public static RoutedCommand AddRowCommand { get; } = new RoutedCommand("AddRow", typeof(AisSessionDeliveryStatusEditWindow));
        public static RoutedCommand RemoveRowCommand { get; } = new RoutedCommand("RemoveRow", typeof(AisSessionDeliveryStatusEditWindow));
    }
}
