using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using KillerApp.DAL.Contexts.Base;
using KillerApp.DAL.Interfaces;
using KillerApp.Domain;
using Microsoft.Extensions.Configuration;

namespace KillerApp.DAL.Contexts.PcBuildContext
{
    public class PcBuildSqlContext : MsSqlConnection, IPcBuildContext
    {
        public PcBuildSqlContext(IConfiguration config) : base(config) { }

        #region SelectMethods

        public IEnumerable<PcPart> GetAllByType(PcPart.PcType type, List<int> propertyIds)
        {
            using (IDbConnection db = OpenConnection())
            {
                db.Open();

                string query;

                if (propertyIds.Count != 0)
                {
                    var propertyIdQuery = new List<string>();
                    for (var i = 0; i < propertyIds.Count; i++)
                    {
                        propertyIdQuery.Add($"pa.propertieId = {propertyIds[i]}");

                        if (i < propertyIds.Count - 1)
                            propertyIdQuery.Add("OR");
                    }

                    query =
                        $"SELECT p.ID, p._Name, p._Type, p.Information, f._Path FROM Parts p, Part_Prop pa, Files f WHERE p.FileID = f.ID AND pa.PartID = p.ID AND p._Type = '{type}' AND ({string.Join(' ', propertyIdQuery)})";
                }
                else
                    query =
                        $"SELECT p.ID, p._Name, p._Type, p.Information, f._Path  FROM Parts p, Files f WHERE p.FileID = f.ID AND _Type = '{type}'";

                var parts = db.Query<PcPart>(query);

                foreach (var part in parts)
                    part.Properties = GetPropertiesForPcPart(part).AsList();

                return parts;
            }
        }

        public IEnumerable<PcPart.PcType> GetAllTypes()
        {
            using (IDbConnection db = OpenConnection())
            {
                db.Open();
                var query =
                    $"SELECT PriorityID FROM ComponentTypes ORDER BY PriorityID";
                return db.Query<PcPart.PcType>(query);
            }
        }

        private IEnumerable<Propertie> GetPropertiesForPcPart(PcPart part)
        {
            using (IDbConnection db = OpenConnection())
            {
                db.Open();

                var query =
                    $"SELECT pr.Id, pr._Value, pr.Type FROM Properties pr, Part_Prop p WHERE pr.Id = p.PropertieId AND p.PartID = '{part.ID}'";
                return db.Query<Propertie>(query);
            }
        }

        public PcPart.PcType GetSelectedType(PcPart.PcType latestType)
        {
            using (IDbConnection db = OpenConnection())
            {
                db.Open();
                return Enum.Parse<PcPart.PcType>(db.QuerySingle<string>("GetCurrentType", new { lastType = latestType.ToString() },
                    commandType: CommandType.StoredProcedure));
            }
        }

        public IEnumerable<Website> GetWebsites()
        {
            using (IDbConnection db = OpenConnection())
            {
                db.Open();

                var query =
                    $"SELECT _Name, _Url, Pathdetails, Pathtitle FROM Webshop";
                return db.Query<Website>(query);
            }
        }

        public IEnumerable<Propertie> GetProperties()
        {
            using (IDbConnection db = OpenConnection())
            {
                db.Open();
                var query = $"SELECT Id, _Value, Type FROM Properties ORDER BY Type";
                return db.Query<Propertie>(query);
            }
        }

        public PcBuild GetBuild(string buildId)
        {
            using (IDbConnection db = OpenConnection())
            {
                db.Open();

                var query1 =
                    $"SELECT ID, _Name, _Type, Information FROM Parts p, partslist pa WHERE p.ID = pa.PartID AND pa.BuildID = '{buildId}'";
                IEnumerable<PcPart> pcParts = db.Query<PcPart>(query1);

                var query2 = $"SELECT _Name, Likes, Dislikes FROM Builds WHERE ID = '{buildId}'";
                PcBuild buildInfo = db.QuerySingle<PcBuild>(query2);

                return new PcBuild(buildInfo._Name, pcParts.AsList(), buildInfo.Likes, buildInfo.Dislikes);
            }
        }

        public IEnumerable<PcBuild> GetAllBuilds()
        {
            using (IDbConnection db = OpenConnection())
            {
                db.Open();

                string s1Query = "SELECT ID, _Name, Likes, Dislikes FROM Builds";
                IEnumerable<PcBuild> builds = db.Query<PcBuild>(s1Query);

                if (builds != null)
                {
                    foreach (PcBuild build in builds)
                    {
                        string s2Query =
                            $"SELECT p._Name, f._Path FROM Parts p, Files f, Partslist pa WHERE p.ID = pa.PartID AND f.ID = p.FileID AND BuildID = '{build.ID}'";

                        build.PartNames = db.Query<PcPart>(s2Query).AsList();
                    }
                }

                return builds;
            }
        }

        public int GetCurrentProgress(PcPart.PcType currentType)
        {
            using (IDbConnection db = OpenConnection())
            {
                db.Open();

                string query = $"SELECT PriorityID FROM ComponentTypes WHERE _Type = '{currentType}'";
                return db.QuerySingle<int>(query);
            }
        }

        public int GetMaxProgress()
        {
            using (IDbConnection db = OpenConnection())
            {
                db.Open();
                return db.QuerySingle<int>("SELECT MAX(PriorityID) FROM ComponentTypes");
            }
        }

        public Account GetUserFromBuild(string buildId)
        {
            using (IDbConnection db = OpenConnection())
            {
                db.Open();

                var query =
                    $"SELECT a.Username, a.UserID FROM Accounts a, Builds b WHERE a.UserID = b.UserID AND b.ID = '{buildId}'";
                return db.QuerySingle<Account>(query);
            }
        }

        #endregion

        #region InsertMethods

        public void SetBuild(PcBuild build, string userId)
        {
            using (IDbConnection db = OpenConnection())
            {
                db.Open();

                var idTable = new DataTable();
                idTable.Columns.Add("ID");

                foreach (PcPart pcPart in build.PartNames)
                    idTable.Rows.Add(pcPart.ID);

                db.Execute("AddBuild", new
                {
                    UserId = userId,
                    _Name = build._Name,
                    ID = Guid.NewGuid(),
                    Parts = idTable
                }, commandType: CommandType.StoredProcedure);
            }
        }

        public void AddPart(PcPart pcPart, string filepath)
        {
            using (IDbConnection db = OpenConnection())
            {
                db.Open();

                var idTable = new DataTable();
                idTable.Columns.Add("ID");

                foreach (var id in pcPart.Properties.Select(x => x.Id))
                    idTable.Rows.Add(id);

                var fileId = filepath == "" ? "" : Guid.NewGuid().ToString();

                db.Execute("AddPcPart", new
                {
                    ID = Guid.NewGuid(),
                    Name = pcPart._Name,
                    Type = pcPart._Type.ToString(),
                    Info = pcPart.Information,
                    Prop = idTable,
                    FileID = fileId,
                    FilePath = filepath
                }, commandType: CommandType.StoredProcedure);
            }
        }

        #endregion
    }
}