using System;

namespace Portal.Modules.OrientalSails.Domain
{
    public class CustomerService
    {
        public virtual int Id { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ExtraOption Service { get; set; }
        public virtual bool IsExcluded { get; set; }
    }

    public enum ServiceTarget
    {
        All,
        Customer,
        Booking
    }
}
