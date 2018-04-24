using System.Collections.Generic;
using KillerApp.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KillerApp.Models
{
    public class PcBuildAddViewModel
    {
        public List<SelectListItem> AllProperties { get; } = new List<SelectListItem>();
        public List<SelectListItem> AllTypes { get; } = new List<SelectListItem>();
        public PcPart PcPart { get; set; }
        public List<int> Properties { get; set; }

        public PcBuildAddViewModel()
        {
        }

        public PcBuildAddViewModel(List<Propertie> allProperties, List<string> allTypes)
        {            
            foreach (string type in allTypes)
            {
                SelectListItem listItem = new SelectListItem
                {
                    Text = type,
                    Value = type
                };
                AllTypes.Add(listItem);
            }

            foreach (Propertie propertie in allProperties)
            {
                SelectListItem listItem = new SelectListItem
                {
                    Text = propertie._Value,
                    Value = propertie.Id.ToString()
                };
                AllProperties.Add(listItem);
            }
        }
    }
}