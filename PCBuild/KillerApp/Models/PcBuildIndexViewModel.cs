﻿using System.Collections.Generic;
using KillerApp.Domain;

namespace KillerApp.Models
{
    public class PcBuildIndexViewModel
    {
        public IEnumerable<PcPart> PcParts { get; set; }
        public PcPart SelectedPcPart { get; set; }
        public IEnumerable<PcPart> SelectedPcParts { get; set; }
    }
}