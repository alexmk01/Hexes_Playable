using System;
using ModestTree;

namespace Zenject
{
	[NoReflectionBaking]
	public class SingleProviderBindingFinalizer : ProviderBindingFinalizer
	{
		private readonly Func<DiContainer, Type, IProvider> _providerFactory;

		public SingleProviderBindingFinalizer(BindInfo bindInfo, Func<DiContainer, Type, IProvider> providerFactory)
			: base(bindInfo)
		{
			_providerFactory = providerFactory;
		}

		protected override void OnFinalizeBinding(DiContainer container)
		{
			if (base.BindInfo.ToChoice == ToChoices.Self)
			{
				Assert.IsEmpty(base.BindInfo.ToTypes);
				RegisterProviderPerContract(container, _providerFactory);
			}
			else if (!base.BindInfo.ToTypes.IsEmpty())
			{
				RegisterProvidersForAllContractsPerConcreteType(container, base.BindInfo.ToTypes, _providerFactory);
			}
		}
	}
}
