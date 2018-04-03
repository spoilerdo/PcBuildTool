using System;
using System.Collections.Generic;
using System.Text;
using API.Models;

namespace Data
{
    public interface IPcBuildService
    {
        IEnumerable<PcPart> GetPartsByType(Build build, string latestType);
        IEnumerable<PcPart> GetSelectedParts(int buildiD);
        IEnumerable<string> GetAllTypes();
        IEnumerable<Result> GetPrices(IEnumerable<PcPart> pcParts, IEnumerable<Website> websites);
        IEnumerable<Website> GetWebsites();

        void SetBuild(int id);
        Build AddPcPart(PcPart pcPart, int buildId);
        void AddPcPartToDb(PcPart pcPart, int buildId);
    }
}
