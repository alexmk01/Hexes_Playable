using System;
using System.Collections.Generic;
using ModestTree;

namespace Zenject
{
	public class ZenjectHashSetPool<T> : StaticMemoryPool<HashSet<T>>
	{
		private static ZenjectHashSetPool<T> _instance = new ZenjectHashSetPool<T>();

		public static ZenjectHashSetPool<T> Instance => _instance;

		public ZenjectHashSetPool()
			: base((Action<HashSet<T>>)null, (Action<HashSet<T>>)null)
		{
			base.OnSpawnMethod = OnSpawned;
			base.OnDespawnedMethod = OnDespawned;
		}

		private static void OnSpawned(HashSet<T> items)
		{
			Assert.That(items.IsEmpty());
		}

		private static void OnDespawned(HashSet<T> items)
		{
			items.Clear();
		}
	}
}
