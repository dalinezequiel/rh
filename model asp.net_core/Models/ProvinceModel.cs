using Microsoft.AspNetCore.Mvc.Rendering;

namespace model_asp.net_core.Models
{
    public class ProvinceModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public DistrictModel District { get; set; }
        public SelectListItem Selected { get; set; }
        public List<SelectListItem> Provinces { get; set; }
    }
}
