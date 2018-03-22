using System;
using System.Collections;
using CMS.Core.Domain;
using System.Collections.Generic;

namespace Portal.Modules.OrientalSails.Domain
{
    public class Agency
    {
        private IList users;
        private IList bookings;
        protected IList history;
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Phone { get; set; }
        public virtual string Address { get; set; }
        public virtual string Contract { get; set; }
        public virtual string Description { get; set; }
        public virtual int ContractStatus { get; set; }
        public virtual string TaxCode { get; set; }
        public virtual string Email { get; set; }
        public virtual string Fax { get; set; }
        public virtual Role Role { get; set; }

        public virtual IList Users
        {
            get
            {
                if (users == null)
                {
                    users = new ArrayList();
                }
                return users;
            }
            set { users = value; }
        }

        public virtual IList History
        {
            get
            {
                if (history == null)
                {
                    history = new ArrayList();
                }
                return history;
            }
            set { history = value; }
        }

        public virtual IList Bookings
        {
            get
            {
                if (bookings == null)
                {
                    bookings = new ArrayList();
                }
                return bookings;
            }
            set { bookings = value; }
        }

        public virtual User Sale { get; set; }
        public virtual string Accountant { get; set; }
        public virtual PaymentPeriod PaymentPeriod { get; set; }
        public virtual DateTime LastBooking { get; set; }
        public virtual DateTime? CreatedDate { get; set; }
        public virtual DateTime? ModifiedDate { get; set; }
        public virtual User CreatedBy { get; set; }
        public virtual User ModifiedBy { get; set; }
        public virtual bool Deleted { get; set; }
        public virtual DateTime? SaleStart { get; set; }
        public virtual AgencyLocation Location { get; set; }
        public virtual IList<Series> ListSeries { get; set; }
    }

    public enum PaymentPeriod
    {
        None,
        Monthly,
        MonthlyCK
    }

    public class PaymentPeriodClass : NHibernate.Type.EnumStringType
    {
        public PaymentPeriodClass()
            : base(typeof(PaymentPeriod), 20)
        {

        }
    }
}
