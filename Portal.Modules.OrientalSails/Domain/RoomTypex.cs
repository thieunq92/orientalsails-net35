using System;
using System.Collections;

namespace Portal.Modules.OrientalSails.Domain
{
    public class RoomTypex
    {
        private string name;
        private IList roomTypeosBookingRooms;
        private IList roomTypeosRooms;
        private IList roomTypeosSailsPriceConfigs;

        public virtual int Id { get; set; }
        public virtual int Capacity { get; set; }
        public virtual int Order { get; set; }
        public virtual bool IsShared { get; set; }
        public virtual bool AllowSingBook { get; set; }

        public virtual string Name
        {
            get { return name; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for Name", value, value);
                name = value;
            }
        }

        public virtual IList BookingRooms
        {
            get
            {
                if (roomTypeosBookingRooms == null)
                {
                    roomTypeosBookingRooms = new ArrayList();
                }
                return roomTypeosBookingRooms;
            }
            set { roomTypeosBookingRooms = value; }
        }

        public virtual IList Rooms
        {
            get
            {
                if (roomTypeosRooms == null)
                {
                    roomTypeosRooms = new ArrayList();
                }
                return roomTypeosRooms;
            }
            set { roomTypeosRooms = value; }
        }

        public virtual IList SailsPriceConfigs
        {
            get
            {
                if (roomTypeosSailsPriceConfigs == null)
                {
                    roomTypeosSailsPriceConfigs = new ArrayList();
                }
                return roomTypeosSailsPriceConfigs;
            }
            set { roomTypeosSailsPriceConfigs = value; }
        }
    }
}