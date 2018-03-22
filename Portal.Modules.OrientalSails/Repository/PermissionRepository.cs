using CMS.Core.Domain;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.Repository
{
    public class PermissionRepository : RepositoryBase<ModulePermission>
    {
        public PermissionRepository() { }

        public PermissionRepository(ISession session) : base(session) { }


        public ModulePermission PermissionGetById(int permissionId)
        {
            return _session.QueryOver<ModulePermission>()
                .Where(x => x.Id == permissionId).SingleOrDefault();
        }
    }
}