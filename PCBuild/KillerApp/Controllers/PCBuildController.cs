using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Data;
using API.Models;
using KillerApp.Models;

namespace KillerApp.Controllers
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
        //[Route("")]
        [HttpPost]
        public ActionResult SelectPCPart(int Submit)
        {
            selectedPCPart = _PCBuildService.GetPartsByID(Submit).First();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult SendPCPart()
        {
            _PCBuildService.AddPCPart(selectedPCPart, 1);
            return RedirectToAction("Index");
        }
    }
}