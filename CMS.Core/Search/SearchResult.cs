using System;

namespace CMS.Core.Search
{
    /// <summary>
    /// Class that holds the result of a single hit.
    /// </summary>
    public class SearchResult
    {
        private string _author;
        private float _boost;
        private string _category;
        private DateTime _dateCreated;
        private string _moduleType;
        private string _path;
        private float _score;
        private int _sectionId;
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
        /// Property DateCreated (DateTime)
        /// </summary>
        public DateTime DateCreated
        {
            get { return _dateCreated; }
            set { _dateCreated = value; }
        }

        /// <summary>
        /// Property Score (float)
        /// </summary>
        public float Score
        {
            get { return _score; }
            set { _score = value; }
        }

        /// <summary>
        /// Property Boost (float)
        /// </summary>
        public float Boost
        {
            get { return _boost; }
            set { _boost = value; }
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