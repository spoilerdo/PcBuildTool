namespace KillerApp.Logic.Logic.Price
{
    public class PricePathBuilder
    {
        private readonly string _element;
        private readonly string _identifier;
        private readonly string _identifierName;

        public PricePathBuilder(string element, string identifier, string identifierName)
        {
            _element = element;
            _identifier = identifier;
            _identifierName = identifierName;
        }

        public string GetPath()
        {
            return $"//{_element}[@{_identifier}='{_identifierName}']";
        }
    }
}