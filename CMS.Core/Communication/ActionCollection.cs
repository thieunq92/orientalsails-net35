using System.Collections;

namespace CMS.Core.Communication
{
    /// <summary>
    /// Collection class for Actions.
    /// </summary>
    public class ActionCollection : CollectionBase
    {
        /// <summary>
        /// Indexer property.
        /// </summary>
        public Action this[int index]
        {
            get { return (Action) List[index]; }
        }

        /// <summary>
        /// Add an Action to the list.
        /// </summary>
        /// <param name="action"></param>
        public void Add(Action action)
        {
            List.Add(action);
        }

        /// <summary>
        /// Remove an Action from the list.
        /// </summary>
        /// <param name="action"></param>
        public void Remove(Action action)
        {
            List.Remove(action);
        }

        /// <summary>
        /// Does the list contain a given Action?
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public bool Contains(Action action)
        {
            return List.Contains(action);
        }

        /// <summary>
        /// Find a specific action in the list.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Action FindByName(string name)
        {
            foreach (Action action in List)
            {
                if (action.Name == name)
                {
                    return action;
                }
            }
            return null;
        }
    }
}