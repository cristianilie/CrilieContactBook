using System.ComponentModel.DataAnnotations;

namespace CrilieContactBook.Model
{
    public class Contact
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Information { get; set; }

        public string Phone { get; set; }

        public string WhatsApp { get; set; }

        public string Skype { get; set; }
    }
}
