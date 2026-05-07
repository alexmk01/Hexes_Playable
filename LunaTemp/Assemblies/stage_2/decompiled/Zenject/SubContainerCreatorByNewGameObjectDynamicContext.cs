using UnityEngine;

namespace Zenject
{
	[NoReflectionBaking]
	public abstract class SubContainerCreatorByNewGameObjectDynamicContext : SubContainerCreatorDynamicContext
	{
		private readonly GameObjectCreationParameters _gameObjectBindInfo;

		public SubContainerCreatorByNewGameObjectDynamicContext(DiContainer container, GameObjectCreationParameters gameObjectBindInfo)
			: base(container)
		{
			_gameObjectBindInfo = gameObjectBindInfo;
		}

		protected override GameObject CreateGameObject(out bool shouldMakeActive)
		{
			shouldMakeActive = true;
			GameObject gameObject = base.Container.CreateEmptyGameObject(_gameObjectBindInfo, null);
			gameObject.SetActive(false);
			return gameObject;
		}
	}
}
