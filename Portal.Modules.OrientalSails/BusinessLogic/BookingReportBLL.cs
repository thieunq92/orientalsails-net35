using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.BusinessLogic
{
    public class BookingReportBLL
    {
        public BookingRepository BookingRepository { get; set; }
        public CruiseRepository CruiseRepository { get; set; }

        public BookingReportBLL() {
            BookingRepository = new BookingRepository();
            CruiseRepository = new CruiseRepository();
        }

        public IList<Booking> BookingReportBLL_BookingSearchBy(DateTime startDate, int cruiseId, int bookingStatus)
        {
           return BookingRepository.BookingReportBLL_BookingSearchBy(startDate, cruiseId, bookingStatus);
        }

        public Cruise CruiseGetById(int cruiseId)
        {
            return CruiseRepository.CruiseGeById(cruiseId);
        }
    }
}