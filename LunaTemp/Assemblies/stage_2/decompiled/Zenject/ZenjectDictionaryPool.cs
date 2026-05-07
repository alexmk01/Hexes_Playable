using System;
using System.Collections.Generic;
using ModestTree;

namespace Zenject
{
	public class ZenjectDictionaryPool<TKey, TValue> : StaticMemoryPool<Dictionary<TKey, TValue>>
	{
		private static ZenjectDictionaryPool<TKey, TValue> _instance = new ZenjectDictionaryPool<TKey, TValue>();

		public static ZenjectDictionaryPool<TKey, TValue> Instance => _instance;

		public ZenjectDictionaryPool()
			: base((Action<Dictionary<TKey, TValue>>)null, (Action<Dictionary<TKey, TValue>>)null)
		{
			base.OnSpawnMethod = OnSpawned;
			base.OnDespawnedMethod = OnDespawned;
		}

		private static void OnSpawned(Dictionary<TKey, TValue> items)
		{
			Assert.That(items.IsEmpty());
		}

		private static void OnDespawned(Dictionary<TKey, TValue> items)
		{
			items.Clear();
		}
	}
}
