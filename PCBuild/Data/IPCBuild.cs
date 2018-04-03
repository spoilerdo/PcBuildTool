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

        void SetBuild(int id);
        void AddPart(PcPart pcPart, int buildId);
    }
}
