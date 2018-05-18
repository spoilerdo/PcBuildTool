using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace KillerApp.Domain
{
    public class PcPart
    {
        public PcPart()
        {
        }

        public PcPart(string name, PcType type, string information, List<Propertie> properties,
            string id = "")
        {
            ID = id;
            _Name = name;
            _Type = type;
            Information = information;
            Properties = properties;
        }

        public string ID { get; set; }
        public string _Name { get; set; }
        public PcType _Type { get; set; }
        public string Information { get; set; }
        public string _Path { get; set; }
        public List<Propertie> Properties { get; set; }
        public IFormFile Image { get; set; }

        public enum PcType
        {
            Null,
            Case,
            CPU_Cooling,
            Memory,
            Motherboard,
            Power,
            Processor,
            RAM
        }
    }
}