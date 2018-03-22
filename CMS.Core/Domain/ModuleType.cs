using System.Collections;

namespace CMS.Core.Domain
{
    /// <summary>
    /// The ModuleType class describes a module.
    /// </summary>
    public class ModuleType
    {
        private string _assemblyName;
        private bool _autoActivate;
        private string _className;
        private string _editPath;
        private IList _moduleServices;
        private IList _moduleSettings;
        private int _moduleTypeId;
        private string _name;
        private string _path;
        private string _frienlyName;
        private IList _modulePermissions;

        #region properties

        /// <summary>
        /// Property ModuleId (int)
        /// </summary>
        public virtual int ModuleTypeId
        {
            get { return _moduleTypeId; }
            set { _moduleTypeId = value; }
        }

        /// <summary>
        /// Property AssemblyName (string)
        /// </summary>
        public virtual string AssemblyName
        {
            get { return _assemblyName; }
            set { _assemblyName = value; }
        }

        /// <summary>
        /// Property ClassName (string)
        /// </summary>
        public virtual string ClassName
        {
            get { return _className; }
            set { _className = value; }
        }

        /// <summary>
        /// Property Name (string)
        /// </summary>
        public virtual string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// Property Path (string)
        /// </summary>
        public virtual string Path
        {
            get { return _path; }
            set { _path = value; }
        }

        /// <summary>
        /// Property EditPath (string)
        /// </summary>
        public virtual string EditPath
        {
            get { return _editPath; }
            set { _editPath = value; }
        }

        /// <summary>
        /// Indicates if the module is loaded at startup.
        /// </summary>
        public virtual bool AutoActivate
        {
            get { return _autoActivate; }
            set { _autoActivate = value; }
        }

        /// <summary>
        /// Property ModuleSettings (IList)
        /// </summary>
        public virtual IList ModuleSettings
        {
            get { return _moduleSettings; }
            set { _moduleSettings = value; }
        }

        /// <summary>
        /// List of module-specific services.
        /// </summary>
        public virtual IList ModuleServices
        {
            get { return _moduleServices; }
            set { _moduleServices = value; }
        }

        public virtual IList ModulePermissions
        {
            get
            {
                if (_modulePermissions==null)
                {
                    _modulePermissions = new ArrayList();
                }
                return _modulePermissions;
            }
            set { _modulePermissions = value; }
        }

        public virtual string FriendlyName
        {
            get { return _frienlyName; }
            set { _frienlyName = value; }
        }

        public virtual string FullName
        {
            get
            {
                if (string.IsNullOrEmpty(FriendlyName))
                {
                    return _name;
                }
                return _frienlyName;
            }
        }

        #endregion

        /// <summary>
        /// Constructor.
        /// </summary>
        public ModuleType()
        {
            _moduleTypeId = -1;
            _moduleSettings = new ArrayList();
            _moduleServices = new ArrayList();
        }
    }
}