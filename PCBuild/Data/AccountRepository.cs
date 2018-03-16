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

        #region SelectMethods
        public IEnumerable<string> GetUsername(string userName)
        {
            using (IDbConnection db = OpenConnection())
            {
                db.Open();

                string sQuery = 
                    $"SELECT Username FROM Accounts WHERE Username = '{userName}'";
                return db.Query<string>(sQuery);
            }
        }
        #endregion

        #region InsertMethods
        public void SetAccount(string username, string password)
        {
            using (IDbConnection db = OpenConnection())
            {
                db.Open();
                string sQuery = $"INSERT INTO Accounts VALUES(NEWID(), {username}, {password})";
                db.Execute(sQuery);
            }
        }
        #endregion
    }
}
