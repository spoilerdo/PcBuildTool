using PCBuild.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace PCBuild.Services
{
    public class PCBuildServices : IPCBuild
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
