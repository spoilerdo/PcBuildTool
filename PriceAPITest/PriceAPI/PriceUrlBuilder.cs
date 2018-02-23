using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceAPI
{
    public class PriceUrlBuilder
    {
        private string SearchURL;
        private string Product;
        private string SpaceSyntax;

        public PriceUrlBuilder(string searchURL, string product, string spaceSyntax)
        {
            SearchURL = searchURL; Product = product; SpaceSyntax = spaceSyntax;
        }

        public string getUrl()
        {
            string[] words = Product.Split(' ');
            string endUrl = "";
            foreach (string word in words)
            {
                endUrl += word + SpaceSyntax;
            }
            return SearchURL + endUrl;
        }
    }
}
