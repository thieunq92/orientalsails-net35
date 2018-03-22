using System;
using System.Collections;

namespace Portal.Modules.OrientalSails.Domain
{
    public class CruiseExpense : IComparable
    {
        public virtual int Id { get; set; }
        public virtual int CustomerFrom { get; set; }
        public virtual int CustomerTo { get; set; }
        public virtual double Price { get; set; }
        public virtual int Currency { get; set; }
        public virtual CruiseExpenseTable Table { get; set; }


        public virtual int CompareTo(object obj)
        {
            return CustomerFrom - ((CruiseExpense)obj).CustomerFrom;
        }
    }
}
