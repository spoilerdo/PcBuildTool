using System;
using System.Collections.Generic;
using System.Text;
using KillerApp.DAL.Interfaces;
using KillerApp.Domain;

namespace KillerApp.DAL.Contexts
{
    public class AccountMemoryContext : IAccountContext
    {
        private readonly List<Account> _accounts = new List<Account>();

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

        public IEnumerable<PCBuild> GetBuilds(string username)
        {
            throw new NotImplementedException();
        }

        public void SetAccount(string username, string password)
        {
            _accounts.Add(new Account(username, password, password));
        }
    }
}
