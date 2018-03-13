using System;
using System.Collections.Generic;
using System.Text;

namespace API.Models
{
    public class Build
    {
        public int Cpu { get; set; }
        public int[] Processor { get; set; }
        public int[] Connection { get; set; }
        public int Power { get; set; }
        public int RAM { get; set; }
        public int Cooler { get; set; }
        public int Case { get; set; }
    }
}
