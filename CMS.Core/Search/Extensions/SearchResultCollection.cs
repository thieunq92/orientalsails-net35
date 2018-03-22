using System;
using System.Collections;

namespace CMS.Core.Search.Extensions
{
	/// <summary>
	/// Summary description for SearchResultCollection.
	/// </summary>
	public class SearchResultCollection<T> : CollectionBase
	{
		private int _totalCount;
		private int _pageIndex;
		private long _executionTime;

		/// <summary>
		/// Property TotalCount (int)
		/// </summary>
		public int TotalCount
		{
			get { return this._totalCount; }
			set { this._totalCount = value; }
		}

		/// <summary>
		/// Property PageIndex (int)
		/// </summary>
		public int PageIndex
		{
			get { return this._pageIndex; }
			set { this._pageIndex = value; }
		}

		/// <summary>
		/// The execution time of the query in ticks.
		/// </summary>
		public long ExecutionTime
		{
			get { return this._executionTime; }
			set { this._executionTime = value; }
		}

		/// <summary>
		/// Indexer property.
		/// </summary>
		public T this[int index]
		{
			get { return (T)this.List[index]; }
		}

		/// <summary>
		/// 
		/// </summary>
		public SearchResultCollection()
		{
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="searchResult"></param>
		public void Add(T searchResult)
		{
			this.List.Add(searchResult);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="searchResult"></param>
		public void Remove(T searchResult)
		{
			this.List.Remove(searchResult);
		}
	}
}
