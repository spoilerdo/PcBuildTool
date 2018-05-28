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
            _logic.SubmitLike(_build, _account.Id);
            Assert.IsTrue(_logic.GetLikeFromUser(_build.ID, _account.Id) && !_logic.GetDislikeFromUser(_build.ID, _account.Id));
        }

        [TestMethod]
        public void TestSubmitDislike()
        {
            _logic.SubmitDislike(_build, _account.Id);
            Assert.IsTrue(_logic.GetDislikeFromUser(_build.ID, _account.Id) && !_logic.GetLikeFromUser(_build.ID, _account.Id));
        }

        [TestMethod]
        public void TestSubmitLikeWhenAlreadyLiked()
        {
            _logic.SubmitLike(_build, _account.Id);
            _logic.SubmitLike(_build, _account.Id);

            Assert.IsTrue(!_logic.GetLikeFromUser(_build.ID, _account.Id) && !_logic.GetDislikeFromUser(_build.ID, _account.Id));
        }

        [TestMethod]
        public void TestSubmitDislikeWhebAlreadyDisliked()
        {
            _logic.SubmitDislike(_build, _account.Id);
            _logic.SubmitDislike(_build, _account.Id);

            Assert.IsTrue(!_logic.GetDislikeFromUser(_build.ID, _account.Id) && !_logic.GetLikeFromUser(_build.ID, _account.Id));
        }

        [TestMethod]
        public void TestSubmitDislikeWhenAlreadyDisliked()
        {
            _logic.SubmitDislike(_build, _account.Id);
            _logic.SubmitDislike(_build, _account.Id);

            Assert.IsTrue(!_logic.GetDislikeFromUser(_build.ID, _account.Id) && !_logic.GetLikeFromUser(_build.ID, _account.Id));
        }

        [TestMethod]
        public void TestSubmitLikeWhenAlreadyDisliked()
        {
            _logic.SubmitDislike(_build, _account.Id);
            _logic.SubmitLike(_build, _account.Id);

            Assert.IsTrue(_logic.GetLikeFromUser(_build.ID, _account.Id) && !_logic.GetDislikeFromUser(_build.ID, _account.Id));
        }
    }
}
