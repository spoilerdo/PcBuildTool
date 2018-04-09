using System;
using System.Collections.Generic;
using System.Text;
using API.Models;

namespace Data
{
    public interface IPcBuild
    {
        IEnumerable<PcPart> GetAllByType(string type, List<int> propertyIds);
        IEnumerable<string> GetAllTypes();
        IEnumerable<PcPart> GetSelectedParts(int buildiD);
        IEnumerable<string> GetSelectedType(string latestType);
        IEnumerable<Website> GetWebsites();
        IEnumerable<Propertie> GetProperties();

        void SetBuild(int id);
        void AddPartToBuild(PcPart pcPart, int buildId);
        void AddPart(PcPart pcPart);
    }
}
