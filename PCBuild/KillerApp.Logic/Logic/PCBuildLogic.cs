using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.IO;
using System.Collections;
using System.Net;
using System.Threading;
using Dapper;
using HtmlAgilityPack;
using KillerApp.DAL.Interfaces;
using KillerApp.Domain;
using KillerApp.Logic.Interfaces;
using KillerApp.Logic.Logic.Price;
using Microsoft.AspNetCore.Http;

namespace KillerApp.Logic.Logic
{
    public class PcBuildService : IPcBuildService
    {
        private readonly IPcBuildContext _pcBuildRepository;

        public PcBuildService(IPcBuildContext pcBuildRepository)
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

            List<int> propertyIds = new List<int>();
            if (build != null)
            {
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
            }
            List<PcPart> parts = _pcBuildRepository.GetAllByType(selectedType, propertyIds).AsList();
            return parts;
        }
        public IEnumerable<PcPart> GetSelectedParts(int buildiD)
        {
            return _pcBuildRepository.GetSelectedParts(buildiD);
        }
        public IEnumerable<string> GetAllTypes()
        {
            return _pcBuildRepository.GetAllTypes();
        }
        public IEnumerable<Result> GetPrices(IEnumerable<PcPart> pcParts, IEnumerable<Website> websites)
        {
            //TODO: check if the selected product on the website is in fact the product your searching. Otherwise the product doesn't exist on that website

            List<Result> results = new List<Result>();
            //Check for every pc part the price in every shop
            foreach (PcPart pcPart in pcParts)
            {
                List<WebsitePrice> prices = new List<WebsitePrice>();
                foreach (Website website in websites)
                {
                    List<string> subpaths = website.Pathdetails.Split(',').ToList();

                    //get the price trough the website url and the price container + class
                    List<HtmlNode> price = new GetPrice(
                        new PriceUrlBuilder(website._Url, pcPart._Name, "+").GetUrl(),
                        new PricePathBuilder(subpaths[0], subpaths[1], subpaths[2]).GetPath()
                    ).Get();

                    if (price != null)
                        prices.Add(new WebsitePrice(website._Name, price.First().InnerText.Replace("\n", "").Replace(" ", "")));
                }
                results.Add(new Result(prices, pcPart));
            }

            return results;
        }
        public IEnumerable<Website> GetWebsites()
        {
            return _pcBuildRepository.GetWebsites();
        }
        public IEnumerable<Propertie> GetProperties()
        {
            return _pcBuildRepository.GetProperties();
        }
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
