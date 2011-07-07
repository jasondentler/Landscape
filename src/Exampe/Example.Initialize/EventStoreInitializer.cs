using System.Configuration;
using System.Data.SqlClient;

namespace Example.Initialize
{
    public class EventStoreInitializer : IInitializer
    {
        public void Initialize()
        {
            var connStr = ConfigurationManager.ConnectionStrings["MsSqlEventStore"]
                .ConnectionString;
            using (var conn = new SqlConnection(connStr))
            {
                conn.Open();
                SwitchToMaster(conn);
                DropIfExists(conn);
                CreateDatabase(conn);
                SwitchToEventStore(conn);
                CreateTables(conn);
                conn.Close();
            }
        }

        private void DropIfExists(SqlConnection connection)
        {
            var sqlString = string.Format(
                "IF  EXISTS (SELECT name FROM sys.databases WHERE name = N'{0}')" +
                "\nDROP DATABASE [{0}]", GetEventStoreDbName(connection));
            
            var cmd = new SqlCommand(sqlString, connection);
            cmd.ExecuteNonQuery();

        }

        private void CreateDatabase(SqlConnection connection)
        {
            var sqlString = string.Format("CREATE DATABASE {0}", GetEventStoreDbName(connection));
            var cmd = new SqlCommand(sqlString, connection);
            cmd.ExecuteNonQuery();
        }

        private void CreateTables(SqlConnection connection)
        {
            const string script = @"CREATE TABLE [dbo].[EventSources](
	[Id] [uniqueidentifier] NOT NULL,
	[Version] [int] NOT NULL,
 CONSTRAINT [PK_EventSources] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

CREATE TABLE [dbo].[Events](
	[EventSourceid] [uniqueidentifier] NOT NULL,
	[Version] [int] NOT NULL,
	[TypeName] [nvarchar](max) NOT NULL,
	[Data] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Events_1] PRIMARY KEY NONCLUSTERED 
(
	[EventSourceid] ASC,
	[Version] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]";

            var cmd = new SqlCommand(script, connection);
            cmd.ExecuteNonQuery();
        }

        private void SwitchToMaster(SqlConnection connection)
        {
            var cmd = new SqlCommand("USE Master", connection);
            cmd.ExecuteNonQuery();
        }

        private void SwitchToEventStore(SqlConnection connection)
        {
            var sqlString = string.Format("USE {0}", GetEventStoreDbName(connection));
            var cmd = new SqlCommand(sqlString, connection);
            cmd.ExecuteNonQuery();
        }

        private string GetEventStoreDbName(SqlConnection connection)
        {
            var connStr = connection.ConnectionString;
            var bldr = new SqlConnectionStringBuilder(connStr);
            return bldr.InitialCatalog;
        }

    }
}
