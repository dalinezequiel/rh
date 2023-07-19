using Microsoft.AspNetCore.Mvc.Rendering;
namespace model_asp.net_core.Models
{
    public class DocumentModel
    {
        public int Id { get; set; }
        public string Document { get; set; }
        public List<SelectListItem> Selected { get; set; }
    }
}
