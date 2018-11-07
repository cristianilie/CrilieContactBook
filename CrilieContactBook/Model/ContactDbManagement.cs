using Dapper;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;

namespace CrilieContactBook.Model
{
    public class ContactDbManagement
    {

        //Loads the Contact database and returns it as an observable collection
        public static ObservableCollection<Contact> LoadContacts()
        {
            using (IDbConnection con = new SQLiteConnection(LoadConnectiobString()))
            {
                var result = con.Query<Contact>("select * from Contact", new DynamicParameters());

                return new ObservableCollection<Contact>(result as List<Contact>);
            }

            
        }

        //Adds a new Contact to the Database
        public static void AddContact(Contact _contact)
        {
            using (IDbConnection con = new SQLiteConnection(LoadConnectiobString()))
            {
                con.Execute("Insert into Contact(FullName, Information, Phone, Skype, WhatsApp) values(@FullName, @Information, @Phone, @Skype, @WhatsApp)", _contact);
            }
        }

        //Updates a Contact's details in the database
        public static void UpdateContact(Contact _contact)
        {
            using (IDbConnection con = new SQLiteConnection(LoadConnectiobString()))
            {
                con.Execute("Update Contact Set FullName=@FullName, Information=@Information, Phone=@Phone, Skype=@Skype, WhatsApp=@WhatsApp Where Id=@Id", _contact);
            }
        }

        //Deletes a Contact from the database
        public static void DeleteContact(Contact _contact)
        {
            using (IDbConnection con = new SQLiteConnection(LoadConnectiobString()))
            {
                con.Execute("Delete from Contact Where Id=@Id", _contact);
            }
        }

        //Loads the ConnectionString so we can connect to the database
        private static string LoadConnectiobString(string _id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[_id].ConnectionString;
        }
    }
}
