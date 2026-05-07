using System;
using System.Collections.Generic;

namespace Zenject
{
	public class ZenjectArrayPool<T> : StaticMemoryPoolBaseBase<T[]>
	{
		private readonly int _length;

		private static readonly Dictionary<int, ZenjectArrayPool<T>> _pools = new Dictionary<int, ZenjectArrayPool<T>>();

		public ZenjectArrayPool(int length)
			: base((Action<T[]>)OnDespawned)
		{
			_length = length;
		}

		private static void OnDespawned(T[] arr)
		{
			for (int i = 0; i < arr.Length; i++)
			{
				arr[i] = default(T);
			}
		}

		public T[] Spawn()
		{
			return SpawnInternal();
		}

		protected override T[] Alloc()
		{
			return new T[_length];
		}

		public static ZenjectArrayPool<T> GetPool(int length)
		{
			if (!_pools.TryGetValue(length, out var pool))
			{
				pool = new ZenjectArrayPool<T>(length);
				_pools.Add(length, pool);
			}
			return pool;
		}
	}
}
