using System.Configuration;

namespace Example.ReadModel
{
    public class ViewTable 
    {
        
        static ViewTable()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["ReadModel"].ConnectionString;
        }

        private static readonly string ConnectionString;

        protected dynamic Db
        {
            get { return Simple.Data.Database.OpenConnection(ConnectionString); }
        }

    }
}
