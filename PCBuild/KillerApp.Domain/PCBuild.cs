using System.Collections.Generic;

namespace KillerApp.Domain
{
    public class PcBuild
    {
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
            ID = id;
            _Name = name;
            PartNames = partNames;
            Likes = likes;
            Dislikes = dislikes;
        }

        public string ID { get; set; }
        public string _Name { get; set; }
        public List<PcPart> PartNames { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
    }
}