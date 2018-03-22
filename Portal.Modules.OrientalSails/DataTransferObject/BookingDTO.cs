using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.DataTransferObject
{
    public class BookingDTO
    {
        public string BookingId { get; set; }

        public string StartDate { get; set; }

        public string TripName { get; set; }

        public string CruiseName { get; set; }

        public int NoOfRoom { get; set; }

        public int NoOfPax { get; set; }

        public string Room{get;set;}

        public string Pax { get; set; }
    }
}