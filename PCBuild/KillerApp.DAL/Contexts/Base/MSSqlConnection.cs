using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace KillerApp.DAL.Contexts.Base
{
    public abstract class MsSqlConnection
    {
        private readonly IConfiguration _config;
        private readonly string _connectionString;

        protected MsSqlConnection(IConfiguration config)
        {
            _config = config;
            _connectionString = config["Data:DefaultConnection"];
        }

        public SqlConnection OpenConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}