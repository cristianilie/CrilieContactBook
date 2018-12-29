using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrilieContactBook.Model
{
    public interface IActivityEntity
    {
        int Id { get; set; }
        string Title { get; set; }
        string Description { get; set; }

    }
}
