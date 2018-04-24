using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Dapper;
using KillerApp.DAL.Contexts.Base;
using KillerApp.DAL.Interfaces;
using Microsoft.Extensions.Configuration;

namespace KillerApp.DAL.Contexts
{
    public class LikeSqlContext : MsSqlConnection, ILikeContext
    {
        public LikeSqlContext(IConfiguration config) : base(config)
        {
        }

        public void AddLike(string buildId, string userId)
        {
            using (IDbConnection db = OpenConnection())
            {
                db.Open();

                var sQuery = $"UPDATE Builds SET Likes = Likes + 1 WHERE ID = '{buildId}'";
                db.Execute(sQuery);

                AddToLdBuilds(buildId, userId, true);
            }
        }

        public void AddDislike(string buildId, string userId)
        {
            using (IDbConnection db = OpenConnection())
            {
                db.Open();

                var sQuery = $"UPDATE Builds SET Dislikes = Dislikes + 1 WHERE ID = '{buildId}'";
                db.Execute(sQuery);

                AddToLdBuilds(buildId, userId, false);
            }
        }

        public void RemoveLike(string buildId, string userId)
        {
            using (IDbConnection db = OpenConnection())
            {
                db.Open();

                var sQuery = $"UPDATE Builds SET Likes = Likes - 1 WHERE ID = '{buildId}'";
                db.Execute(sQuery);

                RemoveToLdBuilds(buildId, userId, true);
            }
        }

        public void RemoveDislike(string buildId, string userId)
        {
            using (IDbConnection db = OpenConnection())
            {
                db.Open();

                var sQuery = $"UPDATE Builds SET Dislikes = Dislikes - 1 WHERE ID = '{buildId}'";
                db.Execute(sQuery);

                RemoveToLdBuilds(buildId, userId, false);
            }
        }

        public string CheckLikeStatus(string buildId, string userId)
        {
            using (IDbConnection db = OpenConnection())
            {
                db.Open();

                var sQuery = $"SELECT Liked FROM LDBuilds WHERE BuildID = '{buildId}' AND UserID = '{userId}'";

                try
                {
                    bool likestatus = db.QuerySingle<bool>(sQuery);

                    if (likestatus)
                        return "true";

                    return "false";
                }
                catch
                {
                    return "none";
                }
            }
        }


        private void AddToLdBuilds(string buildId, string userId, bool liked)
        {
            using (IDbConnection db = OpenConnection())
            {
                db.Open();

                var sQuery = $"INSERT INTO LDBuilds VALUES('{userId}', '{buildId}', '{liked}')";
                db.Execute(sQuery);
            }
        }

        private void RemoveToLdBuilds(string buildId, string userId, bool liked)
        {
            using (IDbConnection db = OpenConnection())
            {
                db.Open();

                var sQuery = $"DELETE FROM LDBuilds WHERE UserID = '{userId}' AND BuildID = '{buildId}' AND Liked = '{liked}'";
                db.Execute(sQuery);
            }
        }
    }
}
