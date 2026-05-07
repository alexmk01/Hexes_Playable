using System;

namespace Zenject
{
	public class PoolWrapperFactory<T> : IFactory<T>, IFactory where T : IDisposable
	{
		private readonly IMemoryPool<T> _pool;

		public PoolWrapperFactory(IMemoryPool<T> pool)
		{
			_pool = pool;
		}

		public T Create()
		{
			return _pool.Spawn();
		}
	}
	public class PoolWrapperFactory<TParam1, TValue> : IFactory<TParam1, TValue>, IFactory where TValue : IDisposable
	{
		private readonly IMemoryPool<TParam1, TValue> _pool;

		public PoolWrapperFactory(IMemoryPool<TParam1, TValue> pool)
		{
			_pool = pool;
		}

		public TValue Create(TParam1 arg)
		{
			return _pool.Spawn(arg);
		}
	}
}
