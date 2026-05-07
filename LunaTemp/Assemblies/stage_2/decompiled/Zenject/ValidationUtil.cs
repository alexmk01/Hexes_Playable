using System;
using System.Collections.Generic;
using System.Linq;
using ModestTree;

namespace Zenject
{
	public static class ValidationUtil
	{
		public static List<TypeValuePair> CreateDefaultArgs(params Type[] argTypes)
		{
			return argTypes.Select((Type x) => new TypeValuePair(x, x.GetDefaultValue())).ToList();
		}
	}
}
