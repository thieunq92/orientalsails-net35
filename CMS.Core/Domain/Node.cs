using System;
using System.Collections;
using System.Security.Principal;
using System.Text.RegularExpressions;

namespace CMS.Core.Domain
{
    /// <summary>
    /// The Node class represents a node in the page hierarchy of the site.
    /// </summary>
    public class Node
    {
        private IList _childNodes;
        private string _culture;
        private int _id;
        private LinkTarget _linkTarget;
        private string _linkUrl;
        private string _metaDescription;
        private string _metaKeywords;
        private Node[] _nodePath;
        private IList _nodePermissions;
        private Node _parentNode;
        private int _position;
        private IList _sections;
        private string _shortDescription;
        private bool _showInNavigation;
        private Site _site;
        private Template _template;
        private string _title;
        private int[] _trail;
        private DateTime _updateTimestamp;

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
        /// Property Title (string)
        /// </summary>
        public virtual string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        /// <summary>
        /// Property ShortDescription (string)
        /// </summary>
        public virtual string ShortDescription
        {
            get { return _shortDescription; }
            set
            {
                if (value.Trim().Length == 0)
                    _shortDescription = null;
                else
                    _shortDescription = value;
            }
        }

        /// <summary>
        /// Property Order (int)
        /// </summary>
        public virtual int Position
        {
            get { return _position; }
            set { _position = value; }
        }

        /// <summary>
        /// Property Culture (string)
        /// </summary>
        public virtual string Culture
        {
            get { return _culture; }
            set { _culture = value; }
        }

        /// <summary>
        /// Property ShowInNavigation (bool)
        /// </summary>
        public virtual bool ShowInNavigation
        {
            get { return _showInNavigation; }
            set { _showInNavigation = value; }
        }

        /// <summary>
        /// Link to external url.
        /// </summary>
        public virtual string LinkUrl
        {
            get { return _linkUrl; }
            set { _linkUrl = value; }
        }

        /// <summary>
        /// Target window for an external url.
        /// </summary>
        public virtual LinkTarget LinkTarget
        {
            get { return _linkTarget; }
            set { _linkTarget = value; }
        }

        /// <summary>
        /// Indicates if the node represents an external link.
        /// </summary>
        public virtual bool IsExternalLink
        {
            get { return _linkUrl != null && _linkUrl != String.Empty; }
        }

        /// <summary>
        /// List of keywords for the page.
        /// </summary>
        public virtual string MetaKeywords
        {
            get { return _metaKeywords; }
            set { _metaKeywords = value; }
        }

        /// <summary>
        /// Description of the page.
        /// </summary>
        public virtual string MetaDescription
        {
            get { return _metaDescription; }
            set { _metaDescription = value; }
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
        /// Property Level (int)
        /// </summary>
        public virtual int Level
        {
            get
            {
                int level = 0;
                Node parentNode = ParentNode;
                while (parentNode != null)
                {
                    parentNode = parentNode.ParentNode;
                    level++;
                }
                return level;
            }
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
        /// Property ParentNode (Node). Lazy loaded.
        /// </summary>
        public virtual Node ParentNode
        {
            get { return _parentNode; }
            set { _parentNode = value; }
        }

        /// <summary>
        /// Property ChildNodes (IList). Lazy loaded.
        /// </summary>
        public virtual IList ChildNodes
        {
            get { return _childNodes; }
            set
            {
                // TODO?
                // Notify that the ChildNodes are loaded. I really want to do this only when the 
                // ChildNodes are loaded (lazy) from the database but I don't know if this happens right now.
                // Implement IInterceptor?
                //OnChildrenLoaded();
                _childNodes = value;
            }
        }

        /// <summary>
        /// Property Sections (IList). Lazy loaded.
        /// </summary>
        public virtual IList Sections
        {
            get { return _sections; }
            set { _sections = value; }
        }

        /// <summary>
        /// Property Template (Template)
        /// </summary>
        public virtual Template Template
        {
            get { return _template; }
            set { _template = value; }
        }

        /// <summary>
        /// Array with all NodeId's from the current node to the root node.
        /// </summary>
        public virtual int[] Trail
        {
            get
            {
                if (_trail == null)
                {
                    SetNodePath();
                }
                return _trail;
            }
        }

        /// <summary>
        /// Array with all Nodes from the current node to the root node.
        /// </summary>
        public virtual Node[] NodePath
        {
            get
            {
                if (_nodePath == null)
                {
                    SetNodePath();
                }
                return _nodePath;
            }
        }

        /// <summary>
        /// Property NodePermissions (IList)
        /// </summary>
        public virtual IList NodePermissions
        {
            get { return _nodePermissions; }
            set { _nodePermissions = value; }
        }

        /// <summary>
        /// Can the node be viewed by anonymous users?
        /// </summary>
        public virtual bool AnonymousViewAllowed
        {
            get
            {
                foreach (NodePermission np in _nodePermissions)
                {
                    if (Array.IndexOf(np.Role.Permissions, AccessLevel.Anonymous) > -1)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        /// <summary>
        /// Indicates if the node a root node (home)?
        /// </summary>
        public virtual bool IsRootNode
        {
            get { return _id > -1 && _parentNode == null; }
        }

        #endregion

        #region constructors and initialization

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Node()
        {
            _id = -1;
            InitNode();
        }

        private void InitNode()
        {
            _shortDescription = null;
            _parentNode = null;
            _template = null;
            _childNodes = null;
            _sections = null;
            _position = -1;
            _trail = null;
            _showInNavigation = true;
            _sections = new ArrayList();
            _nodePermissions = new ArrayList();
        }

        #endregion

        #region methods

        /// <summary>
        /// Move the node to a different position in the tree.
        /// </summary>
        /// <param name="rootNodes">We need the root nodes when the node has no ParentNode</param>
        /// <param name="npm">Direction</param>
        public virtual void Move(IList rootNodes, NodePositionMovement npm)
        {
            switch (npm)
            {
                case NodePositionMovement.Up:
                    MoveUp(rootNodes);
                    break;
                case NodePositionMovement.Down:
                    MoveDown(rootNodes);
                    break;
                case NodePositionMovement.Left:
                    MoveLeft(rootNodes);
                    break;
                case NodePositionMovement.Right:
                    MoveRight(rootNodes);
                    break;
            }
        }

        /// <summary>
        /// Calculate the position of a new node.
        /// </summary>
        /// <param name="rootNodes">The root nodes for the case an item as added at root level.</param>
        public virtual void CalculateNewPosition(IList rootNodes)
        {
            if (ParentNode != null)
            {
                _position = ParentNode.ChildNodes.Count;
            }
            else
            {
                _position = rootNodes.Count;
            }
        }

        /// <summary>
        /// Ensure that there is no gap between the positions of nodes.
        /// </summary>
        /// <param name="nodeListWithGap"></param>
        /// <param name="gapPosition"></param>
        public virtual void ReOrderNodePositions(IList nodeListWithGap, int gapPosition)
        {
            foreach (Node node in nodeListWithGap)
            {
                if (node.Position > gapPosition)
                {
                    node.Position--;
                }
            }
        }

        /// <summary>
        /// Set the sections to null, so they will be loaded from the database next time.
        /// </summary>
        public virtual void ResetSections()
        {
            _sections = null;
        }

        /// <summary>
        /// Indicates if viewing of the node is allowed. Anonymous users get a special treatment because we
        /// can't check their rights because they are no full-blown Cuyahoga users (not authenticated).
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public virtual bool ViewAllowed(IIdentity user)
        {
            User cuyahogaUser = user as User;
            if (AnonymousViewAllowed)
            {
                return true;
            }
            else if (cuyahogaUser != null)
            {
                return cuyahogaUser.CanView(this);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public virtual bool ViewAllowed(Role role)
        {
            foreach (NodePermission np in NodePermissions)
            {
                if (np.Role == role && np.ViewAllowed)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public virtual bool EditAllowed(Role role)
        {
            foreach (NodePermission np in NodePermissions)
            {
                if (np.Role == role && np.EditAllowed)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual void CopyRolesFromParent()
        {
            if (_parentNode != null)
            {
                foreach (NodePermission np in _parentNode.NodePermissions)
                {
                    NodePermission npNew = new NodePermission();
                    npNew.Node = this;
                    npNew.Role = np.Role;
                    npNew.ViewAllowed = np.ViewAllowed;
                    npNew.EditAllowed = np.EditAllowed;
                    NodePermissions.Add(npNew);
                }
            }
        }

        /// <summary>
        /// Generate a short description based on the parent short description and the title.
        /// </summary>
        public virtual void CreateShortDescription(string title)
        {
            string prefix = "";
            if (_parentNode != null)
            {
                prefix += _parentNode.ShortDescription + "/";
            } 
            // Substitute spaces
            string tempTitle = Regex.Replace(title.Trim(), "\\s", "-");
            // Remove illegal characters
            tempTitle = Regex.Replace(tempTitle, "[^A-Za-z0-9+-.]", "");
            _shortDescription = prefix + tempTitle.ToLower();
        }

        /// <summary>
        /// Rebuild an already existing ShortDescription to make it unique by adding a suffix (integer).
        /// </summary>
        /// <param name="suffix"></param>
        public virtual void RecreateShortDescription(int suffix)
        {
            string tmpShortDescription = _shortDescription.Substring(0, _shortDescription.Length - 2);
            _shortDescription = tmpShortDescription + "_" + suffix;
        }

        /// <summary>
        /// Validate the node.
        /// </summary>
        public virtual void Validate()
        {
            // check if the the node is a root node and if so, check the uniqueness of the culture
            if (ParentNode == null) // indicates a root node
            {
                foreach (Node node in Site.RootNodes)
                {
                    if (node.Id != _id && node.Culture == Culture)
                    {
                        throw new Exception(
                            "Found a root node with the same culture. The culture of a root node has to be unique within a site.");
                    }
                }
            }
        }

        /// <summary>
        /// Move the node one position upwards and move the node above this one one position downwards.
        /// </summary>
        /// <param name="rootNodes">We need these when the node has no ParentNode.</param>
        private void MoveUp(IList rootNodes)
        {
            if (_position > 0)
            {
                // HACK: Assume that the node indexes are the same as the value of the positions.
                _position--;
                IList parentChildNodes = (ParentNode == null ? rootNodes : ParentNode.ChildNodes);
                ((Node) parentChildNodes[_position]).Position++;
                parentChildNodes.Remove(this);
                parentChildNodes.Insert(_position, this);
            }
        }

        /// <summary>
        /// Move the node one position downwards and move the node above this one one position upwards.
        /// </summary>
        /// <param name="rootNodes">We need these when the node has no ParentNode.</param>
        private void MoveDown(IList rootNodes)
        {
            if (_position < ParentNode.ChildNodes.Count - 1)
            {
                // HACK: Assume that the node indexes are the same as the value of the positions.
                _position++;
                IList parentChildNodes = (ParentNode == null ? rootNodes : ParentNode.ChildNodes);
                ((Node) parentChildNodes[_position]).Position--;
                parentChildNodes.Remove(this);
                parentChildNodes.Insert(_position, this);
            }
        }

        /// <summary>
        /// Move node to the same level as the parentnode at the position just beneath the parent node.
        /// </summary>
        /// <param name="rootNodes">The root nodes. We need these when a node is moved to the
        /// root level because the nodes that come after this one ahve to be moved and can't be reached
        /// anymore by traversing related nodes.</param>
        private void MoveLeft(IList rootNodes)
        {
            int newPosition = ParentNode.Position + 1;
            if (ParentNode.Level == 0)
            {
                for (int i = newPosition; i < rootNodes.Count; i++)
                {
                    Node nodeAlsoToBeMoved = (Node) rootNodes[i];
                    nodeAlsoToBeMoved.Position++;
                }
            }
            else
            {
                for (int i = newPosition; i < ParentNode.ParentNode.ChildNodes.Count; i++)
                {
                    Node nodeAlsoToBeMoved = (Node) ParentNode.ParentNode.ChildNodes[i];
                    nodeAlsoToBeMoved.Position++;
                }
            }
            ParentNode.ChildNodes.Remove(this);
            ReOrderNodePositions(ParentNode.ChildNodes, Position);
            ParentNode = ParentNode.ParentNode;
            if (ParentNode != null)
            {
                ParentNode.ChildNodes.Add(this);
            }
            Position = newPosition;
        }

        /// <summary>
        /// Add node to the children of the previous node in the list.
        /// </summary>
        /// <param name="rootNodes"></param>
        private void MoveRight(IList rootNodes)
        {
            if (_position > 0)
            {
                Node previousSibling;
                if (ParentNode != null)
                {
                    previousSibling = (Node) ParentNode.ChildNodes[_position - 1];
                    ParentNode.ChildNodes.Remove(this);
                    ReOrderNodePositions(ParentNode.ChildNodes, Position);
                }
                else
                {
                    previousSibling = (Node) rootNodes[_position - 1];
                    ReOrderNodePositions(rootNodes, Position);
                }

                Position = previousSibling.ChildNodes.Count;
                previousSibling.ChildNodes.Add(this);
                ParentNode = previousSibling;
            }
        }

        private void SetNodePath()
        {
            if (Level > -1)
            {
                _trail = new int[Level + 1];
                _nodePath = new Node[Level + 1];
                _trail[Level] = _id;
                _nodePath[Level] = this;
                Node tmpParentNode = ParentNode;
                while (tmpParentNode != null)
                {
                    _trail[tmpParentNode.Level] = tmpParentNode.Id;
                    _nodePath[tmpParentNode.Level] = tmpParentNode;
                    tmpParentNode = tmpParentNode.ParentNode;
                }
            }
        }

        #endregion
    }
}