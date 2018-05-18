using System.Collections.Generic;
using KillerApp.Domain;

namespace KillerApp.DAL.Interfaces
{
    public interface IPcBuildContext
    {
        IEnumerable<PcPart> GetAllByType(PcPart.PcType type, List<int> propertyIds);
        IEnumerable<PcPart.PcType> GetAllTypes();
        PcPart.PcType GetSelectedType(PcPart.PcType latestType);
        IEnumerable<Website> GetWebsites();
        IEnumerable<Propertie> GetProperties();
        PcBuild GetBuild(string buildId);
        IEnumerable<PcBuild> GetAllBuilds();
        int GetCurrentProgress(PcPart.PcType currentType);
        int GetMaxProgress();
        Account GetUserFromBuild(string buildId);

        void SetBuild(PcBuild build, string userId);
        void AddPart(PcPart pcPart, string filepath = null);
    }
}