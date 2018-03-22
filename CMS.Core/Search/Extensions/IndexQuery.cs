using System;
using System.Collections;

using Lucene.Net.Search;
using Lucene.Net.Store;
using Lucene.Net.QueryParsers;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;

namespace CMS.Core.Search.Extensions
{
	/// <summary>
	/// The IndexQuery class provides functionality to search the Full-Text index.
	/// </summary>
	public class IndexQueryEx<T>
	{
		private Directory _indexDirectory;

		/// <summary>
		/// Default constructor.
		/// <param name="physicalIndexDir">The physical directory where the search index resides.</param>
		/// </summary>
		public IndexQueryEx(string physicalIndexDir)
		{
			this._indexDirectory = FSDirectory.GetDirectory(physicalIndexDir, false);
		}

		/// <summary>
		/// Searches the index.
		/// </summary>
		/// <param name="queryText"></param>
		/// <param name="keywordFilter">A Hashtable where the key is the fieldname of the keyword and 
		/// the value the keyword itself.</param>
		/// <param name="pageIndex"></param>
		/// <param name="pageSize"></param>
		/// <returns></returns>
		public SearchResultCollection<T> Find(string queryText, Hashtable keywordFilter, int pageIndex, int pageSize)
		{
			long startTicks = DateTime.Now.Ticks;
			string[] qryFields = SearchGenericUtils.GetSearchContentQueryFields(typeof(T));
			if (qryFields.Length == 0)
				throw new Exception("No query field specified on target class!");

			Query query = MultiFieldQueryParser.Parse(queryText, qryFields, new StandardAnalyzer());
			IndexSearcher searcher = new IndexSearcher(this._indexDirectory);
			Hits hits;
			if (keywordFilter != null && keywordFilter.Count > 0)
			{
				QueryFilter qf = BuildQueryFilterFromKeywordFilter(keywordFilter);
				hits = searcher.Search(query, qf);
			}
			else
			{
				hits = searcher.Search(query);
			}
			int start = pageIndex * pageSize;
			int end = (pageIndex + 1) * pageSize;
			if (hits.Length() <= end)
			{
				end = hits.Length();
			}
			SearchResultCollection<T> results = new SearchResultCollection<T>();
			results.TotalCount = hits.Length();
			results.PageIndex = pageIndex;

			string[] resultFields = SearchGenericUtils.GetSearchContentResultFields(typeof(T));
			for (int i = start; i < end; i++)
			{
				T instance = Activator.CreateInstance<T>();
				for (int j = 0; j < resultFields.Length; j++)
				{
					SearchGenericUtils.SetSearchResultField(instance, resultFields[j], hits.Doc(i).Get(resultFields[j]));
				}

				if (instance is ISearchResultStat)
				{
					SearchGenericUtils.SetSearchResultField(instance, "Boost", hits.Doc(i).GetBoost());
					SearchGenericUtils.SetSearchResultField(instance, "Score", hits.Score(i));
				}
				results.Add(instance);
			}

			searcher.Close();
			results.ExecutionTime = DateTime.Now.Ticks - startTicks;
			return results;
		}

		private QueryFilter BuildQueryFilterFromKeywordFilter(Hashtable keywordFilter)
		{
			BooleanQuery bQuery = new BooleanQuery();
			foreach(DictionaryEntry keywordFilterTerm in keywordFilter)
			{
				string field = keywordFilterTerm.Key.ToString();
				string keyword = keywordFilterTerm.Value.ToString();
				bQuery.Add(new TermQuery(new Term(field, keyword)), true, false);
			}

			return new QueryFilter(bQuery);
		}
	}
}
