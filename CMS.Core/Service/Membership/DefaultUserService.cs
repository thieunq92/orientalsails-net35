using System;
using System.Collections;
using CMS.Core.DataAccess;
using CMS.Core.Domain;

namespace CMS.Core.Service.Membership
{
    /// <summary>
    /// Provides functionality for user management based on Cuyahoga's internal database.
    /// </summary>
    public class DefaultUserService : IUserService
    {
        private readonly ICommonDao _commonDao;
        private readonly IUserDao _userDao;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="userDao"></param>
        public DefaultUserService(IUserDao userDao, ICommonDao commonDao)
        {
            _userDao = userDao;
            _commonDao = commonDao;
        }

        #region IUserService Members

        public IList FindUsersByUsername(string searchString)
        {
            return _userDao.FindUsersByUsername(searchString);
        }

        public User GetUserById(int userId)
        {
            return _commonDao.GetObjectById(typeof (User), userId, true) as User;
        }

        public User GetUserByUsernameAndEmail(string username, string email)
        {
            return _userDao.GetUserByUsernameAndEmail(username, email);
        }

        public string CreateUser(string username, string email, Site currentSite)
        {
            User user = new User();
            user.UserName = username;
            user.Email = email;
            user.IsActive = true;
            string newPassword = user.GeneratePassword();
            // Add the default role from the current site.
            user.Roles.Add(currentSite.DefaultRole);
            _commonDao.SaveOrUpdateObject(user);

            return newPassword;
        }

        public void UpdateUser(User user)
        {
            _commonDao.SaveOrUpdateObject(user);
        }

        public void DeleteUser(User user)
        {
            _commonDao.DeleteObject(user);
        }

        public string ResetPassword(string username, string email)
        {
            User user = _userDao.GetUserByUsernameAndEmail(username, email);
            if (user == null)
            {
                throw new NullReferenceException("No user found with the given username and email");
            }
            string newPassword = user.GeneratePassword();
            _userDao.SaveOrUpdateUser(user);
            return newPassword;
        }

        public IList GetAllRoles()
        {
            return _commonDao.GetAll(typeof (Role));
        }

        public Role GetRoleById(int roleId)
        {
            return (Role) _commonDao.GetObjectById(typeof (Role), roleId);
        }

        #endregion
    }
}