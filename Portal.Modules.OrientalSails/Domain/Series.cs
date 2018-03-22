using System;
using System.Collections;
using CMS.Core.Domain;
using System.Collections.Generic;

namespace Portal.Modules.OrientalSails.Domain
{
    public class Series
    {
        public virtual int Id { get; set; }
        public virtual string SeriesCode { get; set; }
        public virtual DateTime? CutoffDate { get; set; }
        public virtual int NoOfDays { get; set; }
        public virtual Agency Agency { get; set; }
        public virtual AgencyContact Booker { get; set; }
        public virtual IList<Booking> ListBooking { get; set; }
    }
}