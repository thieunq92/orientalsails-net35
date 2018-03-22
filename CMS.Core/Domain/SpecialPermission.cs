using System;
using System.Collections;
using CMS.Core.Domain;

namespace CMS.Core.Domain
{
    /// <summary>
    /// Hotel object for NHibernate mapped table 'tmh_Hotel'.
    /// </summary>
    public class SpecialPermission
    {
        #region -- Static --

        #endregion

        #region Member Variables

        protected int _id;
        protected string _name;
        protected Section _section;
        protected Role _role;
        protected User _user;
        protected ModuleType _moduleType;
       
        #endregion

        #region Constructors

        public SpecialPermission()
        {
            _id = -1;
        }        

        #endregion

        #region Public Properties
        public virtual int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public virtual string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public virtual Section Section
        {
            get { return _section; }
            set { _section = value; }
        }

        public virtual Role Role
        {
            get { return _role; }
            set { _role = value; }
        }

        public virtual User User
        {
            get { return _user; }
            set { _user = value; }
        }

        public virtual ModuleType ModuleType
        {
            get { return _moduleType; }
            set { _moduleType = value; }
        }

        #endregion

        #region -- Methods --
        #endregion
    }
}