using System;

namespace CMS.Core.Search
{
    /// <summary>
    /// Class that holds the content to be indexed for a single document.
    /// </summary>
    public class SearchContent
    {
        private string _author;
        private string _category;
        private string _contents;
        private DateTime _dateCreated;
        private DateTime _dateModified;
        private string _moduleType;
        private string _path;
        private int _sectionId;
        private string _site;
        private string _summary;
        private string _title;

        /// <summary>
        /// Property Title (string)
        /// </summary>
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        /// <summary>
        /// Property Summary (string)
        /// </summary>
        public string Summary
        {
            get { return _summary; }
            set { _summary = value; }
        }

        /// <summary>
        /// Property Contents (string)
        /// </summary>
        public string Contents
        {
            get { return _contents; }
            set { _contents = value; }
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
        /// Property ModuleType (string)
        /// </summary>
        public string ModuleType
        {
            get { return _moduleType; }
            set { _moduleType = value; }
        }

        /// <summary>
        /// Property Path (string)
        /// </summary>
        public string Path
        {
            get { return _path; }
            set { _path = value; }
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
        /// Property Site (string)
        /// </summary>
        public string Site
        {
            get { return _site; }
            set { _site = value; }
        }

        /// <summary>
        /// Property DateCreated (DateTime)
        /// </summary>
        public DateTime DateCreated
        {
            get { return _dateCreated; }
            set { _dateCreated = value; }
        }

        /// <summary>
        /// Property DateModified (DateTime)
        /// </summary>
        public DateTime DateModified
        {
            get { return _dateModified; }
            set { _dateModified = value; }
        }

        /// <summary>
        /// Property SectionId (int)
        /// </summary>
        public int SectionId
        {
            get { return _sectionId; }
            set { _sectionId = value; }
        }
    }
}