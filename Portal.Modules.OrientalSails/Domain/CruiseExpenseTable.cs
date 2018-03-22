using System;
using System.Collections;

namespace Portal.Modules.OrientalSails.Domain
{

    public class CruiseExpenseTable
    {
        private IList expenses;

        public virtual int Id { get; set; }
        public virtual DateTime ValidFrom { get; set; }
        public virtual DateTime? ValidTo { get; set; }
        public virtual Cruise Cruise { get; set; }

        public virtual IList Expenses
        {
            get
            {
                if (expenses == null)
                {
                    expenses = new ArrayList();
                }
                return expenses;
            }
            set
            {
                expenses = value;
            }
        }
    }
}