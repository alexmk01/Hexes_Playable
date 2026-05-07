using System;

namespace Zenject
{
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Field | AttributeTargets.Method | AttributeTargets.Parameter | AttributeTargets.Property, AllowMultiple = false)]
	public class InjectAttribute : InjectAttributeBase
	{
	}
}
