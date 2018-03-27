using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;

namespace KillerApp.Models
{
    public class AccountOverviewModel
    { 
        public List<PCBuild> Builds { get; set; }
        public bool OwnBuilds { get; set; }
    }
}
