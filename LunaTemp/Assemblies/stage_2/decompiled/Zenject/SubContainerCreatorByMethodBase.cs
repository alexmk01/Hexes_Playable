using System.Collections.Generic;

namespace Zenject
{
	[NoReflectionBaking]
	public abstract class SubContainerCreatorByMethodBase : ISubContainerCreator
	{
		private readonly DiContainer _container;

		private readonly SubContainerCreatorBindInfo _containerBindInfo;

		public SubContainerCreatorByMethodBase(DiContainer container, SubContainerCreatorBindInfo containerBindInfo)
		{
			_container = container;
			_containerBindInfo = containerBindInfo;
		}

		public abstract DiContainer CreateSubContainer(List<TypeValuePair> args, InjectContext context);

		protected DiContainer CreateEmptySubContainer()
		{
			DiContainer subContainer = _container.CreateSubContainer();
			SubContainerCreatorUtil.ApplyBindSettings(_containerBindInfo, subContainer);
			return subContainer;
		}
	}
}
