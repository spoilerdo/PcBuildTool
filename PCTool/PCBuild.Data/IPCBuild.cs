using PCBuild_Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PCBuild_Data
{
    public interface IPCBuild
    {
        IEnumerable<PCPart> getAll();
        IEnumerable<PCPart> getByID(int ID);
        void setBuild(int ID);
        void addPart(PCPart pcPart, int BuildID);
        IEnumerable<string> getPartTypes();
        IEnumerable<int> partlistCount(int BuildID);
    }
}
