using NHibernate;
using Portal.Modules.OrientalSails.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.Repository
{
    public class BookingHistoryRepository : RepositoryBase<BookingHistory>
    {
        public BookingHistoryRepository() : base() { }

        public BookingHistoryRepository(ISession session) : base(session) { }


        public IList<BookingHistory> BookingHistoryGetByBookingId(int bookingId)
        {
            return _session.QueryOver<BookingHistory>().Where(x => x.Booking.Id == bookingId).Future().ToList();
        }
    }
}