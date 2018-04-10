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

        public IEnumerable<string> GetUsername(string username)
        {
            return _accountContext.GetUsername(username);
        }

        public bool TryLogin(Account account)
        {
            return _accountContext.TryLogin(account);
        }

        public IEnumerable<PCBuild> GetBuilds(string username)
        {
            return _accountContext.GetBuilds(username);
        }

        public void SetAccount(string username, string password)
        {
            _accountContext.SetAccount(username, password);
        }
    }
}