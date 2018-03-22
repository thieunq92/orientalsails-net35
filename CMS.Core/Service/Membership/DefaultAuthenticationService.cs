using System;
using CMS.Core.DataAccess;
using CMS.Core.Domain;

namespace CMS.Core.Service.Membership
{
    /// <summary>
    /// Provides authentication functionality based on Cuyahoga's internal database.
    /// </summary>
    public class DefaultAuthenticationService : IAuthenticationService
    {
        private readonly IUserDao _userDao;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="userDao"></param>
        public DefaultAuthenticationService(IUserDao userDao)
        {
            _userDao = userDao;
        }

        #region IAuthenticationService Members

        public User AuthenticateUser(string username, string password, string ipAddress)
        {
            string hashedPassword = User.HashPassword(password);
            User user = _userDao.GetUserByUsernameAndPassword(username, hashedPassword);
            if (user != null)
            {
                user.LastIp = ipAddress;
                user.LastLogin = DateTime.Now;
                _userDao.SaveOrUpdateUser(user);
            }
            return user;
        }

        #endregion
    }
}