using System;

namespace CMS.Core.Search.Extensions
{
	public delegate void IndexEventHandlerEx<T>(object sender, IndexEventArgsEx<T> e);

	public class IndexEventArgsEx<T>:EventArgs
	{
		private T _content;

		/// <summary>
		/// Property SearchContent (SearchContent)
		/// </summary>
		public T Content
		{
			get { return this._content; }
		}

		/// <summary>
		/// Create an IndexEventArgs class.
		/// </summary>
		/// <param name="searchContent">The content that needs to be moved around.</param>
		public IndexEventArgsEx(T content)
		{
			this._content = content;
		}
	}
}
