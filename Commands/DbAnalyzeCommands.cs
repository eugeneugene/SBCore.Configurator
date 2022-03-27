using System.Windows.Input;

namespace SBCore.Configurator.Commands
{
    public static class DbAnalyzeCommands
    {
        public static RoutedCommand EditTableCommand { get; } = new RoutedCommand(nameof(EditTableCommand), typeof(DbAnalyze));
        public static RoutedCommand RecreateTableCommand { get; } = new RoutedCommand(nameof(RecreateTableCommand), typeof(DbAnalyze));
        public static RoutedCommand PurgeTableCommand { get; } = new RoutedCommand(nameof(PurgeTableCommand), typeof(DbAnalyze));
        public static RoutedCommand RecreateTablesCommand { get; } = new RoutedCommand(nameof(RecreateTablesCommand), typeof(DbAnalyze));
        public static RoutedCommand RecreateBrokenTablesCommand { get; } = new RoutedCommand(nameof(RecreateBrokenTablesCommand), typeof(DbAnalyze));
        public static RoutedCommand PurgeTablesCommand { get; } = new RoutedCommand(nameof(PurgeTablesCommand), typeof(DbAnalyze));
        public static RoutedCommand StatusDetailsCommand { get; } = new RoutedCommand(nameof(StatusDetailsCommand), typeof(DbAnalyze));
    }
}
