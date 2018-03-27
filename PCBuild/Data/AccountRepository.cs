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
                    $"SELECT UserID FROM Accounts WHERE Username = '{account.UserName}' AND _Password = '{Hasher.Create(account.Password)}'";
                IEnumerable<string> result = db.Query<string>(sQuery);

                if (!String.IsNullOrEmpty(result.First()))
                    return true;

                return false;
            }
        }

        public IEnumerable<PCBuild> GetBuilds(string userName)
        {
            using (IDbConnection db = OpenConnection())
            {
                db.Open();

                string sQuery = 
                    $"SELECT b.BuildID FROM Builds b, Accounts a WHERE b.UserID = a.UserID AND a.Username = '{userName}'";
                IEnumerable<string> buildIDs = db.Query<string>(sQuery);
                List<PCBuild> builds = new List<PCBuild>();
                foreach (string buildId in buildIDs)
                {
                    string s1Query =
                        $"SELECT b._Name FROM Builds b, Accounts a WHERE b.UserID = a.UserID AND a.Username = '{userName}'";
                    string s2Query =
                        $"SELECT p.EAN, p._Name, p._Type, p.Information FROM Parts p, Partslist pa WHERE p.EAN = pa.EAN AND pa.BuildID = '{buildId}'";
                    builds.Add(new PCBuild(db.Query<string>(s1Query).First(), db.Query<PcPart>(s2Query).AsList()));
                }
                return builds;
            }
        }
        #endregion

        #region InsertMethods
        public void SetAccount(string username, string password)
        {
            using (IDbConnection db = OpenConnection())
            {
                db.Open();
                string sQuery = $"INSERT INTO Accounts VALUES(NEWID(), '{username}', '{Hasher.Create(password)}')";
                db.Execute(sQuery);
            }
        }
        #endregion
    }
}
