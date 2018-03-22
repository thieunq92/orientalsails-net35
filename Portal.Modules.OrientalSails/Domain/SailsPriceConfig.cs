using Portal.Modules.OrientalSails.Web.Util;

namespace Portal.Modules.OrientalSails.Domain
{
    public class SailsPriceConfig
    {
        public virtual int Id { get; set; }
        public virtual TripOption TripOption { get; set; }
        public virtual double NetPrice { get; set; }
        public virtual double SpecialPrice { get; set; }
        public virtual RoomClass RoomClass { get; set; }
        public virtual RoomTypex RoomType { get; set; }
        public virtual SailsTrip Trip { get; set; }
        public virtual SailsPriceTable Table { get; set; }
        public virtual Cruise Cruise { get; set; }
        public virtual double NetPriceVND { set; get; }
        public virtual double SpecialPriceVND { set; get; }
    }
}