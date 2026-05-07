using ModestTree;
using UnityEngine;

namespace Zenject
{
	[NoReflectionBaking]
	public class PrefabProvider : IPrefabProvider
	{
		private readonly Object _prefab;

		public PrefabProvider(Object prefab)
		{
			Assert.IsNotNull(prefab);
			_prefab = prefab;
		}

		public Object GetPrefab()
		{
			return _prefab;
		}
	}
}
