using NHibernate.Connection;
using System.Data.Common;

namespace WebApplication1.Repositories.DbContext.Drivers
{
    public class SQLiteConnectionProviderWithForeignKeys : NHibernate.Connection.DriverConnectionProvider
    {
        public override DbConnection GetConnection()
        {
            var connection = base.GetConnection();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "PRAGMA foreign_keys = ON;";
                command.ExecuteNonQuery();
            }
            return connection;
        }
    }
}
