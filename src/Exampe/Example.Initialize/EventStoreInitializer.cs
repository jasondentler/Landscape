using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Reflection;
using log4net;

namespace Example.Initialize
{
    public class EventStoreInitializer : DatabaseInitializer
    {

        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["MsSqlEventStore"].ConnectionString;
        }

        public EventStoreInitializer() 
            : base(GetConnectionString())
        {
        }

        public override void Initialize()
        {
            Log.InfoFormat("Dropping and recreating event store database {0}", GetDatabaseName());
            base.Initialize();
        }

        protected override void InitializeDatabaseObjects(SqlConnection connection)
        {
            CreateTables(connection);
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


    }
}
