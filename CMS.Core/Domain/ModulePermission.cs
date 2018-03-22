using CMS.Core.Domain;

namespace CMS.Core.Domain
{
    /// <summary>
    /// Hotel object for NHibernate mapped table 'tmh_Hotel'.
    /// </summary>
    public class ModulePermission
    {
        #region -- Static --

        #endregion

        #region Member Variables

        protected int _id;
        protected string _name;
        protected ModuleType _moduleType;
        protected string _groupName;
        protected string _friendlyName;
       
        #endregion

        #region Constructors

        public ModulePermission()
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

        public virtual ModuleType ModuleType
        {
            get { return _moduleType; }
            set { _moduleType = value; }
        }

        public virtual string GroupName
        {
            get { return _groupName; }
            set { _groupName = value; }
        }

        public virtual string FriendlyName
        {
            get { return _friendlyName; }
            set { _friendlyName = value; }
        }

        #endregion

        #region -- Methods --
        #endregion
    }
}