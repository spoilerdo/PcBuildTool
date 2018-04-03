using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KillerApp.Models
{
    public class PcBuildIndexViewModel
    {
        public IEnumerable<PcPart> PcParts { get; set; }
        public PcPart SelectedPcPart { get; set; }
        public IEnumerable<PcPart> SelectedPcParts { get; set; }
    }
}
