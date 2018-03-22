using System;
using System.Collections;

namespace Portal.Modules.OrientalSails.Domain
{
    public class RoomClass
    {
        protected string description;
        protected string name;
        protected IList roomClassosBookingRooms;
        protected IList roomClassosRooms;
        protected IList roomClassosSailsPriceConfigs;

        public virtual int Id { get; set; }
        public virtual int Order { get; set; }
        public virtual Cruise Cruise { get; set; }
        public virtual string Name
        {
            get { return name; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for Name", value, value);
                name = value;
            }
        }

        public virtual string Description
        {
            get { return description; }
            set
            {
                if (value != null && value.Length > 250)
                    throw new ArgumentOutOfRangeException("Invalid value for Name", value, value);
                description = value;
            }
        }

        public virtual IList BookingRooms
        {
            get
            {
                if (roomClassosBookingRooms == null)
                {
                    roomClassosBookingRooms = new ArrayList();
                }
                return roomClassosBookingRooms;
            }
            set { roomClassosBookingRooms = value; }
        }

        public virtual IList Rooms
        {
            get
            {
                if (roomClassosRooms == null)
                {
                    roomClassosRooms = new ArrayList();
                }
                return roomClassosRooms;
            }
            set { roomClassosRooms = value; }
        }

        public virtual IList SailsPriceConfigs
        {
            get
            {
                if (roomClassosSailsPriceConfigs == null)
                {
                    roomClassosSailsPriceConfigs = new ArrayList();
                }
                return roomClassosSailsPriceConfigs;
            }
            set { roomClassosSailsPriceConfigs = value; }
        }
    }
}