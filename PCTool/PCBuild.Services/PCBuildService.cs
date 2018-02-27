using Microsoft.EntityFrameworkCore;
using PCBuild.Data;
using PCBuild.Data.Models;
using System;
using System.Collections.Generic;

namespace PCBuild.Services
{
    public class PCBuildService : IPCBuild
    {
        private readonly PCBuildDbContext _ctx;

        public PCBuildService(PCBuildDbContext ctx)
        {
            _ctx = ctx;
        }

        public IEnumerable<PCPart> GetAll()
        {
            return _ctx.PCParts; //maybe a include??
        }
    }
}
