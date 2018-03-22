using System;
using CMS.Core.Domain;
using log4net;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Store;

namespace CMS.Core.Search
{
    /// <summary>
    /// The IndexBuilder class takes care of maintaining the fulltext index.
    /// </summary>
    public class IndexBuilder : IDisposable
    {
        private static readonly ILog log = LogManager.GetLogger(typeof (IndexBuilder));

        private readonly Directory _indexDirectory;
        private readonly bool _rebuildIndex;
        private IndexWriter _indexWriter;
        private bool _isClosed;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="physicalIndexDir">Location of the index files.</param>
        /// <param name="rebuildIndex">Flag to indicate if the index should be rebuilt. 
        /// NOTE: you can not update or delete content when rebuilding the index.
        /// </param>
        public IndexBuilder(string physicalIndexDir, bool rebuildIndex)
        {
            _indexDirectory = FSDirectory.GetDirectory(physicalIndexDir, false);
            _rebuildIndex = rebuildIndex;

            InitIndexWriter();

            log.Info("IndexBuilder created.");
        }

        #region IDisposable Members

        public void Dispose()
        {
            Close();
        }

        #endregion

        /// <summary>
        /// Add content to be indexed.
        /// </summary>
        /// <param name="searchContent"></param>
        public void AddContent(SearchContent searchContent)
        {
            if (_indexWriter == null)
            {
                InitIndexWriter();
            }
            _indexWriter.AddDocument(BuildDocumentFromSearchContent(searchContent));
        }

        /// <summary>
        /// Update existing content in the search index.
        /// </summary>
        /// <param name="searchContent"></param>
        public void UpdateContent(SearchContent searchContent)
        {
            if (_rebuildIndex)
            {
                throw new InvalidOperationException("Cannot update documents when rebuilding the index.");
            }
            else
            {
                // First delete the old content
                DeleteContent(searchContent);
                // Now add the content again
                AddContent(searchContent);
            }
        }

        /// <summary>
        /// Delete existing content from the search index.
        /// </summary>
        /// <param name="searchContent"></param>
        public void DeleteContent(SearchContent searchContent)
        {
            if (_rebuildIndex)
            {
                throw new InvalidOperationException("Cannot delete documents when rebuilding the index.");
            }
            else
            {
                _indexWriter.Close();
                _indexWriter = null;

                // The path uniquely identifies a document in the index.
                Term term = new Term("path", searchContent.Path);
                IndexReader rdr = IndexReader.Open(_indexDirectory);
                rdr.Delete(term);
                rdr.Close();
            }
        }

        public void UpdateContentFromModule(ModuleBase module)
        {
            ISearchable searchableModule = module as ISearchable;
            if (searchableModule != null)
            {
                SearchContent[] searchContentList = searchableModule.GetAllSearchableContent();
                foreach (SearchContent searchContent in searchContentList)
                {
                    UpdateContent(searchContent);
                }
            }
        }

        /// <summary>
        /// Close the IndexWriter (saves the index).
        /// </summary>
        public void Close()
        {
            if (! _isClosed && _indexWriter != null)
            {
                _indexWriter.Optimize();
                _indexWriter.Close();
                _isClosed = true;
                log.Info("New or updated search index written to disk.");
            }
        }

        private void InitIndexWriter()
        {
            if (! IndexReader.IndexExists(_indexDirectory) || _rebuildIndex)
            {
                _indexWriter = new IndexWriter(_indexDirectory, new StandardAnalyzer(), true);
            }
            else
            {
                _indexWriter = new IndexWriter(_indexDirectory, new StandardAnalyzer(), false);
            }
        }

        private Document BuildDocumentFromSearchContent(SearchContent searchContent)
        {
            Document doc = new Document();
            doc.Add(Field.Text("title", searchContent.Title));
            doc.Add(Field.Text("summary", searchContent.Summary));
            doc.Add(Field.UnStored("contents", searchContent.Contents));
            doc.Add(Field.Text("author", searchContent.Author));
            doc.Add(Field.Keyword("moduletype", searchContent.ModuleType));
            doc.Add(Field.Keyword("path", searchContent.Path));
            doc.Add(Field.Keyword("category", searchContent.Category));
            doc.Add(Field.Keyword("site", searchContent.Site));
            doc.Add(Field.Keyword("datecreated", searchContent.DateCreated.ToString("u")));
            doc.Add(Field.Keyword("datemodified", searchContent.DateModified.ToString("u")));
            doc.Add(Field.UnIndexed("sectionid", searchContent.SectionId.ToString()));

            return doc;
        }
    }
}