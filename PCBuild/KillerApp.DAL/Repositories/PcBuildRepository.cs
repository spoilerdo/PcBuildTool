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

        public IEnumerable<PcPart> GetAllByType(PcPart.PcType type, List<int> propertyIds) => _pcBuildContext.GetAllByType(type, propertyIds);

        public IEnumerable<PcPart.PcType> GetAllTypes() => _pcBuildContext.GetAllTypes();

        public PcPart.PcType GetSelectedType(PcPart.PcType latestType) => _pcBuildContext.GetSelectedType(latestType);

        public IEnumerable<Website> GetWebsites() => _pcBuildContext.GetWebsites();

        public IEnumerable<Propertie> GetProperties() => _pcBuildContext.GetProperties();

        public PcBuild GetBuild(string buildId) => _pcBuildContext.GetBuild(buildId);

        public IEnumerable<PcBuild> GetAllBuilds() => _pcBuildContext.GetAllBuilds();

        public int GetCurrentProgress(PcPart.PcType currentType) => _pcBuildContext.GetCurrentProgress(currentType);

        public int GetMaxProgress() => _pcBuildContext.GetMaxProgress();

        public Account GetUserFromBuild(string buildId) => _pcBuildContext.GetUserFromBuild(buildId);

        #endregion

        #region InsertMethods
        public void SetBuild(PcBuild build, string userId) => _pcBuildContext.SetBuild(build, userId);

        public void AddPart(PcPart pcPart, string filepath) => _pcBuildContext.AddPart(pcPart, filepath);

        #endregion
    }
}