using System;

namespace Zenject
{
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property, AllowMultiple = false)]
	public class InjectOptionalAttribute : InjectAttributeBase
	{
		public InjectOptionalAttribute()
		{
			base.Optional = true;
		}
	}
}
