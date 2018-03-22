using NHibernate;
using NHibernate.Criterion;
using Portal.Modules.OrientalSails.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate.Linq;

namespace Portal.Modules.OrientalSails.Repository
{
    public class AgencyContactRepository : RepositoryBase<AgencyContact>
    {
        public AgencyContactRepository() : base() { }

        public AgencyContactRepository(ISession session) : base(session) { }

        public int AgencyContactBirthdayCount()
        {
            return _session.Query<AgencyContact>()
                .Where(x => x.Birthday.Value.Day == DateTime.Today.Day && x.Birthday.Value.Month == DateTime.Today.Month)
                .Count();
        }

        public IList<AgencyContact> AgencyContactGetAllByBirthday()
        {
            return _session.Query<AgencyContact>()
               .Where(x => x.Birthday.Value.Day == DateTime.Today.Day && x.Birthday.Value.Month == DateTime.Today.Month)
               .ToList();
        }

        public IList<AgencyContact> AgencyContactGetAllByAgency(int agencyId)
        {
            return _session.QueryOver<AgencyContact>().Where(x => x.Agency.Id == agencyId).Future().ToList();
        }

        public AgencyContact AgencyContactGetById(int bookerId)
        {
            return _session.QueryOver<AgencyContact>().Where(x => x.Id == bookerId).FutureValue().Value;
        }
    }
}