using System;
using System.Collections.Generic;
using System.Text;

namespace API.Models
{
    public class Propertie
    {
        public int Id { get; set; }
        public string _Value { get; set; }
        public string Type { get; set; }

        public Propertie()
        {
            
        }
        public Propertie(int id, string value = null, string type = null)
        {
            Id = id; _Value = value; Type = type;
        }
    }
}
