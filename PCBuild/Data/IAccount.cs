using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public interface IAccount
    {
        IEnumerable<string> GetUsername(string userName);

        void SetAccount(string username, string password);
    }
}
