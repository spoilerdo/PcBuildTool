using System.Collections.Generic;
using KillerApp.Domain;

namespace KillerApp.DAL.Interfaces
{
    public interface IAccountContext
    {
        IEnumerable<string> GetUsername(string username);
        bool TryLogin(Account account);
        IEnumerable<PCBuild> GetBuilds(string username);

        void SetAccount(string username, string password);
    }
}
