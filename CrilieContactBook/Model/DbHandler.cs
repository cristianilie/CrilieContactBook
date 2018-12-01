using CrilieContactBook.ViewModels;
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
    public class DbHandler<T> where T : I_DB_Query
    {
        //Type of generic type T
        public static Type typeParameterType = typeof(T);


        //Loads the T database table and returns it as an observable collection
        public static ObservableCollection<T> LoadElements()
        {

            using (IDbConnection con = new SQLiteConnection(LoadConnectiobString()))
            {
                var result = con.Query<T>($"select * from {typeParameterType.Name.ToString()}", new DynamicParameters());

                return new ObservableCollection<T>(result as List<T>);
            }
        }

        //Adds a new "Entity" to the asociated Database table
        public static void AddItem(T _t)
        {
            using (IDbConnection con = new SQLiteConnection(LoadConnectiobString()))
            {
                con.Execute(_t.AddQuery, _t);
            }
        }

        //Updates an "Entity"'s details in the asociated Database table
        public static void UpdateItem(T _t)
        {
            using (IDbConnection con = new SQLiteConnection(LoadConnectiobString()))
            {
                con.Execute(_t.EditQuery, _t);
            }
        }

        //Deletes an "Entity" from the asociated Database table
        public static void DeleteItem(T _t)
        {
            using (IDbConnection con = new SQLiteConnection(LoadConnectiobString()))
            {
                con.Execute($"Delete from {typeParameterType.Name.ToString()} Where Id=@Id", _t);
            }
        }

        //Loads the ConnectionString so we can connect to the database
        private static string LoadConnectiobString(string _id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[_id].ConnectionString;
        }
    }
}
