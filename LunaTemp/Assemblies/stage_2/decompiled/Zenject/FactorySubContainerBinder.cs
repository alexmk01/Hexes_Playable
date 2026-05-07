using System;
using ModestTree.Util;
using UnityEngine;

namespace Zenject
{
	[NoReflectionBaking]
	public class FactorySubContainerBinder<TContract> : FactorySubContainerBinderBase<TContract>
	{
		public FactorySubContainerBinder(DiContainer bindContainer, BindInfo bindInfo, FactoryBindInfo factoryBindInfo, object subIdentifier)
			: base(bindContainer, bindInfo, factoryBindInfo, subIdentifier)
		{
		}

		public ScopeConcreteIdArgConditionCopyNonLazyBinder ByMethod(Action<DiContainer> installerMethod)
		{
			SubContainerCreatorBindInfo subcontainerBindInfo = new SubContainerCreatorBindInfo();
			base.ProviderFunc = (DiContainer container) => new SubContainerDependencyProvider(base.ContractType, base.SubIdentifier, new SubContainerCreatorByMethod(container, subcontainerBindInfo, installerMethod), false);
			return new ScopeConcreteIdArgConditionCopyNonLazyBinder(base.BindInfo);
		}

		public NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder ByNewGameObjectMethod(Action<DiContainer> installerMethod)
		{
			GameObjectCreationParameters gameObjectInfo = new GameObjectCreationParameters();
			base.ProviderFunc = (DiContainer container) => new SubContainerDependencyProvider(base.ContractType, base.SubIdentifier, new SubContainerCreatorByNewGameObjectMethod(container, gameObjectInfo, installerMethod), false);
			return new NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder(base.BindInfo, gameObjectInfo);
		}

		public NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder ByNewPrefabMethod(UnityEngine.Object prefab, Action<DiContainer> installerMethod)
		{
			BindingUtil.AssertIsValidPrefab(prefab);
			GameObjectCreationParameters gameObjectInfo = new GameObjectCreationParameters();
			base.ProviderFunc = (DiContainer container) => new SubContainerDependencyProvider(base.ContractType, base.SubIdentifier, new SubContainerCreatorByNewPrefabMethod(container, new PrefabProvider(prefab), gameObjectInfo, installerMethod), false);
			return new NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder(base.BindInfo, gameObjectInfo);
		}

		public NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder ByNewPrefabResourceMethod(string resourcePath, Action<DiContainer> installerMethod)
		{
			BindingUtil.AssertIsValidResourcePath(resourcePath);
			GameObjectCreationParameters gameObjectInfo = new GameObjectCreationParameters();
			base.ProviderFunc = (DiContainer container) => new SubContainerDependencyProvider(base.ContractType, base.SubIdentifier, new SubContainerCreatorByNewPrefabMethod(container, new PrefabProviderResource(resourcePath), gameObjectInfo, installerMethod), false);
			return new NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder(base.BindInfo, gameObjectInfo);
		}

		[Obsolete("ByNewPrefab has been renamed to ByNewContextPrefab to avoid confusion with ByNewPrefabInstaller and ByNewPrefabMethod")]
		public NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder ByNewPrefab(UnityEngine.Object prefab)
		{
			return ByNewContextPrefab(prefab);
		}

		public NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder ByNewContextPrefab(UnityEngine.Object prefab)
		{
			BindingUtil.AssertIsValidPrefab(prefab);
			GameObjectCreationParameters gameObjectInfo = new GameObjectCreationParameters();
			base.ProviderFunc = (DiContainer container) => new SubContainerDependencyProvider(base.ContractType, base.SubIdentifier, new SubContainerCreatorByNewPrefab(container, new PrefabProvider(prefab), gameObjectInfo), false);
			return new NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder(base.BindInfo, gameObjectInfo);
		}

		[Obsolete("ByNewPrefabResource has been renamed to ByNewContextPrefabResource to avoid confusion with ByNewPrefabResourceInstaller and ByNewPrefabResourceMethod")]
		public NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder ByNewPrefabResource(string resourcePath)
		{
			return ByNewContextPrefabResource(resourcePath);
		}

		public NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder ByNewContextPrefabResource(string resourcePath)
		{
			BindingUtil.AssertIsValidResourcePath(resourcePath);
			GameObjectCreationParameters gameObjectInfo = new GameObjectCreationParameters();
			base.ProviderFunc = (DiContainer container) => new SubContainerDependencyProvider(base.ContractType, base.SubIdentifier, new SubContainerCreatorByNewPrefab(container, new PrefabProviderResource(resourcePath), gameObjectInfo), false);
			return new NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder(base.BindInfo, gameObjectInfo);
		}
	}
	[NoReflectionBaking]
	public class FactorySubContainerBinder<TParam1, TContract> : FactorySubContainerBinderWithParams<TContract>
	{
		public FactorySubContainerBinder(DiContainer bindContainer, BindInfo bindInfo, FactoryBindInfo factoryBindInfo, object subIdentifier)
			: base(bindContainer, bindInfo, factoryBindInfo, subIdentifier)
		{
		}

		public ScopeConcreteIdArgConditionCopyNonLazyBinder ByMethod(Action<DiContainer, TParam1> installerMethod)
		{
			SubContainerCreatorBindInfo subcontainerBindInfo = new SubContainerCreatorBindInfo();
			base.ProviderFunc = (DiContainer container) => new SubContainerDependencyProvider(base.ContractType, base.SubIdentifier, new SubContainerCreatorByMethod<TParam1>(container, subcontainerBindInfo, installerMethod), false);
			return new ScopeConcreteIdArgConditionCopyNonLazyBinder(base.BindInfo);
		}

		public NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder ByNewGameObjectMethod(Action<DiContainer, TParam1> installerMethod)
		{
			GameObjectCreationParameters gameObjectInfo = new GameObjectCreationParameters();
			base.ProviderFunc = (DiContainer container) => new SubContainerDependencyProvider(base.ContractType, base.SubIdentifier, new SubContainerCreatorByNewGameObjectMethod<TParam1>(container, gameObjectInfo, installerMethod), false);
			return new NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder(base.BindInfo, gameObjectInfo);
		}

		public NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder ByNewPrefabMethod(UnityEngine.Object prefab, Action<DiContainer, TParam1> installerMethod)
		{
			BindingUtil.AssertIsValidPrefab(prefab);
			GameObjectCreationParameters gameObjectInfo = new GameObjectCreationParameters();
			base.ProviderFunc = (DiContainer container) => new SubContainerDependencyProvider(base.ContractType, base.SubIdentifier, new SubContainerCreatorByNewPrefabMethod<TParam1>(container, new PrefabProvider(prefab), gameObjectInfo, installerMethod), false);
			return new NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder(base.BindInfo, gameObjectInfo);
		}

		public NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder ByNewPrefabResourceMethod(string resourcePath, Action<DiContainer, TParam1> installerMethod)
		{
			BindingUtil.AssertIsValidResourcePath(resourcePath);
			GameObjectCreationParameters gameObjectInfo = new GameObjectCreationParameters();
			base.ProviderFunc = (DiContainer container) => new SubContainerDependencyProvider(base.ContractType, base.SubIdentifier, new SubContainerCreatorByNewPrefabMethod<TParam1>(container, new PrefabProviderResource(resourcePath), gameObjectInfo, installerMethod), false);
			return new NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder(base.BindInfo, gameObjectInfo);
		}
	}
	[NoReflectionBaking]
	public class FactorySubContainerBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TParam9, TParam10, TContract> : FactorySubContainerBinderWithParams<TContract>
	{
		public FactorySubContainerBinder(DiContainer bindContainer, BindInfo bindInfo, FactoryBindInfo factoryBindInfo, object subIdentifier)
			: base(bindContainer, bindInfo, factoryBindInfo, subIdentifier)
		{
		}

		public ScopeConcreteIdArgConditionCopyNonLazyBinder ByMethod(ModestTree.Util.Action<DiContainer, TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TParam9, TParam10> installerMethod)
		{
			SubContainerCreatorBindInfo subcontainerBindInfo = new SubContainerCreatorBindInfo();
			base.ProviderFunc = (DiContainer container) => new SubContainerDependencyProvider(base.ContractType, base.SubIdentifier, new SubContainerCreatorByMethod<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TParam9, TParam10>(container, subcontainerBindInfo, installerMethod), false);
			return new ScopeConcreteIdArgConditionCopyNonLazyBinder(base.BindInfo);
		}

		public NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder ByNewGameObjectMethod(ModestTree.Util.Action<DiContainer, TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TParam9, TParam10> installerMethod)
		{
			GameObjectCreationParameters gameObjectInfo = new GameObjectCreationParameters();
			base.ProviderFunc = (DiContainer container) => new SubContainerDependencyProvider(base.ContractType, base.SubIdentifier, new SubContainerCreatorByNewGameObjectMethod<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TParam9, TParam10>(container, gameObjectInfo, installerMethod), false);
			return new NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder(base.BindInfo, gameObjectInfo);
		}

		public NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder ByNewPrefabMethod(UnityEngine.Object prefab, ModestTree.Util.Action<DiContainer, TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TParam9, TParam10> installerMethod)
		{
			BindingUtil.AssertIsValidPrefab(prefab);
			GameObjectCreationParameters gameObjectInfo = new GameObjectCreationParameters();
			base.ProviderFunc = (DiContainer container) => new SubContainerDependencyProvider(base.ContractType, base.SubIdentifier, new SubContainerCreatorByNewPrefabMethod<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TParam9, TParam10>(container, new PrefabProvider(prefab), gameObjectInfo, installerMethod), false);
			return new NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder(base.BindInfo, gameObjectInfo);
		}

		public NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder ByNewPrefabResourceMethod(string resourcePath, ModestTree.Util.Action<DiContainer, TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TParam9, TParam10> installerMethod)
		{
			BindingUtil.AssertIsValidResourcePath(resourcePath);
			GameObjectCreationParameters gameObjectInfo = new GameObjectCreationParameters();
			base.ProviderFunc = (DiContainer container) => new SubContainerDependencyProvider(base.ContractType, base.SubIdentifier, new SubContainerCreatorByNewPrefabMethod<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TParam9, TParam10>(container, new PrefabProviderResource(resourcePath), gameObjectInfo, installerMethod), false);
			return new NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder(base.BindInfo, gameObjectInfo);
		}
	}
	[NoReflectionBaking]
	public class FactorySubContainerBinder<TParam1, TParam2, TContract> : FactorySubContainerBinderWithParams<TContract>
	{
		public FactorySubContainerBinder(DiContainer bindContainer, BindInfo bindInfo, FactoryBindInfo factoryBindInfo, object subIdentifier)
			: base(bindContainer, bindInfo, factoryBindInfo, subIdentifier)
		{
		}

		public ScopeConcreteIdArgConditionCopyNonLazyBinder ByMethod(Action<DiContainer, TParam1, TParam2> installerMethod)
		{
			SubContainerCreatorBindInfo subcontainerBindInfo = new SubContainerCreatorBindInfo();
			base.ProviderFunc = (DiContainer container) => new SubContainerDependencyProvider(base.ContractType, base.SubIdentifier, new SubContainerCreatorByMethod<TParam1, TParam2>(container, subcontainerBindInfo, installerMethod), false);
			return new ScopeConcreteIdArgConditionCopyNonLazyBinder(base.BindInfo);
		}

		public NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder ByNewGameObjectMethod(Action<DiContainer, TParam1, TParam2> installerMethod)
		{
			GameObjectCreationParameters gameObjectInfo = new GameObjectCreationParameters();
			base.ProviderFunc = (DiContainer container) => new SubContainerDependencyProvider(base.ContractType, base.SubIdentifier, new SubContainerCreatorByNewGameObjectMethod<TParam1, TParam2>(container, gameObjectInfo, installerMethod), false);
			return new NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder(base.BindInfo, gameObjectInfo);
		}

		public NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder ByNewPrefabMethod(UnityEngine.Object prefab, Action<DiContainer, TParam1, TParam2> installerMethod)
		{
			BindingUtil.AssertIsValidPrefab(prefab);
			GameObjectCreationParameters gameObjectInfo = new GameObjectCreationParameters();
			base.ProviderFunc = (DiContainer container) => new SubContainerDependencyProvider(base.ContractType, base.SubIdentifier, new SubContainerCreatorByNewPrefabMethod<TParam1, TParam2>(container, new PrefabProvider(prefab), gameObjectInfo, installerMethod), false);
			return new NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder(base.BindInfo, gameObjectInfo);
		}

		public NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder ByNewPrefabResourceMethod(string resourcePath, Action<DiContainer, TParam1, TParam2> installerMethod)
		{
			BindingUtil.AssertIsValidResourcePath(resourcePath);
			GameObjectCreationParameters gameObjectInfo = new GameObjectCreationParameters();
			base.ProviderFunc = (DiContainer container) => new SubContainerDependencyProvider(base.ContractType, base.SubIdentifier, new SubContainerCreatorByNewPrefabMethod<TParam1, TParam2>(container, new PrefabProviderResource(resourcePath), gameObjectInfo, installerMethod), false);
			return new NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder(base.BindInfo, gameObjectInfo);
		}
	}
	[NoReflectionBaking]
	public class FactorySubContainerBinder<TParam1, TParam2, TParam3, TContract> : FactorySubContainerBinderWithParams<TContract>
	{
		public FactorySubContainerBinder(DiContainer bindContainer, BindInfo bindInfo, FactoryBindInfo factoryBindInfo, object subIdentifier)
			: base(bindContainer, bindInfo, factoryBindInfo, subIdentifier)
		{
		}

		public ScopeConcreteIdArgConditionCopyNonLazyBinder ByMethod(Action<DiContainer, TParam1, TParam2, TParam3> installerMethod)
		{
			SubContainerCreatorBindInfo subcontainerBindInfo = new SubContainerCreatorBindInfo();
			base.ProviderFunc = (DiContainer container) => new SubContainerDependencyProvider(base.ContractType, base.SubIdentifier, new SubContainerCreatorByMethod<TParam1, TParam2, TParam3>(container, subcontainerBindInfo, installerMethod), false);
			return new ScopeConcreteIdArgConditionCopyNonLazyBinder(base.BindInfo);
		}

		public NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder ByNewGameObjectMethod(Action<DiContainer, TParam1, TParam2, TParam3> installerMethod)
		{
			GameObjectCreationParameters gameObjectInfo = new GameObjectCreationParameters();
			base.ProviderFunc = (DiContainer container) => new SubContainerDependencyProvider(base.ContractType, base.SubIdentifier, new SubContainerCreatorByNewGameObjectMethod<TParam1, TParam2, TParam3>(container, gameObjectInfo, installerMethod), false);
			return new NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder(base.BindInfo, gameObjectInfo);
		}

		public NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder ByNewPrefabMethod(UnityEngine.Object prefab, Action<DiContainer, TParam1, TParam2, TParam3> installerMethod)
		{
			BindingUtil.AssertIsValidPrefab(prefab);
			GameObjectCreationParameters gameObjectInfo = new GameObjectCreationParameters();
			base.ProviderFunc = (DiContainer container) => new SubContainerDependencyProvider(base.ContractType, base.SubIdentifier, new SubContainerCreatorByNewPrefabMethod<TParam1, TParam2, TParam3>(container, new PrefabProvider(prefab), gameObjectInfo, installerMethod), false);
			return new NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder(base.BindInfo, gameObjectInfo);
		}

		public NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder ByNewPrefabResourceMethod(string resourcePath, Action<DiContainer, TParam1, TParam2, TParam3> installerMethod)
		{
			BindingUtil.AssertIsValidResourcePath(resourcePath);
			GameObjectCreationParameters gameObjectInfo = new GameObjectCreationParameters();
			base.ProviderFunc = (DiContainer container) => new SubContainerDependencyProvider(base.ContractType, base.SubIdentifier, new SubContainerCreatorByNewPrefabMethod<TParam1, TParam2, TParam3>(container, new PrefabProviderResource(resourcePath), gameObjectInfo, installerMethod), false);
			return new NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder(base.BindInfo, gameObjectInfo);
		}
	}
	[NoReflectionBaking]
	public class FactorySubContainerBinder<TParam1, TParam2, TParam3, TParam4, TContract> : FactorySubContainerBinderWithParams<TContract>
	{
		public FactorySubContainerBinder(DiContainer bindContainer, BindInfo bindInfo, FactoryBindInfo factoryBindInfo, object subIdentifier)
			: base(bindContainer, bindInfo, factoryBindInfo, subIdentifier)
		{
		}

		public ScopeConcreteIdArgConditionCopyNonLazyBinder ByMethod(ModestTree.Util.Action<DiContainer, TParam1, TParam2, TParam3, TParam4> installerMethod)
		{
			SubContainerCreatorBindInfo subcontainerBindInfo = new SubContainerCreatorBindInfo();
			base.ProviderFunc = (DiContainer container) => new SubContainerDependencyProvider(base.ContractType, base.SubIdentifier, new SubContainerCreatorByMethod<TParam1, TParam2, TParam3, TParam4>(container, subcontainerBindInfo, installerMethod), false);
			return new ScopeConcreteIdArgConditionCopyNonLazyBinder(base.BindInfo);
		}

		public NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder ByNewGameObjectMethod(ModestTree.Util.Action<DiContainer, TParam1, TParam2, TParam3, TParam4> installerMethod)
		{
			GameObjectCreationParameters gameObjectInfo = new GameObjectCreationParameters();
			base.ProviderFunc = (DiContainer container) => new SubContainerDependencyProvider(base.ContractType, base.SubIdentifier, new SubContainerCreatorByNewGameObjectMethod<TParam1, TParam2, TParam3, TParam4>(container, gameObjectInfo, installerMethod), false);
			return new NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder(base.BindInfo, gameObjectInfo);
		}

		public NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder ByNewPrefabMethod(UnityEngine.Object prefab, ModestTree.Util.Action<DiContainer, TParam1, TParam2, TParam3, TParam4> installerMethod)
		{
			BindingUtil.AssertIsValidPrefab(prefab);
			GameObjectCreationParameters gameObjectInfo = new GameObjectCreationParameters();
			base.ProviderFunc = (DiContainer container) => new SubContainerDependencyProvider(base.ContractType, base.SubIdentifier, new SubContainerCreatorByNewPrefabMethod<TParam1, TParam2, TParam3, TParam4>(container, new PrefabProvider(prefab), gameObjectInfo, installerMethod), false);
			return new NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder(base.BindInfo, gameObjectInfo);
		}

		public NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder ByNewPrefabResourceMethod(string resourcePath, ModestTree.Util.Action<DiContainer, TParam1, TParam2, TParam3, TParam4> installerMethod)
		{
			BindingUtil.AssertIsValidResourcePath(resourcePath);
			GameObjectCreationParameters gameObjectInfo = new GameObjectCreationParameters();
			base.ProviderFunc = (DiContainer container) => new SubContainerDependencyProvider(base.ContractType, base.SubIdentifier, new SubContainerCreatorByNewPrefabMethod<TParam1, TParam2, TParam3, TParam4>(container, new PrefabProviderResource(resourcePath), gameObjectInfo, installerMethod), false);
			return new NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder(base.BindInfo, gameObjectInfo);
		}
	}
	[NoReflectionBaking]
	public class FactorySubContainerBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TContract> : FactorySubContainerBinderWithParams<TContract>
	{
		public FactorySubContainerBinder(DiContainer bindContainer, BindInfo bindInfo, FactoryBindInfo factoryBindInfo, object subIdentifier)
			: base(bindContainer, bindInfo, factoryBindInfo, subIdentifier)
		{
		}

		public ScopeConcreteIdArgConditionCopyNonLazyBinder ByMethod(ModestTree.Util.Action<DiContainer, TParam1, TParam2, TParam3, TParam4, TParam5> installerMethod)
		{
			SubContainerCreatorBindInfo subcontainerBindInfo = new SubContainerCreatorBindInfo();
			base.ProviderFunc = (DiContainer container) => new SubContainerDependencyProvider(base.ContractType, base.SubIdentifier, new SubContainerCreatorByMethod<TParam1, TParam2, TParam3, TParam4, TParam5>(container, subcontainerBindInfo, installerMethod), false);
			return new ScopeConcreteIdArgConditionCopyNonLazyBinder(base.BindInfo);
		}

		public NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder ByNewGameObjectMethod(ModestTree.Util.Action<DiContainer, TParam1, TParam2, TParam3, TParam4, TParam5> installerMethod)
		{
			GameObjectCreationParameters gameObjectInfo = new GameObjectCreationParameters();
			base.ProviderFunc = (DiContainer container) => new SubContainerDependencyProvider(base.ContractType, base.SubIdentifier, new SubContainerCreatorByNewGameObjectMethod<TParam1, TParam2, TParam3, TParam4, TParam5>(container, gameObjectInfo, installerMethod), false);
			return new NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder(base.BindInfo, gameObjectInfo);
		}

		public NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder ByNewPrefabMethod(UnityEngine.Object prefab, ModestTree.Util.Action<DiContainer, TParam1, TParam2, TParam3, TParam4, TParam5> installerMethod)
		{
			BindingUtil.AssertIsValidPrefab(prefab);
			GameObjectCreationParameters gameObjectInfo = new GameObjectCreationParameters();
			base.ProviderFunc = (DiContainer container) => new SubContainerDependencyProvider(base.ContractType, base.SubIdentifier, new SubContainerCreatorByNewPrefabMethod<TParam1, TParam2, TParam3, TParam4, TParam5>(container, new PrefabProvider(prefab), gameObjectInfo, installerMethod), false);
			return new NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder(base.BindInfo, gameObjectInfo);
		}

		public NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder ByNewPrefabResourceMethod(string resourcePath, ModestTree.Util.Action<DiContainer, TParam1, TParam2, TParam3, TParam4, TParam5> installerMethod)
		{
			BindingUtil.AssertIsValidResourcePath(resourcePath);
			GameObjectCreationParameters gameObjectInfo = new GameObjectCreationParameters();
			base.ProviderFunc = (DiContainer container) => new SubContainerDependencyProvider(base.ContractType, base.SubIdentifier, new SubContainerCreatorByNewPrefabMethod<TParam1, TParam2, TParam3, TParam4, TParam5>(container, new PrefabProviderResource(resourcePath), gameObjectInfo, installerMethod), false);
			return new NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder(base.BindInfo, gameObjectInfo);
		}
	}
	[NoReflectionBaking]
	public class FactorySubContainerBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TContract> : FactorySubContainerBinderWithParams<TContract>
	{
		public FactorySubContainerBinder(DiContainer bindContainer, BindInfo bindInfo, FactoryBindInfo factoryBindInfo, object subIdentifier)
			: base(bindContainer, bindInfo, factoryBindInfo, subIdentifier)
		{
		}

		public ScopeConcreteIdArgConditionCopyNonLazyBinder ByMethod(ModestTree.Util.Action<DiContainer, TParam1, TParam2, TParam3, TParam4, TParam5, TParam6> installerMethod)
		{
			SubContainerCreatorBindInfo subcontainerBindInfo = new SubContainerCreatorBindInfo();
			base.ProviderFunc = (DiContainer container) => new SubContainerDependencyProvider(base.ContractType, base.SubIdentifier, new SubContainerCreatorByMethod<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>(container, subcontainerBindInfo, installerMethod), false);
			return new ScopeConcreteIdArgConditionCopyNonLazyBinder(base.BindInfo);
		}

		public NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder ByNewGameObjectMethod(ModestTree.Util.Action<DiContainer, TParam1, TParam2, TParam3, TParam4, TParam5, TParam6> installerMethod)
		{
			GameObjectCreationParameters gameObjectInfo = new GameObjectCreationParameters();
			base.ProviderFunc = (DiContainer container) => new SubContainerDependencyProvider(base.ContractType, base.SubIdentifier, new SubContainerCreatorByNewGameObjectMethod<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>(container, gameObjectInfo, installerMethod), false);
			return new NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder(base.BindInfo, gameObjectInfo);
		}

		public NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder ByNewPrefabMethod(UnityEngine.Object prefab, ModestTree.Util.Action<DiContainer, TParam1, TParam2, TParam3, TParam4, TParam5, TParam6> installerMethod)
		{
			BindingUtil.AssertIsValidPrefab(prefab);
			GameObjectCreationParameters gameObjectInfo = new GameObjectCreationParameters();
			base.ProviderFunc = (DiContainer container) => new SubContainerDependencyProvider(base.ContractType, base.SubIdentifier, new SubContainerCreatorByNewPrefabMethod<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>(container, new PrefabProvider(prefab), gameObjectInfo, installerMethod), false);
			return new NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder(base.BindInfo, gameObjectInfo);
		}

		public NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder ByNewPrefabResourceMethod(string resourcePath, ModestTree.Util.Action<DiContainer, TParam1, TParam2, TParam3, TParam4, TParam5, TParam6> installerMethod)
		{
			BindingUtil.AssertIsValidResourcePath(resourcePath);
			GameObjectCreationParameters gameObjectInfo = new GameObjectCreationParameters();
			base.ProviderFunc = (DiContainer container) => new SubContainerDependencyProvider(base.ContractType, base.SubIdentifier, new SubContainerCreatorByNewPrefabMethod<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>(container, new PrefabProviderResource(resourcePath), gameObjectInfo, installerMethod), false);
			return new NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder(base.BindInfo, gameObjectInfo);
		}
	}
}
