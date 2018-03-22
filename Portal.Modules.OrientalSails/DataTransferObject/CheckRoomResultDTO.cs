using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.DataTransferObject
{
    public class CheckRoomResultDTO
    {
        public CruiseDTO Cruise { get; set; }

        public int NoOfRoomAvaiable { get; set; }

        public string DetailRooms { get; set; }

        public IList<RoomDTO> RoomDTOs { get; set; }

        public IList<KindOfRoomDTO> KindOfRoomDTOs { get; set; }

        public class CruiseDTO
        {
            public int Id { get; set; }

            public string Name { get; set; }
        }

        public class RoomTypeDTO
        {
            public int Id { get; set; }

            public string Name { get; set; }
        }

        public class RoomClassDTO
        {
            public int Id { get; set; }

            public string Name { get; set; }
        }

        public class RoomDTO
        {
            public RoomClassDTO RoomClassDTO { get; set; }

            public RoomTypeDTO RoomTypeDTO { get; set; }
        }

        public class KindOfRoomDTO
        {
            public RoomClassDTO RoomClassDTO { get; set; }

            public RoomTypeDTO RoomTypeDTO { get; set; }
        }
    }
}