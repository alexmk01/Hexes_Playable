using System;

namespace Zenject
{
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property, AllowMultiple = false)]
	public class InjectLocalAttribute : InjectAttributeBase
	{
		public InjectLocalAttribute()
		{
			base.Source = InjectSources.Local;
		}
	}
}
