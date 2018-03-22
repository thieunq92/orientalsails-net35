using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Reflection.Emit;

namespace CMS.Core.Search.Extensions
{
	/// <summary>
	/// Singleton reflection helper
	/// </summary>
	public sealed class ReflectionHelper
	{
		#region Static Fields And Properties

		static ReflectionHelper _instance = null;
		static readonly object _padlock = new object();
		public static ReflectionHelper Instance
		{
			get
			{
				lock (_padlock)
				{
					if (_instance == null)
					{
						_instance = new ReflectionHelper();
					}
					return _instance;
				}
			}
		}
		
		#endregion Static Fields And Properties

		#region Instance Fields And Properties
		
		private Dictionary<Type, List<SearchContentFieldInfo>> _cache;
		
		#endregion Instance Fields And Properties

		#region CTOR

		private ReflectionHelper()
		{
			_cache = new Dictionary<Type, List<SearchContentFieldInfo>>();
		}

		#endregion //CTOR

		#region Reflection Helper Methods

		public SearchContentFieldInfo[] GetKeyFields(Type t)
		{
			if (!_cache.ContainsKey(t))
				AddTypeToCache(t);


			List<SearchContentFieldInfo> keyFields =  _cache[t].FindAll(
				delegate(SearchContentFieldInfo fi)
				{
					return fi.IsKeyField == true;
				});
			return keyFields.ToArray();
		}

		public SearchContentFieldInfo[] GetKeyFields(Type t, object instance)
		{
			SearchContentFieldInfo[] fields = GetKeyFields(t);
			if (instance == null)
				return fields;

			GenericGetter getMethod;
			for (int i = 0; i < fields.Length; i++)
			{
				getMethod = CreateGetMethod(fields[i].PropertyInfo);
				if (getMethod == null)
					throw new Exception(String.Format("Property \"{0}\" does not have getter!", fields[i].PropertyInfo.Name));

				object val = getMethod(instance);
				fields[i].Value = val == null ? String.Empty : val.ToString();
			}
			return fields;
		}

		public IList<SearchContentFieldInfo> GetFields(Type t, object instance)
		{
			if (!_cache.ContainsKey(t))
				AddTypeToCache(t);

			List<SearchContentFieldInfo>result = new List<SearchContentFieldInfo>(_cache[t].ToArray());

			if (instance == null)
				return result;
			
			GenericGetter getMethod;
			for (int i = 0; i < result.Count; i++)
			{
				SearchContentFieldInfo fi = result[i];
				getMethod = CreateGetMethod(fi.PropertyInfo);
				if (getMethod == null)
					throw new Exception(String.Format("Property \"{0}\" does not have getter!", fi.PropertyInfo.Name));
				
				object val = getMethod(instance);
				fi.Value = val == null ? String.Empty : val.ToString();
			}
			return result;
		}

		public string[] GetQueryFields(Type t)
		{
			if (!_cache.ContainsKey(t))
				AddTypeToCache(t);

			List < SearchContentFieldInfo > qryFields = _cache[t].FindAll(
				delegate(SearchContentFieldInfo fi)
				{
					return fi.IsQueryField == true;
				});

			
			List<string> result = qryFields.ConvertAll<string>(
				delegate(SearchContentFieldInfo fi) 
				{
					return fi.Name;
				});

			return result.ToArray();
		}

		public string[] GetResultFields(Type t)
		{
			if (!_cache.ContainsKey(t))
				AddTypeToCache(t);

			List<SearchContentFieldInfo> qryFields = _cache[t].FindAll(
				delegate(SearchContentFieldInfo fi)
				{
					return fi.IsResultField == true;
				});


			List<string> result = qryFields.ConvertAll<string>(
				delegate(SearchContentFieldInfo fi)
				{
					return fi.Name;
				});

			return result.ToArray();
		}

		public void SetSearchResultField(string fieldName, object instance, object value)
		{
			Type t = instance.GetType();
			if (!_cache.ContainsKey(t))
				AddTypeToCache(t);
			
			SearchContentFieldInfo field = _cache[t].Find(
			delegate(SearchContentFieldInfo fi)
			{
				return fi.Name == fieldName;
			});

			if (field.Name != fieldName)
				throw new Exception(String.Format("Field with name \"{0}\" not found on type \"{1}\"!", fieldName, t));

			GenericSetter setter = CreateSetMethod(field.PropertyInfo);
			if (setter == null)
				throw new Exception(String.Format("Property \"{0}\" does not have setter!", field.PropertyInfo.Name));

			setter(instance, Convert.ChangeType(value,field.PropertyInfo.PropertyType));
		}

		#endregion //Reflection Helper Methods

		#region Cache Operations
		private void AddTypeToCache(Type t)
		{
			if (_cache.ContainsKey(t))
				return;

			List<SearchContentFieldInfo> fields = new List<SearchContentFieldInfo>();

			PropertyInfo[] props = t.GetProperties(BindingFlags.Public | BindingFlags.Instance);
			for (int i = 0; i < props.Length; i++)
			{
				PropertyInfo pi = props[i];
				SearchContentFieldAttribute[] atts = (SearchContentFieldAttribute[])pi.GetCustomAttributes(typeof(SearchContentFieldAttribute), true);
				if (atts.Length > 0)
				{
					SearchContentFieldInfo fi = new SearchContentFieldInfo();
					fi.Name = pi.Name;
					fi.FieldType = atts[0].FieldType;
					fi.IsKeyField = atts[0].IsKeyField;
					fi.IsResultField = atts[0].IsResultField;
					fi.IsQueryField = atts[0].IsQueryField;
					fi.PropertyInfo = pi;
					fields.Add(fi);
				}
			}
			if (fields.Count > 0)
				_cache.Add(t, fields);
		}
		#endregion //Cache Operations

		#region Emit Getter/Setter

		/* Source for CreateSetMethod and CreateGetMethod taken from
		 * http://jachman.wordpress.com/2006/08/22/2000-faster-using-dynamic-method-calls/
		 */
		private GenericSetter CreateSetMethod(PropertyInfo propertyInfo)
		{
			/*
			* If there’s no setter return null
			*/
			MethodInfo setMethod = propertyInfo.GetSetMethod();
			if (setMethod == null)
				return null;

			/*
			* Create the dynamic method
			*/
			Type[] arguments = new Type[2];
			arguments[0] = arguments[1] = typeof(object);

			DynamicMethod setter = new DynamicMethod(
				String.Concat("_Set", propertyInfo.Name, "_"),
				typeof(void), arguments, propertyInfo.DeclaringType);
			ILGenerator generator = setter.GetILGenerator();
			generator.Emit(OpCodes.Ldarg_0);
			generator.Emit(OpCodes.Castclass, propertyInfo.DeclaringType);
			generator.Emit(OpCodes.Ldarg_1);

			if (propertyInfo.PropertyType.IsClass)
				generator.Emit(OpCodes.Castclass, propertyInfo.PropertyType);
			else
				generator.Emit(OpCodes.Unbox_Any, propertyInfo.PropertyType);

			generator.EmitCall(OpCodes.Callvirt, setMethod, null);
			generator.Emit(OpCodes.Ret);

			/*
			* Create the delegate and return it
			*/
			return (GenericSetter)setter.CreateDelegate(typeof(GenericSetter));
		}

		///
		/// Creates a dynamic getter for the property
		///
		private static GenericGetter CreateGetMethod(PropertyInfo propertyInfo)
		{
			/*
			* If there’s no getter return null
			*/
			MethodInfo getMethod = propertyInfo.GetGetMethod();
			if (getMethod == null)
				return null;

			/*
			* Create the dynamic method
			*/
			Type[] arguments = new Type[1];
			arguments[0] = typeof(object);

			DynamicMethod getter = new DynamicMethod(
				String.Concat("_Get", propertyInfo.Name, "_"),
				typeof(object), arguments, propertyInfo.DeclaringType);
			ILGenerator generator = getter.GetILGenerator();
			generator.DeclareLocal(typeof(object));
			generator.Emit(OpCodes.Ldarg_0);
			generator.Emit(OpCodes.Castclass, propertyInfo.DeclaringType);
			generator.EmitCall(OpCodes.Callvirt, getMethod, null);

			if (!propertyInfo.PropertyType.IsClass)
				generator.Emit(OpCodes.Box, propertyInfo.PropertyType);

			generator.Emit(OpCodes.Ret);

			/*
			* Create the delegate and return it
			*/
			return (GenericGetter)getter.CreateDelegate(typeof(GenericGetter));
		}
		#endregion //Emit Getter/Setter
	}

	public delegate void GenericSetter(object target, object value);
	public delegate object GenericGetter(object target);
}
