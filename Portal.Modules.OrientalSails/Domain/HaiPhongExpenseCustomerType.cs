using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.Domain
{
    public class HaiPhongExpenseCustomerType
    {
        public virtual int HaiPhongExpenseCustomerTypeId { get; set; }
        public virtual string Name { get; set; }
        public virtual IList<HaiPhongExpense> HaiPhongExpenses { get; set; }
        public virtual IList<HaiPhongReduceExpense> HaiPhongReduceExpenses { get; set; }
        public virtual IList<HaiPhongExpenseTypeHaiPhongExpenseCustomerType> HaiPhongExpenseTypeHaiPhongExpenseCustomerTypes { get; set; }
    }
}