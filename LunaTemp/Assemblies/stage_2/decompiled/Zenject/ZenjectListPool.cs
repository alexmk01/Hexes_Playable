using System;
using System.Collections.Generic;

namespace Zenject
{
	public class ZenjectListPool<T> : StaticMemoryPool<List<T>>
	{
		private static ZenjectListPool<T> _instance = new ZenjectListPool<T>();

		public static ZenjectListPool<T> Instance => _instance;

		public ZenjectListPool()
			: base((Action<List<T>>)null, (Action<List<T>>)null)
		{
			base.OnDespawnedMethod = OnDespawned;
		}

		private void OnDespawned(List<T> list)
		{
			list.Clear();
		}
	}
}
