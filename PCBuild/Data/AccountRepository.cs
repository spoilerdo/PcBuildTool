using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dapper;
using Data.Base;
using Microsoft.Extensions.Configuration;

namespace Data
{
    public class AccountRepository : BaseRepository, IAccount
    {
        public AccountRepository(IConfiguration config) : base(config)
        {
        }

        public void SetAccount(string username, string password)
        {
            using (IDbConnection db = OpenConnection())
            {
                db.Open();
                string sQuery = $"INSERT INTO Accounts VALUES(NEWID(), {username}, {password})";
                db.Execute(sQuery);
            }
        }
    }
}
