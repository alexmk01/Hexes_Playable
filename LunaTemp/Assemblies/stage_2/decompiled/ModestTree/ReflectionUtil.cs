using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using ModestTree.Util;

namespace ModestTree
{
	public static class ReflectionUtil
	{
		public static Array CreateArray(Type elementType, List<object> instances)
		{
			Array array = Array.CreateInstance(elementType, instances.Count);
			for (int i = 0; i < instances.Count; i++)
			{
				object instance = instances[i];
				if (instance != null)
				{
					Assert.That(instance.GetType().DerivesFromOrEqual(elementType), "Wrong type when creating array, expected something assignable from '" + elementType?.ToString() + "', but found '" + instance.GetType()?.ToString() + "'");
				}
				array.SetValue(instance, i);
			}
			return array;
		}

		public static IList CreateGenericList(Type elementType, List<object> instances)
		{
			Type genericType = typeof(List<>).MakeGenericType(elementType);
			IList list = (IList)Activator.CreateInstance(genericType);
			for (int i = 0; i < instances.Count; i++)
			{
				object instance = instances[i];
				if (instance != null)
				{
					Assert.That(instance.GetType().DerivesFromOrEqual(elementType), "Wrong type when creating generic list, expected something assignable from '" + elementType?.ToString() + "', but found '" + instance.GetType()?.ToString() + "'");
				}
				list.Add(instance);
			}
			return list;
		}

		public static string ToDebugString(this MethodInfo method)
		{
			return "{0}.{1}".Fmt(method.DeclaringType.PrettyName(), method.Name);
		}

		public static string ToDebugString(this Action action)
		{
			return action.Method.ToDebugString();
		}

		public static string ToDebugString<TParam1>(this Action<TParam1> action)
		{
			return action.Method.ToDebugString();
		}

		public static string ToDebugString<TParam1, TParam2>(this Action<TParam1, TParam2> action)
		{
			return action.Method.ToDebugString();
		}

		public static string ToDebugString<TParam1, TParam2, TParam3>(this Action<TParam1, TParam2, TParam3> action)
		{
			return action.Method.ToDebugString();
		}

		public static string ToDebugString<TParam1, TParam2, TParam3, TParam4>(this Action<TParam1, TParam2, TParam3, TParam4> action)
		{
			return action.Method.ToDebugString();
		}

		public static string ToDebugString<TParam1, TParam2, TParam3, TParam4, TParam5>(this ModestTree.Util.Action<TParam1, TParam2, TParam3, TParam4, TParam5> action)
		{
			return action.Method.ToDebugString();
		}

		public static string ToDebugString<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>(this ModestTree.Util.Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6> action)
		{
			return action.Method.ToDebugString();
		}

		public static string ToDebugString<TParam1>(this Func<TParam1> func)
		{
			return func.Method.ToDebugString();
		}

		public static string ToDebugString<TParam1, TParam2>(this Func<TParam1, TParam2> func)
		{
			return func.Method.ToDebugString();
		}

		public static string ToDebugString<TParam1, TParam2, TParam3>(this Func<TParam1, TParam2, TParam3> func)
		{
			return func.Method.ToDebugString();
		}

		public static string ToDebugString<TParam1, TParam2, TParam3, TParam4>(this Func<TParam1, TParam2, TParam3, TParam4> func)
		{
			return func.Method.ToDebugString();
		}
	}
}
