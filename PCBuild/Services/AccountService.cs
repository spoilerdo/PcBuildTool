using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using API.Models;
using Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;

namespace Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccount _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountService(IAccount context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context; _httpContextAccessor = httpContextAccessor;
        }

        #region SelectMethods
        public bool CheckUsername(string userName)
        {
            IEnumerable<string> username = _context.GetUsername(userName);
            if (!username.Any())
                return true;
            return false;
        }
        public bool CheckLogin(Account account)
        {
            try
            {
                if (_context.TryLogin(account))
                {
                    Login(account.UserName);
                    return true;
                }
                return false;
            }
            catch
            {
                return true;
            }
        }
        public bool CheckAccount(Account account)
        {
            if (!String.IsNullOrEmpty(account.UserName) && 
                !String.IsNullOrEmpty(account.Password) &&
                !String.IsNullOrEmpty(account.ConfPassword))
            {
                return true;
            }
            else
                return false;
        }

        public IEnumerable<PCBuild> GetBuilds(string userName)
        {
            return _context.GetBuilds(userName);
        }
        #endregion

        private async void Login(string username)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim("Username", username)
            };
            if (username == "Spoilerdo")
            {
                claims.Add(new Claim("moderator", "true"));
            }
            ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            AuthenticationProperties options = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(120),
                IsPersistent = true
            };

            await _httpContextAccessor.HttpContext.SignInAsync
            (
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity),
                options
                );
        }
        public async void Logout()
        {
            await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        #region InsertMethods
        public bool SetAccount(Account account)
        {
            if (account.Password == account.ConfPassword)
            {
                _context.SetAccount(account.UserName, account.Password);
                return true;
            }
            else
                return false;
        }
        #endregion
    }
}
