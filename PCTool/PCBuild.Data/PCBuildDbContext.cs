using Microsoft.EntityFrameworkCore;
using PCBuild_Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PCBuild_Data
{
    public class PCBuildDbContext : DbContext
    {
        public PCBuildDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<PCPart> PCParts { get; set; }
    }
}
