using System.Collections.Generic;
using KillerApp.Domain;

namespace KillerApp.Logic.Interfaces
{
    public interface IPcBuildLogic
    {
        IEnumerable<PcPart> GetPartsByType(Build build, PcPart.PcType latestType);
        IEnumerable<PcPart.PcType> GetAllTypes();
        IEnumerable<Result> GetPrices(IEnumerable<PcPart> pcParts, IEnumerable<Website> websites);
        IEnumerable<Website> GetWebsites();
        IEnumerable<Propertie> GetProperties();
        PcBuild GetBuild(string buildId);
        IEnumerable<PcBuild> GetAllBuilds();
        int GetProgress(PcPart.PcType currentType);
        Account GetUserFromBuild(string buildId);

        List<Result> CheckBestPriceOption(List<Result> pcResults);

        void SetBuild(PcBuild build, List<PcPart> pcParts, string userId);
        Build AddBuild(Build build, PcPart pcPart);
        void AddPcPart(List<int> properties, PcPart pcPart, string filepath = "");
    }
}