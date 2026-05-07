namespace Zenject
{
	[NoReflectionBaking]
	public class ConcreteIdArgConditionCopyNonLazyBinder : ArgConditionCopyNonLazyBinder
	{
		public ConcreteIdArgConditionCopyNonLazyBinder(BindInfo bindInfo)
			: base(bindInfo)
		{
		}

		public ArgConditionCopyNonLazyBinder WithConcreteId(object id)
		{
			base.BindInfo.ConcreteIdentifier = id;
			return this;
		}
	}
}
