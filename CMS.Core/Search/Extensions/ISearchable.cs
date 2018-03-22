using System;

namespace CMS.Core.Search.Extensions
{
	/// <summary>
	/// The ISearchable interface defines the contract for modules that need to have their content indexed.
	/// </summary>
	public interface ISearchableEx<T>
	{
		event IndexEventHandlerEx<T> ContentCreated;
		event IndexEventHandlerEx<T> ContentUpdated;
		event IndexEventHandlerEx<T> ContentDeleted;

		T[] GetAllSearchableContent();
	}

	public interface ISearchResultStat
	{
		float Boost{get;set;}
		float Score{ get;set;}
	}
}
