namespace Zenject
{
	[NoReflectionBaking]
	public class ConcreteIdBinderNonGeneric : ConcreteBinderNonGeneric
	{
		public ConcreteIdBinderNonGeneric(DiContainer bindContainer, BindInfo bindInfo, BindStatement bindStatement)
			: base(bindContainer, bindInfo, bindStatement)
		{
		}

		public ConcreteBinderNonGeneric WithId(object identifier)
		{
			base.BindInfo.Identifier = identifier;
			return this;
		}
	}
}
