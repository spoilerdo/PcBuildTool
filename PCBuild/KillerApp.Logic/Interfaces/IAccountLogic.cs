using System.Collections.Generic;
using KillerApp.Domain;

namespace KillerApp.Logic.Interfaces
{
    public interface IAccountLogic
    {
        bool CheckUsername(string username);
        bool CheckLogin(Account account);
        bool CheckAccount(Account account);
        bool SetAccount(Account account);
        IEnumerable<PCBuild> GetBuilds(string username);

        void Logout();
    }
}