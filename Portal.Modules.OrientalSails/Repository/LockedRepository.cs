using NHibernate;
using Portal.Modules.OrientalSails.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.Repository
{
    public class LockedRepository : RepositoryBase<Locked>
    {
        public LockedRepository() : base() { }

        public LockedRepository(ISession session) : base(session) { }

        public IList<Locked> LockedGetBy(DateTime? startDate, DateTime? endDate, int cruiseId)
        {
            var query = _session.QueryOver<Locked>();

            if (startDate != null)
                query = query.Where(x => x.Start >= startDate);

            if (endDate != null)
                query = query.Where(x => x.Start <= endDate.Value.AddDays(-1));

            if (cruiseId > -1)
                query = query.Where(x => x.Cruise.Id == cruiseId);

            return query.Future().ToList();
        }
    }
}