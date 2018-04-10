using System;
using System.Collections.Generic;
using System.Text;
using KillerApp.Domain;

namespace KillerApp.DAL.Interfaces
{
    public interface IPcBuildRepository
    {
        IEnumerable<PcPart> GetAllByType(string type, List<int> propertyIds);
        IEnumerable<string> GetAllTypes();
        IEnumerable<PcPart> GetSelectedParts(int buildId);
        IEnumerable<string> GetSelectedType(string latestType);
        IEnumerable<Website> GetWebsites();
        IEnumerable<Propertie> GetProperties();

        void SetBuild(int id);
        void AddPartToBuild(PcPart pcPart, int buildId);
        void AddPart(PcPart pcPart);
    }
}
