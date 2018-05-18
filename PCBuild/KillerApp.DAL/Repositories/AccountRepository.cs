using System.Collections.Generic;
using KillerApp.DAL.Interfaces;
using KillerApp.Domain;

namespace KillerApp.DAL.Repositories
{
    public class AccountRepository : IAccountContext
    {
        private readonly IAccountContext _accountContext;

        public AccountRepository(IAccountContext accountContext)
        {
            _accountContext = accountContext;
        }

        #region SelectMethods

        public IEnumerable<string> GetUsername(string username) => _accountContext.GetUsername(username);

        public bool TryLogin(Account account) => _accountContext.TryLogin(account);

        public IEnumerable<PcBuild> GetOwnedBuilds(string username) => _accountContext.GetOwnedBuilds(username);

        public IEnumerable<PcBuild> GetLikedBuilds(string username) => _accountContext.GetLikedBuilds(username);

        public string GetUserId(string username, string password) => _accountContext.GetUserId(username, password);

        #endregion

        #region InsertMethods

        public void SetAccount(string username, string password) => _accountContext.SetAccount(username, password);

        public void DeleteAccount(string username, string password) =>
            _accountContext.DeleteAccount(username, password);

        #endregion
    }
}