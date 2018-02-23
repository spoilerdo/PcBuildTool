using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceAPI
{
    public class PriceUrlBuilder
    {
        private string BaseUrl;
        private string Product;

        public PriceUrlBuilder(string baseUrl, string product)
        {
            BaseUrl = baseUrl; Product = product;
        }

        public string getUrl()
        {
            return BaseUrl + Product;
        }
    }
}
