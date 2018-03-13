using System;
using System.Collections.Generic;
using System.Text;

namespace API.Models
{
    public class PcPart
    {
        public int EAN { get; set; }
        public string _Name { get; set; }
        public string _Type { get; set; }
        public string Information { get; set; }
        public List<Propertie> Properties { get; set; }

        //Een pcpart heeft een lijst van propertieid's
        //je slaat die propertieid's op in een Session ->
        //je kijkt bij elk part van een bepaald type of hij een bepaalde id bevat aan de hand van de session propid list
    }
}
