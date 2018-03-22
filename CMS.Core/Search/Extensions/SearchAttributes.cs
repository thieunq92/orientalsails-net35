using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace CMS.Core.Search.Extensions
{

	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public class SearchContentFieldAttribute : Attribute
	{
		private SearchContentFieldType _fieldType;
		/// <summary>
		/// Default is Text
		/// </summary>
		public SearchContentFieldType FieldType
		{
			get { return _fieldType; }
			set { _fieldType = value; }
		}

		private bool _isResultField = true;
		/// <summary>
		/// Default is true
		/// </summary>
		public bool IsResultField
		{
			get { return _isResultField; }
			set { _isResultField = value; }
		}

		private bool _isQueryField = false;
		/// <summary>
		/// Default is false
		/// </summary>
		public bool IsQueryField
		{
			get { return _isQueryField; }
			set { _isQueryField = value; }
		}

		private bool _isKeyField = false;
		/// <summary>
		/// Default is false
		/// </summary>
		public bool IsKeyField
		{
			get { return _isKeyField; }
			set { _isKeyField = value; }
		}

		public SearchContentFieldAttribute(SearchContentFieldType fieldType)
		{
			_fieldType = fieldType;
		}

	}

	public enum SearchContentFieldType
	{
		Text,
		UnStored,
		UnIndexed,
		Keyword
	}
}
