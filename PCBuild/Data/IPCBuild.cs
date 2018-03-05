using System;
using System.Collections.Generic;
using System.Text;
using API.Models;

namespace Data
{
    public interface IPCBuild
    {
        IEnumerable<PCPart> GetAll();
        IEnumerable<PCPart> GetByID(int ID);
        void SetBuild(int ID);
        void AddPart(PCPart pcPart, int BuildID);
        IEnumerable<string> GetPartTypes();
        IEnumerable<int> PartlistCount(int BuildID);
    }
}
