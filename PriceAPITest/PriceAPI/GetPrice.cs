using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace PriceAPI
{
    public class GetPrice
    {
        private string Url;
        private string Path;

        public GetPrice(string url, string path)
        {
            Url = url; Path = path;
        }

        public List<HtmlNode> get()
        {
            HtmlWeb webGet = new HtmlWeb();
            HtmlDocument document = webGet.Load(Url);
            try
            {
                return document.DocumentNode.SelectNodes(Path).ToList();
            }
            catch
            {
                return null;
            }
        }
    }
}
