using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml;
using Microsoft.AspNetCore.Http;
using Dapper;

namespace KillerApp.Domain
{
    public class PcPart
    {
        public string Id { get; set; }
        public string _Name { get; set; }
        public PcType _Type { get; set; }
        public string Information { get; set; }
        public string _Path { get; set; }
        public List<Propertie> Properties { get; set; }
        public IFormFile Image { get; set; }

        public PcPart()
        {
        }

        public PcPart(string name, PcType type, string information, List<Propertie> properties,
            string id = "")
        {
            Id = id;
            _Name = name;
            _Type = type;
            Information = information;
            Properties = properties;
        }

        public enum PcType
        {
            Null,
            Case,
            Motherboard,
            Processor,
            CPU_Cooling,
            RAM,
            Memory,
            Power
        }
    }
}