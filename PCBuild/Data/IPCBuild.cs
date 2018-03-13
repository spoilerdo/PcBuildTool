using System;
using System.Collections.Generic;
using System.Text;
using API.Models;

namespace Data
{
    public interface IPcBuild
    {
        IEnumerable<PcPart> GetAllByType(string type, List<int> propertyIds);
        IEnumerable<PcPart> GetSelectedParts(int buildiD);
        IEnumerable<PcPart> GetById(int id);
        IEnumerable<string> GetPartTypes();
        IEnumerable<int> PartlistCount(int buildId);

        void SetBuild(int id);
        void AddPart(PcPart pcPart, int buildId);
    }
}
