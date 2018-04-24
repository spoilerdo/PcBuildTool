using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
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

            if(build.Finished)
                HttpContext.Session.Clear();

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
            PcBuildDetailViewModel model = new PcBuildDetailViewModel
            {
                Build = _pcBuildLogic.GetBuild(buildId)
            };
            if (User.Identity.IsAuthenticated)
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var userId = claimsIdentity.FindFirst("UserId").Value;

                model.Liked = _likeLogic.GetLikeFromUser(buildId, userId);
                model.Disliked = _likeLogic.GetDislikeFromUser(buildId, userId);
            }
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

        public string GetImagePath(string path)
        {
            return "~/Images/" + path;
        }

        #region HttpPost Methods
        [HttpPost]
        public IActionResult SendPcPart(PcBuildIndexViewModel viewModel)
        {
            var parts = HttpContext.Session.GetString("Parts");
            var _parts = JsonConvert.DeserializeObject<List<PcPart>>(parts);

            var pcPart = _parts.Find(PcPart => PcPart.ID == viewModel.SelectedPcPartId);
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
        public async Task<IActionResult> AddPcPart(PcBuildAddViewModel viewModel)
        {
            var path = Path.Combine
            (
                Directory.GetCurrentDirectory(), 
                "wwwroot", 
                "images",
                viewModel.PcPart.Image.FileName
            );

            var serverpath = Path.Combine
            (
                "~/images/",
                viewModel.PcPart.Image.FileName
            );

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await viewModel.PcPart.Image.CopyToAsync(stream);
            }
            _pcBuildLogic.AddPcPart(viewModel.Properties, viewModel.PcPart, serverpath);
            return RedirectToAction("Add");
        }

        [Authorize]
        [HttpPost]
        public IActionResult SaveBuild(PcBuildResultViewModel viewModel)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var userId = claimsIdentity.FindFirst("UserId").Value;

            //TODO: add build logic + SP
            _pcBuildLogic.SetBuild(viewModel.PcBuild, GetSelectedPcParts(), userId);
            return RedirectToAction("Result");
        }

        [HttpPost]
        public IActionResult ChangeLikeStatus(PcBuildDetailViewModel viewModel)
        {
            if (User.Identity is ClaimsIdentity claimsIdentity)
            {
                var userId = claimsIdentity.FindFirst("UserId").Value;

                if(viewModel.Liked) //op de like knop gedrukt -> als de gebruiker al gedisliked heeft moet er dus een dislike vanaf
                    _likeLogic.SubmitLike(viewModel.Build.ID, userId);
                else
                    _likeLogic.SubmitDislike(viewModel.Build.ID, userId);
            }

            return RedirectToAction("Detail", viewModel.Build.ID);
        }
        #endregion
    }
}