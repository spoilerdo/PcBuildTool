using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;

namespace KillerApp.Models
{
    public class PcBuildResultViewModel
    {
        public IEnumerable<Result> PcParts { get; set; }
        public IEnumerable<Website> Webshops { get; set; }
    }
}
