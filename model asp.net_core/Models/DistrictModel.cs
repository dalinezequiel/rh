using Microsoft.AspNetCore.Mvc.Rendering;

namespace model_asp.net_core.Models
{
    public class DistrictModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public int ProvinceId { get; set; }
        public List<SelectListItem> Districts { get; set; }
    }
}
