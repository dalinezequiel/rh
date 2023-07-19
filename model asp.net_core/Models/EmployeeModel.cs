using Microsoft.AspNetCore.Mvc.Rendering;

namespace model_asp.net_core.Models
{
    public class EmployeeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Gender { get; set; }
        public string Document { get; set; }
        public string DocNumber { get; set; }
        public DateTime CreateAt { get; set; }
        public EmployeeModel Employee { get; set; }
        public List<SelectListItem> Genders { get; set; }
        public List<SelectListItem> Documents { get; set; }
    }
}
