using PCBuild_Data.Base;
using System;
using System.Collections.Generic;
using System.Text;
using PCBuild_Data.Models;
using Microsoft.Extensions.Configuration;
using System.Data;
using Dapper;

namespace PCBuild_Data
{
    public class PCBuildRepository : BaseRepository, IPCBuild
    {
        public PCBuildRepository(IConfiguration config) : base(config)
        {
        }
        public IEnumerable<PCPart> getAll()
        {
            using(IDbConnection db = OpenConnection())
            {
                db.Open();
                string sQuery = "SELECT * FROM Parts";
                return db.Query<PCPart>(sQuery).AsList();
            }
        }
        public IEnumerable<PCPart> getByID(int ID)
        {
            using(IDbConnection db = OpenConnection())
            {
                db.Open();
                string sQuery = $"SELECT * FROM Parts WHERE EAN = {ID}";
                return db.Query<PCPart>(sQuery);
            }
        }
        public IEnumerable<string> getPartTypes()
        {
            using (IDbConnection db = OpenConnection())
            {
                db.Open();
                string sQuery = "SELECT _Type FROM ComponentTypes ORDER BY PriorityID";
                return db.Query<string>(sQuery);
            }
        }
        public IEnumerable<int> partlistCount(int BuildID)
        {
            using(IDbConnection db = OpenConnection())
            {
                db.Open();
                string sQuery = $"SELECT COUNT(*) FROM Partslist WHERE BuildID = {BuildID}";
                return db.Query<int>(sQuery);
            }
        }

        public void setBuild(int ID)
        {
            using(IDbConnection db = OpenConnection())
            {
                db.Open();
                string sQuery = $"INSERT INTO Builds VALUES({ID}, 0, 0)";
            }
        }
        public void addPart(PCPart pcPart, int BuildID)
        {
            using(IDbConnection db = OpenConnection())
            {
                db.Open();
                string sQuery = $"INSERT INTO Partslist VALUES({BuildID}, {pcPart.EAN})";
            }
        }
    }
}
