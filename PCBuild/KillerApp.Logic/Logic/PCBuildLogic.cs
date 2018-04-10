using System.Collections.Generic;
using System.Linq;
using Dapper;
using KillerApp.DAL.Interfaces;
using KillerApp.Domain;
using KillerApp.Logic.Interfaces;
using KillerApp.Logic.Logic.Price;

namespace KillerApp.Logic.Logic
{
    public class PcBuildLogic : IPcBuildLogic
    {
        private readonly IPcBuildContext _pcBuildRepository;

        public PcBuildLogic(IPcBuildContext pcBuildRepository)
        {
            _pcBuildRepository = pcBuildRepository;
        }

        #region SelectMethods

        public IEnumerable<PcPart> GetPartsByType(Build build, string latestType)
        {
            string selectedType;
            if (latestType != null)
                selectedType = _pcBuildRepository.GetSelectedType(latestType).First();
            else
                selectedType = "Case";

            var propertyIds = new List<int>();
            if (build != null)
                switch (selectedType)
                {
                    case "Processor":
                        propertyIds.Add(build.Cpu);
                        break;
                    case "RAM":
                        propertyIds.Add(build.RAM);
                        break;
                    case "Memory":
                        propertyIds.Add(build.Memory);
                        break;
                    case "Power":
                        propertyIds.Add(build.Power);
                        break;
                }

            return _pcBuildRepository.GetAllByType(selectedType, propertyIds).AsList();
        }

        public IEnumerable<PcPart> GetSelectedParts(int buildiD) => _pcBuildRepository.GetSelectedParts(buildiD);

        public IEnumerable<string> GetAllTypes() => _pcBuildRepository.GetAllTypes();

        public IEnumerable<Result> GetPrices(IEnumerable<PcPart> pcParts, IEnumerable<Website> websites)
        {
            //TODO: check if the selected product on the website is in fact the product your searching. Otherwise the product doesn't exist on that website

            var results = new List<Result>();
            //Check for every pc part the price in every shop
            foreach (var pcPart in pcParts)
            {
                var prices = new List<WebsitePrice>();
                foreach (var website in websites)
                {
                    var subpaths = website.Pathdetails.Split(',').ToList();

                    //get the price trough the website url and the price container + class
                    var price = new GetPrice(
                        new PriceUrlBuilder(website._Url, pcPart._Name, "+").GetUrl(),
                        new PricePathBuilder(subpaths[0], subpaths[1], subpaths[2]).GetPath()
                    ).Get();

                    if (price != null)
                        prices.Add(new WebsitePrice(website._Name,
                            price.First().InnerText.Replace("\n", "").Replace(" ", "")));
                }

                results.Add(new Result(prices, pcPart));
            }

            return results;
        }

        public IEnumerable<Website> GetWebsites() => _pcBuildRepository.GetWebsites();

        public IEnumerable<Propertie> GetProperties() => _pcBuildRepository.GetProperties();

        #endregion

        #region InsertMethods

        public void SetBuild(int id)
        {
            _pcBuildRepository.SetBuild(id);
        }

        public Build AddPcPart(Build build, PcPart pcPart)
        {
            switch (pcPart._Type)
            {
                case "Case":
                    build.Finished = false;
                    build.Case = pcPart.Properties.Find(x => x.Type == "Case").Id;
                    break;
                case "Motherboard":
                    build.Cpu = pcPart.Properties.Find(x => x.Type == "Motherboard").Id;
                    build.RAM = pcPart.Properties.Find(x => x.Type == "RAM").Id;
                    build.Power = pcPart.Properties.Find(x => x.Type == "Power").Id;
                    build.Memory = pcPart.Properties.Find(x => x.Type == "Memory").Id;
                    break;
                case "Power":
                    build.Finished = true;
                    break;
            }

            return build;
        }

        public void AddPcPartToBuild(PcPart pcPart, int buildId)
        {
            //TODO: make it so you can add a list of pcParts
            _pcBuildRepository.AddPartToBuild(pcPart, buildId);
        }

        public void AddPcPart(PcPart pcPart)
        {
            _pcBuildRepository.AddPart(pcPart);
        }

        #endregion
    }
}