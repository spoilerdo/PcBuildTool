using System;
using System.Collections.Generic;
using System.Text;
using API.Models;

namespace Data
{
    public interface IPcBuildService
    {
        IEnumerable<PcPart> GetPartsByType(Build build);
        IEnumerable<PcPart> GetSelectedParts(int buildiD);

        IEnumerable<PcPart> GetPartsById(int id);
        IEnumerable<string> GetPcTypes();
        IEnumerable<int> PartlistCount(int buildId);

        void SetBuild(int id);
        Build AddPcPart(PcPart pcPart, int buildId);
    }
}
