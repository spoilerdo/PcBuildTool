using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using Data;
using KillerApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

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
            var model = new CreateAccountModel()
            {
                UsernameRight = username,
                PasswordRight = password
            };

            return View(model);
        }
        public IActionResult Overview()
        {
            var model = new AccountOverviewModel()
            {
                Builds = new List<Build>()
            };

            return View();
        }

        #region HttpPost Methods
        //[ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult CheckUsername([FromBody] string userName)
        {
            AJAXError ajaxError = new AJAXError(_accountService.CheckUsername(userName).ToString(), ".usernameError");
            return new JsonResult(JsonConvert.SerializeObject(ajaxError));
        }

        //[ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult SendAccount([FromBody] Account account)
        {
            if (_accountService.CheckAccount(account))
            {
                if (_accountService.SetAccount(account))
                    return RedirectToAction("Overview");

                AJAXError ajaxError1 = new AJAXError("False", ".confpasswordError");
                return new JsonResult(JsonConvert.SerializeObject(ajaxError1));
            }

            AJAXError ajaxError2 = new AJAXError("False", ".passwordError");
            return new JsonResult(JsonConvert.SerializeObject(ajaxError2));
        }

        //[ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult LogIn([FromBody] Account account)
        {
            if (_accountService.CheckAccount(account))
            {
                if (_accountService.CheckLogin(account))
                    return RedirectToAction("Overview");
            }

            return Ok();
        }

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