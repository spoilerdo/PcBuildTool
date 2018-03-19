using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using KillerApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace KillerApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public IActionResult CreateAccount(bool username = true, bool password = true)
        {
            var model = new CreateAccountModel()
            {
                UsernameRight = username,
                PasswordRight = password
            };

            return View(model);
        }
        public IActionResult AccountOverview()
        {
            return View();
        }

        #region HttpPost Methods
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult CheckUsername([FromBody] string userName)
        {
            return new JsonResult(_accountService.CheckUsername(userName));
        }

        [HttpPost]
        public IActionResult SendAccount(string userName, string password, string confPassword)
        {
            if (userName != null && password != null && confPassword != null)
            {
                if (_accountService.SetAccount(userName, password, confPassword))
                {
                    return RedirectToAction("AccountOverview");
                }
                else //TODO: return error with information
                    return new JsonResult(false);
            }
            else
            {
                return new JsonResult(false);
            }
        }
        #endregion
    }
}