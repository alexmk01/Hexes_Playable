using System;
using System.Collections.Generic;
using UnityEngine;

namespace Zenject
{
	[NoReflectionBaking]
	public class FromBinderNonGeneric : FromBinder
	{
		public FromBinderNonGeneric(DiContainer bindContainer, BindInfo bindInfo, BindStatement bindStatement)
			: base(bindContainer, bindInfo, bindStatement)
		{
		}

		public ScopeConcreteIdArgConditionCopyNonLazyBinder FromFactory<TConcrete, TFactory>() where TFactory : IFactory<TConcrete>
		{
			return FromIFactory(delegate(ConcreteBinderGeneric<IFactory<TConcrete>> x)
			{
				x.To<TFactory>().AsCached();
			});
		}

		public ScopeConcreteIdArgConditionCopyNonLazyBinder FromIFactory<TContract>(Action<ConcreteBinderGeneric<IFactory<TContract>>> factoryBindGenerator)
		{
			return FromIFactoryBase(factoryBindGenerator);
		}

		public ScopeConcreteIdArgConditionCopyNonLazyBinder FromMethod<TConcrete>(Func<InjectContext, TConcrete> method)
		{
			return FromMethodBase(method);
		}

		public ScopeConcreteIdArgConditionCopyNonLazyBinder FromMethodMultiple<TConcrete>(Func<InjectContext, IEnumerable<TConcrete>> method)
		{
			BindingUtil.AssertIsDerivedFromTypes(typeof(TConcrete), base.AllParentTypes);
			return FromMethodMultipleBase(method);
		}

		public ScopeConcreteIdArgConditionCopyNonLazyBinder FromResolveGetter<TObj, TContract>(Func<TObj, TContract> method)
		{
			return FromResolveGetter(null, method);
		}

		public ScopeConcreteIdArgConditionCopyNonLazyBinder FromResolveGetter<TObj, TContract>(object identifier, Func<TObj, TContract> method)
		{
			return FromResolveGetter(identifier, method, InjectSources.Any);
		}

		public ScopeConcreteIdArgConditionCopyNonLazyBinder FromResolveGetter<TObj, TContract>(object identifier, Func<TObj, TContract> method, InjectSources source)
		{
			return FromResolveGetterBase(identifier, method, source, false);
		}

		public ScopeConcreteIdArgConditionCopyNonLazyBinder FromResolveAllGetter<TObj, TContract>(Func<TObj, TContract> method)
		{
			return FromResolveAllGetter(null, method);
		}

		public ScopeConcreteIdArgConditionCopyNonLazyBinder FromResolveAllGetter<TObj, TContract>(object identifier, Func<TObj, TContract> method)
		{
			return FromResolveAllGetter(identifier, method, InjectSources.Any);
		}

		public ScopeConcreteIdArgConditionCopyNonLazyBinder FromResolveAllGetter<TObj, TContract>(object identifier, Func<TObj, TContract> method, InjectSources source)
		{
			return FromResolveGetterBase(identifier, method, source, true);
		}

		public ScopeConcreteIdArgConditionCopyNonLazyBinder FromInstance(object instance)
		{
			return FromInstanceBase(instance);
		}

		public ScopeConcreteIdArgConditionCopyNonLazyBinder FromComponentsInChildren(Func<Component, bool> predicate, bool includeInactive = true)
		{
			return FromComponentsInChildren(false, predicate, includeInactive);
		}

		public ScopeConcreteIdArgConditionCopyNonLazyBinder FromComponentsInChildren(bool excludeSelf = false, Func<Component, bool> predicate = null, bool includeInactive = true)
		{
			return FromComponentsInChildrenBase(excludeSelf, predicate, includeInactive);
		}

		public ScopeConcreteIdArgConditionCopyNonLazyBinder FromComponentsInHierarchy(Func<Component, bool> predicate = null, bool includeInactive = true)
		{
			return FromComponentsInHierarchyBase(predicate, includeInactive);
		}
	}
}
