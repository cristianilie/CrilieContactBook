using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrilieContactBook.ViewModels
{
    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum EventListFilter
    {
        [Description("Today")]
        Today,
        [Description("Next 3 days")]
        On_3_Days,
        [Description("Next 7 days")]
        On_7_Days,
        [Description("Next 14 days")]
        On_14_Days,
        [Description("Next 30 days")]
        On_30_Days,
        [Description("All Active")]
        All_Active,
        [Description("Inactive")]
        InactiveEvents
    }
}
