using System.Configuration;
using System.Data.SqlClient;

namespace SmartShelf.Common
{
    public class SqlStrings
    {
        public string TableName { get; set; }
        public string Add { get; set; }
        public string Update { get; set; }
        public string Delete { get; set; }
        public string QueryAll { get; set; }
        public string QueryOne { get; set; }
        public string QueryByPage { get; set; }
    }

    public class SqlConnectionFactory
    {
        public SqlConnection CreateConnection()
        {
            return new SqlConnection(GetConnectionString());
        }

        private static string GetConnectionString()
        {
            var connectionStringKey = "ConnectionString";

            if (ConfigurationManager.ConnectionStrings[connectionStringKey] == null)
                throw new ConfigurationErrorsException("Missing connectionstring for key '" + connectionStringKey + "', add one to the web.config");

            var connectionString = ConfigurationManager.ConnectionStrings[connectionStringKey].ConnectionString;
            return connectionString;
        }
    }
}
