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

        private PCPart selectedPCPart;

        public PCBuildController(IPCBuildService PCBuildService)
        {
            _PCBuildService = PCBuildService;
        }

        public IActionResult Index()
        {
            List<string> PCTypes = _PCBuildService.GetPCTypes().ToList();
            int index = _PCBuildService.PartlistCount(1).First() + 1;
            string selectedType = PCTypes[index];
            var partList = _PCBuildService.GetAllParts();

            var model = new PCBuildIndexModel()
            {
                PCParts = partList,
                SelectedPCParts = new List<PCPart>(),
                SelectedType = selectedType
            };

            return View(model);
        }
        [Route("")]
        [HttpPost]
        public ActionResult SelectPCPart(int Submit)
        {
            selectedPCPart = _PCBuildService.GetPartsByID(Submit).First();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult SendPCPart()
        {
            //Problem: when the action: SelectPCPart is finished it reloades the page and clears the selectedPCPart var!!
            //this isn't rigth because you can't used this var to put the selected pc part to the partslist!!
            _PCBuildService.AddPCPart(selectedPCPart, 1);
            return RedirectToAction("Index");
        }
    }
}