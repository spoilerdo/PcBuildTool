using System.Collections.Generic;
using KillerApp.Domain;

namespace KillerApp.Models
{
    public class AccountOverviewModel
    {
        public List<PcBuild> Builds { get; set; }
        public bool OwnBuilds { get; set; }
    }
}