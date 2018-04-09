using System;
using System.Collections.Generic;
using System.Text;

namespace KillerApp.Domain
{
    public class WebsitePrice
    {
        public string _Name { get; set; }
        public string Price { get; set; }

        public WebsitePrice(string name, string price)
        {
            _Name = name; Price = price;
        }
    }
}
