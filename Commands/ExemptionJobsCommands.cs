using SBCore.Configurator.EditWindows;
using System.Windows.Input;

namespace SBCore.Configurator.Commands
{
    public static class ExemptionJobsCommands
    {
        public static RoutedCommand AddRowCommand { get; } = new RoutedCommand("AddRow", typeof(ExemptionJobsEditWindow));
        public static RoutedCommand RemoveRowCommand { get; } = new RoutedCommand("RemoveRow", typeof(ExemptionJobsEditWindow));
    }
}
