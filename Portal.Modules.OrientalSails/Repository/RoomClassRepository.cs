using NHibernate;
using Portal.Modules.OrientalSails.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.Repository
{
    public class RoomClassRepository : RepositoryBase<RoomClass>
    {
        public RoomClassRepository() : base() { }

        public RoomClassRepository(ISession session) : base() { }

        public IList<RoomClass> RoomClassGetAll()
        {
            return _session.QueryOver<RoomClass>().Future().ToList();
        }

        public RoomClass RoomClassById(int roomClassId)
        {
            return _session.QueryOver<RoomClass>().Where(x => x.Id == roomClassId).FutureValue().Value;
        }
    }
}