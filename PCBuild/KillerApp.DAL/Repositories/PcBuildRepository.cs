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

        #region SelectMethods
        public IEnumerable<PcPart> GetAllByType(string type, List<int> propertyIds) => _pcBuildContext.GetAllByType(type, propertyIds);

        public IEnumerable<string> GetAllTypes() => _pcBuildContext.GetAllTypes();

        public IEnumerable<PcPart> GetSelectedParts(int buildId) => _pcBuildContext.GetSelectedParts(buildId);

        public IEnumerable<string> GetSelectedType(string latestType) => _pcBuildContext.GetSelectedType(latestType);

        public IEnumerable<Website> GetWebsites() => _pcBuildContext.GetWebsites();

        public IEnumerable<Propertie> GetProperties() => _pcBuildContext.GetProperties();

        public PcBuild GetBuild(string buildId) => _pcBuildContext.GetBuild(buildId);

        public IEnumerable<PcBuild> GetAllBuilds() => _pcBuildContext.GetAllBuilds();

        #endregion

        #region InsertMethods
        public void SetBuild(int id) => _pcBuildContext.SetBuild(id);

        public void AddPartToBuild(PcPart pcPart, int buildId) => _pcBuildContext.AddPartToBuild(pcPart, buildId);

        public void AddPart(PcPart pcPart, File file) => _pcBuildContext.AddPart(pcPart, file);
        #endregion
    }
}