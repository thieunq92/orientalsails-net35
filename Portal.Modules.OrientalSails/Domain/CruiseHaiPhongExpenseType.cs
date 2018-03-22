using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.Domain
{
    public class CruiseHaiPhongExpenseType
    {
        public virtual int CruiseHaiPhongExpenseTypeId { get; set; }
        public virtual Cruise Cruise { get; set; }
        public virtual HaiPhongExpenseType HaiPhongExpenseType { get; set; }
    }
}