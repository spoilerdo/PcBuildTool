using PCBuild_Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PCBuild_Data
{
    public interface IPCBuild
    {
        IEnumerable<PCPart> GetAll();
    }
}
