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
            return _context.GetAll();
        }
    }
}
