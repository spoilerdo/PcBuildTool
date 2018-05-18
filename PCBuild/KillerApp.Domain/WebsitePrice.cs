namespace KillerApp.Domain
{
    public class WebsitePrice
    {
        public WebsitePrice(string name, decimal price)
        {
            _Name = name;
            Price = price;
        }

        public string _Name { get; set; }
        public decimal Price { get; set; }
    }
}