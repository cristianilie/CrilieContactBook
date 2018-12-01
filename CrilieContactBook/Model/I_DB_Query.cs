using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrilieContactBook.Model
{
    public interface I_DB_Query
    {
        string AddQuery { get; set; }
        string EditQuery { get; set; }
    }
}
