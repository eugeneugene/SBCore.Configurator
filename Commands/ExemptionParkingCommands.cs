using SBCore.Configurator.EditWindows;
using System.Windows.Input;

namespace SBCore.Configurator.Commands
{
    public static class ExemptionParkingCommands
    {
        public static RoutedCommand AddRowCommand { get; } = new RoutedCommand("AddRow", typeof(ExemptionParkingEditWindow));
        public static RoutedCommand RemoveRowCommand { get; } = new RoutedCommand("RemoveRow", typeof(ExemptionParkingEditWindow));
    }
}
