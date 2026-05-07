using System.Collections.Generic;
using Zenject;

namespace Game.Infrastructure
{
	public static class ListPool<T>
	{
		public static DisposeBlock Get(out List<T> list)
		{
			DisposeBlock block = DisposeBlock.Spawn();
			list = block.SpawnList<T>();
			return block;
		}

		public static List<T> Get()
		{
			return ZenjectListPool<T>.Instance.Spawn();
		}

		public static void Release(List<T> list)
		{
			ZenjectListPool<T>.Instance.Despawn(list);
		}
	}
}
