using System.Collections.Generic;
using KillerApp.Domain;

namespace KillerApp.Models
{
    public class AccountOverviewModel
    {
        public List<PCBuild> Builds { get; set; }
        public bool OwnBuilds { get; set; }
    }
}