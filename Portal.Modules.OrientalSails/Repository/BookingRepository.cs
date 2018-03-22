using NHibernate;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Web.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate.Linq;
using NHibernate.Criterion;
using CMS.Modules.User;
using System.Linq.Expressions;
using Portal.Modules.OrientalSails.Utils;
using NHibernate.Transform;

namespace Portal.Modules.OrientalSails.Repository
{
    public class BookingRepository : RepositoryBase<Booking>
    {
        public BookingRepository() : base() { }

        public BookingRepository(ISession session) : base(session) { }

        public int BookingCountByStatusAndDate(Web.Util.StatusType statusType, DateTime date)
        {
            return _session.QueryOver<Booking>()
                .Where(x => x.Deleted == false)
                .Where(x => x.Status == statusType && x.StartDate > date)
                .Select(Projections.RowCount()).FutureValue<int>().Value;
        }

        public int MyBookingPendingCount()
        {
            var userId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
            return _session.QueryOver<Booking>()
                .Where(x => x.Deleted == false)
                .Where(x => x.Status == StatusType.Pending && x.Deadline >= DateTime.Now)
                .Where(x => x.CreatedBy.Id == userId || x.Sale.Id == userId)
                .Select(Projections.RowCount()).FutureValue<int>().Value;
        }

        public int MyTodayBookingPendingCount()
        {
            var userId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
            return _session.QueryOver<Booking>()
                .Where(x => x.Deleted == false)
                .Where(x => x.Status == StatusType.Pending && x.Deadline >= DateTime.Now)
                .Where(x => x.CreatedBy.Id == userId || x.Sale.Id == userId)
                .Where(x => x.Deadline >= DateTime.Now && x.Deadline <= DateTime.Now.AddHours(36))
                .Select(Projections.RowCount()).FutureValue<int>().Value;
        }

        public int SystemBookingPendingCount()
        {
            return _session.QueryOver<Booking>()
                .Where(x => x.Deleted == false)
                .Where(x => x.Status == StatusType.Pending && x.Deadline >= DateTime.Now)
                .Select(Projections.RowCount()).FutureValue<int>().Value;
        }

        public IList<Booking> BookingListBLL_BookingSearchBy(int bookingId, int tripId, int cruiseId, int status,
            DateTime? startDate, string customerName, int agencyId, int batchId,
            int pageSize, int currentPageIndex, out int count)
        {
            var query = QueryOver.Of<Booking>().Where(x => x.Deleted == false);

            if (bookingId > -1)
                query = query.Where(x => x.Id == bookingId);

            SailsTrip sailsTripAlias = null;
            query = query.JoinAlias(x => x.Trip, () => sailsTripAlias);
            if (tripId > -1)
                query = query.Where(x => x.Trip.Id == tripId);

            Cruise cruiseAlias = null;
            query = query.JoinAlias(x => x.Cruise, () => cruiseAlias);
            if (cruiseId > -1)
                query = query.Where(x => x.Cruise.Id == cruiseId);

            if (status > -1)
            {
                query = query.Where(x => x.Status == (StatusType)status);
            }

            if (startDate != null)
            {
                query = query.And(
                    Restrictions.Eq(
                    Projections.SqlFunction("date",
                    NHibernateUtil.Date,
                    Projections.Property<Booking>(x => x.StartDate)
                    ), startDate.Value.Date));
            }

            BookingRoom bookingRoomAlias = null;
            Customer customerAlias = null;
            if (!string.IsNullOrEmpty(customerName))
            {
                query = query
                    .JoinAlias(x => x.BookingRooms, () => bookingRoomAlias)
                    .JoinAlias(() => bookingRoomAlias.Customers, () => customerAlias)
                    .Where(Restrictions.Like("customerAlias.Fullname", customerName, MatchMode.Anywhere));

            }

            Agency agencyAlias = null;
            query = query.JoinAlias(x => x.Agency, () => agencyAlias);

            if (agencyId > -1)
                query = query.Where(x => agencyAlias.Id == agencyId);

            if (batchId > -1)
                query = query.Where(x => x.Batch.Id == batchId);

            query = query.Select(Projections.Distinct(Projections.Property<Booking>(x => x.Id)));

            var mainQuery = _session.QueryOver<Booking>().WithSubquery.WhereProperty(x => x.Id).In(query);
            mainQuery = mainQuery.OrderBy(x => x.StartDate).Desc;
            count = mainQuery.RowCount();
            return mainQuery.Skip(currentPageIndex * pageSize).Take(pageSize).Future<Booking>().ToList();
        }

        public IList<Booking> PaymentReportBLL_BookingSearchBy(DateTime? from, DateTime? to,
            string agencyName, int cruiseId, int tripId, int agencyId, int bookingId, int salesId)
        {

            var query = _session.QueryOver<Booking>().Where(x => x.Deleted == false)
                .And(Restrictions.Or(Restrictions.Where<Booking>(x => x.Status == StatusType.Approved), Restrictions.Where<Booking>(x => x.Status == StatusType.Cancelled && x.CancelPay > 0)));


            if (from != null)
                query = query.Where(x => x.StartDate >= from);

            if (to != null)
                query = query.Where(x => x.StartDate <= to);

            Agency agencyAlias = null;
            query.JoinAlias(x => x.Agency, () => agencyAlias);
            if (!string.IsNullOrEmpty(agencyName))
            {
                query = query.Where(x => agencyAlias.Name.IsLike(agencyName, MatchMode.Anywhere));
            }

            BookingSale bookingSaleAlias = null;
            query.JoinAlias(x => x.BookingSale, () => bookingSaleAlias);
            if (salesId > -1)
            {
                if (salesId > 0)
                    query = query.Where(x => bookingSaleAlias.Sale.Id == salesId);

                if (salesId == 0)
                    query = query.Where(x => agencyAlias.Sale == null);
            }

            if (cruiseId > -1)
            {
                query = query.Where(x => x.Cruise.Id == cruiseId);
            }

            if (bookingId > -1)
            {
                query = query.Where(x => x.Id == bookingId);
            }

            if (tripId > -1)
            {
                query = query.Where(x => x.Trip.Id == tripId);
            }

            if (agencyId > -1)
            {
                query = query.Where(x => x.Agency.Id == agencyId);
            }

            return query.OrderBy(x => x.StartDate).Asc.List<Booking>();
        }

        public Booking BookingGetById(int bookingId)
        {
            return _session.QueryOver<Booking>().Where(x => x.Deleted == false)
                .Where(x => x.Id == bookingId).FutureValue<Booking>().Value;
        }

        public IList<Booking> BookingReportBLL_BookingSearchBy(DateTime? startDate, int cruiseId, int bookingStatus)
        {
            var query = _session.QueryOver<Booking>().Where(x => x.Deleted == false);

            if (cruiseId > -1)
            {
                query = query.Where(x => x.Cruise.Id == cruiseId);
            }

            if (bookingStatus > -1)
            {
                query = query.Where(x => x.Status == (StatusType)bookingStatus);
            }

            if (startDate != null)
            {
                query = query.Where(Restrictions.Eq(
                    Projections.SqlFunction("date",
                    NHibernateUtil.Date,
                    Projections.Property<Booking>(x => x.StartDate)
                    ), startDate.Value.Date));
            }

            return query.Future<Booking>().ToList();
        }
    }
}