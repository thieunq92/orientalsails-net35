namespace CMS.Core.Domain
{
    /// <summary>
    /// Base class for permission related association objects.
    /// </summary>
    public abstract class Permission
    {
        private bool _editAllowed;
        private int _id;
        private Role _role;
        private bool _viewAllowed;

        /// <summary>
        /// Protected constructor
        /// </summary>
        protected Permission()
        {
            _id = -1;
            _viewAllowed = false;
            _editAllowed = false;
        }

        /// <summary>
        /// Property Id (int)
        /// </summary>
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// Property ViewAllowed (bool)
        /// </summary>
        public bool ViewAllowed
        {
            get { return _viewAllowed; }
            set { _viewAllowed = value; }
        }

        /// <summary>
        /// Property EditAllowed (bool)
        /// </summary>
        public bool EditAllowed
        {
            get { return _editAllowed; }
            set { _editAllowed = value; }
        }

        /// <summary>
        /// Property Role (Role)
        /// </summary>
        public Role Role
        {
            get { return _role; }
            set { _role = value; }
        }
    }
}