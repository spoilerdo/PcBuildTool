using System.Collections.Generic;

namespace KillerApp.Domain
{
    public class Result
    {
        public Result(List<WebsitePrice> priceList, PcPart pcPart)
        {
            PriceList = priceList;
            PcPart = pcPart;
        }

        public List<WebsitePrice> PriceList { get; set; }
        public PcPart PcPart { get; set; }
    }
}