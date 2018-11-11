using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrilieContactBook.Model
{
    public class Event
    {
        public int Id { get; set; }

        public DateTime ScheduledDate { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public bool Finished { get; set; }
    }
}
