using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KillerApp.DAL.Interfaces;
using KillerApp.Domain;

namespace KillerApp.DAL.Contexts.AccountContext
{
    public class AccountMemoryContext : IAccountContext
    {
        private readonly List<Account> _accounts = new List<Account>();

        #region SelectMethods

        public IEnumerable<string> GetUsername(string username)
        {
            throw new NotImplementedException();
        }

        public bool TryLogin(Account account)
        {
            Account acc = _accounts.Find(x => x.UserName == account.UserName);

            if(acc != null)
                return true;

            return false;
        }

        public IEnumerable<PcBuild> GetOwnedBuilds(string username)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PcBuild> GetLikedBuilds(string username)
        {
            throw new NotImplementedException();
        }

        public string GetUserId(string username, string password)
        {
            Account account = _accounts.FirstOrDefault(x => x.UserName == username && x.Password == password);

            return account?.Id;
        }

        #endregion

        #region InsertMethods

        public void SetAccount(string username, string password)
        {
            _accounts.Add(new Account(username, password, password) {Id = "1"});
        }

        public void DeleteAccount(string username, string password)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
