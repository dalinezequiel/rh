using Microsoft.AspNetCore.Mvc.Rendering;
namespace model_asp.net_core.Models
{
    public class Standard
    {
        /*public static List<SelectListItem> Gender { get; set; } = new List<SelectListItem>
        {
             new SelectListItem("Masculino","1"),
             new SelectListItem("Femenino","2",true)
        };*/
        public static SelectListItem masculino, femenino;
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

        /*
         * public static List<SelectListItem> UpdateGender(String gender)
        {
            masculino = new SelectListItem();
            masculino.Text = "Masculino";
            masculino.Value = "Masculino";

            femenino = new SelectListItem();
            femenino.Text = "Femenino";
            femenino.Value = "Femenino";

            selecione = new SelectListItem();
            selecione.Text = "Selecione";
            selecione.Value = "Selecione";

            if (gender.Equals("Masculino"))
                masculino.Selected = true;
            else if(gender.Equals("Femenino"))
                femenino.Selected = true;
            else
                selecione.Selected = true;
            return new List<SelectListItem>() { masculino, femenino, selecione};
        }
         */
    }
}
