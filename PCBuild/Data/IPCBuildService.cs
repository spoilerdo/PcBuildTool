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

        void SetBuild(int id);
        Build AddPcPart(PcPart pcPart, int buildId);
    }
}
