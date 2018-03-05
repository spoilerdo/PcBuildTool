using System;
using System.Collections.Generic;
using System.Text;
using API.Models;
using Data;

namespace Services
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
            return _context.GetAll();
        }
        public IEnumerable<PCPart> GetPartsByID(int ID)
        {
            return _context.GetByID(ID);
        }
        public IEnumerable<string> GetPCTypes()
        {
            return _context.GetPartTypes();
        }
        public IEnumerable<int> PartlistCount(int BuildID)
        {
            return _context.PartlistCount(BuildID);
        }

        public void SetBuild(int ID)
        {
            _context.SetBuild(ID);
        }
        public void AddPCPart(PCPart pcPart, int BuildID)
        {
            _context.AddPart(pcPart, BuildID);
        }
    }
}
