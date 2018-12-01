using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrilieContactBook.Model
{
    public class Event : I_DB_Query
    {
        public int Id { get; set; }

        public DateTime ScheduledDate { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public bool Finished { get; set; }


        public string AddQuery { get; set; } = "Insert into Event(ScheduledDate, Title, Description, Finished) values(@ScheduledDate,@Title, @Description,@Finished)";

        public string EditQuery { get; set; } = "Update Event Set ScheduledDate=@ScheduledDate, Title=@Title, Description=@Description, Finished=@Finished Where Id=@Id";
    }
}
