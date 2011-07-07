using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;

namespace Example.Denormalizers
{

    public class ReadModel
    {
        private readonly string _connStr;

        public ReadModel()
        {
            _connStr = ConfigurationManager.ConnectionStrings["ReadModel"]
                .ConnectionString;
        }

        public void Insert(string tableName, Dictionary<string, object> values)
        {
            var fieldNames = new StringBuilder();
            var fieldValues = new StringBuilder();
            var parameters = new Dictionary<string, object>();
            
            foreach (var value in values)
            {
                if (fieldNames.Length != 0)
                {
                    fieldNames.AppendLine(",");
                    fieldValues.AppendLine(",");
                }
                fieldNames.AppendFormat("[{0}]", value.Key);
                
                var paramName = string.Format("p{0}", parameters.Count);
                fieldValues.AppendFormat("@{0}", paramName);
                parameters[paramName] = value.Value;
            }

            var query = string.Format("INSERT INTO [{0}] ({1}) VALUES ({2})",
                                      tableName,
                                      fieldNames,
                                      fieldValues);
            Execute(query, parameters);
        }

        public void Execute(string query, Dictionary<string, object> parameters)
        {
            var cmd = new SqlCommand(query);
            foreach (var param in parameters)
                cmd.Parameters.AddWithValue(param.Key, param.Value);
            Execute(cmd);
        }

        public void Execute(SqlCommand command)
        {
            Execute(command, rowsAffected => { });
        }

        public void Execute(SqlCommand command, int expectedRowsAffected)
        {
            Execute(command, rowsAffected =>
                                 {
                                     if (rowsAffected != expectedRowsAffected)
                                         throw new ApplicationException(
                                             string.Format(
                                                 "Expected {0} rows affected. {1} rows were actually affected.",
                                                 expectedRowsAffected, rowsAffected));
                                 });
        }

        public void Execute(SqlCommand command, Action<int> checkRowsAffected)
        {
            using (var conn = new SqlConnection(_connStr))
            {
                conn.Open();
                using (command)
                {
                    command.Connection = conn;
                    var rowsAffected = command.ExecuteNonQuery();
                    checkRowsAffected(rowsAffected);
                }
                conn.Close();
            }
        }

    }

}
