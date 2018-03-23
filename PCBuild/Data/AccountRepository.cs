using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using API.Models;
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
        public bool TryLogin(Account account)
        {
            using (IDbConnection db = OpenConnection())
            {
                db.Open();

                string sQuery =
                    $"SELECT UserID FROM Accounts WHERE Username = '{account.UserName}' AND _Password = '{account.Password}'";
                IEnumerable<string> result = db.Query<string>(sQuery);

                if (!String.IsNullOrEmpty(result.First()))
                    return true;

                return false;
            }
        }

        public IEnumerable<Build> GetBuilds(string userName)
        {
            using (IDbConnection db = OpenConnection())
            {
                db.Open();

                //TODO: implement the SELECT query
                return null;
            }
        }
        #endregion

        #region InsertMethods
        public void SetAccount(string username, string password)
        {
            using (IDbConnection db = OpenConnection())
            {
                db.Open();
                string sQuery = $"INSERT INTO Accounts VALUES(NEWID(), '{username}', {password})";
                db.Execute(sQuery);
            }
        }
        #endregion
    }
}
