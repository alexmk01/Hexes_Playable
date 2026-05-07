using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ModestTree
{
	public static class TypeExtensions
	{
		private static readonly Dictionary<Type, bool> _isClosedGenericType = new Dictionary<Type, bool>();

		private static readonly Dictionary<Type, bool> _isOpenGenericType = new Dictionary<Type, bool>();

		private static readonly Dictionary<Type, bool> _isValueType = new Dictionary<Type, bool>();

		private static readonly Dictionary<Type, Type[]> _interfaces = new Dictionary<Type, Type[]>();

		public static bool DerivesFrom<T>(this Type a)
		{
			return a.DerivesFrom(typeof(T));
		}

		public static bool DerivesFrom(this Type a, Type b)
		{
			return b != a && a.DerivesFromOrEqual(b);
		}

		public static bool DerivesFromOrEqual<T>(this Type a)
		{
			return a.DerivesFromOrEqual(typeof(T));
		}

		public static bool DerivesFromOrEqual(this Type a, Type b)
		{
			return b == a || b.IsAssignableFrom(a);
		}

		public static bool IsAssignableToGenericType(Type givenType, Type genericType)
		{
			Type[] interfaceTypes = givenType.Interfaces();
			Type[] array = interfaceTypes;
			foreach (Type it in array)
			{
				if (it.IsGenericType && it.GetGenericTypeDefinition() == genericType)
				{
					return true;
				}
			}
			if (givenType.IsGenericType && givenType.GetGenericTypeDefinition() == genericType)
			{
				return true;
			}
			Type baseType = givenType.BaseType;
			if (baseType == null)
			{
				return false;
			}
			return IsAssignableToGenericType(baseType, genericType);
		}

		public static bool IsEnum(this Type type)
		{
			return type.IsEnum;
		}

		public static bool IsValueType(this Type type)
		{
			if (!_isValueType.TryGetValue(type, out var result))
			{
				result = type.IsValueType;
				_isValueType[type] = result;
			}
			return result;
		}

		public static MethodInfo[] DeclaredInstanceMethods(this Type type)
		{
			return type.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
		}

		public static PropertyInfo[] DeclaredInstanceProperties(this Type type)
		{
			return type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
		}

		public static FieldInfo[] DeclaredInstanceFields(this Type type)
		{
			return type.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
		}

		public static Type BaseType(this Type type)
		{
			return type.BaseType;
		}

		public static bool IsGenericType(this Type type)
		{
			return type.IsGenericType;
		}

		public static bool IsGenericTypeDefinition(this Type type)
		{
			return type.IsGenericTypeDefinition;
		}

		public static bool IsPrimitive(this Type type)
		{
			return type.IsPrimitive;
		}

		public static bool IsInterface(this Type type)
		{
			return type.IsInterface;
		}

		public static bool ContainsGenericParameters(this Type type)
		{
			return type.ContainsGenericParameters;
		}

		public static bool IsAbstract(this Type type)
		{
			return type.IsAbstract;
		}

		public static bool IsSealed(this Type type)
		{
			return type.IsSealed;
		}

		public static MethodInfo Method(this Delegate del)
		{
			return del.Method;
		}

		public static Type[] GenericArguments(this Type type)
		{
			return type.GetGenericArguments();
		}

		public static Type[] Interfaces(this Type type)
		{
			if (!_interfaces.TryGetValue(type, out var result))
			{
				result = type.GetInterfaces();
				_interfaces.Add(type, result);
			}
			return result;
		}

		public static ConstructorInfo[] Constructors(this Type type)
		{
			return type.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
		}

		public static object GetDefaultValue(this Type type)
		{
			if (type.IsValueType())
			{
				return Activator.CreateInstance(type);
			}
			return null;
		}

		public static bool IsClosedGenericType(this Type type)
		{
			if (!_isClosedGenericType.TryGetValue(type, out var result))
			{
				result = type.IsGenericType() && type != type.GetGenericTypeDefinition();
				_isClosedGenericType[type] = result;
			}
			return result;
		}

		public static IEnumerable<Type> GetParentTypes(this Type type)
		{
			if (type == null || type.BaseType() == null || type == typeof(object) || type.BaseType() == typeof(object))
			{
				yield break;
			}
			yield return type.BaseType();
			foreach (Type parentType in type.BaseType().GetParentTypes())
			{
				yield return parentType;
			}
		}

		public static bool IsOpenGenericType(this Type type)
		{
			if (!_isOpenGenericType.TryGetValue(type, out var result))
			{
				result = type.IsGenericType() && type == type.GetGenericTypeDefinition();
				_isOpenGenericType[type] = result;
			}
			return result;
		}

		public static T GetAttribute<T>(this MemberInfo provider) where T : Attribute
		{
			return provider.AllAttributes<T>().Single();
		}

		public static T TryGetAttribute<T>(this MemberInfo provider) where T : Attribute
		{
			return provider.AllAttributes<T>().OnlyOrDefault();
		}

		public static bool HasAttribute(this MemberInfo provider, params Type[] attributeTypes)
		{
			return provider.AllAttributes(attributeTypes).Any();
		}

		public static bool HasAttribute<T>(this MemberInfo provider) where T : Attribute
		{
			return provider.AllAttributes(typeof(T)).Any();
		}

		public static IEnumerable<T> AllAttributes<T>(this MemberInfo provider) where T : Attribute
		{
			return provider.AllAttributes(typeof(T)).Cast<T>();
		}

		public static IEnumerable<Attribute> AllAttributes(this MemberInfo provider, params Type[] attributeTypes)
		{
			Attribute[] allAttributes = Attribute.GetCustomAttributes(provider, typeof(Attribute), true);
			if (attributeTypes.Length == 0)
			{
				return allAttributes;
			}
			return allAttributes.Where((Attribute a) => attributeTypes.Any((Type x) => a.GetType().DerivesFromOrEqual(x)));
		}

		public static bool HasAttribute(this ParameterInfo provider, params Type[] attributeTypes)
		{
			return provider.AllAttributes(attributeTypes).Any();
		}

		public static bool HasAttribute<T>(this ParameterInfo provider) where T : Attribute
		{
			return provider.AllAttributes(typeof(T)).Any();
		}

		public static IEnumerable<T> AllAttributes<T>(this ParameterInfo provider) where T : Attribute
		{
			return provider.AllAttributes(typeof(T)).Cast<T>();
		}

		public static IEnumerable<Attribute> AllAttributes(this ParameterInfo provider, params Type[] attributeTypes)
		{
			Attribute[] allAttributes = Attribute.GetCustomAttributes(provider, typeof(Attribute), true);
			if (attributeTypes.Length == 0)
			{
				return allAttributes;
			}
			return allAttributes.Where((Attribute a) => attributeTypes.Any((Type x) => a.GetType().DerivesFromOrEqual(x)));
		}
	}
}
