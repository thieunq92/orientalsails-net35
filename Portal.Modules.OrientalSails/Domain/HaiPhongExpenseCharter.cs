using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.Domain
{
    public class HaiPhongExpenseCharter
    {
        public virtual int HaiPhongExpenseCharterId { get; set; }
        public virtual String Name { get; set; }
        public virtual Cruise Cruise { get; set; }
        public virtual int Value { get; set; }
        public virtual HaiPhongExpenseType HaiPhongExpenseType { get; set; }
    }
}