using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace KillerApp.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult CreateAccount()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SendAccount(string userName, string password, string confPassword)
        {
            //send to new service

            //TODO: Nadat je een account hebt aangemaakt ga je 
            //TODO: naar de pagina van jouw account
            return RedirectToAction("CreateAccount");
        }
    }
}