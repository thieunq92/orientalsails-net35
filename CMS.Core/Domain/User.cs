using System;
using System.Collections;
using System.Security.Principal;
using CMS.Core.Util;

namespace CMS.Core.Domain
{
    /// <summary>
    /// Summary description for User.
    /// </summary>
    public class User : IIdentity
    {
        private string _email;
        private string _firstName;
        private int _id;
        private DateTime _insertTimestamp;
        private bool _isActive;
        private bool _isAuthenticated;
        private string _lastIp;
        private DateTime? _lastLogin;
        private string _lastName;
        private string _password;
        private AccessLevel[] _permissions;
        private IList _roles;
        private int _timeZone;
        private DateTime _updateTimestamp;
        private string _userName;
        private string _website;
        private string _allName;

        #region properties

        /// <summary>
        /// Property Id (int)
        /// </summary>
        public virtual int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// Property UserName (string)
        /// </summary>
        public virtual string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        /// <summary>
        /// Property Password (string). Internally the MD5 hash of the password is used.
        /// </summary>
        public virtual string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        /// <summary>
        /// Property FirstName (string)
        /// </summary>
        public virtual string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        /// <summary>
        /// Property LastName (string)
        /// </summary>
        public virtual string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        /// <summary>
        /// The full name of the user. This can be used for display purposes. If there is no firstname
        /// and lastname, the username will be returned.
        /// </summary>
        public virtual string FullName
        {
            get
            {
                if (_firstName != null && _firstName != String.Empty
                    && _lastName != null && _lastName != String.Empty)
                {
                    return _firstName + " " + _lastName;
                }
                else
                {
                    return _userName;
                }
            }
        }

        /// <summary>
        /// Property Email (string)
        /// </summary>
        public virtual string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        /// <summary>
        /// Property Website (string)
        /// </summary>
        public virtual string Website
        {
            get { return _website; }
            set { _website = value; }
        }

        /// <summary>
        /// The timezone offset of the user in minutes.
        /// </summary>
        public virtual int TimeZone
        {
            get { return _timeZone; }
            set { _timeZone = value; }
        }

        /// <summary>
        /// Property IsActive (bool)
        /// </summary>
        public virtual bool IsActive
        {
            get { return _isActive; }
            set { _isActive = value; }
        }

        /// <summary>
        /// Property LastLogin (DateTime)
        /// </summary>
        public virtual DateTime? LastLogin
        {
            get { return _lastLogin; }
            set { _lastLogin = value; }
        }

        /// <summary>
        /// Property LastIp (string)
        /// </summary>
        public virtual string LastIp
        {
            get { return _lastIp; }
            set { _lastIp = value; }
        }

        /// <summary>
        /// Property Roles (IList)
        /// </summary>
        public virtual IList Roles
        {
            get { return _roles; }
            set { _roles = value; }
        }

        /// <summary>
        /// Property InsertTimestamp (DateTime)
        /// </summary>
        public virtual DateTime InsertTimestamp
        {
            get { return _insertTimestamp; }
            set { _insertTimestamp = value; }
        }

        /// <summary>
        /// Property UpdateTimestamp (DateTime)
        /// </summary>
        public virtual DateTime UpdateTimestamp
        {
            get { return _updateTimestamp; }
            set { _updateTimestamp = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual AccessLevel[] Permissions
        {
            get
            {
                if (_permissions.Length == 0)
                {
                    ArrayList permissions = new ArrayList();
                    foreach (Role role in Roles)
                    {
                        foreach (AccessLevel permission in role.Permissions)
                        {
                            if (permissions.IndexOf(permission) == -1)
                                permissions.Add(permission);
                        }
                    }
                    _permissions = (AccessLevel[]) permissions.ToArray(typeof (AccessLevel));
                }
                return _permissions;
            }
        }

        /// <summary>
        /// IIdentity property <see cref="System.Security.Principal.IIdentity" />.
        /// </summary>
        public virtual bool IsAuthenticated
        {
            get { return _isAuthenticated; }
            set { _isAuthenticated = value; }
        }

        /// <summary>
        /// IIdentity property. 
        /// <remark>Returns a string with the Id of the user and not the username</remark>
        /// </summary>
        public virtual string Name
        {
            get
            {
                if (_isAuthenticated)
                    return _id.ToString();
                else
                    return "";
            }
        }

        /// <summary>
        /// IIdentity property <see cref="System.Security.Principal.IIdentity" />.
        /// </summary>
        public virtual string AuthenticationType
        {
            get { return "CuyahogaAuthentication"; }
        }

        #endregion

        /// <summary>
        /// Default constructor.
        /// </summary>
        public User()
        {
            _id = -1;
            _isAuthenticated = false;
            _permissions = new AccessLevel[0];
            _roles = new ArrayList();
            // Default to now, otherwise NHibernate tries to insert a NULL.
            _insertTimestamp = DateTime.Now;
        }

        /// <summary>
        /// Check if the user has the requested access rights.
        /// </summary>
        /// <param name="permission"></param>
        /// <returns></returns>
        public virtual bool HasPermission(AccessLevel permission)
        {
            return Array.IndexOf(Permissions, permission) > -1;
        }

        /// <summary>
        /// Indicates if the user has view permissions for a certain Node.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public virtual bool CanView(Node node)
        {
            foreach (Permission p in node.NodePermissions)
            {
                if (p.ViewAllowed && IsInRole(p.Role))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Indicates if the user has view permissions for a certain Section.
        /// </summary>
        /// <param name="section"></param>
        /// <returns></returns>
        public virtual bool CanView(Section section)
        {
            foreach (Permission p in section.SectionPermissions)
            {
                if (p.ViewAllowed && IsInRole(p.Role))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Indicates if the user has edit permissions for a certain Section.
        /// </summary>
        /// <param name="section"></param>
        /// <returns></returns>
        public virtual bool CanEdit(Section section)
        {
            foreach (Permission p in section.SectionPermissions)
            {
                if (p.EditAllowed && IsInRole(p.Role))
                {
                    return true;
                }
            }
            return false;
        }

        public virtual bool CanModify(Section section)
        {
            foreach (SectionPermission p in section.SectionPermissions)
            {
                if (p.ModifyAllowed && IsInRole(p.Role))
                {
                    return true;
                }
            }
            return false;
        }

        public virtual bool CanInsert(Section section)
        {
            foreach (SectionPermission p in section.SectionPermissions)
            {
                if (p.InsertAllowed && IsInRole(p.Role))
                {
                    return true;
                }
            }
            return false;
        }

        public virtual bool CanDelete(Section section)
        {
            foreach (SectionPermission p in section.SectionPermissions)
            {
                if (p.DeleteAllowed && IsInRole(p.Role))
                {
                    return true;
                }
            }
            return false;
        }

        public virtual bool HasFullPermission(Section section)
        {
            foreach (SectionPermission p in section.SectionPermissions)
            {
                if (p.DeleteAllowed && p.InsertAllowed && p.ModifyAllowed && p.EditAllowed && p.ViewAllowed && IsInRole(p.Role))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Create a MD5 hash of the password.
        /// </summary>
        /// <param name="password">The password in clear text</param>
        /// <returns>The MD5 hash of the password</returns>
        public static string HashPassword(string password)
        {
            if (ValidatePassword(password))
            {
                return Encryption.StringToMD5Hash(password);
            }
            else
            {
                throw new ArgumentException("Invalid password");
            }
        }

        /// <summary>
        /// Check if the password is valid.
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool ValidatePassword(string password)
        {
            // Very simple password rule. Extend here when required.
            return (password.Length >= 5);
        }

        /// <summary>
        /// Generates a new password and stores a hashed password in the User instance.
        /// </summary>
        /// <returns>The newly created password.</returns>
        public virtual string GeneratePassword()
        {
            int length = 8;
            string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            string pwd = String.Empty;
            Random rnd = new Random();
            for (int i = 0; i < length; i++)
            {
                pwd += chars[rnd.Next(chars.Length)];
            }
            _password = HashPassword(pwd);
            return pwd;
        }

        /// <summary>
        /// Determine if the user is in a give Role.
        /// </summary>
        /// <param name="roleToCheck"></param>
        /// <returns></returns>
        public virtual bool IsInRole(Role roleToCheck)
        {
            foreach (Role role in Roles)
            {
                if (role.Id == roleToCheck.Id && role.Name == roleToCheck.Name)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Determine if the user is in a give Role.
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public virtual bool IsInRole(string roleName)
        {
            foreach (Role role in Roles)
            {
                if (role.Name == roleName)
                {
                    return true;
                }
            }
            return false;
        }

        public virtual string AllName
        {
            get { return _allName; }
            set { _allName = value; }
        }
    }
}