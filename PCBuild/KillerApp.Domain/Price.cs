using System;
using System.Collections.Generic;
using System.Text;
using HtmlAgilityPack;

namespace KillerApp.Domain
{
    public class Price
    {
        public List<HtmlNode> Prices { get; set; }
        public List<HtmlNode> Title { get; set; }

        public Price(List<HtmlNode> title, List<HtmlNode> prices = null)
        {
            Prices = prices;
            Title = title;
        }
    }
}
