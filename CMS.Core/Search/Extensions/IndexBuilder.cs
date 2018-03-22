using System;
using System.Collections.Generic;
using Lucene.Net.Index;
using Lucene.Net.Documents;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Store;
using log4net;

using CMS.Core.Util;
using CMS.Core.Domain;

namespace CMS.Core.Search.Extensions
{
	/// <summary>
	/// The IndexBuilder class takes care of maintaining the fulltext index.
	/// </summary>
	public class IndexBuilderEx<T> : IDisposable
	{
        private static readonly ILog log = LogManager.GetLogger("CMS.Core.Search.Extensions.IndexBuilderEx");

		private Directory _indexDirectory;
		private IndexWriter _indexWriter;
		private bool _isClosed = false;
		private bool _rebuildIndex;

		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <param name="physicalIndexDir">Location of the index files.</param>
		/// <param name="rebuildIndex">Flag to indicate if the index should be rebuilt. 
		/// NOTE: you can not update or delete content when rebuilding the index.
		/// </param>
		public IndexBuilderEx(string physicalIndexDir, bool rebuildIndex)
		{
			this._indexDirectory = FSDirectory.GetDirectory(physicalIndexDir, false);
			this._rebuildIndex = rebuildIndex;

			InitIndexWriter();

			log.Info("IndexBuilder created.");
		}

		/// <summary>
		/// Add content to be indexed.
		/// </summary>
		/// <param name="searchContent"></param>
		public void AddContent(T searchContent)
		{
			if (this._indexWriter == null)
			{
				InitIndexWriter();
			}
			this._indexWriter.AddDocument(BuildDocumentFromSearchContent(searchContent));
		}

		/// <summary>
		/// Update existing content in the search index.
		/// </summary>
		/// <param name="searchContent"></param>
		public void UpdateContent(T searchContent)
		{
			if (this._rebuildIndex)
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
		public void DeleteContent(T searchContent)
		{
			if (this._rebuildIndex)
			{
				throw new InvalidOperationException("Cannot delete documents when rebuilding the index.");
			}
			else
			{
				this._indexWriter.Close();
				this._indexWriter = null;

				// The SearchContentKey uniquely identifies a document in the index.
				SearchContentFieldInfo ki = SearchGenericUtils.GetSearchContentKeyFieldInfo(typeof(T), searchContent);
				if (String.IsNullOrEmpty(ki.Name))
					throw new Exception("SearchContentKey Field not specified on target class!");

				Term term = new Term(ki.Name, ki.Value);
				IndexReader rdr = IndexReader.Open(this._indexDirectory);
				rdr.Delete(term);
				rdr.Close();
			}
		}

		public void UpdateContentFromModule(ModuleBase module)
		{
			ISearchableEx<T> searchableModule = module as ISearchableEx<T>;
			if (searchableModule != null)
			{
				T[] searchContentList = searchableModule.GetAllSearchableContent();
				foreach (T searchContent in searchContentList)
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
			if (! this._isClosed && this._indexWriter != null)
			{
				this._indexWriter.Optimize();
				this._indexWriter.Close();
				this._isClosed = true; 
				log.Info("New or updated search index written to disk.");
			}
		}

		private void InitIndexWriter()
		{
			if (! IndexReader.IndexExists(this._indexDirectory) || this._rebuildIndex)
			{
				this._indexWriter = new IndexWriter(this._indexDirectory, new StandardAnalyzer(), true);
			}
			else
			{
				this._indexWriter = new IndexWriter(this._indexDirectory, new StandardAnalyzer(), false);
			}
		}

		private Document BuildDocumentFromSearchContent(T searchContent)
		{
			Document doc = new Document();
			IList<SearchContentFieldInfo> fields = SearchGenericUtils.GetSearchContentFields(typeof(T), searchContent);
			for(int i = 0; i < fields.Count ; i++)
			{
				SearchContentFieldInfo fi = fields[i];
				switch (fi.FieldType)
				{
					case SearchContentFieldType.Text:
						doc.Add(Field.Text(fi.Name, fi.Value));
						break;
					case SearchContentFieldType.UnStored:
						doc.Add(Field.UnStored(fi.Name, fi.Value));
						break;
					case SearchContentFieldType.UnIndexed:
						doc.Add(Field.UnIndexed(fi.Name, fi.Value));
						break;
					case SearchContentFieldType.Keyword:
						doc.Add(Field.Keyword(fi.Name, fi.Value));
						break;
					default:
						break;
				}
			}
			return doc;
		}

		#region IDisposable Members

		public void Dispose()
		{
			Close();
		}

		#endregion
	}
}
