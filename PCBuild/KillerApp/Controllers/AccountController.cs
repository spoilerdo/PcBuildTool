using System;
using System.Security.Claims;
using Dapper;
using KillerApp.Domain;
using KillerApp.Factory;
using KillerApp.Logic.Interfaces;
using KillerApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace KillerApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountLogic _accountLogic;

        #region SessionNames

        private const string UsernameSession = "usernameNotTaken";

        #endregion

        public AccountController(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _accountLogic = AccountFactory.CreateLogic(configuration, httpContextAccessor);
        }

        public IActionResult Create(bool username = true, bool password = true)
        {
            var model = new CreateAccountViewModel
            {
                UsernameRight = username,
                PasswordRight = password
            };

            return View(model);
        }
        [Authorize]
        public IActionResult Overview()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var username = claimsIdentity.FindFirst("Username").Value;

            //TODO: switching from OwnBuilds to liked builds
            var model = new AccountOverviewModel
            {
                Builds = _accountLogic.GetBuilds(username).AsList(),
                OwnBuilds = true
            };

            return View(model);
        }

        #region HttpPost Methods

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult CheckUsername(CreateAccountViewModel viewModel)
        {
            HttpContext.Session.SetString(UsernameSession, _accountLogic.CheckUsername(viewModel.Account.UserName).ToString());
            var ajaxError = new AJAXError(HttpContext.Session.GetString(UsernameSession), "#usernameError");
            return new JsonResult(JsonConvert.SerializeObject(ajaxError));
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult SendAccount(CreateAccountViewModel viewModel)
        {
            if (Convert.ToBoolean(HttpContext.Session.GetString(UsernameSession)))
            {
                if (_accountLogic.CheckAccount(viewModel.Account))
                {
                    if (_accountLogic.SetAccount(viewModel.Account))
                    {
                        LogIn(viewModel.Account.UserName, viewModel.Account.Password);
                    }
                    else
                    {
                        var ajaxError1 = new AJAXError("False", "#confpasswordError");
                        return new JsonResult(JsonConvert.SerializeObject(ajaxError1));
                    }
                }
                else
                {
                    var ajaxError2 = new AJAXError("False", "#passwordError");
                    return new JsonResult(JsonConvert.SerializeObject(ajaxError2));
                }
            }
            var ajaxError3 = new AJAXError("False", "#changeUsername");
            return new JsonResult(JsonConvert.SerializeObject(ajaxError3));
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult LogIn(string userName, string password)
        {
            var account = new Account(userName, password, password);
            if (_accountLogic.CheckAccount(account)) _accountLogic.CheckLogin(account);
            return RedirectToAction("Index", "Home");
        }

        [ValidateAntiForgeryToken]
        [Authorize]
        [HttpPost]
        public IActionResult LogOut()
        {
            _accountLogic.Logout();
            return RedirectToAction("Index", "Home");
        }

        #endregion
    }
}