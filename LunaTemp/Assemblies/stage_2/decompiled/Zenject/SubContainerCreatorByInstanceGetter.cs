using System;
using System.Collections.Generic;
using ModestTree;

namespace Zenject
{
	[NoReflectionBaking]
	public class SubContainerCreatorByInstanceGetter : ISubContainerCreator
	{
		private readonly Func<InjectContext, DiContainer> _subcontainerGetter;

		public SubContainerCreatorByInstanceGetter(Func<InjectContext, DiContainer> subcontainerGetter)
		{
			_subcontainerGetter = subcontainerGetter;
		}

		public DiContainer CreateSubContainer(List<TypeValuePair> args, InjectContext context)
		{
			Assert.That(args.IsEmpty());
			return _subcontainerGetter(context);
		}
	}
}
