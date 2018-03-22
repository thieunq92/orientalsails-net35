using NHibernate;
using Portal.Modules.OrientalSails.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.Repository
{
    public class TripRepository : RepositoryBase<SailsTrip>
    {
        public TripRepository() : base() { }

        public TripRepository(ISession session) : base(session) { }

        public IList<SailsTrip> TripGetAll()
        {
            return _session.QueryOver<SailsTrip>().Where(x => x.Deleted == false).Future<SailsTrip>().ToList();
        }

        public SailsTrip TripGetById(int tripId)
        {
            return _session.QueryOver<SailsTrip>().Where(x => x.Deleted == false)
                .Where(x => x.Id == tripId).FutureValue().Value;
        }
    }
}