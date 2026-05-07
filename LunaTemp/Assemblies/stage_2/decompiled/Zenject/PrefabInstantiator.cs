using System;
using System.Collections.Generic;
using System.Linq;
using ModestTree;
using UnityEngine;
using Zenject.Internal;

namespace Zenject
{
	[NoReflectionBaking]
	public class PrefabInstantiator : IPrefabInstantiator
	{
		private readonly IPrefabProvider _prefabProvider;

		private readonly DiContainer _container;

		private readonly List<TypeValuePair> _extraArguments;

		private readonly GameObjectCreationParameters _gameObjectBindInfo;

		private readonly Type _argumentTarget;

		private readonly List<Type> _instantiateCallbackTypes;

		private readonly Action<InjectContext, object> _instantiateCallback;

		public GameObjectCreationParameters GameObjectCreationParameters => _gameObjectBindInfo;

		public Type ArgumentTarget => _argumentTarget;

		public List<TypeValuePair> ExtraArguments => _extraArguments;

		public PrefabInstantiator(DiContainer container, GameObjectCreationParameters gameObjectBindInfo, Type argumentTarget, IEnumerable<Type> instantiateCallbackTypes, IEnumerable<TypeValuePair> extraArguments, IPrefabProvider prefabProvider, Action<InjectContext, object> instantiateCallback)
		{
			_prefabProvider = prefabProvider;
			_extraArguments = extraArguments.ToList();
			_container = container;
			_gameObjectBindInfo = gameObjectBindInfo;
			_argumentTarget = argumentTarget;
			_instantiateCallbackTypes = instantiateCallbackTypes.ToList();
			_instantiateCallback = instantiateCallback;
		}

		public UnityEngine.Object GetPrefab()
		{
			return _prefabProvider.GetPrefab();
		}

		public GameObject Instantiate(InjectContext context, List<TypeValuePair> args, out Action injectAction)
		{
			Assert.That(_argumentTarget == null || _argumentTarget.DerivesFromOrEqual(context.MemberType));
			bool shouldMakeActive;
			GameObject gameObject = _container.CreateAndParentPrefab(GetPrefab(), _gameObjectBindInfo, context, out shouldMakeActive);
			Assert.IsNotNull(gameObject);
			injectAction = delegate
			{
				List<TypeValuePair> list = ZenPools.SpawnList<TypeValuePair>();
				list.AllocFreeAddRange(_extraArguments);
				list.AllocFreeAddRange(args);
				if (_argumentTarget == null)
				{
					Assert.That(list.IsEmpty(), "Unexpected arguments provided to prefab instantiator.  Arguments are not allowed if binding multiple components in the same binding");
				}
				if (_argumentTarget == null || list.IsEmpty())
				{
					_container.InjectGameObject(gameObject);
				}
				else
				{
					_container.InjectGameObjectForComponentExplicit(gameObject, _argumentTarget, list, context, null);
					Assert.That(list.Count == 0);
				}
				ZenPools.DespawnList(list);
				if (shouldMakeActive && !_container.IsValidating)
				{
					gameObject.SetActive(true);
				}
				if (_instantiateCallback != null)
				{
					HashSet<object> hashSet = ZenPools.SpawnHashSet<object>();
					foreach (Type current in _instantiateCallbackTypes)
					{
						Component componentInChildren = gameObject.GetComponentInChildren(current);
						if (componentInChildren != null)
						{
							hashSet.Add(componentInChildren);
						}
					}
					foreach (object current2 in hashSet)
					{
						_instantiateCallback(context, current2);
					}
					ZenPools.DespawnHashSet(hashSet);
				}
			};
			return gameObject;
		}
	}
}
