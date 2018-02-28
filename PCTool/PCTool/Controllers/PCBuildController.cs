using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PCTool.Models;
using PCBuild_Data;
using PCBuild_Data.Models;

namespace PCTool.Controllers
{
    public class PCBuildController : Controller
    {
        private readonly IPCBuildService _PCBuildService;

        public PCBuildController(IPCBuildService PCBuildService)
        {
            _PCBuildService = PCBuildService;
        }

        public IActionResult Index()
        {
            var partList = _PCBuildService.GetAllParts();

            var model = new PCBuildIndexModel()
            {
                PCParts = partList,
                SelectedPCParts = new List<PCPart>()
            };

            return View(model);
        }
    }
}