using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ModestTree;
using UnityEngine;

namespace Zenject.Internal
{
	public static class ReflectionTypeAnalyzer
	{
		private static readonly HashSet<Type> _injectAttributeTypes;

		static ReflectionTypeAnalyzer()
		{
			_injectAttributeTypes = new HashSet<Type>();
			_injectAttributeTypes.Add(typeof(InjectAttributeBase));
		}

		public static void AddCustomInjectAttribute<T>() where T : Attribute
		{
			AddCustomInjectAttribute(typeof(T));
		}

		public static void AddCustomInjectAttribute(Type type)
		{
			Assert.That(type.DerivesFrom<Attribute>());
			_injectAttributeTypes.Add(type);
		}

		public static ReflectionTypeInfo GetReflectionInfo(Type type)
		{
			Assert.That(!type.IsEnum(), "Tried to analyze enum type '{0}'.  This is not supported", type);
			Assert.That(!type.IsArray, "Tried to analyze array type '{0}'.  This is not supported", type);
			Type baseType = type.BaseType();
			if (baseType == typeof(object))
			{
				baseType = null;
			}
			return new ReflectionTypeInfo(type, baseType, GetConstructorInfo(type), GetMethodInfos(type), GetFieldInfos(type), GetPropertyInfos(type));
		}

		private static List<ReflectionTypeInfo.InjectPropertyInfo> GetPropertyInfos(Type type)
		{
			return (from x in type.DeclaredInstanceProperties()
				where _injectAttributeTypes.Any((Type a) => x.HasAttribute(a))
				select new ReflectionTypeInfo.InjectPropertyInfo(x, GetInjectableInfoForMember(type, x))).ToList();
		}

		private static List<ReflectionTypeInfo.InjectFieldInfo> GetFieldInfos(Type type)
		{
			return (from x in type.DeclaredInstanceFields()
				where _injectAttributeTypes.Any((Type a) => x.HasAttribute(a))
				select new ReflectionTypeInfo.InjectFieldInfo(x, GetInjectableInfoForMember(type, x))).ToList();
		}

		private static List<ReflectionTypeInfo.InjectMethodInfo> GetMethodInfos(Type type)
		{
			List<ReflectionTypeInfo.InjectMethodInfo> injectMethodInfos = new List<ReflectionTypeInfo.InjectMethodInfo>();
			List<MethodInfo> methodInfos = (from x in type.DeclaredInstanceMethods()
				where _injectAttributeTypes.Any((Type a) => x.GetCustomAttributes(a, false).Any())
				select x).ToList();
			for (int i = 0; i < methodInfos.Count; i++)
			{
				MethodInfo methodInfo = methodInfos[i];
				InjectAttributeBase injectAttr = methodInfo.AllAttributes<InjectAttributeBase>().SingleOrDefault();
				if (injectAttr != null)
				{
					Assert.That(!injectAttr.Optional && injectAttr.Id == null && injectAttr.Source == InjectSources.Any, "Parameters of InjectAttribute do not apply to constructors and methodInfos");
				}
				List<ReflectionTypeInfo.InjectParameterInfo> injectParamInfos = (from x in methodInfo.GetParameters()
					select CreateInjectableInfoForParam(type, x)).ToList();
				injectMethodInfos.Add(new ReflectionTypeInfo.InjectMethodInfo(methodInfo, injectParamInfos));
			}
			return injectMethodInfos;
		}

		private static ReflectionTypeInfo.InjectConstructorInfo GetConstructorInfo(Type type)
		{
			List<ReflectionTypeInfo.InjectParameterInfo> args = new List<ReflectionTypeInfo.InjectParameterInfo>();
			ConstructorInfo constructor = TryGetInjectConstructor(type);
			if (constructor != null)
			{
				args.AddRange(from x in constructor.GetParameters()
					select CreateInjectableInfoForParam(type, x));
			}
			return new ReflectionTypeInfo.InjectConstructorInfo(constructor, args);
		}

		private static ReflectionTypeInfo.InjectParameterInfo CreateInjectableInfoForParam(Type parentType, ParameterInfo paramInfo)
		{
			List<InjectAttributeBase> injectAttributes = paramInfo.AllAttributes<InjectAttributeBase>().ToList();
			Assert.That(injectAttributes.Count <= 1, "Found multiple 'Inject' attributes on type parameter '{0}' of type '{1}'.  Parameter should only have one", paramInfo.Name, parentType);
			InjectAttributeBase injectAttr = injectAttributes.SingleOrDefault();
			object identifier = null;
			bool isOptional = false;
			InjectSources sourceType = InjectSources.Any;
			if (injectAttr != null)
			{
				identifier = injectAttr.Id;
				isOptional = injectAttr.Optional;
				sourceType = injectAttr.Source;
			}
			bool isOptionalWithADefaultValue = (paramInfo.Attributes & ParameterAttributes.HasDefault) == ParameterAttributes.HasDefault;
			return new ReflectionTypeInfo.InjectParameterInfo(paramInfo, new InjectableInfo(isOptionalWithADefaultValue || isOptional, identifier, paramInfo.Name, paramInfo.ParameterType, isOptionalWithADefaultValue ? paramInfo.DefaultValue : null, sourceType));
		}

		private static InjectableInfo GetInjectableInfoForMember(Type parentType, MemberInfo memInfo)
		{
			List<InjectAttributeBase> injectAttributes = memInfo.AllAttributes<InjectAttributeBase>().ToList();
			Assert.That(injectAttributes.Count <= 1, "Found multiple 'Inject' attributes on type field '{0}' of type '{1}'.  Field should only container one Inject attribute", memInfo.Name, parentType);
			InjectAttributeBase injectAttr = injectAttributes.SingleOrDefault();
			object identifier = null;
			bool isOptional = false;
			InjectSources sourceType = InjectSources.Any;
			if (injectAttr != null)
			{
				identifier = injectAttr.Id;
				isOptional = injectAttr.Optional;
				sourceType = injectAttr.Source;
			}
			Type memberType = ((memInfo is FieldInfo) ? ((FieldInfo)memInfo).FieldType : ((PropertyInfo)memInfo).PropertyType);
			return new InjectableInfo(isOptional, identifier, memInfo.Name, memberType, null, sourceType);
		}

		private static ConstructorInfo TryGetInjectConstructor(Type type)
		{
			if (type.DerivesFromOrEqual<Component>())
			{
				return null;
			}
			if (type.IsAbstract())
			{
				return null;
			}
			ConstructorInfo[] constructors = type.Constructors();
			if (constructors.IsEmpty())
			{
				return null;
			}
			if (constructors.HasMoreThan(1))
			{
				ConstructorInfo explicitConstructor = constructors.Where((ConstructorInfo c) => _injectAttributeTypes.Any((Type a) => c.HasAttribute(a))).SingleOrDefault();
				if (explicitConstructor != null)
				{
					return explicitConstructor;
				}
				ConstructorInfo singlePublicConstructor = constructors.Where((ConstructorInfo x) => x.IsPublic).OnlyOrDefault();
				if (singlePublicConstructor != null)
				{
					return singlePublicConstructor;
				}
				return constructors.OrderBy((ConstructorInfo x) => x.GetParameters().Count()).First();
			}
			return constructors[0];
		}
	}
}
