using System;
using System.Collections.Generic;
using ModestTree;

namespace Zenject
{
	[NoReflectionBaking]
	public class ScopableBindingFinalizer : ProviderBindingFinalizer
	{
		private readonly Func<DiContainer, Type, IProvider> _providerFactory;

		public ScopableBindingFinalizer(BindInfo bindInfo, Func<DiContainer, Type, IProvider> providerFactory)
			: base(bindInfo)
		{
			_providerFactory = providerFactory;
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
			if (concreteTypes.Count == 0)
			{
				return;
			}
			switch (GetScope())
			{
			case ScopeTypes.Transient:
				RegisterProvidersForAllContractsPerConcreteType(container, concreteTypes, _providerFactory);
				break;
			case ScopeTypes.Singleton:
				RegisterProvidersForAllContractsPerConcreteType(container, concreteTypes, (DiContainer _, Type concreteType) => BindingUtil.CreateCachedProvider(_providerFactory(container, concreteType)));
				break;
			default:
				throw Assert.CreateException();
			}
		}

		private void FinalizeBindingSelf(DiContainer container)
		{
			switch (GetScope())
			{
			case ScopeTypes.Transient:
				RegisterProviderPerContract(container, _providerFactory);
				break;
			case ScopeTypes.Singleton:
				RegisterProviderPerContract(container, (DiContainer _, Type contractType) => BindingUtil.CreateCachedProvider(_providerFactory(container, contractType)));
				break;
			default:
				throw Assert.CreateException();
			}
		}
	}
}
