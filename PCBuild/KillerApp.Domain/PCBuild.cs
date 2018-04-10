using System.Collections.Generic;

namespace KillerApp.Domain
{
    public class PCBuild
    {
        public PCBuild(string name, List<PcPart> pcParts)
        {
            Name = name;
            PcParts = pcParts;
        }

        public string Name { get; set; }
        public List<PcPart> PcParts { get; set; }
    }
}