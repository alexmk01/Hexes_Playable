using System;
using UnityEngine;

namespace Zenject
{
	public static class FactoryFromBinder4Extensions
	{
		public static ArgConditionCopyNonLazyBinder FromIFactory<TParam1, TParam2, TParam3, TParam4, TContract>(this FactoryFromBinder<TParam1, TParam2, TParam3, TParam4, TContract> fromBinder, Action<ConcreteBinderGeneric<IFactory<TParam1, TParam2, TParam3, TParam4, TContract>>> factoryBindGenerator)
		{
			factoryBindGenerator(fromBinder.CreateIFactoryBinder<IFactory<TParam1, TParam2, TParam3, TParam4, TContract>>(out var factoryId));
			fromBinder.ProviderFunc = (DiContainer container) => new IFactoryProvider<TParam1, TParam2, TParam3, TParam4, TContract>(container, factoryId);
			return new ArgConditionCopyNonLazyBinder(fromBinder.BindInfo);
		}

		public static ArgConditionCopyNonLazyBinder FromPoolableMemoryPool<TParam1, TParam2, TParam3, TParam4, TContract>(this FactoryFromBinder<TParam1, TParam2, TParam3, TParam4, TContract> fromBinder) where TContract : IPoolable<TParam1, TParam2, TParam3, TParam4, IMemoryPool>
		{
			return fromBinder.FromPoolableMemoryPool(delegate
			{
			});
		}

		public static ArgConditionCopyNonLazyBinder FromPoolableMemoryPool<TParam1, TParam2, TParam3, TParam4, TContract>(this FactoryFromBinder<TParam1, TParam2, TParam3, TParam4, TContract> fromBinder, Action<MemoryPoolInitialSizeMaxSizeBinder<TContract>> poolBindGenerator) where TContract : IPoolable<TParam1, TParam2, TParam3, TParam4, IMemoryPool>
		{
			return fromBinder.FromPoolableMemoryPool<TParam1, TParam2, TParam3, TParam4, TContract, PoolableMemoryPool<TParam1, TParam2, TParam3, TParam4, IMemoryPool, TContract>>(poolBindGenerator);
		}

		public static ArgConditionCopyNonLazyBinder FromMonoPoolableMemoryPool<TParam1, TParam2, TParam3, TParam4, TContract>(this FactoryFromBinder<TParam1, TParam2, TParam3, TParam4, TContract> fromBinder) where TContract : Component, IPoolable<TParam1, TParam2, TParam3, TParam4, IMemoryPool>
		{
			return fromBinder.FromMonoPoolableMemoryPool(delegate
			{
			});
		}

		public static ArgConditionCopyNonLazyBinder FromMonoPoolableMemoryPool<TParam1, TParam2, TParam3, TParam4, TContract>(this FactoryFromBinder<TParam1, TParam2, TParam3, TParam4, TContract> fromBinder, Action<MemoryPoolInitialSizeMaxSizeBinder<TContract>> poolBindGenerator) where TContract : Component, IPoolable<TParam1, TParam2, TParam3, TParam4, IMemoryPool>
		{
			return fromBinder.FromPoolableMemoryPool<TParam1, TParam2, TParam3, TParam4, TContract, MonoPoolableMemoryPool<TParam1, TParam2, TParam3, TParam4, IMemoryPool, TContract>>(poolBindGenerator);
		}

		public static ArgConditionCopyNonLazyBinder FromPoolableMemoryPool<TParam1, TParam2, TParam3, TParam4, TContract, TMemoryPool>(this FactoryFromBinder<TParam1, TParam2, TParam3, TParam4, TContract> fromBinder) where TContract : IPoolable<TParam1, TParam2, TParam3, TParam4, IMemoryPool> where TMemoryPool : MemoryPool<TParam1, TParam2, TParam3, TParam4, IMemoryPool, TContract>
		{
			return fromBinder.FromPoolableMemoryPool<TParam1, TParam2, TParam3, TParam4, TContract, TMemoryPool>(delegate
			{
			});
		}

		public static ArgConditionCopyNonLazyBinder FromPoolableMemoryPool<TParam1, TParam2, TParam3, TParam4, TContract, TMemoryPool>(this FactoryFromBinder<TParam1, TParam2, TParam3, TParam4, TContract> fromBinder, Action<MemoryPoolInitialSizeMaxSizeBinder<TContract>> poolBindGenerator) where TContract : IPoolable<TParam1, TParam2, TParam3, TParam4, IMemoryPool> where TMemoryPool : MemoryPool<TParam1, TParam2, TParam3, TParam4, IMemoryPool, TContract>
		{
			Guid poolId = Guid.NewGuid();
			MemoryPoolInitialSizeMaxSizeBinder<TContract> binder = fromBinder.BindContainer.BindMemoryPoolCustomInterfaceNoFlush<TContract, TMemoryPool, TMemoryPool>().WithId(poolId);
			binder.NonLazy();
			poolBindGenerator(binder);
			fromBinder.ProviderFunc = (DiContainer container) => new PoolableMemoryPoolProvider<TParam1, TParam2, TParam3, TParam4, TContract, TMemoryPool>(container, poolId);
			return new ArgConditionCopyNonLazyBinder(fromBinder.BindInfo);
		}
	}
}
