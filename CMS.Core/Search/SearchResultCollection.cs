using System.Collections;

namespace CMS.Core.Search
{
    /// <summary>
    /// Summary description for SearchResultCollection.
    /// </summary>
    public class SearchResultCollection : CollectionBase
    {
        private long _executionTime;
        private int _pageIndex;
        private int _totalCount;

        /// <summary>
        /// Property TotalCount (int)
        /// </summary>
        public int TotalCount
        {
            get { return _totalCount; }
            set { _totalCount = value; }
        }

        /// <summary>
        /// Property PageIndex (int)
        /// </summary>
        public int PageIndex
        {
            get { return _pageIndex; }
            set { _pageIndex = value; }
        }

        /// <summary>
        /// The execution time of the query in ticks.
        /// </summary>
        public long ExecutionTime
        {
            get { return _executionTime; }
            set { _executionTime = value; }
        }

        /// <summary>
        /// Indexer property.
        /// </summary>
        public SearchResult this[int index]
        {
            get { return (SearchResult) List[index]; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchResult"></param>
        public void Add(SearchResult searchResult)
        {
            List.Add(searchResult);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchResult"></param>
        public void Remove(SearchResult searchResult)
        {
            List.Remove(searchResult);
        }
    }
}