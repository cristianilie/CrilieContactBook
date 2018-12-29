using CrilieContactBook.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrilieContactBook.Model
{
    public class TaskToComplete : I_DB_Query, IActivityEntity
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int Importance { get; set; }

        public DateTime Deadline { get; set; }

        public bool Completed { get; set; }


        public string AddQuery { get; set; } = "Insert into TaskToComplete(Title, Description, Importance, Deadline, Completed) values(@Title,@Description, @Importance, @Deadline, @Completed)";

        public string EditQuery { get; set; } = "Update TaskToComplete Set Title=@Title, Description=@Description, Importance=@Importance, Deadline=@Deadline, Completed=@Completed Where Id=@Id";
    }
}
