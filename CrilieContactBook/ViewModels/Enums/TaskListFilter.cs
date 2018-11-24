using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrilieContactBook.ViewModels
{
    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum TaskListFilter
    {
        [Description("All Tasks")]
        All,
        [Description("Active Tasks")]
        Active,
        [Description("Completed Tasks")]
        Completed,
        [Description("Failed Tasks")]
        Failed
    }
}
