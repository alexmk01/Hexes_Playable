using System;

namespace Zenject
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false)]
	public class ZenjectAllowDuringValidationAttribute : Attribute
	{
	}
}
