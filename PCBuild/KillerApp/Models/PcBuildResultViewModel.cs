using System.Collections.Generic;
using KillerApp.Domain;

namespace KillerApp.Models
{
    public class PcBuildResultViewModel
    {
        public IEnumerable<Result> BestWebshop { get; set; }
        public IEnumerable<Result> BestOption { get; set; }
        public IEnumerable<Result> PcParts { get; set; }
        public PcBuild PcBuild { get; set; }
    }
}