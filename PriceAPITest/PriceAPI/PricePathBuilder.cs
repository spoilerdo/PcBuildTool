using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceAPI
{
    class PricePathBuilder
    {
        private string Element;
        private string Identifier;
        private string IdentifierName;

        public PricePathBuilder(string element, string identifier, string identifierName)
        {
            Element = element; Identifier = identifier; IdentifierName = identifierName;
        }

        public string GetPath()
        {
            //TODO: this isn't correct because bol.com has his cents in another container!!
            return $"//{Element}[@{Identifier}='{IdentifierName}']";
        }
    }
}
