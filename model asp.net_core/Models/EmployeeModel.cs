using Microsoft.AspNetCore.Mvc.Rendering;

namespace model_asp.net_core.Models
{
    public class EmployeeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Gender { get; set; }
        public DateTime Birthday { get; set; }
        public string Birth { get; set; }
        public DocumentModel Document { get; set; }
        public AddressModel Address { get; set; }
        public ContactModel Contact { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Status { get; set; }
        public EmployeeModel Employee { get; set; }
        public List<SelectListItem> Genders { get; set; }
        public List<SelectListItem> Documents { get; set; }
        public List<SelectListItem> States { get; set; }
    }
}
