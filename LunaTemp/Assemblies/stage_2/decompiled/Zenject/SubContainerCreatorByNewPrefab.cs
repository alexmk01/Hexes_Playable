using System.Collections.Generic;
using ModestTree;
using UnityEngine;

namespace Zenject
{
	[NoReflectionBaking]
	public class SubContainerCreatorByNewPrefab : ISubContainerCreator
	{
		private readonly GameObjectCreationParameters _gameObjectBindInfo;

		private readonly IPrefabProvider _prefabProvider;

		private readonly DiContainer _container;

		public SubContainerCreatorByNewPrefab(DiContainer container, IPrefabProvider prefabProvider, GameObjectCreationParameters gameObjectBindInfo)
		{
			_gameObjectBindInfo = gameObjectBindInfo;
			_prefabProvider = prefabProvider;
			_container = container;
		}

		public DiContainer CreateSubContainer(List<TypeValuePair> args, InjectContext parentContext)
		{
			Assert.That(args.IsEmpty());
			Object prefab = _prefabProvider.GetPrefab();
			GameObject gameObject = _container.InstantiatePrefab(prefab, _gameObjectBindInfo);
			GameObjectContext context = gameObject.GetComponent<GameObjectContext>();
			Assert.That(context != null, "Expected prefab with name '{0}' to container a component of type 'GameObjectContext'", prefab.name);
			return context.Container;
		}
	}
}
