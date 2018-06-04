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
        bool DeleteAccount(Account account);
        IEnumerable<PcBuild> GetOwnedBuilds(string username);
        IEnumerable<PcBuild> GetLikedBuilds(string username);

        void Logout();
    }
}