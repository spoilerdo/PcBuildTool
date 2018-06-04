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

        public PcBuildAddViewModel(List<Propertie> allProperties, List<PcPart.PcType> allTypes)
        {            
            foreach (var type in allTypes)
            {
                SelectListItem listItem = new SelectListItem
                {
                    Text = type.ToString(),
                    Value = type.ToString()
                };
                AllTypes.Add(listItem);
            }

            foreach (var propertie in allProperties)
            {
                var listItem = new SelectListItem
                {
                    Text = propertie._Value,
                    Value = propertie.Id.ToString()
                };
                AllProperties.Add(listItem);
            }
        }
    }
}