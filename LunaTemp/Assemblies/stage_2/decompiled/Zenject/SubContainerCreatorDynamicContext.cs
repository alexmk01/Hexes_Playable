using System.Collections.Generic;
using UnityEngine;

namespace Zenject
{
	[NoReflectionBaking]
	public abstract class SubContainerCreatorDynamicContext : ISubContainerCreator
	{
		private readonly DiContainer _container;

		protected DiContainer Container => _container;

		public SubContainerCreatorDynamicContext(DiContainer container)
		{
			_container = container;
		}

		public DiContainer CreateSubContainer(List<TypeValuePair> args, InjectContext parentContext)
		{
			bool shouldMakeActive;
			GameObject gameObj = CreateGameObject(out shouldMakeActive);
			GameObjectContext context = gameObj.AddComponent<GameObjectContext>();
			AddInstallers(args, context);
			_container.Inject(context);
			if (shouldMakeActive && !_container.IsValidating)
			{
				gameObj.SetActive(true);
			}
			return context.Container;
		}

		protected abstract void AddInstallers(List<TypeValuePair> args, GameObjectContext context);

		protected abstract GameObject CreateGameObject(out bool shouldMakeActive);
	}
}
