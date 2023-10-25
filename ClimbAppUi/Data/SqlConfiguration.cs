using System.Security.Cryptography.X509Certificates;

namespace BlazorCLIMB.UI.Data
{
    public class SqlConfiguration
    {

        public SqlConfiguration(string connectionString) => ConnectionString = connectionString;

        public string ConnectionString { get; }
    
    }
    
}
