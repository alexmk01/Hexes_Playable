namespace Zenject
{
	[NoReflectionBaking]
	public class MemoryPoolIdInitialSizeMaxSizeBinder<TContract> : MemoryPoolInitialSizeMaxSizeBinder<TContract>
	{
		public MemoryPoolIdInitialSizeMaxSizeBinder(DiContainer bindContainer, BindInfo bindInfo, FactoryBindInfo factoryBindInfo, MemoryPoolBindInfo poolBindInfo)
			: base(bindContainer, bindInfo, factoryBindInfo, poolBindInfo)
		{
		}

		public MemoryPoolInitialSizeMaxSizeBinder<TContract> WithId(object identifier)
		{
			base.BindInfo.Identifier = identifier;
			return this;
		}
	}
}
