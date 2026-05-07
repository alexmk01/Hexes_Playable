using System;
using System.Linq;
using ModestTree;

namespace Zenject
{
	[NoReflectionBaking]
	public class ConditionCopyNonLazyBinder : CopyNonLazyBinder
	{
		public ConditionCopyNonLazyBinder(BindInfo bindInfo)
			: base(bindInfo)
		{
		}

		public CopyNonLazyBinder When(BindingCondition condition)
		{
			base.BindInfo.Condition = condition;
			return this;
		}

		public CopyNonLazyBinder WhenInjectedIntoInstance(object instance)
		{
			return When((InjectContext r) => r.ObjectInstance == instance);
		}

		public CopyNonLazyBinder WhenInjectedInto(params Type[] targets)
		{
			return When((InjectContext r) => targets.Where((Type x) => r.ObjectType != null && r.ObjectType.DerivesFromOrEqual(x)).Any());
		}

		public CopyNonLazyBinder WhenInjectedInto<T>()
		{
			return When((InjectContext r) => r.ObjectType != null && r.ObjectType.DerivesFromOrEqual(typeof(T)));
		}

		public CopyNonLazyBinder WhenNotInjectedInto<T>()
		{
			return When((InjectContext r) => r.ObjectType == null || !r.ObjectType.DerivesFromOrEqual(typeof(T)));
		}
	}
}
