using System;
using System.Collections.Generic;
using System.Text;

namespace PCBuild_Data.Models
{
    public class Build
    {
        public int BuildID { get; set; }
        public int UserID { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
    }
}
