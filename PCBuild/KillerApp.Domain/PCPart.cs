using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace KillerApp.Domain
{
    public class PcPart
    {
        public PcPart()
        {
        }

        public PcPart(string name, string type, string information, List<Propertie> properties, IFormFile image,
            int ean = 0)
        {
            EAN = ean;
            _Name = name;
            _Type = type;
            Information = information;
            Properties = properties;
            Image = image;
        }

        public int EAN { get; set; }
        public string _Name { get; set; }
        public string _Type { get; set; }
        public string Information { get; set; }
        public List<Propertie> Properties { get; set; }
        public IFormFile Image { get; set; }
    }
}