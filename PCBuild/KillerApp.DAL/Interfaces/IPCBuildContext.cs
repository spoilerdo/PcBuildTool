using System.Collections.Generic;
using KillerApp.Domain;

namespace KillerApp.DAL.Interfaces
{
    public interface IPcBuildContext
    {
        IEnumerable<PcPart> GetAllByType(string type, List<int> propertyIds);
        IEnumerable<string> GetAllTypes();
        IEnumerable<PcPart> GetSelectedParts(int buildiD);
        IEnumerable<string> GetSelectedType(string latestType);
        IEnumerable<Website> GetWebsites();
        IEnumerable<Propertie> GetProperties();
        PcBuild GetBuild(string buildId);
        IEnumerable<PcBuild> GetAllBuilds();

        void SetBuild(int id);
        void AddPartToBuild(PcPart pcPart, int buildId);
        void AddPart(PcPart pcPart, File file);
    }
}