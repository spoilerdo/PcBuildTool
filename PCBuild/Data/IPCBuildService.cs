using System;
using System.Collections.Generic;
using System.Text;
using API.Models;

namespace Data
{
    public interface IPCBuildService
    {
        IEnumerable<PCPart> GetAllParts();
        IEnumerable<PCPart> GetPartsByID(int ID);
        void SetBuild(int ID);
        void AddPCPart(PCPart pcPart, int BuildID);
        IEnumerable<string> GetPCTypes();
        IEnumerable<int> PartlistCount(int BuildID);
    }
}
