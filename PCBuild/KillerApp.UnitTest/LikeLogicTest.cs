using System;
using System.Collections.Generic;
using System.Text;
using KillerApp.Domain;
using KillerApp.Factory;
using KillerApp.Logic.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KillerApp.UnitTest
{
    [TestClass]
    public class LikeLogicTest
    {
        private ILikeLogic _logic;
        private PcBuild _build;
        private Account _account;

        [TestInitialize]
        public void TestInitialize()
        {
            _logic = LikeFactory.CreateTestLogic();
            _build = new PcBuild("test _build", new List<PcPart>(), 0, 0, "1");
            _account = new Account("testuser", "1234", "1234") {Id = "1"};
        }

        [TestMethod]
        public void TestSubmitLike()
        {
            //Kijk of de gebruiker al geliked heeft. Zo niet dan moet de build geliked worden. Controleer of de build geliked wordt.
            //Want de build is namelijk nog niet geliked.

            _logic.SubmitLike(_build, _account.Id);
        }
    }
}
