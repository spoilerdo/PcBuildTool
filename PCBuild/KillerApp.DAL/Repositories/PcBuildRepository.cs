using System.Collections.Generic;
using KillerApp.DAL.Interfaces;
using KillerApp.Domain;

namespace KillerApp.DAL.Repositories
{
    public class PcBuildRepository : IPcBuildContext
    {
        private readonly IPcBuildContext _pcBuildContext;

        public PcBuildRepository(IPcBuildContext pcBuildContext)
        {
            _pcBuildContext = pcBuildContext;
        }

        public IEnumerable<PcPart> GetAllByType(string type, List<int> propertyIds)
        {
            return _pcBuildContext.GetAllByType(type, propertyIds);
        }

        public IEnumerable<string> GetAllTypes()
        {
            return _pcBuildContext.GetAllTypes();
        }

        public IEnumerable<PcPart> GetSelectedParts(int buildId)
        {
            return _pcBuildContext.GetSelectedParts(buildId);
        }

        public IEnumerable<string> GetSelectedType(string latestType)
        {
            return _pcBuildContext.GetSelectedType(latestType);
        }

        public IEnumerable<Website> GetWebsites()
        {
            return _pcBuildContext.GetWebsites();
        }

        public IEnumerable<Propertie> GetProperties()
        {
            return _pcBuildContext.GetProperties();
        }

        public void SetBuild(int id)
        {
            _pcBuildContext.SetBuild(id);
        }

        public void AddPartToBuild(PcPart pcPart, int buildId)
        {
            _pcBuildContext.AddPartToBuild(pcPart, buildId);
        }

        public void AddPart(PcPart pcPart)
        {
            _pcBuildContext.AddPart(pcPart);
        }
    }
}