using System;
using System.Linq;
using ModestTree;
using ModestTree.Util;
using UnityEngine;

namespace Zenject
{
	[NoReflectionBaking]
	public class FactoryFromBinder<TContract> : FactoryFromBinderBase
	{
		public FactoryFromBinder(DiContainer container, BindInfo bindInfo, FactoryBindInfo factoryBindInfo)
			: base(container, typeof(TContract), bindInfo, factoryBindInfo)
		{
		}

		public ConditionCopyNonLazyBinder FromResolveGetter<TObj>(Func<TObj, TContract> method)
		{
			return FromResolveGetter(null, method);
		}

		public ConditionCopyNonLazyBinder FromResolveGetter<TObj>(object subIdentifier, Func<TObj, TContract> method)
		{
			return FromResolveGetter(subIdentifier, method, InjectSources.Any);
		}

		public ConditionCopyNonLazyBinder FromResolveGetter<TObj>(object subIdentifier, Func<TObj, TContract> method, InjectSources source)
		{
			base.FactoryBindInfo.ProviderFunc = (DiContainer container) => new GetterProvider<TObj, TContract>(subIdentifier, method, container, source, false);
			return this;
		}

		public ConditionCopyNonLazyBinder FromMethod(Func<DiContainer, TContract> method)
		{
			base.ProviderFunc = (DiContainer container) => new MethodProviderWithContainer<TContract>(method);
			return this;
		}

		public ArgConditionCopyNonLazyBinder FromFactory<TSubFactory>() where TSubFactory : IFactory<TContract>
		{
			return this.FromIFactory(delegate(ConcreteBinderGeneric<IFactory<TContract>> x)
			{
				x.To<TSubFactory>().AsCached();
			});
		}

		public FactorySubContainerBinder<TContract> FromSubContainerResolve()
		{
			return FromSubContainerResolve(null);
		}

		public FactorySubContainerBinder<TContract> FromSubContainerResolve(object subIdentifier)
		{
			return new FactorySubContainerBinder<TContract>(base.BindContainer, base.BindInfo, base.FactoryBindInfo, subIdentifier);
		}

		public ConditionCopyNonLazyBinder FromComponentInHierarchy(bool includeInactive = true)
		{
			BindingUtil.AssertIsInterfaceOrComponent(base.ContractType);
			return FromMethod(delegate
			{
				TContract val = (from x in base.BindContainer.Resolve<Context>().GetRootGameObjects()
					select x.GetComponentInChildren<TContract>(includeInactive) into x
					where x != null
					select x).FirstOrDefault();
				Assert.IsNotNull(val, "Could not find component '{0}' through FromComponentInHierarchy factory binding", typeof(TContract));
				return val;
			});
		}
	}
	[NoReflectionBaking]
	public class FactoryFromBinder<TParam1, TContract> : FactoryFromBinderBase
	{
		public FactoryFromBinder(DiContainer container, BindInfo bindInfo, FactoryBindInfo factoryBindInfo)
			: base(container, typeof(TContract), bindInfo, factoryBindInfo)
		{
		}

		public ConditionCopyNonLazyBinder FromMethod(Func<DiContainer, TParam1, TContract> method)
		{
			base.ProviderFunc = (DiContainer container) => new MethodProviderWithContainer<TParam1, TContract>(method);
			return this;
		}

		public ConditionCopyNonLazyBinder FromFactory<TSubFactory>() where TSubFactory : IFactory<TParam1, TContract>
		{
			return this.FromIFactory(delegate(ConcreteBinderGeneric<IFactory<TParam1, TContract>> x)
			{
				x.To<TSubFactory>().AsCached();
			});
		}

		public FactorySubContainerBinder<TParam1, TContract> FromSubContainerResolve()
		{
			return FromSubContainerResolve(null);
		}

		public FactorySubContainerBinder<TParam1, TContract> FromSubContainerResolve(object subIdentifier)
		{
			return new FactorySubContainerBinder<TParam1, TContract>(base.BindContainer, base.BindInfo, base.FactoryBindInfo, subIdentifier);
		}
	}
	[NoReflectionBaking]
	public class FactoryFromBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TParam9, TParam10, TContract> : FactoryFromBinderBase
	{
		public FactoryFromBinder(DiContainer container, BindInfo bindInfo, FactoryBindInfo factoryBindInfo)
			: base(container, typeof(TContract), bindInfo, factoryBindInfo)
		{
		}

		public ConditionCopyNonLazyBinder FromMethod(ModestTree.Util.Func<DiContainer, TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TParam9, TParam10, TContract> method)
		{
			base.ProviderFunc = (DiContainer container) => new MethodProviderWithContainer<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TParam9, TParam10, TContract>(method);
			return this;
		}

		public ConditionCopyNonLazyBinder FromFactory<TSubFactory>() where TSubFactory : IFactory<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TParam9, TParam10, TContract>
		{
			return FromIFactory(delegate(ConcreteBinderGeneric<IFactory<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TParam9, TParam10, TContract>> x)
			{
				x.To<TSubFactory>().AsCached();
			});
		}

		public ArgConditionCopyNonLazyBinder FromIFactory(Action<ConcreteBinderGeneric<IFactory<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TParam9, TParam10, TContract>>> factoryBindGenerator)
		{
			factoryBindGenerator(CreateIFactoryBinder<IFactory<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TParam9, TParam10, TContract>>(out var factoryId));
			base.ProviderFunc = (DiContainer container) => new IFactoryProvider<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TParam9, TParam10, TContract>(container, factoryId);
			return new ArgConditionCopyNonLazyBinder(base.BindInfo);
		}

		public FactorySubContainerBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TParam9, TParam10, TContract> FromSubContainerResolve()
		{
			return FromSubContainerResolve(null);
		}

		public FactorySubContainerBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TParam9, TParam10, TContract> FromSubContainerResolve(object subIdentifier)
		{
			return new FactorySubContainerBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TParam9, TParam10, TContract>(base.BindContainer, base.BindInfo, base.FactoryBindInfo, subIdentifier);
		}
	}
	[NoReflectionBaking]
	public class FactoryFromBinder<TParam1, TParam2, TContract> : FactoryFromBinderBase
	{
		public FactoryFromBinder(DiContainer container, BindInfo bindInfo, FactoryBindInfo factoryBindInfo)
			: base(container, typeof(TContract), bindInfo, factoryBindInfo)
		{
		}

		public ConditionCopyNonLazyBinder FromMethod(Func<DiContainer, TParam1, TParam2, TContract> method)
		{
			base.ProviderFunc = (DiContainer container) => new MethodProviderWithContainer<TParam1, TParam2, TContract>(method);
			return this;
		}

		public ConditionCopyNonLazyBinder FromFactory<TSubFactory>() where TSubFactory : IFactory<TParam1, TParam2, TContract>
		{
			return this.FromIFactory(delegate(ConcreteBinderGeneric<IFactory<TParam1, TParam2, TContract>> x)
			{
				x.To<TSubFactory>().AsCached();
			});
		}

		public FactorySubContainerBinder<TParam1, TParam2, TContract> FromSubContainerResolve()
		{
			return FromSubContainerResolve(null);
		}

		public FactorySubContainerBinder<TParam1, TParam2, TContract> FromSubContainerResolve(object subIdentifier)
		{
			return new FactorySubContainerBinder<TParam1, TParam2, TContract>(base.BindContainer, base.BindInfo, base.FactoryBindInfo, subIdentifier);
		}
	}
	[NoReflectionBaking]
	public class FactoryFromBinder<TParam1, TParam2, TParam3, TContract> : FactoryFromBinderBase
	{
		public FactoryFromBinder(DiContainer container, BindInfo bindInfo, FactoryBindInfo factoryBindInfo)
			: base(container, typeof(TContract), bindInfo, factoryBindInfo)
		{
		}

		public ConditionCopyNonLazyBinder FromMethod(Func<DiContainer, TParam1, TParam2, TParam3, TContract> method)
		{
			base.ProviderFunc = (DiContainer container) => new MethodProviderWithContainer<TParam1, TParam2, TParam3, TContract>(method);
			return this;
		}

		public ConditionCopyNonLazyBinder FromFactory<TSubFactory>() where TSubFactory : IFactory<TParam1, TParam2, TParam3, TContract>
		{
			return this.FromIFactory(delegate(ConcreteBinderGeneric<IFactory<TParam1, TParam2, TParam3, TContract>> x)
			{
				x.To<TSubFactory>().AsCached();
			});
		}

		public FactorySubContainerBinder<TParam1, TParam2, TParam3, TContract> FromSubContainerResolve()
		{
			return FromSubContainerResolve(null);
		}

		public FactorySubContainerBinder<TParam1, TParam2, TParam3, TContract> FromSubContainerResolve(object subIdentifier)
		{
			return new FactorySubContainerBinder<TParam1, TParam2, TParam3, TContract>(base.BindContainer, base.BindInfo, base.FactoryBindInfo, subIdentifier);
		}
	}
	[NoReflectionBaking]
	public class FactoryFromBinder<TParam1, TParam2, TParam3, TParam4, TContract> : FactoryFromBinderBase
	{
		public FactoryFromBinder(DiContainer container, BindInfo bindInfo, FactoryBindInfo factoryBindInfo)
			: base(container, typeof(TContract), bindInfo, factoryBindInfo)
		{
		}

		public ConditionCopyNonLazyBinder FromMethod(ModestTree.Util.Func<DiContainer, TParam1, TParam2, TParam3, TParam4, TContract> method)
		{
			base.ProviderFunc = (DiContainer container) => new MethodProviderWithContainer<TParam1, TParam2, TParam3, TParam4, TContract>(method);
			return this;
		}

		public ConditionCopyNonLazyBinder FromFactory<TSubFactory>() where TSubFactory : IFactory<TParam1, TParam2, TParam3, TParam4, TContract>
		{
			return this.FromIFactory(delegate(ConcreteBinderGeneric<IFactory<TParam1, TParam2, TParam3, TParam4, TContract>> x)
			{
				x.To<TSubFactory>().AsCached();
			});
		}

		public FactorySubContainerBinder<TParam1, TParam2, TParam3, TParam4, TContract> FromSubContainerResolve()
		{
			return FromSubContainerResolve(null);
		}

		public FactorySubContainerBinder<TParam1, TParam2, TParam3, TParam4, TContract> FromSubContainerResolve(object subIdentifier)
		{
			return new FactorySubContainerBinder<TParam1, TParam2, TParam3, TParam4, TContract>(base.BindContainer, base.BindInfo, base.FactoryBindInfo, subIdentifier);
		}
	}
	[NoReflectionBaking]
	public class FactoryFromBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TContract> : FactoryFromBinderBase
	{
		public FactoryFromBinder(DiContainer container, BindInfo bindInfo, FactoryBindInfo factoryBindInfo)
			: base(container, typeof(TContract), bindInfo, factoryBindInfo)
		{
		}

		public ConditionCopyNonLazyBinder FromMethod(ModestTree.Util.Func<DiContainer, TParam1, TParam2, TParam3, TParam4, TParam5, TContract> method)
		{
			base.ProviderFunc = (DiContainer container) => new MethodProviderWithContainer<TParam1, TParam2, TParam3, TParam4, TParam5, TContract>(method);
			return this;
		}

		public ConditionCopyNonLazyBinder FromFactory<TSubFactory>() where TSubFactory : IFactory<TParam1, TParam2, TParam3, TParam4, TParam5, TContract>
		{
			return this.FromIFactory(delegate(ConcreteBinderGeneric<IFactory<TParam1, TParam2, TParam3, TParam4, TParam5, TContract>> x)
			{
				x.To<TSubFactory>().AsCached();
			});
		}

		public FactorySubContainerBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TContract> FromSubContainerResolve()
		{
			return FromSubContainerResolve(null);
		}

		public FactorySubContainerBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TContract> FromSubContainerResolve(object subIdentifier)
		{
			return new FactorySubContainerBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TContract>(base.BindContainer, base.BindInfo, base.FactoryBindInfo, subIdentifier);
		}
	}
	[NoReflectionBaking]
	public class FactoryFromBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TContract> : FactoryFromBinderBase
	{
		public FactoryFromBinder(DiContainer container, BindInfo bindInfo, FactoryBindInfo factoryBindInfo)
			: base(container, typeof(TContract), bindInfo, factoryBindInfo)
		{
		}

		public ConditionCopyNonLazyBinder FromMethod(ModestTree.Util.Func<DiContainer, TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TContract> method)
		{
			base.ProviderFunc = (DiContainer container) => new MethodProviderWithContainer<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TContract>(method);
			return this;
		}

		public ConditionCopyNonLazyBinder FromFactory<TSubFactory>() where TSubFactory : IFactory<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TContract>
		{
			return this.FromIFactory(delegate(ConcreteBinderGeneric<IFactory<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TContract>> x)
			{
				x.To<TSubFactory>().AsCached();
			});
		}

		public FactorySubContainerBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TContract> FromSubContainerResolve()
		{
			return FromSubContainerResolve(null);
		}

		public FactorySubContainerBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TContract> FromSubContainerResolve(object subIdentifier)
		{
			return new FactorySubContainerBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TContract>(base.BindContainer, base.BindInfo, base.FactoryBindInfo, subIdentifier);
		}
	}
}
