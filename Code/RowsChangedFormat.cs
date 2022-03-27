using SBCore.Configurator.TextResources;

namespace SBCore.Configurator.Code
{
    public static class RowsChangedFormat
    {
        public static string Format(dynamic num)
        {
            return num switch
            {
                1 => string.Format(Russian.RowsChanged1A1, num),
                2 => string.Format(Russian.RowsChanged2A1, num),
                3 => string.Format(Russian.RowsChanged2A1, num),
                4 => string.Format(Russian.RowsChanged2A1, num),
                _ => string.Format(Russian.RowsChanged5A1, num),
            };
        }
    }
}
