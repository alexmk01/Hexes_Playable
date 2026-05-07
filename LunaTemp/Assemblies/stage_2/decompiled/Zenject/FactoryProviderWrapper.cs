using ModestTree;

namespace Zenject
{
	public class FactoryProviderWrapper<TContract> : IFactory<TContract>, IFactory
	{
		private readonly IProvider _provider;

		private readonly InjectContext _injectContext;

		public FactoryProviderWrapper(IProvider provider, InjectContext injectContext)
		{
			Assert.That(injectContext.MemberType.DerivesFromOrEqual<TContract>());
			_provider = provider;
			_injectContext = injectContext;
		}

		public TContract Create()
		{
			object instance = _provider.GetInstance(_injectContext);
			if (_injectContext.Container.IsValidating)
			{
				return default(TContract);
			}
			Assert.That(instance?.GetType().DerivesFromOrEqual(_injectContext.MemberType) ?? true);
			return (TContract)instance;
		}
	}
}
