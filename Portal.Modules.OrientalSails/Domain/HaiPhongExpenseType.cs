using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.Domain
{
    public class HaiPhongExpenseType
    {
        public virtual int HaiPhongExpenseTypeId { get; set; }
        public virtual string Name { get; set; }
        public virtual IList<HaiPhongExpense> HaiPhongExpense { get; set; }
        public virtual IList<HaiPhongExpenseTypeHaiPhongExpenseCustomerType> HaiPhongExpenseTypeHaiPhongExpenseCustomerTypes { get; set; }
        public virtual IList<HaiPhongExpenseCharter> HaiPhongExpenseCharter { get; set; }
        public virtual IList<CruiseHaiPhongExpenseType> CruiseHaiPhongExpenseTypes { get; set; }
    }
}