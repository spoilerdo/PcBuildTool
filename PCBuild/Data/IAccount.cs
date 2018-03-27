using System;
using System.Collections.Generic;
using System.Text;
using API.Models;

namespace Data
{
    public interface IAccount
    {
        IEnumerable<string> GetUsername(string userName);
        bool TryLogin(Account account);
        IEnumerable<PCBuild> GetBuilds(string userName);

        void SetAccount(string username, string password);
    }
}
