using NHibernate;
using Portal.Modules.OrientalSails.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.Repository
{
    public class AgencyRepository : RepositoryBase<Agency>
    {
        public AgencyRepository() : base() { }

        public AgencyRepository(ISession session) : base(session) { }

        public IList<Agency> AgencyGetAll()
        {
            return _session.QueryOver<Agency>().Where(x => x.Deleted == false).Future().ToList();
        }

        public Agency AgencyGetById(int agencyId)
        {
            return _session.QueryOver<Agency>().Where(x => x.Deleted == false)
                .Where(x => x.Id == agencyId).FutureValue().Value;
        }
    }
}