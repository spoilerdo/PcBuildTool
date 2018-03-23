using System;
using System.Collections.Generic;
using System.Text;
using API.Models;

namespace Data
{
    public interface IAccountService
    {
        bool CheckUsername(string userName);
        bool CheckLogin(Account account);
        bool CheckAccount(Account account);
        bool SetAccount(Account account);
        IEnumerable<Build> GetBuilds(string userName);

        void Logout();
    }
}
