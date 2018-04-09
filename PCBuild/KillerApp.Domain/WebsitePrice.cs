namespace KillerApp.Domain
{
    public class WebsitePrice
    {
        public WebsitePrice(string name, string price)
        {
            _Name = name;
            Price = price;
        }

        public string _Name { get; set; }
        public string Price { get; set; }
    }
}