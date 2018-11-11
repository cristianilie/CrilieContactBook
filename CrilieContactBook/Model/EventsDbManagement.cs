using Dapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrilieContactBook.Model
{
    public class EventsDbManagement
    {
        //Loads the TaskToComplete database and returns it as an observable collection
        public static ObservableCollection<Event> LoadEvents()
        {
            using (IDbConnection con = new SQLiteConnection(LoadConnectiobString()))
            {
                var result = con.Query<Event>("select * from Event", new DynamicParameters());

                return new ObservableCollection<Event>(result as List<Event>);
            }
        }

        //Adds a new Event to the Database
        public static void AddEvent(Event _event)
        {
            using (IDbConnection con = new SQLiteConnection(LoadConnectiobString()))
            {
                con.Execute("Insert into Event(ScheduledDate, Title, Description) values(@ScheduledDate,@Title, @Description)", _event);
            }
        }

        //Updates an Event's details in the database
        public static void UpdateEvent(Event _event)
        {
            using (IDbConnection con = new SQLiteConnection(LoadConnectiobString()))
            {
                con.Execute("Update Event Set ScheduledDate=@ScheduledDate, Title=@Title, Description=@Description Where Id=@Id", _event);
            }
        }

        //Deletes an Event from the database
        public static void DeleteEvent(Event _event)
        {
            using (IDbConnection con = new SQLiteConnection(LoadConnectiobString()))
            {
                con.Execute("Delete from Event Where Id=@Id", _event);
            }
        }

        //Loads the ConnectionString so we can connect to the database
        private static string LoadConnectiobString(string _id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[_id].ConnectionString;
        }

    }
}
