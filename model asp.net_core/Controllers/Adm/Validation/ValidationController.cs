namespace model_asp.net_core.Controllers.Admin.Validation
{
    public class ValidationController
    {
        public static string translateAction(string action)
        {
            if (action.Equals("Index"))
            {
                action = "Consulta";
            }else if (action.Equals("Create"))
            {
                action = "Criação";
            }else
            {
                action = "Actualização";
            }
            return action;
        }
    }
}
