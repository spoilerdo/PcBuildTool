using System.Collections.Generic;
using KillerApp.Domain;

namespace KillerApp.Models
{
    public class PcBuildResultViewModel
    {
        public IEnumerable<Result> PcParts { get; set; }
    }
}