using System.Collections.Generic;
using KillerApp.Domain;

namespace KillerApp.DAL.Interfaces
{
    public interface IAccountContext
    {
        IEnumerable<string> GetUsername(string username);
        bool TryLogin(Account account);
        IEnumerable<PcBuild> GetOwnedBuilds(string username);
        IEnumerable<PcBuild> GetLikedBuilds(string username);
        string GetUserId(string username, string password);

        void SetAccount(string username, string password);
        void DeleteAccount(string username, string password);
    }
}