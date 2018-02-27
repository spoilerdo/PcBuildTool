using Microsoft.EntityFrameworkCore;
using PCBuild.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PCBuild.Data
{
    public class PCBuildDbContext : DbContext
    {
        public PCBuildDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<PCPart> PCParts { get; set; }
    }
}
