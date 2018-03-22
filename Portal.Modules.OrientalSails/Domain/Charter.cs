using System;
using System.Collections.Generic;
using System.Web;
using NHibernate.Mapping;
using Portal.Modules.OrientalSails.Web.Util;

namespace Portal.Modules.OrientalSails.Domain
{
    public class Charter
    {
        public virtual int Id { set; get; }
        public virtual int RoomFrom { set; get; }
        public virtual int RoomTo { set; get; }
        public virtual double PriceUSD { set; get; }
        public virtual double PriceVND { set; get; }
        public virtual TripOption TripOption { set; get; }
        public virtual SailsPriceTable SailsPriceTable { set; get; }
        public virtual Cruise Cruise { set; get; }
        public virtual SailsTrip Trip { set; get; }
    }
}