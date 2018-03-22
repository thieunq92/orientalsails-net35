using System;
using System.Collections;

namespace CMS.Core.Domain
{
    /// <summary>
    /// Summary description for Site.
    /// </summary>
    public class Site
    {
        private string _defaultCulture;
        private string _defaultPlaceholder;
        private Role _defaultRole;
        private Template _defaultTemplate;
        private int _id;
        private string _metaDescription;
        private string _metaKeywords;
        private string _name;
        private IList _rootNodes;
        private string _siteUrl;
        private DateTime _updateTimestamp;
        private bool _useFriendlyUrls;
        private string _webmasterEmail;

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
        /// Property Name (string)
        /// </summary>
        public virtual string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// Property HomeUrl (string)
        /// </summary>
        public virtual string SiteUrl
        {
            get { return _siteUrl; }
            set { _siteUrl = value; }
        }

        /// <summary>
        /// Property DefaultCulture (string)
        /// </summary>
        public virtual string DefaultCulture
        {
            get { return _defaultCulture; }
            set { _defaultCulture = value; }
        }

        /// <summary>
        /// Property DefaultTemplate (Template)
        /// </summary>
        public virtual Template DefaultTemplate
        {
            get { return _defaultTemplate; }
            set { _defaultTemplate = value; }
        }

        /// <summary>
        /// The default role for registered users.
        /// </summary>
        public virtual Role DefaultRole
        {
            get { return _defaultRole; }
            set { _defaultRole = value; }
        }

        /// <summary>
        /// Property DefaultPlaceholder (string)
        /// </summary>
        public virtual string DefaultPlaceholder
        {
            get { return _defaultPlaceholder; }
            set { _defaultPlaceholder = value; }
        }

        /// <summary>
        /// Property WebmasterEmail (string)
        /// </summary>
        public virtual string WebmasterEmail
        {
            get { return _webmasterEmail; }
            set { _webmasterEmail = value; }
        }

        /// <summary>
        /// Indicates if the site uses friendly 'readable' urls by default.
        /// </summary>
        public virtual bool UseFriendlyUrls
        {
            get { return _useFriendlyUrls; }
            set { _useFriendlyUrls = value; }
        }

        /// <summary>
        /// List of global keywords for the site.
        /// </summary>
        public virtual string MetaKeywords
        {
            get { return _metaKeywords; }
            set { _metaKeywords = value; }
        }

        /// <summary>
        /// Global description for the site.
        /// </summary>
        public virtual string MetaDescription
        {
            get { return _metaDescription; }
            set { _metaDescription = value; }
        }

        /// <summary>
        /// Property RootNodes (IList)
        /// </summary>
        public virtual IList RootNodes
        {
            get { return _rootNodes; }
            set { _rootNodes = value; }
        }

        /// <summary>
        /// Property UpdateTimestamp (DateTime)
        /// </summary>
        public virtual DateTime UpdateTimestamp
        {
            get { return _updateTimestamp; }
            set { _updateTimestamp = value; }
        }

        #endregion

        public Site()
        {
            _id = -1;
        }
    }
}