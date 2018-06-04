using System.ComponentModel.DataAnnotations.Schema;

namespace KillerApp.Domain
{
    public class WebsitePrice
    {
        public string Name { get; set; }
        public decimal Price { get; set; }

        public WebsitePrice(string name, decimal price)
        {
            Name = name;
            Price = price;
        }
    }
}