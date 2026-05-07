using System;
using System.Collections.Generic;
using UnityEngine;

namespace Zenject
{
	[NoReflectionBaking]
	public class FromBinderGeneric<TContract> : FromBinder
	{
		public FromBinderGeneric(DiContainer bindContainer, BindInfo bindInfo, BindStatement bindStatement)
			: base(bindContainer, bindInfo, bindStatement)
		{
			BindingUtil.AssertIsDerivedFromTypes(typeof(TContract), base.BindInfo.ContractTypes);
		}

		public ScopeConcreteIdArgConditionCopyNonLazyBinder FromFactory<TFactory>() where TFactory : IFactory<TContract>
		{
			return FromIFactory(delegate(ConcreteBinderGeneric<IFactory<TContract>> x)
			{
				x.To<TFactory>().AsCached();
			});
		}

		public ScopeConcreteIdArgConditionCopyNonLazyBinder FromIFactory(Action<ConcreteBinderGeneric<IFactory<TContract>>> factoryBindGenerator)
		{
			return FromIFactoryBase(factoryBindGenerator);
		}

		public ScopeConcreteIdArgConditionCopyNonLazyBinder FromMethod(Func<TContract> method)
		{
			return FromMethodBase((InjectContext ctx) => method());
		}

		public ScopeConcreteIdArgConditionCopyNonLazyBinder FromMethod(Func<InjectContext, TContract> method)
		{
			return FromMethodBase(method);
		}

		public ScopeConcreteIdArgConditionCopyNonLazyBinder FromMethodMultiple(Func<InjectContext, IEnumerable<TContract>> method)
		{
			BindingUtil.AssertIsDerivedFromTypes(typeof(TContract), base.AllParentTypes);
			return FromMethodMultipleBase(method);
		}

		public ScopeConcreteIdArgConditionCopyNonLazyBinder FromResolveGetter<TObj>(Func<TObj, TContract> method)
		{
			return FromResolveGetter(null, method);
		}

		public ScopeConcreteIdArgConditionCopyNonLazyBinder FromResolveGetter<TObj>(object identifier, Func<TObj, TContract> method)
		{
			return FromResolveGetter(identifier, method, InjectSources.Any);
		}

		public ScopeConcreteIdArgConditionCopyNonLazyBinder FromResolveGetter<TObj>(object identifier, Func<TObj, TContract> method, InjectSources source)
		{
			return FromResolveGetterBase(identifier, method, source, false);
		}

		public ScopeConcreteIdArgConditionCopyNonLazyBinder FromResolveAllGetter<TObj>(Func<TObj, TContract> method)
		{
			return FromResolveAllGetter(null, method);
		}

		public ScopeConcreteIdArgConditionCopyNonLazyBinder FromResolveAllGetter<TObj>(object identifier, Func<TObj, TContract> method)
		{
			return FromResolveAllGetter(identifier, method, InjectSources.Any);
		}

		public ScopeConcreteIdArgConditionCopyNonLazyBinder FromResolveAllGetter<TObj>(object identifier, Func<TObj, TContract> method, InjectSources source)
		{
			return FromResolveGetterBase(identifier, method, source, true);
		}

		public ScopeConcreteIdArgConditionCopyNonLazyBinder FromInstance(TContract instance)
		{
			return FromInstanceBase(instance);
		}

		public ScopeConcreteIdArgConditionCopyNonLazyBinder FromComponentsInChildren(Func<TContract, bool> predicate, bool includeInactive = true)
		{
			return FromComponentsInChildren(false, predicate, includeInactive);
		}

		public ScopeConcreteIdArgConditionCopyNonLazyBinder FromComponentsInChildren(bool excludeSelf = false, Func<TContract, bool> predicate = null, bool includeInactive = true)
		{
			Func<Component, bool> subPredicate = ((predicate == null) ? null : ((Func<Component, bool>)((Component component) => predicate((TContract)(object)component))));
			return FromComponentsInChildrenBase(excludeSelf, subPredicate, includeInactive);
		}

		public ScopeConcreteIdArgConditionCopyNonLazyBinder FromComponentsInHierarchy(Func<TContract, bool> predicate = null, bool includeInactive = true)
		{
			Func<Component, bool> subPredicate = ((predicate == null) ? null : ((Func<Component, bool>)((Component component) => predicate((TContract)(object)component))));
			return FromComponentsInHierarchyBase(subPredicate, includeInactive);
		}
	}
}
