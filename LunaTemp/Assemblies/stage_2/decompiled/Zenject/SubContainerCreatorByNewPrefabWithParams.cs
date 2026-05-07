using System;
using System.Collections.Generic;
using System.Linq;
using ModestTree;
using UnityEngine;
using Zenject.Internal;

namespace Zenject
{
	[NoReflectionBaking]
	public class SubContainerCreatorByNewPrefabWithParams : ISubContainerCreator
	{
		private readonly DiContainer _container;

		private readonly IPrefabProvider _prefabProvider;

		private readonly Type _installerType;

		private readonly GameObjectCreationParameters _gameObjectBindInfo;

		protected DiContainer Container => _container;

		public SubContainerCreatorByNewPrefabWithParams(Type installerType, DiContainer container, IPrefabProvider prefabProvider, GameObjectCreationParameters gameObjectBindInfo)
		{
			_gameObjectBindInfo = gameObjectBindInfo;
			_prefabProvider = prefabProvider;
			_container = container;
			_installerType = installerType;
		}

		private DiContainer CreateTempContainer(List<TypeValuePair> args)
		{
			DiContainer tempSubContainer = Container.CreateSubContainer();
			InjectTypeInfo installerInjectables = TypeAnalyzer.GetInfo(_installerType);
			foreach (TypeValuePair argPair in args)
			{
				InjectableInfo match = (from x in installerInjectables.AllInjectables
					where argPair.Type.DerivesFromOrEqual(x.MemberType)
					orderby ZenUtilInternal.GetInheritanceDelta(argPair.Type, x.MemberType)
					select x).FirstOrDefault();
				Assert.That(match != null, "Could not find match for argument type '{0}' when injecting into sub container installer '{1}'", argPair.Type, _installerType);
				tempSubContainer.Bind(match.MemberType).FromInstance(argPair.Value).WhenInjectedInto(_installerType);
			}
			return tempSubContainer;
		}

		public DiContainer CreateSubContainer(List<TypeValuePair> args, InjectContext parentContext)
		{
			Assert.That(!args.IsEmpty());
			UnityEngine.Object prefab = _prefabProvider.GetPrefab();
			GameObject gameObject = CreateTempContainer(args).InstantiatePrefab(prefab, _gameObjectBindInfo);
			GameObjectContext context = gameObject.GetComponent<GameObjectContext>();
			Assert.That(context != null, "Expected prefab with name '{0}' to container a component of type 'GameObjectContext'", prefab.name);
			return context.Container;
		}
	}
}
