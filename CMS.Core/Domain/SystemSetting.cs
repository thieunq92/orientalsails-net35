using System;
using System.Collections;
using CMS.Core.Domain;

namespace CMS.Core.Domain
{
    /// <summary>
    /// Customer object for NHibernate mapped table 'os_Customer'.
    /// </summary>
    public class SystemSetting
    {
        #region Static Columns Name

        public const string KEY = "Key";
        public const string VALUE = "Value";
        public const string MODULETYPE = "ModuleType";
        #endregion

        #region Member Variables

        protected int _id;
        protected string _key;
        protected string _value;
        protected ModuleType _moduleType;

        #endregion

        #region Constructors

        public SystemSetting()
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

        public virtual string Key
        {
            get { return _key; }
            set { _key = value; }
        }

        public virtual string Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public virtual ModuleType ModuleType
        {
            get { return _moduleType; }
            set { _moduleType = value; }
        }

        #endregion
    }
}