using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Example.Initialize
{
    public abstract class DatabaseInitializer : IInitializer
    {
        private readonly string _connectionString;

        protected DatabaseInitializer(string connectionString)
        {
            _connectionString = connectionString;
        }

        public virtual void Initialize()
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SwitchToMaster(conn);
                DropIfExists(conn);
                CreateDatabase(conn);
                SwitchToDatabase(conn);
                InitializeDatabaseObjects(conn);
                conn.Close();
            }
        }

        protected abstract void InitializeDatabaseObjects(SqlConnection connection);

        private void DropIfExists(SqlConnection connection)
        {
            var sqlString = string.Format(
                "IF  EXISTS (SELECT name FROM sys.databases WHERE name = N'{0}')" +
                "\nDROP DATABASE [{0}]", GetDatabaseName());

            var cmd = new SqlCommand(sqlString, connection);
            cmd.ExecuteNonQuery();

        }

        private void CreateDatabase(SqlConnection connection)
        {
            var sqlString = string.Format("CREATE DATABASE {0}", GetDatabaseName());
            var cmd = new SqlCommand(sqlString, connection);
            cmd.ExecuteNonQuery();
        }

        private void SwitchToMaster(SqlConnection connection)
        {
            var cmd = new SqlCommand("USE Master", connection);
            cmd.ExecuteNonQuery();
        }

        private void SwitchToDatabase(SqlConnection connection)
        {
            var sqlString = string.Format("USE {0}", GetDatabaseName());
            var cmd = new SqlCommand(sqlString, connection);
            cmd.ExecuteNonQuery();
        }

        protected virtual string GetDatabaseName()
        {
            var bldr = new SqlConnectionStringBuilder(_connectionString);
            return bldr.InitialCatalog;
        }




    }
}
