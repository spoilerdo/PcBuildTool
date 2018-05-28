using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using KillerApp.DAL.Interfaces;
using KillerApp.Domain;
using KillerApp.Logic.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace KillerApp.Logic.Logic
{
    public class AccountLogic : IAccountLogic
    {
        private readonly IAccountContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountLogic(IAccountContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        #region InsertMethods

        public bool SetAccount(Account account)
        {
            if (account.Password == account.ConfPassword)
            {
                _context.SetAccount(account.UserName, account.Password);
                return true;
            }

            return false;
        }

        public bool DeleteAccount(Account account)
        {
            if (account.Password == account.ConfPassword)
            {
                _context.DeleteAccount(account.UserName, account.Password);
                Logout();
                return true;
            }

            return false;
        }

        #endregion

        public async void Logout()
        {
            await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        private async void Login(string username, string userId)
        {
            var claims = new List<Claim>
            {
                new Claim("Username", username),
                new Claim("UserId", userId)
            };
            if (username == "Spoilerdo")
                claims.Add(new Claim("moderator", "true"));

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var options = new AuthenticationProperties
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

        #region SelectMethods

        public bool CheckUsername(string username)
        {
            var usernames = _context.GetUsername(username);
            if (!usernames.Any())
                return true;
            return false;
        }

        public bool CheckLogin(Account account)
        {
            try
            {
                if (!_context.TryLogin(account))
                    return false;

                if(_httpContextAccessor != null)
                    Login(account.UserName, _context.GetUserId(account.UserName, account.Password));
                return true;

            }
            catch
            {
                return false;
            }
        }

        public bool CheckAccount(Account account)
        {
            if (!string.IsNullOrEmpty(account.UserName) &&
                !string.IsNullOrEmpty(account.Password) &&
                !string.IsNullOrEmpty(account.ConfPassword))
                return true;
            return false;
        }

        public IEnumerable<PcBuild> GetOwnedBuilds(string username) => _context.GetOwnedBuilds(username);

        public IEnumerable<PcBuild> GetLikedBuilds(string username) => _context.GetLikedBuilds(username);

        #endregion
    }
}