using System.Configuration;
using System.Data.SqlClient;
using System.Linq;

namespace Example.ReadModel.Tests
{

    public abstract class SimpleDataFixture : Fixture
    {

        private string ConnectionString
        {
            get
            {
                return ConfigurationManager
                    .ConnectionStrings["ReadModel"]
                    .ConnectionString;
            }
        }

        protected dynamic Db
        {
            get { return Simple.Data.Database.OpenConnection(ConnectionString); }
        }

        private readonly string[] _tableNames;

        protected SimpleDataFixture(string tableName, params string[] tableNames)
        {
            _tableNames = new[] {tableName}.Concat(tableNames).ToArray();
        }

        protected override void OnSetup()
        {
            base.OnSetup();

            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                foreach (var tableName in _tableNames)
                    TruncateTable(tableName, conn);
                conn.Close();
            }

        }

        private void TruncateTable(string tableName, SqlConnection connection)
        {
            var sql = string.Format("TRUNCATE TABLE [{0}]", tableName);
            var cmd = new SqlCommand(sql, connection);
            cmd.ExecuteNonQuery();
        }

    }
}
