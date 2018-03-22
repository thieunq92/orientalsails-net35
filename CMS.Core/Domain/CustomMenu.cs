using System;
using System.Collections;

namespace CMS.Core.Domain
{
    /// <summary>
    /// The Menu class serves as a container for links to Nodes that need to be displayed outside 
    /// the regular Node hierarchy.
    /// </summary>
    public class CustomMenu
    {
        private int _id;
        private string _name;
        private IList _nodes;
        private string _placeholder;
        private Node _rootNode;
        private DateTime _updateTimestamp;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public CustomMenu()
        {
            _id = -1;
            _nodes = new ArrayList();
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
        /// Property Name (string)
        /// </summary>
        public virtual string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// Property Placeholder (string)
        /// </summary>
        public virtual string Placeholder
        {
            get { return _placeholder; }
            set { _placeholder = value; }
        }

        /// <summary>
        /// Property RootNode (Node)
        /// </summary>
        public virtual Node RootNode
        {
            get { return _rootNode; }
            set { _rootNode = value; }
        }

        /// <summary>
        /// Property Nodes (IList)
        /// </summary>
        public virtual IList Nodes
        {
            get { return _nodes; }
            set { _nodes = value; }
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