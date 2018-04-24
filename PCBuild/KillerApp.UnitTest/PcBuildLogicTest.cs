using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KillerApp.Domain;
using KillerApp.Factory;
using KillerApp.Logic.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KillerApp.Logic.Logic;

namespace KillerApp.UnitTest
{
    [TestClass]
    public class PcBuildLogicTest
    {
        private IPcBuildLogic _logic;
        private PcPart _pcPart;
        private Build _build;
        private Website _website;


        [TestInitialize]
        public void TestInitialize()
        {
            _logic = PcBuildFactory.CreateTestLogic();
            _pcPart = new PcPart("Sharkoon TG5", "Motherboard", "Dit is een test", new List<Propertie>{ new Propertie(1, "TestProp1", "TestPropType") }, null, 1 );
            _build = new Build();
            _website = new Website("Bol.com", "https://www.bol.com/nl/s/computer/zoekresultaten/N/16430/sc/computer_all/index.html?searchtext=", "span,class,promo-price");
        }

        [TestMethod]
        public void TestPcPartToevoegen()
        {
            //Arrange
            int amountOfPcParts = _logic.GetPartsByType(_build, "Case").Count();

            //Act
            _logic.AddPcPart(_pcPart);

            //Assert
            Assert.AreEqual(amountOfPcParts + 1, _logic.GetPartsByType(_build, "Case").Count());
        }

        [TestMethod]
        public void TestGivePriceFromPcPart()
        {
            //Act
            decimal price = Convert.ToDecimal(_logic.GetPrices(new List<PcPart> { _pcPart }, new List<Website> { _website }).First().PriceList.First().Price);

            //Assert
            Assert.IsTrue(price > 0, "The price is greater than zero so it found the website and the product on that website");
        }
    }
}
