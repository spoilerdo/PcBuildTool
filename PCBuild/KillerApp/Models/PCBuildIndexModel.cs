using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KillerApp.Models
{
    public class PCBuildIndexModel
    {
        public IEnumerable<PCPart> PCParts { get; set; }
        public IEnumerable<PCPart> SelectedPCParts { get; set; }
        public string SelectedType { get; set; }
    }
}
