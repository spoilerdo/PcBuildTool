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
    public class AccountLogicTest
    {
        private IAccountLogic _logic;
        private Account _account;

        [TestInitialize]
        public void TestInitialize()
        {
            _logic = AccountFactory.CreateTestLogic();
            _account = new Account("testname", "1234", "1234");
        }

        [TestMethod]
        public void TestLoginAndAccountCreation()
        {
            Assert.IsTrue(_logic.SetAccount(_account));

            Assert.IsTrue(_logic.CheckLogin(_account), "Tesdata is logged in");
        }
    }
}
