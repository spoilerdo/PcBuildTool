using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using Dapper;
using Data;
using KillerApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Security.Claims;

namespace KillerApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public IActionResult Create(bool username = true, bool password = true)
        {
            var model = new CreateAccountViewModel()
            {
                UsernameRight = username,
                PasswordRight = password
            };

            return View(model);
        }
        public IActionResult Overview()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            string username = claimsIdentity.FindFirst("Username").Value;

            //TODO: switching from OwnBuilds to liked builds
            var model = new AccountOverviewModel()
            {
                Builds = _accountService.GetBuilds(username).AsList(),
                OwnBuilds = true
            };

            return View(model);
        }

        #region HttpPost Methods
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult CheckUsername([FromBody] string userName)
        {
            AJAXError ajaxError = new AJAXError(_accountService.CheckUsername(userName).ToString(), ".usernameError");
            return new JsonResult(JsonConvert.SerializeObject(ajaxError));
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult SendAccount([FromBody] Account account)
        {
            if (_accountService.CheckAccount(account))
            {
                if (_accountService.SetAccount(account))
                    LogIn(account.UserName, account.Password);
                else
                {
                    AJAXError ajaxError1 = new AJAXError("False", ".confpasswordError");
                    return new JsonResult(JsonConvert.SerializeObject(ajaxError1));
                }
            }

            AJAXError ajaxError2 = new AJAXError("False", ".passwordError");
            return new JsonResult(JsonConvert.SerializeObject(ajaxError2));
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult LogIn(string userName, string password)
        {
            Account account = new Account(userName, password, password);
            if (_accountService.CheckAccount(account))
            {
                _accountService.CheckLogin(account);
            }
            return RedirectToAction("Index", "Home");
        }

        [ValidateAntiForgeryToken]
        [Authorize]
        [HttpPost]
        public IActionResult LogOut()
        {
            _accountService.Logout();
            return RedirectToAction("Index", "Home");
        }
        #endregion
    }
}