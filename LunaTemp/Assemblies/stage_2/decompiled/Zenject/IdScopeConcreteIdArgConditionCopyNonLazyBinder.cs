namespace Zenject
{
	[NoReflectionBaking]
	public class IdScopeConcreteIdArgConditionCopyNonLazyBinder : ScopeConcreteIdArgConditionCopyNonLazyBinder
	{
		public IdScopeConcreteIdArgConditionCopyNonLazyBinder(BindInfo bindInfo)
			: base(bindInfo)
		{
		}

		public ScopeConcreteIdArgConditionCopyNonLazyBinder WithId(object identifier)
		{
			base.BindInfo.Identifier = identifier;
			return this;
		}
	}
}
