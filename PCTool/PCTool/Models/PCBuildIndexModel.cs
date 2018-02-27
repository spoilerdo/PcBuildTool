using PCBuild.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCTool.Models
{
    public class PCBuildIndexModel
    {
        public IEnumerable<PCPart> PCParts { get; set; }
        public IEnumerable<PCPart> SelectedPCParts { get; set; }
    }
}
