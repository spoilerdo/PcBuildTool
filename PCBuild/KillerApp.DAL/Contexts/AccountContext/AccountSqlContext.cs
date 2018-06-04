using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using KillerApp.DAL.Contexts.Base;
using KillerApp.DAL.Interfaces;
using KillerApp.Domain;
using Microsoft.Extensions.Configuration;

namespace KillerApp.DAL.Contexts.AccountContext
{
    public class AccountSqlContext : MsSqlConnection, IAccountContext
    {
        public AccountSqlContext(IConfiguration config) : base(config)
        {
        }

        #region SelectMethods

        public string GetUsername(string username)
        {
            using (IDbConnection db = OpenConnection())
            {
                db.Open();

                var sQuery =
                    $"SELECT Username FROM Accounts WHERE Username = '{username}'";
                return db.QuerySingleOrDefault<string>(sQuery);
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

        public IEnumerable<PcBuild> GetOwnedBuilds(string username)
        {
            using (IDbConnection db = OpenConnection())
            {
                db.Open();

                var sQuery =
                    $"SELECT b.Id FROM Builds b, Accounts a WHERE b.UserID = a.UserID AND a.Username = '{username}'";
                var buildIDs = db.Query<string>(sQuery);
                var builds = new List<PcBuild>();
                foreach (var buildId in buildIDs)
                {
                    var s2Query =
                        $"SELECT p.Id, p._Name, p._Type, p.Information, f._Path FROM Parts p, Partslist pa, Files f WHERE p.FileID = f.Id AND p.Id = pa.PartID AND pa.BuildID = '{buildId}'";
                    builds.Add(new PcBuild(GetBuildName(buildId), db.Query<PcPart>(s2Query).AsList()) {Id = buildId});
                }

                return builds;
            }
        }

        public IEnumerable<PcBuild> GetLikedBuilds(string username)
        {
            using (IDbConnection db = OpenConnection())
            {
                db.Open();

                var sQuery =
                    $"SELECT ld.BuildID FROM LDBuilds ld, Accounts a WHERE ld.UserID = a.UserID AND a.Username = '{username}'";
                var buildIDs = db.Query<string>(sQuery);
                var builds = new List<PcBuild>();
                foreach (var buildId in buildIDs)
                {
                    var s2Query =
                        $"SELECT p.Id, p._Name, p._Type, p.Information, f._Path FROM Parts p, Partslist pa, Files f WHERE p.FileID = f.Id AND p.Id = pa.PartID AND pa.BuildID = '{buildId}'";

                    builds.Add(new PcBuild(GetBuildName(buildId), db.Query<PcPart>(s2Query).AsList()) { Id = buildId });
                }

                return builds;
            }
        }

        private string GetBuildName(string buildId)
        {
            using (IDbConnection db = OpenConnection())
            {
                db.Open();

                var query = 
                    $"SELECT _Name FROM Builds WHERE Id = '{buildId}'";
                return db.QuerySingle<string>(query);
            }
        }

        public string GetUserId(string username, string password)
        {
            using (IDbConnection db = OpenConnection())
            {
                db.Open();

                var sQuery = $"SELECT UserID FROM Accounts WHERE Username = '{username}' AND _Password = '{Hasher.Create(password)}'";
                return db.QuerySingle<string>(sQuery);
            }
        }

        #endregion

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

        public void DeleteAccount(string username, string password)
        {
            using (IDbConnection db = OpenConnection())
            {
                db.Open();

                db.Execute("DeleteAccount", new
                {
                    Username = username,
                    Password = Hasher.Create(password)
                }, commandType: CommandType.StoredProcedure);
            }
        }

        #endregion
    }
}