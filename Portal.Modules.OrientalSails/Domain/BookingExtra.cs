
using System;
using System.Collections;
using System.Web.UI.WebControls;

namespace Portal.Modules.OrientalSails.Domain
{
    public class BookingExtra
    {
        public BookingExtra() { }

        public BookingExtra(Booking booking, ExtraOption extraOption)
        {
            Booking = booking;
            ExtraOption = extraOption;
        }

        public virtual int Id { get; set; }
        public virtual Booking Booking { get; set; }
        public virtual ExtraOption ExtraOption { get; set; }
    }
}
