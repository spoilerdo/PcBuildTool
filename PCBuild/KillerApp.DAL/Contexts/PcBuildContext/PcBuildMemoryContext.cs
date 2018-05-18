using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using KillerApp.DAL.Interfaces;
using KillerApp.Domain;

namespace KillerApp.DAL.Contexts.PcBuildContext
{
    public class PcBuildMemoryContext : IPcBuildContext
    {
        private readonly List<PcPart> _pcParts = new List<PcPart>();

        #region SelectMethods

        public IEnumerable<PcPart> GetAllByType(PcPart.PcType type, List<int> propertyIds)
        {
            if (propertyIds.Count == 0)
                return _pcParts.FindAll(x => x._Type == type);

            List<PcPart> parts = new List<PcPart>();

            foreach (int id in propertyIds)
            {
                parts.AddRange(_pcParts.FindAll(x => x._Type == type && x.Properties.FindAll(y => y.Id == id).Count != 0));
            }

            return parts;
        }

        public IEnumerable<PcPart.PcType> GetAllTypes()
        {
            throw new NotImplementedException();
        }

        public PcPart.PcType GetSelectedType(PcPart.PcType latestType)
        {
            List<PcPart.PcType> types = Enum.GetValues(typeof(PcPart.PcType)).Cast<PcPart.PcType>().ToList();
            int selectedTypeNumber = types.FindIndex(x => x == latestType) + 1;

            return types.ElementAt(selectedTypeNumber);
        }

        public IEnumerable<Website> GetWebsites()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Propertie> GetProperties()
        {
            throw new NotImplementedException();
        }

        public PcBuild GetBuild(string buildId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PcBuild> GetAllBuilds()
        {
            throw new NotImplementedException();
        }

        public int GetCurrentProgress(PcPart.PcType currentType)
        {
            Dictionary<PcPart.PcType, int> progresses = new Dictionary<PcPart.PcType, int>
            {
                [PcPart.PcType.Case] =1,
                [PcPart.PcType.Motherboard] =2,
                [PcPart.PcType.Processor] =3,
                [PcPart.PcType.CPU_Cooling] =4,
                [PcPart.PcType.RAM] =5,
                [PcPart.PcType.RAM] =6,
                [PcPart.PcType.Power] =7
            };

            progresses.TryGetValue(currentType, out int id);
            return id;
        }

        public int GetMaxProgress()
        {
            return 7;
        }

        public Account GetUserFromBuild(string buildId)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region InsertMethods

        public void SetBuild(PcBuild build, string userId)
        {
            throw new NotImplementedException();
        }

        public void AddPart(PcPart pcPart, string filepath)
        {
            _pcParts.Add(pcPart);
        }

        #endregion
    }
}
