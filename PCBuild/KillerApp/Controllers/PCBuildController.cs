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

        #region HttpGetMethods

        public PcBuildController(IConfiguration configuration)
        {
            _pcBuildLogic = PcBuildFactory.CreateLogic(configuration);
            _likeLogic = LikeFactory.CreateLogic(configuration);
        }

        public IActionResult Index(bool newBuild = false)
        {
            if(newBuild)
                HttpContext.Session.Clear();

            var build = new Build();
            var selectedPcParts = new List<PcPart>();

            if (HttpContext.Session.Keys.Any())
            {
                if (HttpContext.Session.Keys.Contains("Build"))
                    build = GetBuild();

                selectedPcParts = GetSelectedPcParts();
            }

            if (!build.Finished)
            {
                if (selectedPcParts.Count != 0)
                    _parts = _pcBuildLogic.GetPartsByType(build, selectedPcParts.Last()._Type).AsList();
                else
                    _parts = _pcBuildLogic.GetPartsByType(build, PcPart.PcType.Null).AsList();

                var partsObject = _parts;
                HttpContext.Session.SetString("Parts", JsonConvert.SerializeObject(partsObject));
            }

            int progress = 0;
            if (selectedPcParts.Count() != 0)
                progress = _pcBuildLogic.GetProgress(selectedPcParts.Last()._Type);

            var model = new PcBuildIndexViewModel
            {
                PcParts = _parts,
                SelectedPcParts = selectedPcParts,
                BuildFinished = build.Finished,
                Progress = progress
            };

            return View(model);
        }

        public IActionResult Result()
        {
            var websites = _pcBuildLogic.GetWebsites();

            IEnumerable <Result> pcParts = _pcBuildLogic.GetPrices(GetSelectedPcParts(), websites);
            var model = new PcBuildResultViewModel
            {
                PcParts = pcParts,
                BestOption = _pcBuildLogic.CheckBestPriceOption(pcParts.AsList())
            };
            return View(model);
        }

        public IActionResult Detail(string buildId)
        {
            PcBuildDetailViewModel model = new PcBuildDetailViewModel
            {
                Build = _pcBuildLogic.GetBuild(buildId),
                Account = _pcBuildLogic.GetUserFromBuild(buildId)
            };

            if (User.Identity is ClaimsIdentity claimsIdentity && User.Identity.IsAuthenticated)
            {
                var userId = GetUserId();

                model.Liked = _likeLogic.GetLikeFromUser(buildId, userId);
                model.Disliked = _likeLogic.GetDislikeFromUser(buildId, userId);
            }
            model.Build.Id = buildId;

            return View(model);
        }

        [Authorize(Policy = "Moderator")]
        public IActionResult Add()
        {
            var model = new PcBuildAddViewModel(_pcBuildLogic.GetProperties().AsList(),
                _pcBuildLogic.GetAllTypes().AsList());
            return View(model);
        }

        #endregion

        #region PrivateMethods

        private List<PcPart> GetSelectedPcParts()
        {
            var selectedPcParts = new List<PcPart>();
            var types = _pcBuildLogic.GetAllTypes().AsList();

            foreach (var type in types)
                if (HttpContext.Session.TryGetValue(type.ToString(), out var value))
                    selectedPcParts.Add(JsonConvert.DeserializeObject<PcPart>(HttpContext.Session.GetString(type.ToString())));

            return selectedPcParts;
        }

        private Build GetBuild()
        {
            var buildObject = HttpContext.Session.GetString("Build");

            Build build = new Build();
            if(buildObject != null)
            {
                build = JsonConvert.DeserializeObject<Build>(buildObject);
            }
            return build;
        }

        private void AddBuild(Build build)
        {
            var builObject = build;
            HttpContext.Session.SetString("Build", JsonConvert.SerializeObject(builObject));
        }

        private string GetUserId()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            return claimsIdentity.FindFirst("UserId").Value;
        }

        #endregion

        #region HttpPost Methods

        [HttpPost]
        public IActionResult SendPcPart(PcBuildIndexViewModel viewModel)
        {
            var parts = HttpContext.Session.GetString("Parts");
            var _parts = JsonConvert.DeserializeObject<List<PcPart>>(parts);

            var pcPart = _parts.Find(PcPart => PcPart.Id == viewModel.SelectedPcPartId);
            HttpContext.Session.SetString(pcPart._Type.ToString(), JsonConvert.SerializeObject(pcPart));

            var build = GetBuild();
            if(build != null)
                build = _pcBuildLogic.AddBuild(build, pcPart);

            AddBuild(build);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> AddPcPart(PcBuildAddViewModel viewModel)
        {
            if (viewModel.PcPart.Image != null)
            {
                var path = Path.Combine
                (
                    Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "images",
                    viewModel.PcPart.Image.FileName
                );

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await viewModel.PcPart.Image.CopyToAsync(stream);
                }
                _pcBuildLogic.AddPcPart(viewModel.Properties, viewModel.PcPart, viewModel.PcPart.Image.FileName);
            }
            else
            {
                _pcBuildLogic.AddPcPart(viewModel.Properties, viewModel.PcPart);
            }

            return RedirectToAction("Add");
        }

        [Authorize]
        [HttpPost]
        public IActionResult SaveBuild(PcBuildResultViewModel viewModel)
        {
            var userId = GetUserId();

            _pcBuildLogic.SetBuild(viewModel.PcBuild, GetSelectedPcParts(), userId);
            return RedirectToAction("Result");
        }

        [HttpPost]
        public IActionResult ChangeLikeStatus(PcBuildDetailViewModel viewModel)
        {
            var userId = GetUserId();

            if(viewModel.Liked)
                _likeLogic.SubmitLike(viewModel.Build, userId);
            else
                _likeLogic.SubmitDislike(viewModel.Build, userId);

            return RedirectToAction("Detail", viewModel.Build.Id);
        }

        #endregion
    }
}