using Microsoft.AspNetCore.Mvc.Rendering;
namespace model_asp.net_core.Controllers.Adm.Status
{
    public class StatusController
    {
        protected SelectListItem active, inactive = null;
        public List<SelectListItem> UpdateStatus(string status)
        {
            active = new SelectListItem();
            active.Text = "Activo";
            active.Value = active.Text;

            inactive = new SelectListItem();
            inactive.Text = "Inactivo";
            inactive.Value = inactive.Text;

            if (active.Text.Equals(status))
                active.Selected = true;
            else if (inactive.Text.Equals(status))
                inactive.Selected = true;

            return new List<SelectListItem> { active, inactive };
        }
    }
}
