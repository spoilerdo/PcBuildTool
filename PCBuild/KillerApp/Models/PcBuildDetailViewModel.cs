﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KillerApp.Domain;

namespace KillerApp.Models
{
    public class PcBuildDetailViewModel
    {
        public PcBuild Build { get; set; }
        public Account Account { get; set; }
        public bool Liked { get; set; }
        public bool Disliked { get; set; }
    }
}
