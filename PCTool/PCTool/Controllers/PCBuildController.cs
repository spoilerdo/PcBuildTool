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
        private string selectedType = "Case";

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
                SelectedPCParts = new List<PCPart>(),
                SelectedType = selectedType
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult SendPCPart(int Submit)
        {
            //Maybe get a id and link that to a PCPart from the db
            //Send the selected pc part to the DB and make a new page with new products this time from another type!!
            PCPart SelectedPCPart = (PCPart)_PCBuildService.GetPartsByID(Submit);
            return Ok();
        }
    }
}