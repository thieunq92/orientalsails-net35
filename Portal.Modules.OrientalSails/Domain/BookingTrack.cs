using System;
using System.Collections;
using CMS.Core.Domain;

namespace Portal.Modules.OrientalSails.Domain
{
    public class BookingTrack
    {
        private IList changes;

        public virtual int Id { get; set; }
        public virtual Booking Booking { get; set; }
        public virtual DateTime ModifiedDate { get; set; }
        public virtual User User { get; set; }

        public virtual IList Changes
        {
            get
            {
                if (changes == null)
                {
                    changes = new ArrayList();
                }
                return changes;
            }
            set { changes = value; }
        }
    }
}
