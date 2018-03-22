using System;
using System.Collections;
using System.Web.UI.WebControls;

namespace Portal.Modules.OrientalSails.Domain
{
    public class BookingService
    {
        public virtual int Id { get; set; }
        public virtual Booking Booking { get; set; }
        public virtual string Name { get; set; }
        public virtual double UnitPrice { get; set; }
        public virtual int Quantity { get; set; }
        public virtual bool IsByCustomer { get; set; }
        public virtual bool Deleted { get; set; }

    }
}
