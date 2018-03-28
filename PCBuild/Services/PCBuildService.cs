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
using Microsoft.AspNetCore.Http;

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
