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
        private PcPart _motherboard;
        private PcPart _processor1150;
        private PcPart _processor1151;
        private Website _website;


        [TestInitialize]
        public void TestInitialize()
        {
            _logic = PcBuildFactory.CreateTestLogic();

            _motherboard = new PcPart("test motherboard", "Motherboard", "Dit is een test motherboard",
                new List<Propertie>
                {
                    new Propertie(1, "1150", "Motherboard"),
                    new Propertie(3, "TestProp1", "RAM"),
                    new Propertie(4, "TestProp1", "Power"),
                    new Propertie(5, "TestProp1", "Memory")
                }, "1") {_Path = "testPath"};
            _processor1150 = new PcPart("test Processor 1150", "Processor", "Dit is een test proseccor met een sockettype: 1150", 
                new List<Propertie>
            {
                new Propertie(1, "1150", "Motherboard")
            }, "1") {_Path = "Test"};
            _processor1151 = new PcPart("test Processore 1151", "Processor", "Dit is een test proseccor met een sockettype: 1151", 
                new List<Propertie>
            {
                new Propertie(2, "1151", "Motherboard")
            }, "1") {_Path = "Test"};

            _website = new Website("Bol.com", "https://www.bol.com/nl/s/computer/zoekresultaten/N/16430/sc/computer_all/index.html?searchtext=", "span,class,promo-price");
        }

        [TestMethod]
        public void TestPcPartToevoegen()
        {
            //Arrange
            Build build = new Build();
            int amountOfPcParts = _logic.GetPartsByType(build, "Case").Count();

            //Act
            _logic.AddPcPart(GetPropertieIds(_motherboard.Properties), _motherboard, _motherboard._Path);

            //Assert
            Assert.AreEqual(amountOfPcParts + 1, _logic.GetPartsByType(build, "Case").Count());
        }

        [TestMethod]
        public void TestGivePriceFromPcPart()
        {
            //Act
            decimal price = Convert.ToDecimal(_logic.GetPrices(new List<PcPart> { _motherboard }, new List<Website> { _website }).First().PriceList.First().Price);

            //Assert
            Assert.IsTrue(price > 0, "The price is greater than zero so it found the website and the product on that website");
        }

        [TestMethod]
        public void TestPcBuildLogic()
        {
            //Arange
            Build build = new Build();

            //Act
            build = _logic.AddBuild(build, _motherboard);
            _logic.AddPcPart(GetPropertieIds(_processor1150.Properties), _processor1150, _processor1150._Path);
            _logic.AddPcPart(GetPropertieIds(_processor1151.Properties), _processor1151, _processor1151._Path);

            //Assert
            Assert.AreEqual(1, _logic.GetPartsByType(build, _motherboard._Type).Count());
            Assert.IsTrue(_logic.GetPartsByType(build, _motherboard._Type).First() == _processor1150);
        }

        [TestMethod]
        public void TestGetProgress()
        {
            Assert.IsTrue(_logic.GetProgress("Motherboard") == 29);
        }

        private List<int> GetPropertieIds(List<Propertie> properties)
        {
            List<int> propertieIds = new List<int>();
            foreach (Propertie propertie in properties)
            {
                propertieIds.Add(propertie.Id);
            }

            return propertieIds;
        }
    }
}
