using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.BusinessLogic
{
    public class SeriesManagerBLL
    {
        public SeriesRepository SeriesRepository { get; set; }
        public BookingRepository BookingRepository { get; set; }

        public SeriesManagerBLL()
        {
            SeriesRepository = new SeriesRepository();
            BookingRepository = new BookingRepository();
        }

        public void Dispose()
        {
            if (SeriesRepository != null)
            {
                SeriesRepository.Dispose();
                SeriesRepository = null;
            }
            if (BookingRepository != null)
            {
                BookingRepository.Dispose();
                BookingRepository = null;
            }
        }

        public IList<Series> SeriesBookingGetByQueryString(System.Collections.Specialized.NameValueCollection nvcQueryString, int pageSize, int currentPageIndex, out int count)
        {
            string partnerName = "";
            if (nvcQueryString["p"] != null)
                partnerName = nvcQueryString["p"];

            string seriesCode = "";
            if (nvcQueryString["sc"] != null)
                partnerName = nvcQueryString["sc"];

            return SeriesRepository.SeriesBookingsGetByQueryString(partnerName, seriesCode, pageSize, currentPageIndex, out count);
        }

        public Series SeriesGetById(int seriesId)
        {
            return SeriesRepository.SeriesGetById(seriesId);
        }

        public void BookingSaveOrUpdate(Booking booking)
        {
            SeriesRepository.CloseSession();
            BookingRepository.SaveOrUpdate(booking);
        }
    }
}