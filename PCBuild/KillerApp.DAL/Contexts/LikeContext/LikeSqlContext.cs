using System.Data;
using Dapper;
using KillerApp.DAL.Contexts.Base;
using KillerApp.DAL.Interfaces;
using KillerApp.Domain;
using Microsoft.Extensions.Configuration;

namespace KillerApp.DAL.Contexts.LikeContext
{
    public class LikeSqlContext : MsSqlConnection, ILikeContext
    {
        public LikeSqlContext(IConfiguration config) : base(config)
        {
        }

        #region SelectMethods

        public bool? CheckLikeStatus(string buildId, string userId)
        {
            using (IDbConnection db = OpenConnection())
            {
                db.Open();

                var query = $"SELECT Liked FROM LDBuilds WHERE BuildID = '{buildId}' AND UserID = '{userId}'";

                try
                {
                    return db.QuerySingle<bool>(query);
                }
                catch
                {
                    return null;
                }
            }
        }

        #endregion

        #region InsertMethods

        public void AddLike(PcBuild build, string userId)
        {
            using (IDbConnection db = OpenConnection())
            {
                db.Open();

                var query = $"UPDATE Builds SET Likes = Likes + 1 WHERE Id = '{build.Id}'";
                db.Execute(query);

                AddToLdBuilds(build.Id, userId, true);
            }
        }

        public void AddDislike(PcBuild build, string userId)
        {
            using (IDbConnection db = OpenConnection())
            {
                db.Open();

                var query = $"UPDATE Builds SET Dislikes = Dislikes + 1 WHERE Id = '{build.Id}'";
                db.Execute(query);

                AddToLdBuilds(build.Id, userId, false);
            }
        }

        public void RemoveLike(PcBuild build, string userId)
        {
            using (IDbConnection db = OpenConnection())
            {
                db.Open();

                var query = $"UPDATE Builds SET Likes = Likes - 1 WHERE Id = '{build.Id}'";
                db.Execute(query);

                RemoveToLdBuilds(build.Id, userId, true);
            }
        }

        public void RemoveDislike(PcBuild build, string userId)
        {
            using (IDbConnection db = OpenConnection())
            {
                db.Open();

                var query = $"UPDATE Builds SET Dislikes = Dislikes - 1 WHERE Id = '{build.Id}'";
                db.Execute(query);

                RemoveToLdBuilds(build.Id, userId, false);
            }
        }

        private void AddToLdBuilds(string buildId, string userId, bool liked)
        {
            using (IDbConnection db = OpenConnection())
            {
                db.Open();

                var query = $"INSERT INTO LDBuilds VALUES('{userId}', '{buildId}', '{liked}')";
                db.Execute(query);
            }
        }

        private void RemoveToLdBuilds(string buildId, string userId, bool liked)
        {
            using (IDbConnection db = OpenConnection())
            {
                db.Open();

                var query = $"DELETE FROM LDBuilds WHERE UserID = '{userId}' AND BuildID = '{buildId}' AND Liked = '{liked}'";
                db.Execute(query);
            }
        }

        #endregion
    }
}
