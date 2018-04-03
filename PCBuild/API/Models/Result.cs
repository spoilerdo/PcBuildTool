using System;
using System.Collections.Generic;
using System.Text;

namespace API.Models
{
    public class Result
    {
        public List<string> PriceList { get; set; }
        public PcPart PcPart { get; set; }

        public Result(List<string> priceList, PcPart pcPart)
        {
            PriceList = priceList; PcPart = pcPart;
        }
    }
}
