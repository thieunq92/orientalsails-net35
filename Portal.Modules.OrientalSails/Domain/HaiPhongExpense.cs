using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.Domain
{
    public class HaiPhongExpense
    {
        public virtual int HaiPhongExpenseId { get; set; }

        public virtual string Name { get; set; }

        public virtual HaiPhongExpenseCustomerType HaiPhongExpenseCustomerType { get; set; }

        public virtual HaiPhongExpenseType HaiPhongExpenseType { get; set; }

        public virtual HaiPhongReduceExpense HaiPhongReduceExpense { get; set; }

        public virtual HaiPhongExpenseTypeHaiPhongExpenseCustomerType HaiPhongExpenseTypeHaiPhongExpenseCustomerType { get; set; }
    }
}