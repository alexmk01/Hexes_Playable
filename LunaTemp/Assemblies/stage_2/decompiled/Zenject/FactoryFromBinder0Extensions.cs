using System;
using UnityEngine;

namespace Zenject
{
	public static class FactoryFromBinder0Extensions
	{
		public static ArgConditionCopyNonLazyBinder FromPoolableMemoryPool<TContract, TMemoryPool>(this FactoryFromBinder<TContract> fromBinder, Action<MemoryPoolInitialSizeMaxSizeBinder<TContract>> poolBindGenerator) where TContract : IPoolable<IMemoryPool> where TMemoryPool : MemoryPool<IMemoryPool, TContract>
		{
			Guid poolId = Guid.NewGuid();
			MemoryPoolInitialSizeMaxSizeBinder<TContract> binder = fromBinder.BindContainer.BindMemoryPoolCustomInterfaceNoFlush<TContract, TMemoryPool, TMemoryPool>().WithId(poolId);
			binder.NonLazy();
			poolBindGenerator(binder);
			fromBinder.ProviderFunc = (DiContainer container) => new PoolableMemoryPoolProvider<TContract, TMemoryPool>(container, poolId);
			return new ArgConditionCopyNonLazyBinder(fromBinder.BindInfo);
		}

		public static ArgConditionCopyNonLazyBinder FromPoolableMemoryPool<TContract>(this FactoryFromBinder<TContract> fromBinder) where TContract : IPoolable<IMemoryPool>
		{
			return fromBinder.FromPoolableMemoryPool(delegate
			{
			});
		}

		public static ArgConditionCopyNonLazyBinder FromPoolableMemoryPool<TContract>(this FactoryFromBinder<TContract> fromBinder, Action<MemoryPoolInitialSizeMaxSizeBinder<TContract>> poolBindGenerator) where TContract : IPoolable<IMemoryPool>
		{
			return fromBinder.FromPoolableMemoryPool<TContract, PoolableMemoryPool<IMemoryPool, TContract>>(poolBindGenerator);
		}

		public static ArgConditionCopyNonLazyBinder FromMonoPoolableMemoryPool<TContract>(this FactoryFromBinder<TContract> fromBinder) where TContract : Component, IPoolable<IMemoryPool>
		{
			return fromBinder.FromMonoPoolableMemoryPool(delegate
			{
			});
		}

		public static ArgConditionCopyNonLazyBinder FromMonoPoolableMemoryPool<TContract>(this FactoryFromBinder<TContract> fromBinder, Action<MemoryPoolInitialSizeMaxSizeBinder<TContract>> poolBindGenerator) where TContract : Component, IPoolable<IMemoryPool>
		{
			return fromBinder.FromPoolableMemoryPool<TContract, MonoPoolableMemoryPool<IMemoryPool, TContract>>(poolBindGenerator);
		}

		public static ArgConditionCopyNonLazyBinder FromPoolableMemoryPool<TContract, TMemoryPool>(this FactoryFromBinder<TContract> fromBinder) where TContract : IPoolable<IMemoryPool> where TMemoryPool : MemoryPool<IMemoryPool, TContract>
		{
			return fromBinder.FromPoolableMemoryPool<TContract, TMemoryPool>(delegate
			{
			});
		}

		public static ArgConditionCopyNonLazyBinder FromIFactory<TContract>(this FactoryFromBinder<TContract> fromBinder, Action<ConcreteBinderGeneric<IFactory<TContract>>> factoryBindGenerator)
		{
			factoryBindGenerator(fromBinder.CreateIFactoryBinder<IFactory<TContract>>(out var factoryId));
			fromBinder.ProviderFunc = (DiContainer container) => new IFactoryProvider<TContract>(container, factoryId);
			return new ArgConditionCopyNonLazyBinder(fromBinder.BindInfo);
		}
	}
}
