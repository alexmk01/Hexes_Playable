using ModestTree;
using UnityEngine;

namespace Zenject
{
	[NoReflectionBaking]
	public abstract class SubContainerCreatorByNewPrefabDynamicContext : SubContainerCreatorDynamicContext
	{
		private readonly IPrefabProvider _prefabProvider;

		private readonly GameObjectCreationParameters _gameObjectBindInfo;

		public SubContainerCreatorByNewPrefabDynamicContext(DiContainer container, IPrefabProvider prefabProvider, GameObjectCreationParameters gameObjectBindInfo)
			: base(container)
		{
			_prefabProvider = prefabProvider;
			_gameObjectBindInfo = gameObjectBindInfo;
		}

		protected override GameObject CreateGameObject(out bool shouldMakeActive)
		{
			Object prefab = _prefabProvider.GetPrefab();
			GameObject gameObj = base.Container.CreateAndParentPrefab(prefab, _gameObjectBindInfo, null, out shouldMakeActive);
			if (gameObj.GetComponent<GameObjectContext>() != null)
			{
				throw Assert.CreateException("Found GameObjectContext already attached to prefab with name '{0}'!  When using ByNewPrefabMethod or ByNewPrefabInstaller, the GameObjectContext is added to the prefab dynamically", prefab.name);
			}
			return gameObj;
		}
	}
}
