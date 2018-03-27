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

        void SetBuild(int id);
        Build AddPcPart(PcPart pcPart, int buildId);
        void AddPcPartToDb(PcPart pcPart, int buildId);
    }
}
