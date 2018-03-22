using System;

namespace CMS.Core.Domain
{
    /// <summary>
    /// The SiteAlias class enables mapping of an alternative url to an existing Cuyahoga site.
    /// Optionally you can specify a Node to where the alias should point.
    /// </summary>
    public class SiteAlias
    {
        private Node _entryNode;
        private int _id;
        private Site _site;
        private DateTime _updateTimestamp;
        private string _url;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public SiteAlias()
        {
            _id = -1;
        }

        /// <summary>
        /// Property Id (int)
        /// </summary>
        public virtual int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// Property Site (Site)
        /// </summary>
        public virtual Site Site
        {
            get { return _site; }
            set { _site = value; }
        }

        /// <summary>
        /// Optional entry point.
        /// </summary>
        public virtual Node EntryNode
        {
            get { return _entryNode; }
            set { _entryNode = value; }
        }

        /// <summary>
        /// Property Url (string)
        /// </summary>
        public virtual string Url
        {
            get { return _url; }
            set { _url = value; }
        }

        /// <summary>
        /// Property UpdateTimestamp (DateTime)
        /// </summary>
        public virtual DateTime UpdateTimestamp
        {
            get { return _updateTimestamp; }
            set { _updateTimestamp = value; }
        }
    }
}