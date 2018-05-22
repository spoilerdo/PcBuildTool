using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using KillerApp.Domain;

namespace KillerApp.Logic.Logic.Price
{
    public class GetPrice
    {
        private readonly string _url;
        private readonly string _path;
        private readonly List<string> _titles;

        public GetPrice(string url, string path, List<string> titles)
        {
            _url = url;
            _path = path;
            _titles = titles;
        }

        public Domain.Price Get()
        {
            var webGet = new HtmlWeb();
            var document = webGet.Load(_url);

            List<HtmlNode> titles = new List<HtmlNode>();
            foreach (string title in _titles)
            {
                try
                {
                    List<string> titleElements = title.Split(',').ToList();
                    titles = document.DocumentNode.SelectNodes(new PricePathBuilder(titleElements[0], titleElements[1], titleElements[2]).GetPath()).ToList();
                    break;
                }
                catch
                {

                }
            }

            try
            {
                return new Domain.Price(titles, document.DocumentNode.SelectNodes(_path).ToList());
            }
            catch
            {
                return null;
            }
        }
    }
}