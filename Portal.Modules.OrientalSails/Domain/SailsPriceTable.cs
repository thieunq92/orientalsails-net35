using System;
using CMS.Core.Domain;
using Portal.Modules.OrientalSails.Web.Util;

namespace Portal.Modules.OrientalSails.Domain
{
    public class SailsPriceTable
    {
        public virtual int Id { get; set; }
        public virtual DateTime? StartDate { get; set; }
        public virtual DateTime? EndDate { get; set; }
        public virtual string Note { get; set; }
        public virtual SailsTrip Trip { get; set; }
        public virtual TripOption TripOption { get; set; }
        public virtual Cruise Cruise { get; set; }
        public virtual Agency Agency { get; set; }
        public virtual Role Role { get; set; }
        public virtual bool IsVND { get; set; }
    }


    public class TripAndOption
    {
        public TripAndOption(SailsTrip trip, TripOption option)
        {
            Trip = trip;
            Option = option;
        }

        public SailsTrip Trip { get; set; }
        public TripOption Option { get; set; }
    }
}
