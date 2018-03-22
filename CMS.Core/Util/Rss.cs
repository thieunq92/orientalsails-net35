using System;
using System.Collections;

namespace CMS.Core.Util
{
    /// <summary>
    /// A container class for a RSS feed (for generation purposes).
    /// </summary>
    public class RssChannel
    {
        private string _description;
        private string _generator;
        private string _language;
        private DateTime _lastBuildDate;
        private string _link;
        private DateTime _pubDate;
        private IList _rssItems;
        private string _title;
        private string _imageUrl;
        private int _ttl;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public RssChannel()
        {
            // Set some defaults.
            _generator = "KIT Website Framework";
            _ttl = 60;
            _rssItems = new ArrayList();
        }

        /// <summary>
        /// Property Title (string)
        /// </summary>
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        public string ImageUrl
        {
            get { return _imageUrl; }
            set { _imageUrl = value; }
        }

        /// <summary>
        /// Property Link (string)
        /// </summary>
        public string Link
        {
            get { return _link; }
            set { _link = value; }
        }

        /// <summary>
        /// Property Description (string)
        /// </summary>
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        /// <summary>
        /// Property Language (string)
        /// </summary>
        public string Language
        {
            get { return _language; }
            set { _language = value; }
        }

        /// <summary>
        /// Property PubDate (DateTime)
        /// </summary>
        public DateTime PubDate
        {
            get { return _pubDate; }
            set { _pubDate = value; }
        }

        /// <summary>
        /// Property LastBuildDate (DateTime)
        /// </summary>
        public DateTime LastBuildDate
        {
            get { return _lastBuildDate; }
            set { _lastBuildDate = value; }
        }

        /// <summary>
        /// Property Generator (string)
        /// </summary>
        public string Generator
        {
            get { return _generator; }
            set { _generator = value; }
        }

        /// <summary>
        /// Property Ttl (int)
        /// </summary>
        public int Ttl
        {
            get { return _ttl; }
            set { _ttl = value; }
        }

        /// <summary>
        /// Property RssItems (IList)
        /// </summary>
        public IList RssItems
        {
            get { return _rssItems; }
            set { _rssItems = value; }
        }
    }

    /// <summary>
    /// A RSS feed item.
    /// </summary>
    public class RssItem
    {
        private string _author;
        private string _category;
        private string _description;
        private int _itemId;
        private string _link;
        private DateTime _pubDate;
        private string _title;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public RssItem()
        {
            _itemId = -1;
        }

        /// <summary>
        /// Property ItemId (int)
        /// </summary>
        public int ItemId
        {
            get { return _itemId; }
            set { _itemId = value; }
        }

        /// <summary>
        /// Property Title (string)
        /// </summary>
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        /// <summary>
        /// Property Link (string)
        /// </summary>
        public string Link
        {
            get { return _link; }
            set { _link = value; }
        }

        /// <summary>
        /// Property Description (string)
        /// </summary>
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        /// <summary>
        /// Property Author (string)
        /// </summary>
        public string Author
        {
            get { return _author; }
            set { _author = value; }
        }

        /// <summary>
        /// Property Category (string)
        /// </summary>
        public string Category
        {
            get { return _category; }
            set { _category = value; }
        }

        /// <summary>
        /// Property PubDate (DateTime)
        /// </summary>
        public DateTime PubDate
        {
            get { return _pubDate; }
            set { _pubDate = value; }
        }
    }
}