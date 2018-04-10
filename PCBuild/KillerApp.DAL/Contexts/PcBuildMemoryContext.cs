using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using KillerApp.DAL.Interfaces;
using KillerApp.Domain;

namespace KillerApp.DAL.Contexts
{
    public class PcBuildMemoryContext : IPcBuildContext
    {
        private List<PcPart> _pcParts = new List<PcPart>();

        public IEnumerable<PcPart> GetAllByType(string type, List<int> propertyIds)
        {
            return _pcParts.FindAll(x => x._Type == type);
        }

        public IEnumerable<string> GetAllTypes()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PcPart> GetSelectedParts(int buildiD)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetSelectedType(string latestType)
        {
            List<string> pcPartTypes = new List<string>
            {
                "Case",
                "Motherboard",
                "Processor"
            };

            int selectedTypeNumber = pcPartTypes.FindIndex(x => x.StartsWith(latestType)) + 1;

            return new List<string>{ pcPartTypes.ElementAt(selectedTypeNumber) };
        }

        public IEnumerable<Website> GetWebsites()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Propertie> GetProperties()
        {
            throw new NotImplementedException();
        }

        public void SetBuild(int id)
        {
            throw new NotImplementedException();
        }

        public void AddPartToBuild(PcPart pcPart, int buildId)
        {
            throw new NotImplementedException();
        }

        public void AddPart(PcPart pcPart)
        {
            _pcParts.Add(pcPart);
        }
    }
}
