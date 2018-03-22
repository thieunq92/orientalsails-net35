using NHibernate;
using Portal.Modules.OrientalSails.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.Repository
{
    public class RoomTypeRepository:RepositoryBase<RoomTypex>
    {
        
        public RoomTypeRepository() : base() { }

        public RoomTypeRepository(ISession session) : base() { }

        public IList<RoomTypex> RoomTypeGetAll()
        {
            return _session.QueryOver<RoomTypex>().Future().ToList();
        }

        public RoomTypex RoomTypeGetById(int roomTypeId)
        {
            return _session.QueryOver<RoomTypex>().Where(x => x.Id == roomTypeId).FutureValue().Value;
        }
    }
}