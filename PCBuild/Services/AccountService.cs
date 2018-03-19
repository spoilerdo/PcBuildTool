using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data;

namespace Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccount _context;

        public AccountService(IAccount context)
        {
            _context = context;
        }

        #region SelectMethods
        public bool CheckUsername(string userName)
        {
            try
            {
                string username = _context.GetUsername(userName).First();
                return false;
            }
            catch
            {
                return true;
            }
        }
        #endregion

        #region InsertMethods
        public bool SetAccount(string userName, string password, string confPassword)
        {
            if (password == confPassword)
            {
                _context.SetAccount(userName, password);
                return true;
            }
            else
                return false;
        }
        #endregion
    }
}
