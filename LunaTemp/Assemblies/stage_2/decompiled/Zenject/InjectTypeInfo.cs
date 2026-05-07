using System;
using System.Collections.Generic;
using System.Linq;

namespace Zenject
{
	[NoReflectionBaking]
	public class InjectTypeInfo
	{
		[NoReflectionBaking]
		public class InjectMemberInfo
		{
			public readonly ZenMemberSetterMethod Setter;

			public readonly InjectableInfo Info;

			public InjectMemberInfo(ZenMemberSetterMethod setter, InjectableInfo info)
			{
				Setter = setter;
				Info = info;
			}
		}

		[NoReflectionBaking]
		public class InjectConstructorInfo
		{
			public readonly ZenFactoryMethod Factory;

			public readonly InjectableInfo[] Parameters;

			public InjectConstructorInfo(ZenFactoryMethod factory, InjectableInfo[] parameters)
			{
				Parameters = parameters;
				Factory = factory;
			}
		}

		[NoReflectionBaking]
		public class InjectMethodInfo
		{
			public readonly string Name;

			public readonly ZenInjectMethod Action;

			public readonly InjectableInfo[] Parameters;

			public InjectMethodInfo(ZenInjectMethod action, InjectableInfo[] parameters, string name)
			{
				Parameters = parameters;
				Action = action;
				Name = name;
			}
		}

		public readonly Type Type;

		public readonly InjectMethodInfo[] InjectMethods;

		public readonly InjectMemberInfo[] InjectMembers;

		public readonly InjectConstructorInfo InjectConstructor;

		public InjectTypeInfo BaseTypeInfo { get; set; }

		public IEnumerable<InjectableInfo> AllInjectables => InjectConstructor.Parameters.Concat(InjectMembers.Select((InjectMemberInfo x) => x.Info)).Concat(InjectMethods.SelectMany((InjectMethodInfo x) => x.Parameters));

		public InjectTypeInfo(Type type, InjectConstructorInfo injectConstructor, InjectMethodInfo[] injectMethods, InjectMemberInfo[] injectMembers)
		{
			Type = type;
			InjectMethods = injectMethods;
			InjectMembers = injectMembers;
			InjectConstructor = injectConstructor;
		}
	}
}
