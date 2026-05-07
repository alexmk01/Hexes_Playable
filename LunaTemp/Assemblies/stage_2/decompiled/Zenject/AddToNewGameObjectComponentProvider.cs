using System;
using System.Collections.Generic;
using UnityEngine;

namespace Zenject
{
	[NoReflectionBaking]
	public class AddToNewGameObjectComponentProvider : AddToGameObjectComponentProviderBase
	{
		private readonly GameObjectCreationParameters _gameObjectBindInfo;

		protected override bool ShouldToggleActive => true;

		public AddToNewGameObjectComponentProvider(DiContainer container, Type componentType, IEnumerable<TypeValuePair> extraArguments, GameObjectCreationParameters gameObjectBindInfo, object concreteIdentifier, Action<InjectContext, object> instantiateCallback)
			: base(container, componentType, extraArguments, concreteIdentifier, instantiateCallback)
		{
			_gameObjectBindInfo = gameObjectBindInfo;
		}

		protected override GameObject GetGameObject(InjectContext context)
		{
			if (_gameObjectBindInfo.Name == null)
			{
				_gameObjectBindInfo.Name = base.ComponentType.Name;
			}
			return base.Container.CreateEmptyGameObject(_gameObjectBindInfo, context);
		}
	}
}
