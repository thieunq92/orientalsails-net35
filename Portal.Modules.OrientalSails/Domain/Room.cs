using System;
using System.Collections;

namespace Portal.Modules.OrientalSails.Domain
{
    public class Room
    {
        private bool isAvailable = true;
        private string name;
        private IList roomosBookingRooms;

        public Room() { }

        public Room(string name, bool deleted, RoomClass roomClass, RoomTypex roomType)
        {
            this.name = name;
            Deleted = deleted;
            RoomClass = roomClass;
            RoomType = roomType;
        }

        public virtual int Id { get; set; }
        public virtual int Order { get; set; }
        public virtual bool Deleted { get; set; }
        public virtual RoomClass RoomClass { get; set; }
        public virtual RoomTypex RoomType { get; set; }
        public virtual int Adult { get; set; }
        public virtual int Child { get; set; }
        public virtual int Baby { get; set; }
        public virtual Cruise Cruise { get; set; }

        public virtual bool IsAvailable
        {
            get { return isAvailable; }
            set { isAvailable = value; }
        }
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

        public virtual string RoomName
        {
            get { return string.Format("{0} {1}", RoomClass.Name, RoomType.Name); }
        }

        public virtual IList BookingRooms
        {
            get
            {
                if (roomosBookingRooms == null)
                {
                    roomosBookingRooms = new ArrayList();
                }
                return roomosBookingRooms;
            }
            set { roomosBookingRooms = value; }
        }

    }
}