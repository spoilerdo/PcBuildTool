﻿using System;
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

        public IEnumerable<PcPart> GetPartsByType(Build build, PcPart.PcType latestType)
        {
            var selectedType = latestType == PcPart.PcType.Null ? PcPart.PcType.Case : _pcBuildRepository.GetSelectedType(latestType);

            var propertyIds = new List<int>();
            if (build != null)
                switch (selectedType)
                {
                    case PcPart.PcType.Processor:
                        propertyIds.Add(build.Cpu);
                        break;
                    case PcPart.PcType.RAM:
                        propertyIds.Add(build.RAM);
                        break;
                    case PcPart.PcType.Memory:
                        propertyIds.Add(build.Memory);
                        break;
                    case PcPart.PcType.Power:
                        propertyIds.Add(build.Power);
                        break;
                }

            return _pcBuildRepository.GetAllByType(selectedType, propertyIds).AsList();
        }

        public IEnumerable<PcPart.PcType> GetAllTypes() => _pcBuildRepository.GetAllTypes();

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
                    var priceSubpaths = website.Pathdetails.Split(',').ToList();

                    var titleSubpaths = website.Pathtitle.Split(';').ToList();

                    //get the price trough the website url and the price container + class
                    var price = new GetPrice(
                        new PriceUrlBuilder(website._Url, pcPart._Name, "+").GetUrl(),
                        new PricePathBuilder(priceSubpaths[0], priceSubpaths[1], priceSubpaths[2]).GetPath(),
                        titleSubpaths
                    ).Get();

                    if (price != null)
                    {
                        prices.Add(new WebsitePrice(website._Name,
                            Convert.ToDecimal(price.Prices[0].InnerText.Replace("\n", "").Replace(" ", "").Replace("-", ""))));

                        /*for (int i = 0; i < price.Title.Count; i++)
                        {
                            if (price.Title[i].InnerText.Contains(pcPart._Name))
                            {
                                

                                break;
                            }
                        }*/
                    }
                }

                results.Add(new Result(prices, pcPart));
            }

            return results;
        }

        public IEnumerable<Website> GetWebsites() => _pcBuildRepository.GetWebsites();

        public IEnumerable<Propertie> GetProperties() => _pcBuildRepository.GetProperties();

        public PcBuild GetBuild(string buildId) => _pcBuildRepository.GetBuild(buildId);

        public IEnumerable<PcBuild> GetAllBuilds() => _pcBuildRepository.GetAllBuilds();

        public int GetProgress(PcPart.PcType currentType)
        {
            decimal maxProgress = _pcBuildRepository.GetMaxProgress();
            decimal currentProgress = _pcBuildRepository.GetCurrentProgress(currentType);

            decimal answer = currentProgress / maxProgress * 100;
            return Convert.ToInt32(answer);
        }

        public Account GetUserFromBuild(string buildId) => _pcBuildRepository.GetUserFromBuild(buildId);

        #endregion

        public List<Result> CheckBestPriceOption(List<Result> pcResults)
        {
            List<Result> results = new List<Result>();

            foreach (Result result in pcResults)
            {
                List<WebsitePrice> bestPrices = new List<WebsitePrice>();

                foreach (WebsitePrice price in result.PriceList)
                {
                    if (bestPrices.Count == 0 || price.Price <= bestPrices.First().Price)
                    {
                        if (bestPrices.Count != 0 && price.Price < bestPrices.First().Price)
                            bestPrices.Clear();

                        bestPrices.Add(price);
                    }
                }

                results.Add(new Result(bestPrices, result.PcPart));
            }

            return results;
        }

        #region InsertMethods

        public void SetBuild(PcBuild build, List<PcPart> pcParts, string userId)
        {
            build.PartNames = new List<PcPart>();

            foreach (PcPart pcPart in pcParts)
            {
                build.PartNames.Add(pcPart);
            }

            _pcBuildRepository.SetBuild(build, userId);
        }

        public Build AddBuild(Build build, PcPart pcPart)
        {
            switch (pcPart._Type)
            {
                case PcPart.PcType.Case:
                    build.Finished = false;
                    build.Case = pcPart.Properties.Find(x => x.Type == "Case").Id;
                    break;
                case PcPart.PcType.Motherboard:
                    build.Cpu = pcPart.Properties.Find(x => x.Type == "Motherboard").Id;
                    build.RAM = pcPart.Properties.Find(x => x.Type == "RAM").Id;
                    build.Power = pcPart.Properties.Find(x => x.Type == "Power").Id;
                    build.Memory = pcPart.Properties.Find(x => x.Type == "Memory").Id;
                    break;
                case PcPart.PcType.Power:
                    build.Finished = true;
                    break;
            }

            return build;
        }

        public void AddPcPart(List<int> properties, PcPart pcPart, string filepath)
        {
            pcPart.Properties = new List<Propertie>();

            foreach (int property in properties)
                pcPart.Properties.Add(new Propertie(property));

            _pcBuildRepository.AddPart(pcPart, filepath);
        }

        #endregion
    }
}