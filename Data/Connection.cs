using Microsoft.Data.Sqlite;

namespace SBCore.Configurator.Data
{
    public class Connection
    {
        public SqliteConnection SqliteConnection { get; } = (SqliteConnection)SqliteFactory.Instance.CreateConnection();
    }
}
