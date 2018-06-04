using System.ComponentModel.DataAnnotations.Schema;

namespace KillerApp.Domain
{
    public class Website
    {
        public string _Name { get; set; }
        public string _Url { get; set; }
        public string Pathdetails { get; set; }
        public string Pathtitle { get; set; }

        public Website()
        {  
        }
        public Website(string name, string url, string pathdetails, string pathtitle)
        {
            _Name = name;
            _Url = url;
            Pathdetails = pathdetails;
            Pathtitle = pathtitle;
        }
    }
}