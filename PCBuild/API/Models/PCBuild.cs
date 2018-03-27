using System;
using System.Collections.Generic;
using System.Text;

namespace API.Models
{
    public class PCBuild
    {
        public string Name { get; set; }
        public List<PcPart> PcParts { get; set; }

        public PCBuild(string name, List<PcPart> pcParts)
        {
            Name = name;
            PcParts = pcParts;
        }
    }
}
