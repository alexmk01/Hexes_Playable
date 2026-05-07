using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ModestTree;
using Zenject.Internal;

namespace Zenject
{
	public static class TypeAnalyzer
	{
		private static Dictionary<Type, InjectTypeInfo> _typeInfo = new Dictionary<Type, InjectTypeInfo>();

		private static Dictionary<Type, bool> _allowDuringValidation = new Dictionary<Type, bool>();

		public const string ReflectionBakingGetInjectInfoMethodName = "__zenCreateInjectTypeInfo";

		public const string ReflectionBakingFactoryMethodName = "__zenCreate";

		public const string ReflectionBakingInjectMethodPrefix = "__zenInjectMethod";

		public const string ReflectionBakingFieldSetterPrefix = "__zenFieldSetter";

		public const string ReflectionBakingPropertySetterPrefix = "__zenPropertySetter";

		public static ReflectionBakingCoverageModes ReflectionBakingCoverageMode { get; set; }

		public static bool ShouldAllowDuringValidation<T>()
		{
			return ShouldAllowDuringValidation(typeof(T));
		}

		public static bool ShouldAllowDuringValidation(Type type)
		{
			if (!_allowDuringValidation.TryGetValue(type, out var shouldAllow))
			{
				shouldAllow = ShouldAllowDuringValidationInternal(type);
				_allowDuringValidation.Add(type, shouldAllow);
			}
			return shouldAllow;
		}

		private static bool ShouldAllowDuringValidationInternal(Type type)
		{
			if (type.DerivesFrom<IInstaller>() || type.DerivesFrom<IValidatable>())
			{
				return true;
			}
			if (type.DerivesFrom<Context>())
			{
				return true;
			}
			return type.HasAttribute<ZenjectAllowDuringValidationAttribute>();
		}

		public static bool HasInfo<T>()
		{
			return HasInfo(typeof(T));
		}

		public static bool HasInfo(Type type)
		{
			return TryGetInfo(type) != null;
		}

		public static InjectTypeInfo GetInfo<T>()
		{
			return GetInfo(typeof(T));
		}

		public static InjectTypeInfo GetInfo(Type type)
		{
			InjectTypeInfo info = TryGetInfo(type);
			Assert.IsNotNull(info, "Unable to get type info for type '{0}'", type);
			return info;
		}

		public static InjectTypeInfo TryGetInfo<T>()
		{
			return TryGetInfo(typeof(T));
		}

		public static InjectTypeInfo TryGetInfo(Type type)
		{
			if (_typeInfo.TryGetValue(type, out var info))
			{
				return info;
			}
			info = GetInfoInternal(type);
			if (info != null)
			{
				Assert.IsEqual(info.Type, type);
				Assert.IsNull(info.BaseTypeInfo);
				Type baseType = type.BaseType();
				if (baseType != null && !ShouldSkipTypeAnalysis(baseType))
				{
					info.BaseTypeInfo = TryGetInfo(baseType);
				}
			}
			_typeInfo.Add(type, info);
			return info;
		}

		private static InjectTypeInfo GetInfoInternal(Type type)
		{
			if (ShouldSkipTypeAnalysis(type))
			{
				return null;
			}
			MethodInfo getInfoMethod = type.GetMethod("__zenCreateInjectTypeInfo", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
			if (getInfoMethod != null)
			{
				ZenTypeInfoGetter infoGetter = (ZenTypeInfoGetter)Delegate.CreateDelegate(typeof(ZenTypeInfoGetter), getInfoMethod);
				return infoGetter();
			}
			if (ReflectionBakingCoverageMode == ReflectionBakingCoverageModes.NoCheckAssumeFullCoverage)
			{
				return null;
			}
			if (ReflectionBakingCoverageMode == ReflectionBakingCoverageModes.FallbackToDirectReflectionWithWarning)
			{
				Log.Warn("No reflection baking information found for type '{0}' - using more costly direct reflection instead", type);
			}
			return CreateTypeInfoFromReflection(type);
		}

		public static bool ShouldSkipTypeAnalysis(Type type)
		{
			return type == null || type.IsEnum() || type.IsArray || type.IsInterface() || type.ContainsGenericParameters() || IsStaticType(type) || type == typeof(object);
		}

		private static bool IsStaticType(Type type)
		{
			return type.IsAbstract() && type.IsSealed();
		}

		private static InjectTypeInfo CreateTypeInfoFromReflection(Type type)
		{
			ReflectionTypeInfo reflectionInfo = ReflectionTypeAnalyzer.GetReflectionInfo(type);
			InjectTypeInfo.InjectConstructorInfo injectConstructor = ReflectionInfoTypeInfoConverter.ConvertConstructor(reflectionInfo.InjectConstructor, type);
			InjectTypeInfo.InjectMethodInfo[] injectMethods = reflectionInfo.InjectMethods.Select(ReflectionInfoTypeInfoConverter.ConvertMethod).ToArray();
			InjectTypeInfo.InjectMemberInfo[] memberInfos = reflectionInfo.InjectFields.Select((ReflectionTypeInfo.InjectFieldInfo x) => ReflectionInfoTypeInfoConverter.ConvertField(type, x)).Concat(reflectionInfo.InjectProperties.Select((ReflectionTypeInfo.InjectPropertyInfo x) => ReflectionInfoTypeInfoConverter.ConvertProperty(type, x))).ToArray();
			return new InjectTypeInfo(type, injectConstructor, injectMethods, memberInfos);
		}
	}
}
