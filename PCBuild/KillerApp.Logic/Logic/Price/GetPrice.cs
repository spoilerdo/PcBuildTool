using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;

namespace KillerApp.Logic.Logic.Price
{
    public class GetPrice
    {
        private readonly string _path;
        private readonly string _url;

        public GetPrice(string url, string path)
        {
            _url = url;
            _path = path;
        }

        public List<HtmlNode> Get()
        {
            var webGet = new HtmlWeb();
            var document = webGet.Load(_url);
            try
            {
                return document.DocumentNode.SelectNodes(_path).ToList();
            }
            catch
            {
                return null;
            }
        }
    }
}