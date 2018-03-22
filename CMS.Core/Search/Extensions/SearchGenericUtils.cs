using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace CMS.Core.Search.Extensions
{
	public class SearchContentFieldInfo
	{
		public string Name;
		public string Value;
		public SearchContentFieldType FieldType;
		public bool IsResultField;
		public bool IsQueryField;
		public bool IsKeyField;
		public PropertyInfo PropertyInfo;
	}

	internal static class SearchGenericUtils
	{
		internal static SearchContentFieldInfo GetSearchContentKeyFieldInfo(Type t, object instance)
		{
			SearchContentFieldInfo[] keyFields =  ReflectionHelper.Instance.GetKeyFields(t,instance);
			
			if (keyFields.Length == 0)
				throw new Exception(String.Format("No key filed defined for type {0}!", t));
			
			if(keyFields.Length > 1)
				throw new Exception(String.Format("Only one key filed allowed for type {0}!", t));

			return keyFields[0];
		}

		internal static SearchContentFieldInfo GetSearchContentKeyFieldInfo(Type t)
		{
			return GetSearchContentKeyFieldInfo(t, null);
		}

		internal static IList<SearchContentFieldInfo> GetSearchContentFields(Type t, object instance)
		{
			if (instance == null)
				throw new Exception("Instance parameter is null!");

			return ReflectionHelper.Instance.GetFields(t, instance);
		}

		internal static string[] GetSearchContentQueryFields(Type t)
		{
			return ReflectionHelper.Instance.GetQueryFields(t);
		}

		internal static string[] GetSearchContentResultFields(Type t)
		{
			return ReflectionHelper.Instance.GetResultFields(t);
		}

		internal static void SetSearchResultField(object instance , string fieldName, object value)
		{

			if (instance == null)
				throw new Exception("Object instance parameter is null!");
			if (String.IsNullOrEmpty(fieldName))
				throw new Exception("Field name is empty!");

			ReflectionHelper.Instance.SetSearchResultField(fieldName, instance, value);	
		}


	}
}
