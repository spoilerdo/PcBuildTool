using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using KillerApp.DAL.Contexts.Base;
using KillerApp.DAL.Interfaces;
using KillerApp.Domain;
using Microsoft.Extensions.Configuration;

namespace KillerApp.DAL.Contexts
{
    public class AccountSqlContext : MsSqlConnection, IAccountContext
    {
        public AccountSqlContext(IConfiguration config) : base(config)
        {
        }

        #region InsertMethods

        public void SetAccount(string username, string password)
        {
            using (IDbConnection db = OpenConnection())
            {
                db.Open();
                var sQuery = $"INSERT INTO Accounts VALUES(NEWID(), '{username}', '{Hasher.Create(password)}')";
                db.Execute(sQuery);
            }
        }

        #endregion

        #region SelectMethods

        public IEnumerable<string> GetUsername(string username)
        {
            using (IDbConnection db = OpenConnection())
            {
                db.Open();

                var sQuery =
                    $"SELECT Username FROM Accounts WHERE Username = '{username}'";
                return db.Query<string>(sQuery);
            }
        }

        public bool TryLogin(Account account)
        {
            using (IDbConnection db = OpenConnection())
            {
                db.Open();

                var sQuery =
                    $"SELECT UserID FROM Accounts WHERE Username = '{account.UserName}' AND _Password = '{Hasher.Create(account.Password)}'";
                var result = db.Query<string>(sQuery);

                if (!string.IsNullOrEmpty(result.First()))
                    return true;

                return false;
            }
        }

        public IEnumerable<PcBuild> GetBuilds(string username)
        {
            using (IDbConnection db = OpenConnection())
            {
                db.Open();

                var sQuery =
                    $"SELECT b.ID FROM Builds b, Accounts a WHERE b.UserID = a.UserID AND a.Username = '{username}'";
                var buildIDs = db.Query<string>(sQuery);
                var builds = new List<PcBuild>();
                foreach (var buildId in buildIDs)
                {
                    var s1Query =
                        $"SELECT b._Name FROM Builds b, Accounts a WHERE b.UserID = a.UserID AND a.Username = '{username}'";
                    var s2Query =
                        $"SELECT p.ID, p._Name, p._Type, p.Information FROM Parts p, Partslist pa WHERE p.ID = pa.PartID AND pa.BuildID = '{buildId}'";
                    builds.Add(new PcBuild(db.Query<string>(s1Query).First(), db.Query<PcPart>(s2Query).AsList()));
                }

                return builds;
            }
        }

        public string GetUserId(string username, string password)
        {
            using (IDbConnection db = OpenConnection())
            {
                db.Open();

                var sQuery = $"SELECT UserID FROM Accounts WHERE Username = '{username}' AND _Password = '{password}'";
                return db.QuerySingle<string>(sQuery);
            }
        }
        #endregion
    }
}