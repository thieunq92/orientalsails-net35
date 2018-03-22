using System;

namespace Portal.Modules.OrientalSails.Domain
{
    public class BookingChanged
    {
        public virtual int Id { get; set; }
        public virtual BookingAction Action { get; set; }
        public virtual string Parameter { get; set; }
        public virtual BookingTrack Track { get; set; }
    }

    public enum BookingAction
    {
        RemoveRoom,
        Created,
        Approved,
        Cancelled,
        AddRoom,
        ChangeRoomType,
        ChangeRoomNumber,
        AddCustomer,
        RemoveCustomer,
        ChangeTrip,
        ChangeDate,
        ChangeTotal,
        Transfer,
        Untransfer,
        ChangeTransfer
    }

    public class BookingActionClass : NHibernate.Type.EnumStringType
    {
        public BookingActionClass()
            : base(typeof(BookingAction), 30)
        {

        }
    }
}
