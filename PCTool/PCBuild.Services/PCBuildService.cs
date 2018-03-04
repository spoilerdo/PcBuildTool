using Microsoft.EntityFrameworkCore;
using PCBuild_Data;
using PCBuild_Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PCBuild_Services
{
    public class PCBuildService : IPCBuildService
    {
        private readonly IPCBuild _context;

        public PCBuildService(IPCBuild context)
        {
            _context = context;
        }
        public IEnumerable<PCPart> GetAllParts()
        {
            return _context.getAll();
        }
        public IEnumerable<PCPart> GetPartsByID(int ID)
        {
            return _context.getByID(ID);
        }
        public IEnumerable<string> GetPCTypes()
        {
            return _context.getPartTypes();
        }
        public IEnumerable<int> PartlistCount(int BuildID)
        {
            return _context.partlistCount(BuildID);
        }

        public void SetBuild(int ID)
        {
            _context.setBuild(ID);
        }
        public void AddPCPart(PCPart pcPart, int BuildID)
        {
            _context.addPart(pcPart, BuildID);
        }
    }
}
