using NHibernate;
using NHibernate.Criterion;
using Portal.Modules.OrientalSails.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.Repository
{
    public class SeriesRepository : RepositoryBase<Series>
    {
        public SeriesRepository() : base() { }

        public SeriesRepository(ISession session) : base(session) { }


        public Series SeriesGetById(int seriesId)
        {
            return _session.QueryOver<Series>().Where(x => x.Id == seriesId).FutureValue().Value;
        }

        public IList<Series> SeriesBookingsGetByQueryString(string partnerName, string seriesCode, int pageSize, int currentPageIndex, out int count)
        {
            var query = _session.QueryOver<Series>();
            if (!string.IsNullOrEmpty(partnerName))
            { 
                query = query.Where(Restrictions.Like("Agency.Name", partnerName, MatchMode.Anywhere));
            }

            if (!string.IsNullOrEmpty(partnerName))
            {
                query = query.Where(Restrictions.Like("SeriesCode", seriesCode, MatchMode.Anywhere));
            }

            Booking bookingAlias = null;
            query.JoinQueryOver(x => x.ListBooking, () => bookingAlias);
            query = query.Where(x => bookingAlias.Deleted == false)
                .Where(x => bookingAlias.StartDate >= DateTime.Now.Date)
                .Where(x => bookingAlias.Status != Web.Util.StatusType.Cancelled);

            count = query.RowCount();
            return query.Skip(currentPageIndex * pageSize).Take(pageSize).Future().ToList();
        }
    }
}