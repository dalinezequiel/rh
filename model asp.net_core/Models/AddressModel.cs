using Microsoft.AspNetCore.Mvc.Rendering;

namespace model_asp.net_core.Models
{
    public class AddressModel
    {
        public int Id { get; set; }
        public ProvinceModel Province { get; set; }
        public string Neighborhood { get; set; }
        public string Road { get;set; }
        public string Avenue { get; set; }
        protected List<SelectListItem> Provinces { get; set; }
        protected List<SelectListItem> Districts { get; set; }
    }
}
