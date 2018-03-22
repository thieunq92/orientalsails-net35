using NHibernate;
using Portal.Modules.OrientalSails.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate.Criterion;
using NHibernate.Linq;

namespace Portal.Modules.OrientalSails.Repository
{
    public class CruiseRepository : RepositoryBase<Cruise>
    {
        public CruiseRepository() : base() { }

        public CruiseRepository(ISession session) : base(session) { }

        public IList<Cruise> CruiseGetAll()
        {
            return _session.QueryOver<Cruise>().Where(x => x.Deleted == false).Future<Cruise>().ToList();
        }

        public Cruise CruiseGeById(int cruiseId)
        {
            return _session.QueryOver<Cruise>().Where(x => x.Deleted == false)
                .Where(x => x.Id == cruiseId).FutureValue().Value;
        }

        public IList<Cruise> CruiseGetAllByTrip(SailsTrip trip)
        {
            return _session.Query<Cruise>().Where(x => x.Deleted == false)
                .Where(x => x.Trips.Contains(trip)).ToFuture().ToList();
        }

        public Cruise CruiseGetById(int cruiseId)
        {
            return _session.QueryOver<Cruise>().Where(x => x.Deleted == false)
                .Where(x => x.Id == cruiseId)
                .FutureValue()
                .Value;
        }
    }
}