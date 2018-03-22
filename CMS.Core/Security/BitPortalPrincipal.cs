using System.Security;
using System.Security.Principal;
using CMS.Core.Domain;

namespace CMS.Core.Security
{
    /// <summary>
    /// Summary description for BitPortalPrincipal.
    /// </summary>
    public class BitPortalPrincipal : IPrincipal
    {
        private readonly User _user;

        /// <summary>
        /// Default constructor. An instance of an authenticated user is required when creating this principal.
        /// </summary>
        /// <param name="user"></param>
        public BitPortalPrincipal(User user)
        {
            if (user != null && user.IsAuthenticated)
            {
                _user = user;
            }
            else
            {
                throw new SecurityException("Cannot create a principal without u valid user");
            }
        }

        #region IPrincipal Members

        /// <summary>
        /// 
        /// </summary>
        public IIdentity Identity
        {
            get { return _user; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public bool IsInRole(string role)
        {
            foreach (Role roleObject in _user.Roles)
            {
                if (roleObject.Name.Equals(role))
                    return true;
            }
            return false;
        }

        #endregion
    }
}