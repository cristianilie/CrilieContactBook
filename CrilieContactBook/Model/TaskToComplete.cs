using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrilieContactBook.Model
{
    public class TaskToComplete
    {
        public int Id { get; set; }

        public string TaskName { get; set; }

        public int Importance { get; set; }

        public DateTime Deadline { get; set; }
    }
}
