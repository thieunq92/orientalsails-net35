namespace CMS.Core.Domain
{
    /// <summary>
    /// Association class between Section and Role.
    /// </summary>
    public class SectionPermission : Permission
    {
        private Section _section;
        private bool _modifyAllowed;
        private bool _insertAllowed;
        private bool _deltedAllowed;

        public SectionPermission()
        {
            _modifyAllowed = EditAllowed;
            _insertAllowed = EditAllowed;
            _deltedAllowed = EditAllowed;
        }

        /// <summary>
        /// Property Section (Section)
        /// </summary>
        public Section Section
        {
            get { return _section; }
            set { _section = value; }
        }

        public bool ModifyAllowed
        {
            get { return _modifyAllowed; }
            set { _modifyAllowed = value; }
        }

        public bool InsertAllowed
        {
            get { return _insertAllowed; }
            set { _insertAllowed = value; }
        }

        public bool DeleteAllowed
        {
            get { return _deltedAllowed; }
            set { _deltedAllowed = value; }
        }
    }
}