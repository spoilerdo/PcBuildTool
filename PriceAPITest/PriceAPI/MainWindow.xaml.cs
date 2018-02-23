using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HtmlAgilityPack;

namespace PriceAPI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            List<HtmlNode> Prices = new GetPrice(
                new PriceUrlBuilder("https://www.bol.com/nl/p/", "gigabyte-ga-h110m-s2h-intel-h110-express-chipset-lga-1151-micro-atx-moederbord/9200000050037093/?suggestionType=browse&bltgh=ca4aaf73-9e3b-4182-9eed-f480119d43b1.1.11.ProductTitle").getUrl(), 
                new PricePathBuilder("span", "class", "promo-price").GetPath()
                ).get();

            foreach (var item in Prices)
            {
                Console.Write(item.InnerHtml);
            }
        }
    }
}
