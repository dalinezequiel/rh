using Microsoft.AspNetCore.Mvc.Rendering;

namespace model_asp.net_core.Models
{
    public class DocumentModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public string PlaceOfIssue { get; set; }
        public DateTime DateOfIssue { get; set; }
        public string DateIssue { get; set; }
        public DocumentModel Document { get; set; }
        public List<SelectListItem> Selected { get; set; }
        public List<SelectListItem> Documents { get; set; }
    }
}
