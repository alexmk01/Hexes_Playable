namespace Zenject
{
	[NoReflectionBaking]
	public class MemoryPoolMaxSizeBinder<TContract> : MemoryPoolExpandBinder<TContract>
	{
		public MemoryPoolMaxSizeBinder(DiContainer bindContainer, BindInfo bindInfo, FactoryBindInfo factoryBindInfo, MemoryPoolBindInfo poolBindInfo)
			: base(bindContainer, bindInfo, factoryBindInfo, poolBindInfo)
		{
		}

		public MemoryPoolExpandBinder<TContract> WithMaxSize(int size)
		{
			base.MemoryPoolBindInfo.MaxSize = size;
			return this;
		}
	}
}
