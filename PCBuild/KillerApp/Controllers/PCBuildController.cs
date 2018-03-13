using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Data;
using API.Models;
using Dapper;
using KillerApp.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace KillerApp.Controllers
{
    public class PcBuildController : Controller
    {
        private readonly IPcBuildService _ipcBuildService;

        List<PcPart> _parts = new List<PcPart>();

        public PcBuildController(IPcBuildService ipcBuildService)
        {
            _ipcBuildService = ipcBuildService;
        }

        public IActionResult Index()
        {
            //TODO: use interface
            var buildObject = HttpContext.Session.GetString("Build");
            Build build;
            if (buildObject != null)
                build = JsonConvert.DeserializeObject<Build>(buildObject);
            else
                build = null;

            _parts = _ipcBuildService.GetPartsByType(build).AsList();
            HttpContext.Session.SetString("parts", JsonConvert.SerializeObject(_parts));

            var model = new PcBuildIndexModel()
            {
                PcParts = _parts,
                SelectedPcParts = _ipcBuildService.GetSelectedParts(1)
            };

            return View(model);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult SelectPcPart([FromBody] PcPart pcPart)
        {
            var parts = HttpContext.Session.GetString("parts");
            List<PcPart> _parts = JsonConvert.DeserializeObject<List<PcPart>>(parts);
            var partObject = _parts.Find(PcPart => PcPart.EAN == pcPart.EAN);
            HttpContext.Session.SetString("selectedPcPart", JsonConvert.SerializeObject(partObject));
            return null;
        }
        [HttpPost]
        public ActionResult SendPcPart()
        {
            //TODO: use interface
            var partObject = HttpContext.Session.GetString("selectedPcPart");
            PcPart part = JsonConvert.DeserializeObject<PcPart>(partObject);

            Build build = _ipcBuildService.AddPcPart(part, 1);
            var builObject = new Build();
            HttpContext.Session.SetString("Build", JsonConvert.SerializeObject(builObject));

            return RedirectToAction("Index");
        }
    }
}