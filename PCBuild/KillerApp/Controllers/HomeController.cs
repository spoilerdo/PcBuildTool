using System.Diagnostics;
using Dapper;
using KillerApp.Factory;
using KillerApp.Logic.Interfaces;
using KillerApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace KillerApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPcBuildLogic _pcBuildLogic;

        public HomeController(IConfiguration configuration)
        {
            _pcBuildLogic = PcBuildFactory.CreateLogic(configuration);
        }

        public IActionResult Index()
        {
            var model = new HomeIndexViewModel()
            {
                Builds = _pcBuildLogic.GetAllBuilds().AsList()
            };
            return View(model);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}