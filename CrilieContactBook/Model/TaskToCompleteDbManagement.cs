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
    public class TaskToCompleteDbManagement
    {
        //Loads the TaskToComplete database and returns it as an observable collection
        public static ObservableCollection<TaskToComplete> LoadTasks()
        {
            using (IDbConnection con = new SQLiteConnection(LoadConnectiobString()))
            {
                var result = con.Query<TaskToComplete>("select * from TaskToComplete", new DynamicParameters());

                return new ObservableCollection<TaskToComplete>(result as List<TaskToComplete>);
            }
        }

        //Adds a new TaskToComplete to the Database
        public static void AddTaskToComplete(TaskToComplete _task)
        {
            using (IDbConnection con = new SQLiteConnection(LoadConnectiobString()))
            {
                con.Execute("Insert into TaskToComplete(Name, Description, Importance, Deadline, Completed) values(@Name,@Description, @Importance, @Deadline, @Completed)", _task);
            }
        }

        //Updates a TaskToComplete's details in the database
        public static void UpdateTaskToComplete(TaskToComplete _task)
        {
            using (IDbConnection con = new SQLiteConnection(LoadConnectiobString()))
            {
                con.Execute("Update TaskToComplete Set Name=@Name, Description=@Description, Importance=@Importance, Deadline=@Deadline, Completed=@Completed Where Id=@Id", _task);
            }
        }

        //Deletes a Contact from the database
        public static void DeleteTask(TaskToComplete _task)
        {
            using (IDbConnection con = new SQLiteConnection(LoadConnectiobString()))
            {
                con.Execute("Delete from TaskToComplete Where Id=@Id", _task);
            }
        }

        //Loads the ConnectionString so we can connect to the database
        private static string LoadConnectiobString(string _id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[_id].ConnectionString;
        }
    }
}
