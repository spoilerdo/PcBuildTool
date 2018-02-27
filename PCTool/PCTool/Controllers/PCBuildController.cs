using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PCTool.Models;

namespace PCTool.Controllers
{
    public class PCBuildController : Controller
    {
        public IActionResult Index()
        {

            var model = new PCBuildIndexModel()
            {

            };

            return View(model);
        }
    }
}