namespace model_asp.net_core.Models
{
    public class ContactModel
    {
        public int Id { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string WhatsApp { get; set; }
        public ContactModel Contact { get; set; }
    }
}
