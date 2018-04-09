using System;
using System.Collections.Generic;
using System.Text;

namespace KillerApp.Domain
{
    public class Account
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfPassword { get; set; }

        public Account(string userName, string password, string confPassword)
        {
            UserName = userName; Password = password; ConfPassword = confPassword;
        }
    }
}
