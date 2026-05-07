using System;
using System.Collections.Generic;
using ModestTree;
using UnityEngine;

namespace Zenject
{
	[NoReflectionBaking]
	public class PrefabInstantiatorCached : IPrefabInstantiator
	{
		private readonly IPrefabInstantiator _subInstantiator;

		private GameObject _gameObject;

		public List<TypeValuePair> ExtraArguments => _subInstantiator.ExtraArguments;

		public Type ArgumentTarget => _subInstantiator.ArgumentTarget;

		public GameObjectCreationParameters GameObjectCreationParameters => _subInstantiator.GameObjectCreationParameters;

		public PrefabInstantiatorCached(IPrefabInstantiator subInstantiator)
		{
			_subInstantiator = subInstantiator;
		}

		public UnityEngine.Object GetPrefab()
		{
			return _subInstantiator.GetPrefab();
		}

		public GameObject Instantiate(InjectContext context, List<TypeValuePair> args, out Action injectAction)
		{
			Assert.IsEmpty(args);
			if (_gameObject != null)
			{
				injectAction = null;
				return _gameObject;
			}
			_gameObject = _subInstantiator.Instantiate(context, new List<TypeValuePair>(), out injectAction);
			return _gameObject;
		}
	}
}
