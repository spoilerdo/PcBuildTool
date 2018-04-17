using System;
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
    public class PcBuildSqlContext : MsSqlConnection, IPcBuildContext
    {
        public PcBuildSqlContext(IConfiguration config) : base(config) { }

        #region SelectMethods
        public IEnumerable<PcPart> GetAllByType(string type, List<int> propertyIds)
        {
            using (IDbConnection db = OpenConnection())
            {
                db.Open();

                IEnumerable<PcPart> parts;
                if (propertyIds.Count != 0)
                {
                    var propertyIdQuery = new List<string>();
                    for (var i = 0; i < propertyIds.Count; i++)
                    {
                        propertyIdQuery.Add($"pa.propertieId = {propertyIds[i]}");
                        if (i < propertyIds.Count - 1) propertyIdQuery.Add("OR");
                    }

                    var sQuery =
                        $"SELECT p.ID, p._Name, p._Type, p.Information FROM Parts p, Part_Prop pa WHERE pa.PartID = p.ID AND p._Type = '{type}' AND ({string.Join(' ', propertyIdQuery)})";
                    parts = db.Query<PcPart>(sQuery);
                }
                else
                {
                    var s2Query =
                        $"SELECT * FROM Parts WHERE _Type = '{type}'";
                    parts = db.Query<PcPart>(s2Query);
                }

                foreach (var part in parts) part.Properties = GetPropertiesForPcPart(part).AsList();

                return parts;
            }
        }

        public IEnumerable<string> GetAllTypes()
        {
            using (IDbConnection db = OpenConnection())
            {
                db.Open();
                var sQuery =
                    $"SELECT _Type FROM ComponentTypes ORDER BY PriorityID";
                return db.Query<string>(sQuery);
            }
        }

        private IEnumerable<Propertie> GetPropertiesForPcPart(PcPart part)
        {
            using (IDbConnection db = OpenConnection())
            {
                db.Open();

                var sQuery =
                    $"SELECT pr.Id, pr._Value, pr.Type FROM Properties pr, Part_Prop p WHERE pr.Id = p.PropertieId AND p.PartID = '{part.ID}'";
                return db.Query<Propertie>(sQuery);
            }
        }

        public IEnumerable<PcPart> GetSelectedParts(int buildiD)
        {
            using (IDbConnection db = OpenConnection())
            {
                db.Open();

                var sQuery =
                    $"SELECT p.ID, p._Name, p._Type, p.Information FROM Parts p, Partslist pa WHERE pa.PartID = p.ID AND pa.BuildID = '{buildiD}'";
                return db.Query<PcPart>(sQuery);
            }
        }

        public IEnumerable<string> GetSelectedType(string latestType)
        {
            using (IDbConnection db = OpenConnection())
            {
                db.Open();
                return db.Query<string>("GetCurrentType", new {lastType = latestType, type = "", Index = 0},
                    commandType: CommandType.StoredProcedure);
            }
        }

        public IEnumerable<Website> GetWebsites()
        {
            using (IDbConnection db = OpenConnection())
            {
                db.Open();

                var sQuery =
                    $"SELECT _Name, _Url, Pathdetails FROM Webshop";
                return db.Query<Website>(sQuery);
            }
        }

        public IEnumerable<Propertie> GetProperties()
        {
            using (IDbConnection db = OpenConnection())
            {
                db.Open();
                var sQuery = $"SELECT Id, _Value, Type FROM Properties";
                return db.Query<Propertie>(sQuery);
            }
        }

        public PcBuild GetBuild(string buildId)
        {
            using (IDbConnection db = OpenConnection())
            {
                db.Open();

                var s1Query =
                    $"SELECT ID, _Name, _Type, Information FROM Parts p, partslist pa WHERE p.ID = pa.PartID AND pa.BuildID = '{buildId}'";
                IEnumerable<PcPart> pcParts = db.Query<PcPart>(s1Query);

                var s2Query = $"SELECT _Name, Likes, Dislikes FROM Builds WHERE ID = '{buildId}'";
                PcBuild buildInfo = db.QuerySingle<PcBuild>(s2Query);

                return new PcBuild(buildInfo._Name, pcParts.AsList(), buildInfo.Likes, buildInfo.Dislikes);
            }
        }

        public IEnumerable<PcBuild> GetAllBuilds()
        {
            using (IDbConnection db = OpenConnection())
            {
                db.Open();

                string sQuery = "SELECT ID, _Name, Likes, Dislikes FROM Builds";
                return db.Query<PcBuild>(sQuery);
            }
        }

        #endregion

        #region InsertMethods
        public void SetBuild(int id)
        {
            using (IDbConnection db = OpenConnection())
            {
                db.Open();
                var sQuery = $"INSERT INTO Builds VALUES({id}, 0, 0)";
            }
        }

        public void AddPartToBuild(PcPart pcPart, int buildId)
        {
            using (IDbConnection db = OpenConnection())
            {
                db.Open();
                var sQuery = $"INSERT INTO Partslist VALUES({buildId}, {pcPart.ID})";
                db.Execute(sQuery);
            }
        }

        public void AddPart(PcPart pcPart, File file)
        {
            using (IDbConnection db = OpenConnection())
            {
                db.Open();

                var idTable = new DataTable();
                idTable.Columns.Add("ID");
                foreach (var id in pcPart.Properties.Select(x => x.Id)) idTable.Rows.Add(id);
                db.Execute("AddPcPart", new
                {
                    ID = Guid.NewGuid(),
                    Name = pcPart._Name,
                    Type = pcPart._Type,
                    Info = pcPart.Information,
                    Prop = idTable,
                    FileID = Guid.NewGuid(),
                    FileName = file._Name,
                    FileContentType = file.ContentType,
                    FileContent = file._Content,
                    FileType = file._Type
                }, commandType: CommandType.StoredProcedure);
            }
        }
        #endregion
    }
}