using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.DataTransferObject
{
    public class AvaiableRoomDTO
    {
        public string KindOfRoom { get; set; }

        public RoomClassDTO RoomClass { get; set; }

        public RoomTypeDTO RoomType { get; set; }

        public int NoOfAdult { get; set; }

        public int NoOfChild { get; set; }

        public int NoOfBaby { get; set; }

        public string selectedRoom { get; set; }

        public string selectedChild { get; set; }

        public string selectedBaby { get; set; }


        public class RoomClassDTO
        {
            public int Id { get; set; }

            public string Name { get; set; }
        }

        public class RoomTypeDTO
        {
            public int Id { get; set; }

            public string Name { get; set; }
        }
    }
}