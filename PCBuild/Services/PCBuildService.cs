using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using API.Models;
using Dapper;
using Data;
using System.IO;
using System.Collections;
using System.Net;
using System.Threading;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Http;
using Services.Price;

namespace Services
{
    public class PcBuildService : IPcBuildService
    {
        private readonly IPcBuild _context;

        public PcBuildService(IPcBuild context)
        {
            _context = context;
        }

        #region SelectMethods
        public IEnumerable<PcPart> GetPartsByType(Build build, string latestType)
        {
            string selectedType;
            if (latestType != null)
                selectedType = _context.GetSelectedType(latestType).First();
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
            List<PcPart> parts = _context.GetAllByType(selectedType, propertyIds).AsList();
            return parts;
        }
        public IEnumerable<PcPart> GetSelectedParts(int buildiD)
        {
            return _context.GetSelectedParts(buildiD);
        }
        public IEnumerable<string> GetAllTypes()
        {
            return _context.GetAllTypes();
        }
        public IEnumerable<Result> GetPrices(IEnumerable<PcPart> pcParts, IEnumerable<Website> websites)
        {
            //TODO: als je: Intel Core i7-7700K zoekt op bol.com krijg je niet de processor maar een computer hoe ga je dit fixen?

            List<Result> results = new List<Result>();
            //Check for every pc part the price in every shop
            foreach (PcPart pcPart in pcParts)
            {
                List<string> prices = new List<string>();
                foreach (Website website in websites)
                {
                    List<string> subpaths = website.Pathdetails.Split(',').ToList();

                    List<HtmlNode> price = new GetPrice(
                        new PriceUrlBuilder(website._Url, pcPart._Name, "+").GetUrl(),
                        new PricePathBuilder(subpaths[0], subpaths[1], subpaths[2]).GetPath()
                    ).Get();

                    if (price != null)
                        prices.Add(price.First().InnerText.Replace("\n", "").Replace(" ", ""));
                }
                results.Add(new Result(prices, pcPart));
            }

            return results;
        }
        public IEnumerable<Website> GetWebsites()
        {
            return _context.GetWebsites();
        }
        #endregion

        #region InsertMethods
        public void SetBuild(int id)
        {
            _context.SetBuild(id);
        }
        public Build AddPcPart(PcPart pcPart, int buildId)
        {
            Build build = new Build();
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
        public void AddPcPartToDb(PcPart pcPart, int buildId)
        {
            //TODO: make it so you can add a list of pcParts
            _context.AddPart(pcPart, buildId);
        }

        #endregion
    }
}
