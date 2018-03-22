using System;
using System.Collections;

namespace CMS.Core.Domain
{
    /// <summary>
    /// Represents a Cuyahoga template. This is not restricted to one physical file.
    /// It's possible to create multiple template objects based on the same template
    /// UserControl and stylesheet.
    /// </summary>
    public class Template
    {
        private string _basePath;
        private string _css;
        private int _id;
        private string _name;
        private IDictionary _sections;
        private string _templateControl;
        private DateTime _updateTimestamp;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Template()
        {
            _id = -1;
            _sections = new Hashtable();
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
        /// Property UpdateTimestamp (DateTime)
        /// </summary>
        public virtual DateTime UpdateTimestamp
        {
            get { return _updateTimestamp; }
            set { _updateTimestamp = value; }
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
        /// Property BasePath (string)
        /// </summary>
        public virtual string BasePath
        {
            get { return _basePath; }
            set { _basePath = value; }
        }

        /// <summary>
        /// Property TemplateControl (string)
        /// </summary>
        public virtual string TemplateControl
        {
            get { return _templateControl; }
            set { _templateControl = value; }
        }

        /// <summary>
        /// The path to the Template control from the application root.
        /// </summary>
        /// <remarks>
        /// This is a combination of BasePath and TemplateControl. This is the same as pre-0.7 Path property.
        /// </remarks>
        public virtual string Path
        {
            get { return BasePath + "/" + TemplateControl; }
        }


        /// <summary>
        /// The filename of the stylesheet file to be used with this Template.
        /// </summary>
        /// <remarks>
        /// The stylesheet file has to be in the [BasePath]/Css directory.
        /// </remarks>
        public virtual string Css
        {
            get { return _css; }
            set { _css = value; }
        }

        /// <summary>
        /// The sections that are directly related to the template. The key represents the placeholder
        /// where the section belongs.
        /// </summary>
        public virtual IDictionary Sections
        {
            get { return _sections; }
            set { _sections = value; }
        }
    }
}