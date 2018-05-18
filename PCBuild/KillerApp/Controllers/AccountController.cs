using System;
using System.Collections.Generic;
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

        public IActionResult Delete()
        {
            return View();
        }

        [Authorize]
        public IActionResult Overview()
        {
            return View();
        }

        public IActionResult Builds(bool ownBuilds)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var username = claimsIdentity.FindFirst("Username").Value;

            if (ownBuilds)
                return PartialView("~/Views/Account/Build.cshtml", _accountLogic.GetOwnedBuilds(username).AsList());

            return PartialView("~/Views/Account/Build.cshtml", _accountLogic.GetLikedBuilds(username).AsList());
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

                        return RedirectToAction("Overview");
                    }

                    ModelState.AddModelError(string.Empty, "De ingevoerde wachtwoorden zijn niet gelijk.");
                }
                else
                    ModelState.AddModelError(string.Empty, "Wachtwoord klopt niet probeer opnieuw.");
            }
            ModelState.AddModelError(string.Empty, "Accountnaam is al in gebruik.");
            return View("Create");
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult DeleteAccount(string password, string confpassword)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var username = claimsIdentity.FindFirst("Username").Value;

            Account account = new Account(username, password, confpassword);

            if (_accountLogic.DeleteAccount(account))
                return RedirectToAction("Index", "Home");

            ModelState.AddModelError(String.Empty, "De ingevoerde wachtwoorden zijn niet gelijk");
            return Ok();
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