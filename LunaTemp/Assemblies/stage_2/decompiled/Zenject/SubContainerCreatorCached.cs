using System.Collections.Generic;
using ModestTree;

namespace Zenject
{
	[NoReflectionBaking]
	public class SubContainerCreatorCached : ISubContainerCreator
	{
		private readonly ISubContainerCreator _subCreator;

		private bool _isLookingUp;

		private DiContainer _subContainer;

		public SubContainerCreatorCached(ISubContainerCreator subCreator)
		{
			_subCreator = subCreator;
		}

		public DiContainer CreateSubContainer(List<TypeValuePair> args, InjectContext context)
		{
			Assert.IsEmpty(args);
			if (_subContainer == null)
			{
				Assert.That(!_isLookingUp, "Found unresolvable circular dependency when looking up sub container!  Object graph:\n {0}", context.GetObjectGraphString());
				_isLookingUp = true;
				_subContainer = _subCreator.CreateSubContainer(new List<TypeValuePair>(), context);
				_isLookingUp = false;
				Assert.IsNotNull(_subContainer);
			}
			return _subContainer;
		}
	}
}
