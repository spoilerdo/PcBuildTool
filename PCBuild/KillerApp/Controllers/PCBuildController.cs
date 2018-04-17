using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using Dapper;
using KillerApp.Domain;
using KillerApp.Factory;
using KillerApp.Logic.Interfaces;
using KillerApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using File = KillerApp.Domain.File;

namespace KillerApp.Controllers
{
    public class PcBuildController : Controller
    {
        private readonly IPcBuildLogic _pcBuildLogic;
        private readonly ILikeLogic _likeLogic;

        private List<PcPart> _parts = new List<PcPart>();

        public PcBuildController(IConfiguration configuration)
        {
            _pcBuildLogic = PcBuildFactory.CreateLogic(configuration);
            _likeLogic = LikeFactory.CreateLogic(configuration);
        }

        public IActionResult Index()
        {
            var build = new Build();
            var selectedPcParts = new List<PcPart>();

            if (HttpContext.Session.Keys.Any())
            {
                if (HttpContext.Session.Keys.Contains("Build"))
                {
                    var buildObject = HttpContext.Session.GetString("Build");
                    build = JsonConvert.DeserializeObject<Build>(buildObject);
                }

                selectedPcParts = GetSelectedPcParts();
            }

            if (selectedPcParts.Count != 0)
                _parts = _pcBuildLogic.GetPartsByType(build, selectedPcParts.Last()._Type).AsList();
            else
                _parts = _pcBuildLogic.GetPartsByType(build, null).AsList();

            var partsObject = _parts;
            HttpContext.Session.SetString("Parts", JsonConvert.SerializeObject(partsObject));

            var model = new PcBuildIndexViewModel
            {
                PcParts = _parts,
                SelectedPcParts = selectedPcParts
            };

            return View(model);
        }
        public IActionResult Result()
        {
            var websites = _pcBuildLogic.GetWebsites();

            var model = new PcBuildResultViewModel
            {
                PcParts = _pcBuildLogic.GetPrices(GetSelectedPcParts(), websites)
            };
            return View(model);
        }
        public IActionResult Detail(string buildId)
        {
            var model = new PcBuildDetailViewModel
            {
                //TODO: get the build trough a buildId given to this IActionResult (make a stored procedure that gets all the parts from the build and the build info itself)
                //TODO: get the account from the builder of the given build
                Build = _pcBuildLogic.GetBuild(buildId)
            };
            model.Build.ID = buildId;

            return View(model);
        }
        [Authorize(Policy = "Moderator")]
        public IActionResult Add()
        {
            var model = new PcBuildAddViewModel(_pcBuildLogic.GetProperties().AsList(),
                _pcBuildLogic.GetAllTypes().AsList());
            return View(model);
        }

        private List<PcPart> GetSelectedPcParts()
        {
            var selectedPcParts = new List<PcPart>();
            var types = _pcBuildLogic.GetAllTypes().AsList();
            foreach (var type in types)
                if (HttpContext.Session.TryGetValue(type, out var value))
                    selectedPcParts.Add(JsonConvert.DeserializeObject<PcPart>(HttpContext.Session.GetString(type)));

            return selectedPcParts;
        }

        #region HttpPost Methods
        [HttpPost]
        public IActionResult SendPcPart(string id)
        {
            var parts = HttpContext.Session.GetString("Parts");
            var _parts = JsonConvert.DeserializeObject<List<PcPart>>(parts);

            var pcPart = _parts.Find(PcPart => PcPart.ID == id);
            HttpContext.Session.SetString(pcPart._Type, JsonConvert.SerializeObject(pcPart));

            var build = new Build();
            var buildObject = HttpContext.Session.GetString("Build");
            if (buildObject != null)
                build = JsonConvert.DeserializeObject<Build>(buildObject);

            build = _pcBuildLogic.AddPcPart(build, pcPart);
            var builObject = build;
            HttpContext.Session.SetString("Build", JsonConvert.SerializeObject(builObject));

            if (build.Finished)
                return RedirectToAction("Result");

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult AddPcPart([FromBody] string viewModel) //[FromBody] PcPart viewModel
        {
            var DeserializedModel = JsonConvert.DeserializeObject<PcBuildAddViewModel>(viewModel);
            //TODO: Give a response back to the user (succes or error)
            /*File file = new File(Path.GetFileName(viewModel.Image.FileName), viewModel.Image.ContentType,
                Domain.File.FileType.PcImage);*/

            /*using (var reader = new BinaryReader(viewModel.Image.OpenReadStream()))
            {
                file._Content = reader.ReadString();
            }

            _pcBuildLogic.AddPcPart(viewModel.PcPart, file);*/
            return RedirectToAction("Add");
        }

        [HttpPost]
        public IActionResult LikeSubmit(string buildId)
        {
            if (User.Identity is ClaimsIdentity claimsIdentity)
            {
                var userId = claimsIdentity.FindFirst("UserId").Value;
                _likeLogic.SubmitLike(buildId, userId);
            }

            return Ok();
        }

        [HttpPost]
        public IActionResult DislikeSubmit(string buildId)
        {
            if (User.Identity is ClaimsIdentity claimsIdentity)
            {
                var userId = claimsIdentity.FindFirst("UserId").Value;
                _likeLogic.SubmitDislike(buildId, userId);
            }

            return Ok();
        }
        #endregion
    }
}