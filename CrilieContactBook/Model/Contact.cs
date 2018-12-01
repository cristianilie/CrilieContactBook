using System.ComponentModel.DataAnnotations;
using System.Windows.Controls;

namespace CrilieContactBook.Model
{
    public class Contact : I_DB_Query
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Information { get; set; }

        public string Phone { get; set; }

        public string WhatsApp { get; set; }

        public string Skype { get; set; }


        public string AddQuery { get; set; } = "Insert into Contact(FullName, Information, Phone, Skype, WhatsApp) values(@FullName, @Information, @Phone, @Skype, @WhatsApp)";

        public string EditQuery { get; set; } = "Update Contact Set FullName=@FullName, Information=@Information, Phone=@Phone, Skype=@Skype, WhatsApp=@WhatsApp Where Id=@Id";
    }
}
