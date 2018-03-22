namespace CMS.Core.Search
{
    public delegate void IndexEventHandler(object sender, IndexEventArgs e);

    public class IndexEventArgs
    {
        private readonly SearchContent _searchContent;

        /// <summary>
        /// Create an IndexEventArgs class.
        /// </summary>
        /// <param name="searchContent">The content that needs to be moved around.</param>
        public IndexEventArgs(SearchContent searchContent)
        {
            _searchContent = searchContent;
        }

        /// <summary>
        /// Property SearchContent (SearchContent)
        /// </summary>
        public SearchContent SearchContent
        {
            get { return _searchContent; }
        }
    }
}