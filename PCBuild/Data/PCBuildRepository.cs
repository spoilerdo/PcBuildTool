using Dapper;
using Data.Base;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using API.Models;

namespace Data
{
    public class PCBuildRepository : BaseRepository, IPCBuild
    {
        public PCBuildRepository(IConfiguration config) : base(config)
        {
        }
        public IEnumerable<PCPart> GetAll()
        {
            using (IDbConnection db = OpenConnection())
            {
                db.Open();
                string sQuery = "SELECT * FROM Parts";
                return db.Query<PCPart>(sQuery).AsList();
            }
        }
        public IEnumerable<PCPart> GetByID(int ID)
        {
            using (IDbConnection db = OpenConnection())
            {
                db.Open();
                string sQuery = $"SELECT * FROM Parts WHERE EAN = {ID}";
                return db.Query<PCPart>(sQuery);
            }
        }
        public IEnumerable<string> GetPartTypes()
        {
            using (IDbConnection db = OpenConnection())
            {
                db.Open();
                string sQuery = "SELECT _Type FROM ComponentTypes ORDER BY PriorityID";
                return db.Query<string>(sQuery);
            }
        }
        public IEnumerable<int> PartlistCount(int BuildID)
        {
            using (IDbConnection db = OpenConnection())
            {
                db.Open();
                string sQuery = $"SELECT COUNT(*) FROM Partslist WHERE BuildID = {BuildID}";
                return db.Query<int>(sQuery);
            }
        }

        public void SetBuild(int ID)
        {
            using (IDbConnection db = OpenConnection())
            {
                db.Open();
                string sQuery = $"INSERT INTO Builds VALUES({ID}, 0, 0)";
            }
        }
        public void AddPart(PCPart pcPart, int BuildID)
        {
            using (IDbConnection db = OpenConnection())
            {
                db.Open();
                string sQuery = $"INSERT INTO Partslist VALUES({BuildID}, {pcPart.EAN})";
            }
        }
    }
}
