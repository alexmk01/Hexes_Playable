using System.Collections.Generic;
using ModestTree;

namespace Zenject
{
	[NoReflectionBaking]
	public class SubContainerCreatorByInstance : ISubContainerCreator
	{
		private readonly DiContainer _subcontainer;

		public SubContainerCreatorByInstance(DiContainer subcontainer)
		{
			_subcontainer = subcontainer;
		}

		public DiContainer CreateSubContainer(List<TypeValuePair> args, InjectContext context)
		{
			Assert.That(args.IsEmpty());
			return _subcontainer;
		}
	}
}
