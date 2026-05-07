namespace Zenject
{
	[NoReflectionBaking]
	public class ScopeConcreteIdArgConditionCopyNonLazyBinder : ConcreteIdArgConditionCopyNonLazyBinder
	{
		public ScopeConcreteIdArgConditionCopyNonLazyBinder(BindInfo bindInfo)
			: base(bindInfo)
		{
		}

		public ConcreteIdArgConditionCopyNonLazyBinder AsCached()
		{
			base.BindInfo.Scope = ScopeTypes.Singleton;
			return this;
		}

		public ConcreteIdArgConditionCopyNonLazyBinder AsSingle()
		{
			base.BindInfo.Scope = ScopeTypes.Singleton;
			base.BindInfo.MarkAsUniqueSingleton = true;
			return this;
		}

		public ConcreteIdArgConditionCopyNonLazyBinder AsTransient()
		{
			base.BindInfo.Scope = ScopeTypes.Transient;
			return this;
		}
	}
}
