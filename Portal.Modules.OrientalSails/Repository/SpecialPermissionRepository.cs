using CMS.Core.Domain;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate.Criterion;

namespace Portal.Modules.OrientalSails.Repository
{
    public class SpecialPermissionRepository : RepositoryBase<SpecialPermission>
    {
        public SpecialPermissionRepository() : base() { }

        public SpecialPermissionRepository(ISession session) : base(session) { }

        public SpecialPermission SpecialPermissionGetByUserIdAndPermissionName(int userId, string permissionName)
        {
            var user = _session.QueryOver<User>()
                .Where(x => x.IsActive == true)
                .Where(x => x.Id == userId)
                .SingleOrDefault();

            return _session.QueryOver<SpecialPermission>()
                .Where(x => x.Name == permissionName)
                .Where(
                Restrictions.Or(
                Restrictions.Where<SpecialPermission>(x => x.User.Id == userId),
                Restrictions.On<SpecialPermission>(x => x.Role)
                .IsIn(user.Roles)
                )
                )
                .Take(1).SingleOrDefault();
        }
    }
}