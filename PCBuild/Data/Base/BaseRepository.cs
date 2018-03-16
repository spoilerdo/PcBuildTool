using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Data.Base
{
    public abstract class BaseRepository
    {
        private readonly IConfiguration config;
        private readonly string connectionString;

        public BaseRepository(IConfiguration config)
        {
            this.config = config;
            this.connectionString = config["Data:DefaultConnection"];
        }

        public SqlConnection OpenConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}