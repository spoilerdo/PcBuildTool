using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using Data;
using KillerApp.Models;
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
        //TOKEN DIDNT WORK
        [HttpPost]
        public IActionResult CheckUsername([FromBody] string userName)
        {
            AJAXError ajaxError = new AJAXError(_accountService.CheckUsername(userName).ToString(), ".usernameError");
            string error = JsonConvert.SerializeObject(ajaxError);
            return new JsonResult(error);
        }

        //TOKEN DIDNT WORK
        [HttpPost]
        public IActionResult SendAccount([FromBody] Account account)
        {
            if (account.UserName != "" && account.Password != "" && account.ConfPassword != "")
            {
                if (_accountService.SetAccount(account.UserName, account.Password, account.ConfPassword))
                {
                    return RedirectToAction("AccountOverview");
                }
            }

            AJAXError ajaxError = new AJAXError("False", ".passwordError");
            string error = JsonConvert.SerializeObject(ajaxError);
            return new JsonResult(error);
        }
        #endregion
    }
}