namespace CMS.Core.Domain
{
    /// <summary>
    /// Association class between Node and Role.
    /// </summary>
    public class NodePermission : Permission
    {
        private Node _node;

        /// <summary>
        /// Property Node (Node)
        /// </summary>
        public Node Node
        {
            get { return _node; }
            set { _node = value; }
        }
    }
}