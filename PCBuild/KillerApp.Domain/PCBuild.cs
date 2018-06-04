using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace KillerApp.Domain
{
    public class PcBuild
    {
        public string Id { get; set; }
        public string _Name { get; set; }
        public List<PcPart> PartNames { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }

        public PcBuild(string name, List<PcPart> partNames)
        {
            _Name = name;
            PartNames = partNames;
        }
        public PcBuild()
        {
            
        }
        public PcBuild(string name, List<PcPart> partNames, int likes, int dislikes, string id = "")
        {
            Id = id;
            _Name = name;
            PartNames = partNames;
            Likes = likes;
            Dislikes = dislikes;
        }
    }
}