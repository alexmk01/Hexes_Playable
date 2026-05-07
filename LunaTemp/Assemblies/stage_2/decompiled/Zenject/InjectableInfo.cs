using System;

namespace Zenject
{
	[NoReflectionBaking]
	public class InjectableInfo
	{
		public readonly bool Optional;

		public readonly object Identifier;

		public readonly InjectSources SourceType;

		public readonly string MemberName;

		public readonly Type MemberType;

		public readonly object DefaultValue;

		public InjectableInfo(bool optional, object identifier, string memberName, Type memberType, object defaultValue, InjectSources sourceType)
		{
			Optional = optional;
			MemberType = memberType;
			MemberName = memberName;
			Identifier = identifier;
			DefaultValue = defaultValue;
			SourceType = sourceType;
		}
	}
}
