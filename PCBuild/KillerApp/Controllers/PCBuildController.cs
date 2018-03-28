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

            Build build = new Build();
            List<PcPart> selectedPcParts = new List<PcPart>();

            if (HttpContext.Session.Keys.Any())
            {
                if (HttpContext.Session.Keys.Contains("Build"))
                {
                    var buildObject = HttpContext.Session.GetString("Build");
                    build = JsonConvert.DeserializeObject<Build>(buildObject);
                }

                List<string> types = _ipcBuildService.GetAllTypes().AsList();
                foreach (string type in types)
                {
                    if(HttpContext.Session.TryGetValue(type, out byte[] value))
                    {
                        selectedPcParts.Add(JsonConvert.DeserializeObject<PcPart>(HttpContext.Session.GetString(type)));
                    }
                }
            }
            if(selectedPcParts.Count != 0)
                _parts = _ipcBuildService.GetPartsByType(build, selectedPcParts.Last()._Type).AsList();
            else
                _parts = _ipcBuildService.GetPartsByType(build, null).AsList();

            var partsObject = _parts;
            HttpContext.Session.SetString("Parts", JsonConvert.SerializeObject(partsObject));

            var model = new PcBuildIndexModel()
            {
                PcParts = _parts,
                SelectedPcParts = selectedPcParts
            };

            return View(model);
        }
        public IActionResult BuildResult()
        {
            return View();
        }

        #region HttpPost Methods
        [HttpPost]
        public ActionResult SendPcPart(int EAN)
        {
            var parts = HttpContext.Session.GetString("Parts");
            List<PcPart> _parts = JsonConvert.DeserializeObject<List<PcPart>>(parts);

            PcPart pcPart = _parts.Find(PcPart => PcPart.EAN == EAN);
            HttpContext.Session.SetString(pcPart._Type, JsonConvert.SerializeObject(pcPart));

            Build build = _ipcBuildService.AddPcPart(pcPart, 1);
            var builObject = build;
            HttpContext.Session.SetString("Build", JsonConvert.SerializeObject(builObject));

            if (build.Finished)
                return RedirectToAction("BuildResult");

            return RedirectToAction("Index");
        }
        #endregion
    }
}