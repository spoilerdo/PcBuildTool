using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KillerApp.Models
{
    public class PcBuildAddViewModel
    {
        public List<Propertie> AllProperties { get; set; }
        public List<string> AllTypes { get; set; }
    }
}
