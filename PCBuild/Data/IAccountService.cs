using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public interface IAccountService
    {
        bool CheckUsername(string userName);
        bool SetAccount(string userName, string password, string confPassword);
    }
}
