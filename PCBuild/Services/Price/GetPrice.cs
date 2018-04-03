using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;

namespace Services.Price
{
    public class GetPrice
    {
        private readonly string _url;
        private readonly string _path;

        public GetPrice(string url, string path)
        {
            _url = url; _path = path;
        }

        public List<HtmlNode> Get()
        {
            HtmlWeb webGet = new HtmlWeb();
            webGet.PreRequest += request =>
            {
                request.CookieContainer = new System.Net.CookieContainer();
                return true;
            };
            HtmlDocument document = webGet.Load(_url);
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
