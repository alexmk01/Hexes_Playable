namespace Zenject
{
	[NoReflectionBaking]
	public class MemoryPoolInitialSizeMaxSizeBinder<TContract> : MemoryPoolMaxSizeBinder<TContract>
	{
		public MemoryPoolInitialSizeMaxSizeBinder(DiContainer bindContainer, BindInfo bindInfo, FactoryBindInfo factoryBindInfo, MemoryPoolBindInfo poolBindInfo)
			: base(bindContainer, bindInfo, factoryBindInfo, poolBindInfo)
		{
		}

		public MemoryPoolMaxSizeBinder<TContract> WithInitialSize(int size)
		{
			base.MemoryPoolBindInfo.InitialSize = size;
			return this;
		}

		public FactoryArgumentsToChoiceBinder<TContract> WithFixedSize(int size)
		{
			base.MemoryPoolBindInfo.InitialSize = size;
			base.MemoryPoolBindInfo.MaxSize = size;
			base.MemoryPoolBindInfo.ExpandMethod = PoolExpandMethods.Disabled;
			return this;
		}
	}
}
