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
        public IEnumerable<PcPart> GetPartsByType(Build build)
        {
            List<string> pcTypes = GetPcTypes().ToList();
            int index = PartlistCount(1).First();
            string selectedType = pcTypes[index];

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
                    case "Power":
                        propertyIds.Add(build.Cpu);
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

        public IEnumerable<PcPart> GetPartsById(int id)
        {
            return _context.GetById(id);
        }
        public IEnumerable<string> GetPcTypes()
        {
            return _context.GetPartTypes();
        }
        public IEnumerable<int> PartlistCount(int buildId)
        {
            return _context.PartlistCount(buildId);
        }

        public void SetBuild(int id)
        {
            _context.SetBuild(id);
        }
        public Build AddPcPart(PcPart pcPart, int buildId)
        {
            _context.AddPart(pcPart, buildId);

            Build build = new Build();
            switch (pcPart._Type)
            {
                case "Case":
                    build.Case = pcPart.Properties[0].Id;
                    break;
                case "Motherboard":
                    build.Cpu = pcPart.Properties[0].Id;
                    //add memory type
                    break;
            }
            return build;
        }
    }
}
