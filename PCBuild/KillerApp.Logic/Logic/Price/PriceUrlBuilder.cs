using System;
using System.Collections.Generic;
using System.Text;

namespace KillerApp.Logic.Logic.Price
{
    public class PriceUrlBuilder
    {
        private readonly string _searchUrl;
        private readonly string _product;
        private readonly string _spaceSyntax;

        public PriceUrlBuilder(string searchUrl, string product, string spaceSyntax)
        {
            _searchUrl = searchUrl; _product = product; _spaceSyntax = spaceSyntax;
        }

        public string GetUrl()
        {
            string[] words = _product.Split(' ');
            string endUrl = "";
            foreach (string word in words)
            {
                endUrl += word + _spaceSyntax;
            }
            return _searchUrl + endUrl;
        }

    }
}
