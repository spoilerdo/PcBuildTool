using Dapper;
using Data.Base;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.Metadata;
using System.Text;
using API.Models;

namespace Data
{
    public class PcBuildRepository : BaseRepository, IPcBuild
    {
        public PcBuildRepository(IConfiguration config) : base(config)
        {
        }

        #region SelectMethods
        public IEnumerable<PcPart> GetAllByType(string type, List<int> propertyIds)
        {
            using (IDbConnection db = OpenConnection())
            {
                db.Open();

                IEnumerable<PcPart> parts;
                if (propertyIds.Count != 0)
                {
                    List<string> propertyIdQuery = new List<string>();
                    for (int i = 0; i < propertyIds.Count; i++)
                    {
                        propertyIdQuery.Add($"pa.propertieId = {propertyIds[i]}");
                        if (i < propertyIds.Count - 1)
                        {
                            propertyIdQuery.Add("OR");
                        }
                    }
                    string sQuery =
                        $"SELECT p.EAN, p._Name, p._Type, p.Information FROM Parts p, Part_Prop pa WHERE pa.EAN = p.EAN AND p._Type = '{type}' AND ({String.Join(' ', propertyIdQuery)})";
                    parts = db.Query<PcPart>(sQuery);
                }
                else
                {
                    string s2Query =
                        $"SELECT * FROM Parts WHERE _Type = '{type}'";
                    parts = db.Query<PcPart>(s2Query);
                }

                foreach (PcPart part in parts)
                {
                    part.Properties = GetProperties(part).AsList();
                }

                return parts;
            }
        }
        private IEnumerable<Propertie> GetProperties(PcPart part)
        {
            using (IDbConnection db = OpenConnection())
            {
                db.Open();

                string sQuery = 
                    $"SELECT pr.Id, pr.Propertie FROM Properties pr, Part_Prop p WHERE pr.Id = p.PropertieId AND p.EAN = {part.EAN}";
                return db.Query<Propertie>(sQuery);
            }
        }

        public IEnumerable<PcPart> GetSelectedParts(int buildiD)
        {
            using (IDbConnection db = OpenConnection())
            {
                db.Open();

                string sQuery =
                    $"SELECT p.EAN, p._Name, p._Type, p.Information FROM Parts p, Partslist pa WHERE pa.EAN = p.EAN AND pa.BuildID = {buildiD}";
                return db.Query<PcPart>(sQuery);
            }
        }
        public IEnumerable<string> GetSelectedType()
        {
            using (IDbConnection db = OpenConnection())
            {
                db.Open();
                return db.Query<string>("GetCurrentType", new {t = "", Index = 0}, commandType: CommandType.StoredProcedure);
            }
        }
        #endregion

        #region InsertMethods
        public void SetBuild(int id)
        {
            using (IDbConnection db = OpenConnection())
            {
                db.Open();
                string sQuery = $"INSERT INTO Builds VALUES({id}, 0, 0)";
            }
        }
        public void AddPart(PcPart pcPart, int buildId)
        {
            using (IDbConnection db = OpenConnection())
            {
                db.Open();
                string sQuery = $"INSERT INTO Partslist VALUES({buildId}, {pcPart.EAN})";
                db.Execute(sQuery);
            }
        }
        #endregion
    }
}
