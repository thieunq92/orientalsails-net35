using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Repository;
using Portal.Modules.OrientalSails.Web.Util;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Portal.Modules.OrientalSails.BusinessLogic
{
    public class BookingListBLL
    {
        public BookingRepository BookingRepository { get; set; }
        public CruiseRepository CruiseRepository { get; set; }
        public TripRepository TripRepository { get; set; }
        public CustomerRepository CustomerRepository { get; set; }

        public BookingListBLL()
        {
            BookingRepository = new BookingRepository();
            CruiseRepository = new CruiseRepository();
            TripRepository = new TripRepository();
            CustomerRepository = new CustomerRepository();
        }

        public void Dispose()
        {
            if (BookingRepository != null)
            {
                BookingRepository.Dispose();
                BookingRepository = null;
            }
            if (CruiseRepository != null)
            {
                CruiseRepository.Dispose();
                CruiseRepository = null;
            }
            if (TripRepository != null)
            {
                TripRepository.Dispose();
                TripRepository = null;
            }
            if (CustomerRepository != null)
            {
                CustomerRepository.Dispose();
                CustomerRepository = null;
            }
        }

        public int BookingCountByStatusAndDate(StatusType statusType, DateTime date)
        {
            return BookingRepository.BookingCountByStatusAndDate(statusType, date);
        }

        public IList<Cruise> CruiseGetAll()
        {
            return CruiseRepository.CruiseGetAll();
        }

        public IList<SailsTrip> TripGetAll()
        {
            return TripRepository.TripGetAll();
        }


        public IList<Booking> BookingGetByQueryString(NameValueCollection nvcQueryString, int pageSize, int currentPageIndex, out int count)
        {
            var bookingId = -1;
            try
            {
                if (nvcQueryString["bi"] != null)
                {
                    var resultString = Regex.Match(nvcQueryString["bi"], @"\d+").Value;
                    bookingId = Convert.ToInt32(resultString);
                }
            }
            catch { }

            var tripId = -1;
            try
            {
                if (nvcQueryString["ti"] != null)
                    tripId = Convert.ToInt32(nvcQueryString["ti"]);
            }
            catch { }

            var cruiseId = -1;
            try
            {
                if (nvcQueryString["ci"] != null)
                    cruiseId = Convert.ToInt32(nvcQueryString["ci"]);
            }
            catch { }

            DateTime? startDate = null;
            try
            {
                if (nvcQueryString["sd"] != null)
                    startDate = DateTime.ParseExact(nvcQueryString["sd"], "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch { }


            int status = -1;
            try
            {
                if (nvcQueryString["s"] != null)
                {
                    status = Convert.ToInt32(nvcQueryString["s"]);
                }
            }
            catch { }

            int agencyId = -1;
            try
            {
                if (nvcQueryString["ai"] != null)
                    agencyId = Convert.ToInt32(nvcQueryString["ai"]);
            }
            catch { }

            string customerName = "";
            if (nvcQueryString["cn"] != null)
                customerName = nvcQueryString["cn"];

            int batchId = -1;
            try
            {
                if (nvcQueryString["batchid"] != null)
                    batchId = Convert.ToInt32(nvcQueryString["batchid"]);
            }
            catch { }

            return BookingRepository.BookingListBLL_BookingSearchBy(bookingId, tripId, cruiseId, status, startDate, customerName, agencyId
                , batchId, pageSize, currentPageIndex, out count);
        }

        public string CustomerGetNameByBookingId(int bookingId)
        {
            return CustomerRepository.CustomerGetNameByBookingId(bookingId);
        }

        public int CustomerCountPaxByBookingId(int bookingId)
        {
            return CustomerRepository.CustomerCountPaxByBookingId(bookingId);
        }

    }
}