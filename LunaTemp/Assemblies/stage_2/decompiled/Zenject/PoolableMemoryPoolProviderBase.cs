using System;
using System.Collections.Generic;

namespace Zenject
{
	public abstract class PoolableMemoryPoolProviderBase<TContract> : IProvider
	{
		public bool IsCached => false;

		protected Guid PoolId { get; private set; }

		protected DiContainer Container { get; private set; }

		public bool TypeVariesBasedOnMemberType => false;

		public PoolableMemoryPoolProviderBase(DiContainer container, Guid poolId)
		{
			Container = container;
			PoolId = poolId;
		}

		public Type GetInstanceType(InjectContext context)
		{
			return typeof(TContract);
		}

		public abstract void GetAllInstancesWithInjectSplit(InjectContext context, List<TypeValuePair> args, out Action injectAction, List<object> buffer);
	}
}
