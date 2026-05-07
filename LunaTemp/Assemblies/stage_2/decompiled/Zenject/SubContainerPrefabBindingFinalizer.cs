using System;
using System.Collections.Generic;
using ModestTree;

namespace Zenject
{
	[NoReflectionBaking]
	public class SubContainerPrefabBindingFinalizer : ProviderBindingFinalizer
	{
		private readonly object _subIdentifier;

		private readonly bool _resolveAll;

		private readonly Func<DiContainer, ISubContainerCreator> _subContainerCreatorFactory;

		public SubContainerPrefabBindingFinalizer(BindInfo bindInfo, object subIdentifier, bool resolveAll, Func<DiContainer, ISubContainerCreator> subContainerCreatorFactory)
			: base(bindInfo)
		{
			_subIdentifier = subIdentifier;
			_resolveAll = resolveAll;
			_subContainerCreatorFactory = subContainerCreatorFactory;
		}

		protected override void OnFinalizeBinding(DiContainer container)
		{
			if (base.BindInfo.ToChoice == ToChoices.Self)
			{
				Assert.IsEmpty(base.BindInfo.ToTypes);
				FinalizeBindingSelf(container);
			}
			else
			{
				FinalizeBindingConcrete(container, base.BindInfo.ToTypes);
			}
		}

		private void FinalizeBindingConcrete(DiContainer container, List<Type> concreteTypes)
		{
			switch (GetScope())
			{
			case ScopeTypes.Transient:
				RegisterProvidersForAllContractsPerConcreteType(container, concreteTypes, (DiContainer _, Type concreteType) => new SubContainerDependencyProvider(concreteType, _subIdentifier, _subContainerCreatorFactory(container), _resolveAll));
				break;
			case ScopeTypes.Singleton:
			{
				SubContainerCreatorCached containerCreator = new SubContainerCreatorCached(_subContainerCreatorFactory(container));
				RegisterProvidersForAllContractsPerConcreteType(container, concreteTypes, (DiContainer _, Type concreteType) => new SubContainerDependencyProvider(concreteType, _subIdentifier, containerCreator, _resolveAll));
				break;
			}
			default:
				throw Assert.CreateException();
			}
		}

		private void FinalizeBindingSelf(DiContainer container)
		{
			switch (GetScope())
			{
			case ScopeTypes.Transient:
				RegisterProviderPerContract(container, (DiContainer _, Type contractType) => new SubContainerDependencyProvider(contractType, _subIdentifier, _subContainerCreatorFactory(container), _resolveAll));
				break;
			case ScopeTypes.Singleton:
			{
				SubContainerCreatorCached containerCreator = new SubContainerCreatorCached(_subContainerCreatorFactory(container));
				RegisterProviderPerContract(container, (DiContainer _, Type contractType) => new SubContainerDependencyProvider(contractType, _subIdentifier, containerCreator, _resolveAll));
				break;
			}
			default:
				throw Assert.CreateException();
			}
		}
	}
}
