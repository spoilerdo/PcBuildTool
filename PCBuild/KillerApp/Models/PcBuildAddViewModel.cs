using System.Collections.Generic;
using KillerApp.Domain;

namespace KillerApp.Models
{
    public class PcBuildAddViewModel
    {
        public List<Propertie> AllProperties { get; set; }
        public List<string> AllTypes { get; set; }
    }
}