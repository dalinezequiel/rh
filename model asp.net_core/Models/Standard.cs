using Microsoft.AspNetCore.Mvc.Rendering;
namespace model_asp.net_core.Models
{
    public class Standard
    {
        public static SelectListItem masculino, femenino, documents, province, district;
        public static List<SelectListItem> UpdateGender(String gender)
        {
            masculino = new SelectListItem();
            masculino.Text = "Masculino";
            masculino.Value = "Masculino";

            femenino = new SelectListItem();
            femenino.Text = "Femenino";
            femenino.Value = "Femenino";

            if (gender.Equals("Masculino"))
                masculino.Selected = true;
            else
                femenino.Selected = true;
            return new List<SelectListItem>() { masculino, femenino};
        }
    }
}
