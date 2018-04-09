using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Data;
using API.Models;
using Dapper;
using KillerApp.Models;
using Microsoft.AspNetCore.Authorization;
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
            Build build = new Build();
            List<PcPart> selectedPcParts = new List<PcPart>();

            if (HttpContext.Session.Keys.Any())
            {
                if (HttpContext.Session.Keys.Contains("Build"))
                {
                    var buildObject = HttpContext.Session.GetString("Build");
                    build = JsonConvert.DeserializeObject<Build>(buildObject);
                }

                selectedPcParts = GetSelectedPcParts();
            }
            if(selectedPcParts.Count != 0)
                _parts = _ipcBuildService.GetPartsByType(build, selectedPcParts.Last()._Type).AsList();
            else
                _parts = _ipcBuildService.GetPartsByType(build, null).AsList();

            var partsObject = _parts;
            HttpContext.Session.SetString("Parts", JsonConvert.SerializeObject(partsObject));

            var model = new PcBuildIndexViewModel()
            {
                PcParts = _parts,
                SelectedPcParts = selectedPcParts
            };

            return View(model);
        }
        public IActionResult Result()
        {
            IEnumerable<Website> websites = _ipcBuildService.GetWebsites();

            var model = new PcBuildResultViewModel()
            {
                PcParts = _ipcBuildService.GetPrices(GetSelectedPcParts(), websites)
            };
            return View(model);
        }
        [Authorize(Policy = "Moderator")]
        public IActionResult Add()
        {
            var model = new PcBuildAddViewModel()
            {
                AllProperties = _ipcBuildService.GetProperties().AsList(),
                AllTypes = _ipcBuildService.GetAllTypes().AsList()
            };
            return View(model);
        }
        private List<PcPart> GetSelectedPcParts()
        {
            List<PcPart> selectedPcParts = new List<PcPart>();
            List<string> types = _ipcBuildService.GetAllTypes().AsList();
            foreach (string type in types)
            {
                if (HttpContext.Session.TryGetValue(type, out byte[] value))
                {
                    selectedPcParts.Add(JsonConvert.DeserializeObject<PcPart>(HttpContext.Session.GetString(type)));
                }
            }

            return selectedPcParts;
        }

        #region HttpPost Methods
        [HttpPost]
        public IActionResult SendPcPart(int ean)
        {
            var parts = HttpContext.Session.GetString("Parts");
            List<PcPart> _parts = JsonConvert.DeserializeObject<List<PcPart>>(parts);

            PcPart pcPart = _parts.Find(PcPart => PcPart.EAN == ean);
            HttpContext.Session.SetString(pcPart._Type, JsonConvert.SerializeObject(pcPart));

            Build build = new Build();
            var buildObject = HttpContext.Session.GetString("Build");
            if(buildObject != null)
                build = JsonConvert.DeserializeObject<Build>(buildObject);

            build = _ipcBuildService.AddPcPart(build, pcPart);
            var builObject = build;
            HttpContext.Session.SetString("Build", JsonConvert.SerializeObject(builObject));

            if (build.Finished)
                return RedirectToAction("Result");

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult AddPcPart([FromBody]PcPart pcPart)
        {
            _ipcBuildService.AddPcPart(pcPart);
            return RedirectToAction("Add");
        }
        #endregion
    }
}