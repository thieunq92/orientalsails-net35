using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Repository;
using Portal.Modules.OrientalSails.Utils;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Portal.Modules.OrientalSails.BusinessLogic
{
    public class PaymentReportBLL
    {
        public TripRepository TripRepository { get; set; }
        public BookingRepository BookingRepository { get; set; }
        public CustomerRepository CustomerRepository { get; set; }

        public PaymentReportBLL()
        {
            TripRepository = new TripRepository();
            BookingRepository = new BookingRepository();
            CustomerRepository = new CustomerRepository();
        }

        public void Dispose()
        {
            if (TripRepository != null)
            {
                TripRepository.Dispose();
                TripRepository = null;
            }

            if (BookingRepository != null)
            {
                BookingRepository.Dispose();
                BookingRepository = null;
            }

            if (CustomerRepository != null)
            {
                CustomerRepository.Dispose();
                CustomerRepository = null;
            }
        }

        public IList<SailsTrip> TripGetAll()
        {
            return TripRepository.TripGetAll();
        }



        public IList<Booking> BookingGetByRequestString(NameValueCollection nvcQueryString)
        {
            DateTime? from = DateTimeUtil.DateGetDefaultFromDate();
            try
            {
                if (nvcQueryString["f"] != null)
                    from = DateTime.ParseExact(nvcQueryString["f"], "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch (Exception) { }

            DateTime? to = DateTimeUtil.DateGetDefaultToDate();
            try
            {
                if (nvcQueryString["t"] != null)
                    to = DateTime.ParseExact(nvcQueryString["t"], "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch (Exception) { }

            var agencyName = "";
            try
            {
                if (nvcQueryString["an"] != null)
                    agencyName = nvcQueryString["an"];
            }
            catch (Exception) { }

            var cruiseId = -1;
            try
            {
                if (nvcQueryString["ci"] != null)
                    cruiseId = Convert.ToInt32(nvcQueryString["ci"]);
            }
            catch (Exception) { }

            var tripId = -1;
            try
            {
                if (nvcQueryString["ti"] != null)
                    tripId = Convert.ToInt32(nvcQueryString["ti"]);
            }
            catch (Exception) { }

            var agencyId = -1;
            try
            {
                if (nvcQueryString["ai"] != null)
                    agencyId = Convert.ToInt32(nvcQueryString["ai"]);
            }
            catch (Exception) { }

            var bookingId = -1;
            try
            {
                if (nvcQueryString["bi"] != null)
                {
                    var resultString = Regex.Match(nvcQueryString["bi"], @"\d+").Value;
                    bookingId = Convert.ToInt32(resultString);
                }
            }
            catch (Exception) { }

            var salesId = -1;
            try
            {
                if (nvcQueryString["si"] != null)
                    salesId = Convert.ToInt32(nvcQueryString["si"]);
            }
            catch (Exception) { }

            return BookingRepository.PaymentReportBLL_BookingSearchBy(from, to, agencyName, cruiseId, tripId, agencyId, bookingId, salesId);
        }

        public int CustomerCountAdult(int bookingId)
        {
            return CustomerRepository.CustomerCountAdult(bookingId);
        }
    }
}