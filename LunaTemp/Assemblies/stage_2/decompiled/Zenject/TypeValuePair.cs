using System;
using System.Diagnostics;

namespace Zenject
{
	[DebuggerStepThrough]
	public struct TypeValuePair
	{
		public Type Type;

		public object Value;

		public TypeValuePair(Type type, object value)
		{
			Type = type;
			Value = value;
		}
	}
}
