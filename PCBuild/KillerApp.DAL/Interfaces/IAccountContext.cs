using System.Collections.Generic;
using KillerApp.Domain;

namespace KillerApp.DAL.Interfaces
{
    public interface IAccountContext
    {
        IEnumerable<string> GetUsername(string username);
        bool TryLogin(Account account);
        IEnumerable<PcBuild> GetBuilds(string username);
        string GetUserId(string username, string password);

        void SetAccount(string username, string password);
    }
}