using System;
using System.Collections.Generic;
using UnityEngine;

namespace Zenject
{
	[NoReflectionBaking]
	public class AddToExistingGameObjectComponentProvider : AddToGameObjectComponentProviderBase
	{
		private readonly GameObject _gameObject;

		protected override bool ShouldToggleActive => false;

		public AddToExistingGameObjectComponentProvider(GameObject gameObject, DiContainer container, Type componentType, IEnumerable<TypeValuePair> extraArguments, object concreteIdentifier, Action<InjectContext, object> instantiateCallback)
			: base(container, componentType, extraArguments, concreteIdentifier, instantiateCallback)
		{
			_gameObject = gameObject;
		}

		protected override GameObject GetGameObject(InjectContext context)
		{
			return _gameObject;
		}
	}
}
