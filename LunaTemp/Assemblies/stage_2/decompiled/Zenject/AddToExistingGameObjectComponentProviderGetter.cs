using System;
using System.Collections.Generic;
using ModestTree;
using UnityEngine;

namespace Zenject
{
	[NoReflectionBaking]
	public class AddToExistingGameObjectComponentProviderGetter : AddToGameObjectComponentProviderBase
	{
		private readonly Func<InjectContext, GameObject> _gameObjectGetter;

		protected override bool ShouldToggleActive => false;

		public AddToExistingGameObjectComponentProviderGetter(Func<InjectContext, GameObject> gameObjectGetter, DiContainer container, Type componentType, List<TypeValuePair> extraArguments, object concreteIdentifier, Action<InjectContext, object> instantiateCallback)
			: base(container, componentType, extraArguments, concreteIdentifier, instantiateCallback)
		{
			_gameObjectGetter = gameObjectGetter;
		}

		protected override GameObject GetGameObject(InjectContext context)
		{
			GameObject gameObj = _gameObjectGetter(context);
			Assert.IsNotNull(gameObj, "Provided Func<InjectContext, GameObject> returned null value for game object when using FromComponentOn");
			return gameObj;
		}
	}
}
