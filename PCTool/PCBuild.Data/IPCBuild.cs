using PCBuild.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PCBuild.Data
{
    public interface IPCBuild
    {
        IEnumerable<PCPart> GetAll();
    }
}
