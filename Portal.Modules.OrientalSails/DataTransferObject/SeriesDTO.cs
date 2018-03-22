using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.DataTransferObject
{
    public class SeriesDTO
    {
        public int Id { get; set; }

        public string SeriesCode { get; set; }

        public string CutoffDate { get; set; }

        public int NoOfDays { get; set; }

        public AgencyDTO Agency { get; set; }

        public BookerDTO Booker { get; set; }

        public class AgencyDTO
        {
            public int Id { get; set; }

            public string Name { get; set; }
        }

        public class BookerDTO
        {
            public int Id { get; set; }

            public string Name { get; set; }
        }
    }
}