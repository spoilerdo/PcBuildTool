namespace KillerApp.Domain
{
    public class Website
    {
        public string _Name { get; set; }
        public string _Url { get; set; }
        public string Pathdetails { get; set; }

        public Website()
        {  
        }
        public Website(string name, string url, string pathdetails)
        {
            _Name = name;
            _Url = url;
            Pathdetails = pathdetails;
        }
    }
}