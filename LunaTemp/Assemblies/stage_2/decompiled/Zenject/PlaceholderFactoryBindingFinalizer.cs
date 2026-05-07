using System.Linq;
using ModestTree;

namespace Zenject
{
	[NoReflectionBaking]
	public class PlaceholderFactoryBindingFinalizer<TContract> : ProviderBindingFinalizer
	{
		private readonly FactoryBindInfo _factoryBindInfo;

		public PlaceholderFactoryBindingFinalizer(BindInfo bindInfo, FactoryBindInfo factoryBindInfo)
			: base(bindInfo)
		{
			Assert.That(factoryBindInfo.FactoryType.DerivesFrom<IPlaceholderFactory>());
			_factoryBindInfo = factoryBindInfo;
		}

		protected override void OnFinalizeBinding(DiContainer container)
		{
			IProvider provider = _factoryBindInfo.ProviderFunc(container);
			TransientProvider transientProvider = new TransientProvider(_factoryBindInfo.FactoryType, container, _factoryBindInfo.Arguments.Concat(InjectUtil.CreateArgListExplicit(provider, new InjectContext(container, typeof(TContract)))).ToList(), base.BindInfo.ContextInfo, base.BindInfo.ConcreteIdentifier, null);
			IProvider mainProvider;
			if (base.BindInfo.Scope == ScopeTypes.Unset || base.BindInfo.Scope == ScopeTypes.Singleton)
			{
				mainProvider = BindingUtil.CreateCachedProvider(transientProvider);
			}
			else
			{
				Assert.IsEqual(base.BindInfo.Scope, ScopeTypes.Transient);
				mainProvider = transientProvider;
			}
			RegisterProviderForAllContracts(container, mainProvider);
		}
	}
}
