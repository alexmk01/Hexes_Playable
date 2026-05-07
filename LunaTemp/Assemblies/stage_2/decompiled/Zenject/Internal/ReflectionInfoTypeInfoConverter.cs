using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ModestTree;
using UnityEngine;

namespace Zenject.Internal
{
	public static class ReflectionInfoTypeInfoConverter
	{
		public static InjectTypeInfo.InjectMethodInfo ConvertMethod(ReflectionTypeInfo.InjectMethodInfo injectMethod)
		{
			MethodInfo methodInfo = injectMethod.MethodInfo;
			ZenInjectMethod action = TryCreateActionForMethod(methodInfo);
			if (action == null)
			{
				action = delegate(object obj, object[] args)
				{
					methodInfo.Invoke(obj, args);
				};
			}
			return new InjectTypeInfo.InjectMethodInfo(action, injectMethod.Parameters.Select((ReflectionTypeInfo.InjectParameterInfo x) => x.InjectableInfo).ToArray(), methodInfo.Name);
		}

		public static InjectTypeInfo.InjectConstructorInfo ConvertConstructor(ReflectionTypeInfo.InjectConstructorInfo injectConstructor, Type type)
		{
			return new InjectTypeInfo.InjectConstructorInfo(TryCreateFactoryMethod(type, injectConstructor), injectConstructor.Parameters.Select((ReflectionTypeInfo.InjectParameterInfo x) => x.InjectableInfo).ToArray());
		}

		public static InjectTypeInfo.InjectMemberInfo ConvertField(Type parentType, ReflectionTypeInfo.InjectFieldInfo injectField)
		{
			return new InjectTypeInfo.InjectMemberInfo(GetSetter(parentType, injectField.FieldInfo), injectField.InjectableInfo);
		}

		public static InjectTypeInfo.InjectMemberInfo ConvertProperty(Type parentType, ReflectionTypeInfo.InjectPropertyInfo injectProperty)
		{
			return new InjectTypeInfo.InjectMemberInfo(GetSetter(parentType, injectProperty.PropertyInfo), injectProperty.InjectableInfo);
		}

		private static ZenFactoryMethod TryCreateFactoryMethod(Type type, ReflectionTypeInfo.InjectConstructorInfo reflectionInfo)
		{
			if (type.DerivesFromOrEqual<Component>())
			{
				return null;
			}
			if (type.IsAbstract())
			{
				Assert.That(reflectionInfo.Parameters.IsEmpty());
				return null;
			}
			ConstructorInfo constructor = reflectionInfo.ConstructorInfo;
			ZenFactoryMethod factoryMethod = TryCreateFactoryMethodCompiledLambdaExpression(type, constructor);
			if (factoryMethod == null)
			{
				factoryMethod = ((!(constructor == null)) ? new ZenFactoryMethod(constructor.Invoke) : ((ZenFactoryMethod)delegate(object[] args)
				{
					Assert.That(args.Length == 0);
					return Activator.CreateInstance(type, new object[0]);
				}));
			}
			return factoryMethod;
		}

		private static ZenFactoryMethod TryCreateFactoryMethodCompiledLambdaExpression(Type type, ConstructorInfo constructor)
		{
			return null;
		}

		private static ZenInjectMethod TryCreateActionForMethod(MethodInfo methodInfo)
		{
			return null;
		}

		private static IEnumerable<FieldInfo> GetAllFields(Type t, BindingFlags flags)
		{
			if (t == null)
			{
				return Enumerable.Empty<FieldInfo>();
			}
			return t.GetFields(flags).Concat(GetAllFields(t.BaseType, flags)).Distinct();
		}

		private static ZenMemberSetterMethod GetOnlyPropertySetter(Type parentType, string propertyName)
		{
			Assert.That(parentType != null);
			Assert.That(!string.IsNullOrEmpty(propertyName));
			List<FieldInfo> allFields = GetAllFields(parentType, BindingFlags.FlattenHierarchy | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).ToList();
			List<FieldInfo> writeableFields = allFields.Where((FieldInfo f) => f.Name == string.Format("<" + propertyName + ">k__BackingField", propertyName)).ToList();
			if (!writeableFields.Any())
			{
				throw new ZenjectException(string.Format("Can't find backing field for get only property {0} on {1}.\r\n{2}", propertyName, parentType.FullName, string.Join(";", allFields.Select((FieldInfo f) => f.Name).ToArray())));
			}
			return delegate(object injectable, object value)
			{
				writeableFields.ForEach(delegate(FieldInfo f)
				{
					f.SetValue(injectable, value);
				});
			};
		}

		private static ZenMemberSetterMethod GetSetter(Type parentType, MemberInfo memInfo)
		{
			ZenMemberSetterMethod setterMethod = TryGetSetterAsCompiledExpression(parentType, memInfo);
			if (setterMethod != null)
			{
				return setterMethod;
			}
			FieldInfo fieldInfo = memInfo as FieldInfo;
			PropertyInfo propInfo = memInfo as PropertyInfo;
			if (fieldInfo != null)
			{
				return delegate(object injectable, object value)
				{
					fieldInfo.SetValue(injectable, value);
				};
			}
			Assert.IsNotNull(propInfo);
			if (propInfo.CanWrite)
			{
				return delegate(object injectable, object value)
				{
					propInfo.SetValue(injectable, value, null);
				};
			}
			return GetOnlyPropertySetter(parentType, propInfo.Name);
		}

		private static ZenMemberSetterMethod TryGetSetterAsCompiledExpression(Type parentType, MemberInfo memInfo)
		{
			return null;
		}
	}
}
