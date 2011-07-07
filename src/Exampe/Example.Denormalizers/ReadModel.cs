using System;
using System.Configuration;

namespace Example.Denormalizers
{
    public class ReadModel 
    {

        static ReadModel()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["ReadModel"].ConnectionString;

            // Make an explicit reference to the Simple.Data.SqlServer.dll 
            // so that it will get included in the final build output.
            var t = typeof (Simple.Data.SqlServer.SqlConnectionProvider);

        }

        private static readonly string ConnectionString;

        protected dynamic Db
        {
            get { return Simple.Data.Database.OpenConnection(ConnectionString); }
        }

    }
}
