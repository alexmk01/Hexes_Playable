using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ModestTree;
using ModestTree.Util;
using UnityEngine;
using Zenject.Internal;

namespace Zenject
{
	[NoReflectionBaking]
	public class DiContainer : IInstantiator
	{
		private class ProviderInfo
		{
			public readonly DiContainer Container;

			public readonly bool NonLazy;

			public readonly IProvider Provider;

			public readonly BindingCondition Condition;

			public ProviderInfo(IProvider provider, BindingCondition condition, bool nonLazy, DiContainer container)
			{
				Provider = provider;
				Condition = condition;
				NonLazy = nonLazy;
				Container = container;
			}
		}

		private readonly Dictionary<Type, IDecoratorProvider> _decorators = new Dictionary<Type, IDecoratorProvider>();

		private readonly Dictionary<BindingId, List<ProviderInfo>> _providers = new Dictionary<BindingId, List<ProviderInfo>>();

		private readonly DiContainer[][] _containerLookups = new DiContainer[4][];

		private readonly HashSet<LookupId> _resolvesInProgress = new HashSet<LookupId>();

		private readonly HashSet<LookupId> _resolvesTwiceInProgress = new HashSet<LookupId>();

		private readonly LazyInstanceInjector _lazyInjector;

		private readonly SingletonMarkRegistry _singletonMarkRegistry = new SingletonMarkRegistry();

		private readonly Queue<BindStatement> _currentBindings = new Queue<BindStatement>();

		private readonly List<BindStatement> _childBindings = new List<BindStatement>();

		private readonly HashSet<Type> _validatedTypes = new HashSet<Type>();

		private readonly List<IValidatable> _validationQueue = new List<IValidatable>();

		private Transform _contextTransform;

		private bool _hasLookedUpContextTransform;

		private Transform _inheritedDefaultParent;

		private Transform _explicitDefaultParent;

		private bool _hasExplicitDefaultParent;

		private ZenjectSettings _settings;

		private bool _hasResolvedRoots;

		private bool _isFinalizingBinding;

		private bool _isValidating;

		private bool _isInstalling;

		public ZenjectSettings Settings
		{
			get
			{
				return _settings;
			}
			set
			{
				_settings = value;
				Rebind<ZenjectSettings>().FromInstance(value);
			}
		}

		internal SingletonMarkRegistry SingletonMarkRegistry => _singletonMarkRegistry;

		public IEnumerable<IProvider> AllProviders => (from x in _providers.Values.SelectMany((List<ProviderInfo> x) => x)
			select x.Provider).Distinct();

		private Transform ContextTransform
		{
			get
			{
				if (!_hasLookedUpContextTransform)
				{
					_hasLookedUpContextTransform = true;
					Context context = TryResolve<Context>();
					if (context != null)
					{
						_contextTransform = context.transform;
					}
				}
				return _contextTransform;
			}
		}

		public bool AssertOnNewGameObjects { get; set; }

		public Transform InheritedDefaultParent => _inheritedDefaultParent;

		public Transform DefaultParent
		{
			get
			{
				return _explicitDefaultParent;
			}
			set
			{
				_explicitDefaultParent = value;
				_hasExplicitDefaultParent = true;
			}
		}

		public DiContainer[] ParentContainers => _containerLookups[2];

		public DiContainer[] AncestorContainers => _containerLookups[3];

		public bool ChecksForCircularDependencies => true;

		public bool IsValidating => _isValidating;

		public bool IsInstalling
		{
			get
			{
				return _isInstalling;
			}
			set
			{
				_isInstalling = value;
			}
		}

		public IEnumerable<BindingId> AllContracts
		{
			get
			{
				FlushBindings();
				return _providers.Keys;
			}
		}

		public DiContainer(IEnumerable<DiContainer> parentContainersEnumerable, bool isValidating)
		{
			_isValidating = isValidating;
			_lazyInjector = new LazyInstanceInjector(this);
			InstallDefaultBindings();
			FlushBindings();
			Assert.That(_currentBindings.Count == 0);
			_settings = ZenjectSettings.Default;
			DiContainer[] selfLookup = new DiContainer[1] { this };
			_containerLookups[1] = selfLookup;
			DiContainer[] parentContainers = parentContainersEnumerable.ToArray();
			_containerLookups[2] = parentContainers;
			DiContainer[] ancestorContainers = FlattenInheritanceChain().ToArray();
			_containerLookups[3] = ancestorContainers;
			_containerLookups[0] = selfLookup.Concat(ancestorContainers).ToArray();
			if (!parentContainers.IsEmpty())
			{
				for (int i = 0; i < parentContainers.Length; i++)
				{
					parentContainers[i].FlushBindings();
				}
				_inheritedDefaultParent = parentContainers.First().DefaultParent;
				foreach (DiContainer ancestorContainer in ancestorContainers.Distinct())
				{
					foreach (BindStatement binding in ancestorContainer._childBindings)
					{
						if (ShouldInheritBinding(binding, ancestorContainer))
						{
							FinalizeBinding(binding);
						}
					}
				}
				Assert.That(_currentBindings.Count == 0);
				Assert.That(_childBindings.Count == 0);
			}
			ZenjectSettings settings = TryResolve<ZenjectSettings>();
			if (settings != null)
			{
				_settings = settings;
			}
		}

		public DiContainer(bool isValidating)
			: this(Enumerable.Empty<DiContainer>(), isValidating)
		{
		}

		public DiContainer()
			: this(Enumerable.Empty<DiContainer>(), false)
		{
		}

		public DiContainer(DiContainer parentContainer, bool isValidating)
			: this(new DiContainer[1] { parentContainer }, isValidating)
		{
		}

		public DiContainer(DiContainer parentContainer)
			: this(new DiContainer[1] { parentContainer }, false)
		{
		}

		public DiContainer(IEnumerable<DiContainer> parentContainers)
			: this(parentContainers, false)
		{
		}

		private void InstallDefaultBindings()
		{
			Bind(typeof(DiContainer), typeof(IInstantiator)).FromInstance(this);
			Bind(typeof(LazyInject<>)).FromMethodUntyped(CreateLazyBinding).Lazy();
		}

		private object CreateLazyBinding(InjectContext context)
		{
			InjectContext newContext = context.Clone();
			newContext.MemberType = context.MemberType.GenericArguments().Single();
			object result = Activator.CreateInstance(typeof(LazyInject<>).MakeGenericType(newContext.MemberType), this, newContext);
			if (_isValidating)
			{
				QueueForValidate((IValidatable)result);
			}
			return result;
		}

		public void QueueForValidate(IValidatable validatable)
		{
			if (!_hasResolvedRoots)
			{
				Type concreteType = validatable.GetType();
				if (!_validatedTypes.Contains(concreteType))
				{
					_validatedTypes.Add(concreteType);
					_validationQueue.Add(validatable);
				}
			}
		}

		private bool ShouldInheritBinding(BindStatement binding, DiContainer ancestorContainer)
		{
			if (binding.BindingInheritanceMethod == BindingInheritanceMethods.CopyIntoAll || binding.BindingInheritanceMethod == BindingInheritanceMethods.MoveIntoAll)
			{
				return true;
			}
			if ((binding.BindingInheritanceMethod == BindingInheritanceMethods.CopyDirectOnly || binding.BindingInheritanceMethod == BindingInheritanceMethods.MoveDirectOnly) && ParentContainers.Contains(ancestorContainer))
			{
				return true;
			}
			return false;
		}

		public void ResolveRoots()
		{
			Assert.That(!_hasResolvedRoots);
			FlushBindings();
			ResolveDependencyRoots();
			_lazyInjector.LazyInjectAll();
			if (IsValidating)
			{
				FlushValidationQueue();
			}
			Assert.That(!_hasResolvedRoots);
			_hasResolvedRoots = true;
		}

		private void ResolveDependencyRoots()
		{
			List<BindingId> rootBindings = new List<BindingId>();
			List<ProviderInfo> rootProviders = new List<ProviderInfo>();
			foreach (KeyValuePair<BindingId, List<ProviderInfo>> bindingPair in _providers)
			{
				foreach (ProviderInfo provider in bindingPair.Value)
				{
					if (provider.NonLazy)
					{
						rootBindings.Add(bindingPair.Key);
						rootProviders.Add(provider);
					}
				}
			}
			Assert.IsEqual(rootProviders.Count, rootBindings.Count);
			List<object> instances = ZenPools.SpawnList<object>();
			try
			{
				for (int i = 0; i < rootProviders.Count; i++)
				{
					BindingId bindId = rootBindings[i];
					ProviderInfo providerInfo = rootProviders[i];
					using (InjectContext context = ZenPools.SpawnInjectContext(this, bindId.Type))
					{
						context.Identifier = bindId.Identifier;
						context.SourceType = InjectSources.Local;
						context.Optional = false;
						instances.Clear();
						SafeGetInstances(providerInfo, context, instances);
					}
				}
			}
			finally
			{
				ZenPools.DespawnList(instances);
			}
		}

		private void ValidateFullResolve()
		{
			Assert.That(!_hasResolvedRoots);
			Assert.That(IsValidating);
			foreach (BindingId bindingId in _providers.Keys.ToList())
			{
				if (!bindingId.Type.IsOpenGenericType())
				{
					using (InjectContext context = ZenPools.SpawnInjectContext(this, bindingId.Type))
					{
						context.Identifier = bindingId.Identifier;
						context.SourceType = InjectSources.Local;
						context.Optional = true;
						ResolveAll(context);
					}
				}
			}
		}

		private void FlushValidationQueue()
		{
			Assert.That(!_hasResolvedRoots);
			Assert.That(IsValidating);
			List<IValidatable> validatables = new List<IValidatable>();
			while (_validationQueue.Any())
			{
				validatables.Clear();
				validatables.AllocFreeAddRange(_validationQueue);
				_validationQueue.Clear();
				for (int i = 0; i < validatables.Count; i++)
				{
					validatables[i].Validate();
				}
			}
		}

		public DiContainer CreateSubContainer()
		{
			return CreateSubContainer(_isValidating);
		}

		public void QueueForInject(object instance)
		{
			_lazyInjector.AddInstance(instance);
		}

		public T LazyInject<T>(T instance)
		{
			_lazyInjector.LazyInject(instance);
			return instance;
		}

		private DiContainer CreateSubContainer(bool isValidating)
		{
			return new DiContainer(new DiContainer[1] { this }, isValidating);
		}

		public void RegisterProvider(BindingId bindingId, BindingCondition condition, IProvider provider, bool nonLazy)
		{
			ProviderInfo info = new ProviderInfo(provider, condition, nonLazy, this);
			if (!_providers.TryGetValue(bindingId, out var providerInfos))
			{
				providerInfos = new List<ProviderInfo>();
				_providers.Add(bindingId, providerInfos);
			}
			providerInfos.Add(info);
		}

		private void GetProviderMatches(InjectContext context, List<ProviderInfo> buffer)
		{
			Assert.IsNotNull(context);
			Assert.That(buffer.Count == 0);
			List<ProviderInfo> allMatches = ZenPools.SpawnList<ProviderInfo>();
			try
			{
				GetProvidersForContract(context.BindingId, context.SourceType, allMatches);
				for (int i = 0; i < allMatches.Count; i++)
				{
					ProviderInfo match = allMatches[i];
					if (match.Condition == null || match.Condition(context))
					{
						buffer.Add(match);
					}
				}
			}
			finally
			{
				ZenPools.DespawnList(allMatches);
			}
		}

		private ProviderInfo TryGetUniqueProvider(InjectContext context)
		{
			Assert.IsNotNull(context);
			BindingId bindingId = context.BindingId;
			InjectSources sourceType = context.SourceType;
			DiContainer[] containerLookups = _containerLookups[(int)sourceType];
			for (int i = 0; i < containerLookups.Length; i++)
			{
				containerLookups[i].FlushBindings();
			}
			List<ProviderInfo> localProviders = ZenPools.SpawnList<ProviderInfo>();
			try
			{
				ProviderInfo selected = null;
				int selectedDistance = int.MaxValue;
				bool selectedHasCondition = false;
				bool ambiguousSelection = false;
				foreach (DiContainer container in containerLookups)
				{
					int curDistance = GetContainerHeirarchyDistance(container);
					if (curDistance > selectedDistance)
					{
						continue;
					}
					localProviders.Clear();
					container.GetLocalProviders(bindingId, localProviders);
					for (int k = 0; k < localProviders.Count; k++)
					{
						ProviderInfo provider = localProviders[k];
						bool curHasCondition = provider.Condition != null;
						if (curHasCondition && !provider.Condition(context))
						{
							continue;
						}
						Assert.That(selected == null || selectedDistance == curDistance);
						if (curHasCondition)
						{
							ambiguousSelection = (selectedHasCondition ? true : false);
						}
						else
						{
							if (selectedHasCondition)
							{
								continue;
							}
							if (selected != null)
							{
								ambiguousSelection = true;
							}
						}
						if (!ambiguousSelection)
						{
							selectedDistance = curDistance;
							selectedHasCondition = curHasCondition;
							selected = provider;
						}
					}
				}
				if (ambiguousSelection)
				{
					throw Assert.CreateException("Found multiple matches when only one was expected for type '{0}'{1}. Object graph:\n {2}", context.MemberType, (context.ObjectType == null) ? "" : " while building object with type '{0}'".Fmt(context.ObjectType), context.GetObjectGraphString());
				}
				return selected;
			}
			finally
			{
				ZenPools.DespawnList(localProviders);
			}
		}

		private List<DiContainer> FlattenInheritanceChain()
		{
			List<DiContainer> processed = new List<DiContainer>();
			Queue<DiContainer> containerQueue = new Queue<DiContainer>();
			containerQueue.Enqueue(this);
			while (containerQueue.Count > 0)
			{
				DiContainer current = containerQueue.Dequeue();
				DiContainer[] parentContainers = current.ParentContainers;
				foreach (DiContainer parent in parentContainers)
				{
					if (!processed.Contains(parent))
					{
						processed.Add(parent);
						containerQueue.Enqueue(parent);
					}
				}
			}
			return processed;
		}

		private void GetLocalProviders(BindingId bindingId, List<ProviderInfo> buffer)
		{
			if (_providers.TryGetValue(bindingId, out var localProviders))
			{
				buffer.AllocFreeAddRange(localProviders);
			}
			else if (bindingId.Type.IsGenericType() && _providers.TryGetValue(new BindingId(bindingId.Type.GetGenericTypeDefinition(), bindingId.Identifier), out localProviders))
			{
				buffer.AllocFreeAddRange(localProviders);
			}
		}

		private void GetProvidersForContract(BindingId bindingId, InjectSources sourceType, List<ProviderInfo> buffer)
		{
			DiContainer[] containerLookups = _containerLookups[(int)sourceType];
			for (int j = 0; j < containerLookups.Length; j++)
			{
				containerLookups[j].FlushBindings();
			}
			for (int i = 0; i < containerLookups.Length; i++)
			{
				containerLookups[i].GetLocalProviders(bindingId, buffer);
			}
		}

		public void Install<TInstaller>() where TInstaller : Installer
		{
			Instantiate<TInstaller>().InstallBindings();
		}

		public void Install<TInstaller>(object[] extraArgs) where TInstaller : Installer
		{
			Instantiate<TInstaller>(extraArgs).InstallBindings();
		}

		public IList ResolveAll(InjectContext context)
		{
			List<object> buffer = ZenPools.SpawnList<object>();
			try
			{
				ResolveAll(context, buffer);
				return ReflectionUtil.CreateGenericList(context.MemberType, buffer);
			}
			finally
			{
				ZenPools.DespawnList(buffer);
			}
		}

		public void ResolveAll(InjectContext context, List<object> buffer)
		{
			Assert.IsNotNull(context);
			FlushBindings();
			CheckForInstallWarning(context);
			List<ProviderInfo> matches = ZenPools.SpawnList<ProviderInfo>();
			try
			{
				GetProviderMatches(context, matches);
				if (matches.Count == 0)
				{
					if (!context.Optional)
					{
						throw Assert.CreateException("Could not find required dependency with type '{0}' Object graph:\n {1}", context.MemberType, context.GetObjectGraphString());
					}
					return;
				}
				List<object> instances = ZenPools.SpawnList<object>();
				List<object> allInstances = ZenPools.SpawnList<object>();
				try
				{
					for (int j = 0; j < matches.Count; j++)
					{
						ProviderInfo match = matches[j];
						instances.Clear();
						SafeGetInstances(match, context, instances);
						for (int k = 0; k < instances.Count; k++)
						{
							allInstances.Add(instances[k]);
						}
					}
					if (allInstances.Count == 0 && !context.Optional)
					{
						throw Assert.CreateException("Could not find required dependency with type '{0}'.  Found providers but they returned zero results!", context.MemberType);
					}
					if (IsValidating)
					{
						for (int i = 0; i < allInstances.Count; i++)
						{
							object instance = allInstances[i];
							if (instance is ValidationMarker)
							{
								allInstances[i] = context.MemberType.GetDefaultValue();
							}
						}
					}
					buffer.AllocFreeAddRange(allInstances);
				}
				finally
				{
					ZenPools.DespawnList(instances);
					ZenPools.DespawnList(allInstances);
				}
			}
			finally
			{
				ZenPools.DespawnList(matches);
			}
		}

		private void CheckForInstallWarning(InjectContext context)
		{
			if (_settings.DisplayWarningWhenResolvingDuringInstall)
			{
				Assert.IsNotNull(context);
			}
		}

		public Type ResolveType<T>()
		{
			return ResolveType(typeof(T));
		}

		public Type ResolveType(Type type)
		{
			using (InjectContext context = ZenPools.SpawnInjectContext(this, type))
			{
				return ResolveType(context);
			}
		}

		public Type ResolveType(InjectContext context)
		{
			Assert.IsNotNull(context);
			FlushBindings();
			ProviderInfo providerInfo = TryGetUniqueProvider(context);
			if (providerInfo == null)
			{
				throw Assert.CreateException("Unable to resolve {0}{1}. Object graph:\n{2}", context.BindingId, (context.ObjectType == null) ? "" : " while building object with type '{0}'".Fmt(context.ObjectType), context.GetObjectGraphString());
			}
			return providerInfo.Provider.GetInstanceType(context);
		}

		public List<Type> ResolveTypeAll(Type type)
		{
			return ResolveTypeAll(type, null);
		}

		public List<Type> ResolveTypeAll(Type type, object identifier)
		{
			using (InjectContext context = ZenPools.SpawnInjectContext(this, type))
			{
				context.Identifier = identifier;
				return ResolveTypeAll(context);
			}
		}

		public List<Type> ResolveTypeAll(InjectContext context)
		{
			Assert.IsNotNull(context);
			FlushBindings();
			List<ProviderInfo> matches = ZenPools.SpawnList<ProviderInfo>();
			try
			{
				GetProviderMatches(context, matches);
				if (matches.Count > 0)
				{
					return (from x in matches
						select x.Provider.GetInstanceType(context) into x
						where x != null
						select x).ToList();
				}
				return new List<Type>();
			}
			finally
			{
				ZenPools.DespawnList(matches);
			}
		}

		public object Resolve(BindingId id)
		{
			using (InjectContext context = ZenPools.SpawnInjectContext(this, id.Type))
			{
				context.Identifier = id.Identifier;
				return Resolve(context);
			}
		}

		public object Resolve(InjectContext context)
		{
			Assert.IsNotNull(context);
			Type memberType = context.MemberType;
			FlushBindings();
			CheckForInstallWarning(context);
			InjectContext lookupContext = context;
			if (memberType.IsGenericType() && memberType.GetGenericTypeDefinition() == typeof(LazyInject<>))
			{
				lookupContext = context.Clone();
				lookupContext.Identifier = null;
				lookupContext.SourceType = InjectSources.Local;
				lookupContext.Optional = false;
			}
			ProviderInfo providerInfo = TryGetUniqueProvider(lookupContext);
			if (providerInfo == null)
			{
				if (memberType.IsArray && memberType.GetArrayRank() == 1)
				{
					Type subType2 = memberType.GetElementType();
					InjectContext subContext2 = context.Clone();
					subContext2.MemberType = subType2;
					subContext2.Optional = true;
					List<object> results = ZenPools.SpawnList<object>();
					try
					{
						ResolveAll(subContext2, results);
						return ReflectionUtil.CreateArray(subContext2.MemberType, results);
					}
					finally
					{
						ZenPools.DespawnList(results);
					}
				}
				if (memberType.IsGenericType() && (memberType.GetGenericTypeDefinition() == typeof(List<>) || memberType.GetGenericTypeDefinition() == typeof(IList<>) || memberType.GetGenericTypeDefinition() == typeof(IEnumerable<>)))
				{
					Type subType = memberType.GenericArguments().Single();
					InjectContext subContext = context.Clone();
					subContext.MemberType = subType;
					subContext.Optional = true;
					return ResolveAll(subContext);
				}
				if (context.Optional)
				{
					return context.FallBackValue;
				}
				throw Assert.CreateException("Unable to resolve '{0}'{1}. Object graph:\n{2}", context.BindingId, (context.ObjectType == null) ? "" : " while building object with type '{0}'".Fmt(context.ObjectType), context.GetObjectGraphString());
			}
			List<object> instances = ZenPools.SpawnList<object>();
			try
			{
				SafeGetInstances(providerInfo, context, instances);
				if (instances.Count == 0)
				{
					if (context.Optional)
					{
						return context.FallBackValue;
					}
					throw Assert.CreateException("Unable to resolve '{0}'{1}. Object graph:\n{2}", context.BindingId, (context.ObjectType == null) ? "" : " while building object with type '{0}'".Fmt(context.ObjectType), context.GetObjectGraphString());
				}
				if (instances.Count() > 1)
				{
					throw Assert.CreateException("Provider returned multiple instances when only one was expected!  While resolving '{0}'{1}. Object graph:\n{2}", context.BindingId, (context.ObjectType == null) ? "" : " while building object with type '{0}'".Fmt(context.ObjectType), context.GetObjectGraphString());
				}
				return instances.First();
			}
			finally
			{
				ZenPools.DespawnList(instances);
			}
		}

		private void SafeGetInstances(ProviderInfo providerInfo, InjectContext context, List<object> instances)
		{
			Assert.IsNotNull(context);
			IProvider provider = providerInfo.Provider;
			if (ChecksForCircularDependencies)
			{
				LookupId lookupId = ZenPools.SpawnLookupId(provider, context.BindingId);
				try
				{
					DiContainer providerContainer = providerInfo.Container;
					if (providerContainer._resolvesTwiceInProgress.Contains(lookupId))
					{
						throw Assert.CreateException("Circular dependency detected! Object graph:\n {0}", context.GetObjectGraphString());
					}
					bool twice = false;
					if (!providerContainer._resolvesInProgress.Add(lookupId))
					{
						bool added = providerContainer._resolvesTwiceInProgress.Add(lookupId);
						Assert.That(added);
						twice = true;
					}
					try
					{
						GetDecoratedInstances(provider, context, instances);
						return;
					}
					finally
					{
						if (twice)
						{
							bool removed2 = providerContainer._resolvesTwiceInProgress.Remove(lookupId);
							Assert.That(removed2);
						}
						else
						{
							bool removed = providerContainer._resolvesInProgress.Remove(lookupId);
							Assert.That(removed);
						}
					}
				}
				finally
				{
					ZenPools.DespawnLookupId(lookupId);
				}
			}
			GetDecoratedInstances(provider, context, instances);
		}

		public DecoratorToChoiceFromBinder<TContract> Decorate<TContract>()
		{
			BindStatement bindStatement = StartBinding();
			BindInfo bindInfo = bindStatement.SpawnBindInfo();
			bindInfo.ContractTypes.Add(typeof(IFactory<TContract, TContract>));
			FactoryBindInfo factoryBindInfo = new FactoryBindInfo(typeof(PlaceholderFactory<TContract, TContract>));
			bindStatement.SetFinalizer(new PlaceholderFactoryBindingFinalizer<TContract>(bindInfo, factoryBindInfo));
			Guid bindId = Guid.NewGuid();
			bindInfo.Identifier = bindId;
			if (!_decorators.TryGetValue(typeof(TContract), out var decoratorProvider))
			{
				decoratorProvider = new DecoratorProvider<TContract>(this);
				_decorators.Add(typeof(TContract), decoratorProvider);
			}
			((DecoratorProvider<TContract>)decoratorProvider).AddFactoryId(bindId);
			return new DecoratorToChoiceFromBinder<TContract>(this, bindInfo, factoryBindInfo);
		}

		private void GetDecoratedInstances(IProvider provider, InjectContext context, List<object> buffer)
		{
			IDecoratorProvider decoratorProvider = TryGetDecoratorProvider(context.BindingId.Type);
			if (decoratorProvider != null)
			{
				decoratorProvider.GetAllInstances(provider, context, buffer);
			}
			else
			{
				provider.GetAllInstances(context, buffer);
			}
		}

		private IDecoratorProvider TryGetDecoratorProvider(Type contractType)
		{
			if (_decorators.TryGetValue(contractType, out var decoratorProvider))
			{
				return decoratorProvider;
			}
			DiContainer[] ancestorContainers = AncestorContainers;
			for (int i = 0; i < ancestorContainers.Length; i++)
			{
				if (ancestorContainers[i]._decorators.TryGetValue(contractType, out decoratorProvider))
				{
					return decoratorProvider;
				}
			}
			return null;
		}

		private int GetContainerHeirarchyDistance(DiContainer container)
		{
			return GetContainerHeirarchyDistance(container, 0).Value;
		}

		private int? GetContainerHeirarchyDistance(DiContainer container, int depth)
		{
			if (container == this)
			{
				return depth;
			}
			int? result = null;
			DiContainer[] parentContainers = ParentContainers;
			foreach (DiContainer parent in parentContainers)
			{
				int? distance = parent.GetContainerHeirarchyDistance(container, depth + 1);
				if (distance.HasValue && (!result.HasValue || distance.Value < result.Value))
				{
					result = distance;
				}
			}
			return result;
		}

		public IEnumerable<Type> GetDependencyContracts<TContract>()
		{
			return GetDependencyContracts(typeof(TContract));
		}

		public IEnumerable<Type> GetDependencyContracts(Type contract)
		{
			FlushBindings();
			InjectTypeInfo info = TypeAnalyzer.TryGetInfo(contract);
			if (info == null)
			{
				yield break;
			}
			foreach (InjectableInfo injectMember in info.AllInjectables)
			{
				yield return injectMember.MemberType;
			}
		}

		private object InstantiateInternal(Type concreteType, bool autoInject, List<TypeValuePair> extraArgs, InjectContext context, object concreteIdentifier)
		{
			Assert.That(!concreteType.DerivesFrom<Component>(), "Error occurred while instantiating object of type '{0}'. Instantiator should not be used to create new mono behaviours.  Must use InstantiatePrefabForComponent, InstantiatePrefab, or InstantiateComponent.", concreteType);
			Assert.That(!concreteType.IsAbstract(), "Expected type '{0}' to be non-abstract", concreteType);
			FlushBindings();
			CheckForInstallWarning(context);
			InjectTypeInfo typeInfo = TypeAnalyzer.TryGetInfo(concreteType);
			Assert.IsNotNull(typeInfo, "Tried to create type '{0}' but could not find type information", concreteType);
			bool allowDuringValidation = IsValidating && TypeAnalyzer.ShouldAllowDuringValidation(concreteType);
			object newObj;
			if (concreteType.DerivesFrom<ScriptableObject>())
			{
				Assert.That(typeInfo.InjectConstructor.Parameters.Length == 0, "Found constructor parameters on ScriptableObject type '{0}'.  This is not allowed.  Use an [Inject] method or fields instead.");
				newObj = ((!(!IsValidating || allowDuringValidation)) ? ((object)new ValidationMarker(concreteType)) : ((object)ScriptableObject.CreateInstance(concreteType)));
			}
			else
			{
				Assert.IsNotNull(typeInfo.InjectConstructor.Factory, "More than one (or zero) constructors found for type '{0}' when creating dependencies.  Use one [Inject] attribute to specify which to use.", concreteType);
				object[] paramValues = ZenPools.SpawnArray<object>(typeInfo.InjectConstructor.Parameters.Length);
				try
				{
					for (int i = 0; i < typeInfo.InjectConstructor.Parameters.Length; i++)
					{
						InjectableInfo injectInfo = typeInfo.InjectConstructor.Parameters[i];
						if (!InjectUtil.PopValueWithType(extraArgs, injectInfo.MemberType, out var value))
						{
							using (InjectContext subContext = ZenPools.SpawnInjectContext(this, injectInfo, context, null, concreteType, concreteIdentifier))
							{
								value = Resolve(subContext);
							}
						}
						if (value == null || value is ValidationMarker)
						{
							paramValues[i] = injectInfo.MemberType.GetDefaultValue();
						}
						else
						{
							paramValues[i] = value;
						}
					}
					if (!IsValidating || allowDuringValidation)
					{
						try
						{
							newObj = typeInfo.InjectConstructor.Factory(paramValues);
						}
						catch (Exception e)
						{
							throw Assert.CreateException(e, "Error occurred while instantiating object with type '{0}'", concreteType);
						}
					}
					else
					{
						newObj = new ValidationMarker(concreteType);
					}
				}
				finally
				{
					ZenPools.DespawnArray(paramValues);
				}
			}
			if (autoInject)
			{
				InjectExplicit(newObj, concreteType, extraArgs, context, concreteIdentifier);
				if (extraArgs.Count > 0 && !(newObj is ValidationMarker))
				{
					throw Assert.CreateException("Passed unnecessary parameters when injecting into type '{0}'. \nExtra Parameters: {1}\nObject graph:\n{2}", newObj.GetType(), string.Join(",", extraArgs.Select((TypeValuePair x) => x.Type.PrettyName()).ToArray()), context.GetObjectGraphString());
				}
			}
			return newObj;
		}

		public void InjectExplicit(object injectable, List<TypeValuePair> extraArgs)
		{
			Type injectableType = ((!(injectable is ValidationMarker)) ? injectable.GetType() : ((ValidationMarker)injectable).MarkedType);
			InjectExplicit(injectable, injectableType, extraArgs, new InjectContext(this, injectableType, null), null);
		}

		public void InjectExplicit(object injectable, Type injectableType, List<TypeValuePair> extraArgs, InjectContext context, object concreteIdentifier)
		{
			if (IsValidating)
			{
				if (injectable is ValidationMarker marker && marker.InstantiateFailed)
				{
					return;
				}
				if (_settings.ValidationErrorResponse != ValidationErrorResponses.Throw)
				{
					try
					{
						InjectExplicitInternal(injectable, injectableType, extraArgs, context, concreteIdentifier);
						return;
					}
					catch (Exception e)
					{
						Log.ErrorException(e);
						return;
					}
				}
				InjectExplicitInternal(injectable, injectableType, extraArgs, context, concreteIdentifier);
			}
			else
			{
				InjectExplicitInternal(injectable, injectableType, extraArgs, context, concreteIdentifier);
			}
		}

		private void CallInjectMethodsTopDown(object injectable, Type injectableType, InjectTypeInfo typeInfo, List<TypeValuePair> extraArgs, InjectContext context, object concreteIdentifier, bool isDryRun)
		{
			if (typeInfo.BaseTypeInfo != null)
			{
				CallInjectMethodsTopDown(injectable, injectableType, typeInfo.BaseTypeInfo, extraArgs, context, concreteIdentifier, isDryRun);
			}
			for (int i = 0; i < typeInfo.InjectMethods.Length; i++)
			{
				InjectTypeInfo.InjectMethodInfo method = typeInfo.InjectMethods[i];
				object[] paramValues = ZenPools.SpawnArray<object>(method.Parameters.Length);
				try
				{
					for (int j = 0; j < method.Parameters.Length; j++)
					{
						InjectableInfo injectInfo = method.Parameters[j];
						if (!InjectUtil.PopValueWithType(extraArgs, injectInfo.MemberType, out var value))
						{
							using (InjectContext subContext = ZenPools.SpawnInjectContext(this, injectInfo, context, injectable, injectableType, concreteIdentifier))
							{
								value = Resolve(subContext);
							}
						}
						if (value is ValidationMarker)
						{
							Assert.That(IsValidating);
							paramValues[j] = injectInfo.MemberType.GetDefaultValue();
						}
						else
						{
							paramValues[j] = value;
						}
					}
					if (!isDryRun)
					{
						method.Action(injectable, paramValues);
					}
				}
				finally
				{
					ZenPools.DespawnArray(paramValues);
				}
			}
		}

		private void InjectMembersTopDown(object injectable, Type injectableType, InjectTypeInfo typeInfo, List<TypeValuePair> extraArgs, InjectContext context, object concreteIdentifier, bool isDryRun)
		{
			if (typeInfo.BaseTypeInfo != null)
			{
				InjectMembersTopDown(injectable, injectableType, typeInfo.BaseTypeInfo, extraArgs, context, concreteIdentifier, isDryRun);
			}
			for (int i = 0; i < typeInfo.InjectMembers.Length; i++)
			{
				InjectableInfo injectInfo = typeInfo.InjectMembers[i].Info;
				ZenMemberSetterMethod setterMethod = typeInfo.InjectMembers[i].Setter;
				if (InjectUtil.PopValueWithType(extraArgs, injectInfo.MemberType, out var value))
				{
					if (!isDryRun)
					{
						if (value is ValidationMarker)
						{
							Assert.That(IsValidating);
						}
						else
						{
							setterMethod(injectable, value);
						}
					}
					continue;
				}
				using (InjectContext subContext = ZenPools.SpawnInjectContext(this, injectInfo, context, injectable, injectableType, concreteIdentifier))
				{
					value = Resolve(subContext);
				}
				if ((!injectInfo.Optional || value != null) && !isDryRun)
				{
					if (value is ValidationMarker)
					{
						Assert.That(IsValidating);
					}
					else
					{
						setterMethod(injectable, value);
					}
				}
			}
		}

		private void InjectExplicitInternal(object injectable, Type injectableType, List<TypeValuePair> extraArgs, InjectContext context, object concreteIdentifier)
		{
			Assert.That(injectable != null);
			InjectTypeInfo typeInfo = TypeAnalyzer.TryGetInfo(injectableType);
			if (typeInfo == null)
			{
				Assert.That(extraArgs.IsEmpty());
				return;
			}
			bool allowDuringValidation = IsValidating && TypeAnalyzer.ShouldAllowDuringValidation(injectableType);
			bool isDryRun = IsValidating && !allowDuringValidation;
			if (!isDryRun)
			{
				Assert.IsEqual(injectable.GetType(), injectableType);
			}
			if (injectableType == typeof(GameObject))
			{
				Assert.CreateException("Use InjectGameObject to Inject game objects instead of Inject method. Object graph: {0}", context.GetObjectGraphString());
			}
			FlushBindings();
			CheckForInstallWarning(context);
			InjectMembersTopDown(injectable, injectableType, typeInfo, extraArgs, context, concreteIdentifier, isDryRun);
			CallInjectMethodsTopDown(injectable, injectableType, typeInfo, extraArgs, context, concreteIdentifier, isDryRun);
			if (extraArgs.Count <= 0)
			{
				return;
			}
			throw Assert.CreateException("Passed unnecessary parameters when injecting into type '{0}'. \nExtra Parameters: {1}\nObject graph:\n{2}", injectableType, string.Join(",", extraArgs.Select((TypeValuePair x) => x.Type.PrettyName()).ToArray()), context.GetObjectGraphString());
		}

		internal GameObject CreateAndParentPrefabResource(string resourcePath, GameObjectCreationParameters gameObjectBindInfo, InjectContext context, out bool shouldMakeActive)
		{
			GameObject prefab = (GameObject)Resources.Load(resourcePath);
			Assert.IsNotNull(prefab, "Could not find prefab at resource location '{0}'".Fmt(resourcePath));
			return CreateAndParentPrefab(prefab, gameObjectBindInfo, context, out shouldMakeActive);
		}

		private GameObject GetPrefabAsGameObject(UnityEngine.Object prefab)
		{
			if (prefab is GameObject)
			{
				return (GameObject)prefab;
			}
			Assert.That(prefab is Component, "Invalid type given for prefab. Given object name: '{0}'", prefab.name);
			return ((Component)prefab).gameObject;
		}

		internal GameObject CreateAndParentPrefab(UnityEngine.Object prefab, GameObjectCreationParameters gameObjectBindInfo, InjectContext context, out bool shouldMakeActive)
		{
			Assert.That(prefab != null, "Null prefab found when instantiating game object");
			Assert.That(!AssertOnNewGameObjects, "Given DiContainer does not support creating new game objects");
			FlushBindings();
			GameObject prefabAsGameObject = GetPrefabAsGameObject(prefab);
			bool prefabWasActive = (shouldMakeActive = prefabAsGameObject.activeSelf);
			Transform parent = GetTransformGroup(gameObjectBindInfo, context);
			if (prefabWasActive)
			{
				prefabAsGameObject.SetActive(false);
			}
			Transform initialParent = ((!(parent != null)) ? ContextTransform : parent);
			GameObject gameObj;
			bool positionAndRotationWereSet;
			if (gameObjectBindInfo.Position.HasValue && gameObjectBindInfo.Rotation.HasValue)
			{
				gameObj = UnityEngine.Object.Instantiate(prefabAsGameObject, gameObjectBindInfo.Position.Value, gameObjectBindInfo.Rotation.Value, initialParent);
				positionAndRotationWereSet = true;
			}
			else if (gameObjectBindInfo.Position.HasValue)
			{
				gameObj = UnityEngine.Object.Instantiate(prefabAsGameObject, gameObjectBindInfo.Position.Value, prefabAsGameObject.transform.rotation, initialParent);
				positionAndRotationWereSet = true;
			}
			else if (gameObjectBindInfo.Rotation.HasValue)
			{
				gameObj = UnityEngine.Object.Instantiate(prefabAsGameObject, prefabAsGameObject.transform.position, gameObjectBindInfo.Rotation.Value, initialParent);
				positionAndRotationWereSet = true;
			}
			else
			{
				gameObj = UnityEngine.Object.Instantiate(prefabAsGameObject, initialParent);
				positionAndRotationWereSet = false;
			}
			if (prefabWasActive)
			{
				prefabAsGameObject.SetActive(true);
			}
			if (gameObj.transform.parent != parent)
			{
				gameObj.transform.SetParent(parent, positionAndRotationWereSet);
			}
			if (gameObjectBindInfo.Name != null)
			{
				gameObj.name = gameObjectBindInfo.Name;
			}
			return gameObj;
		}

		public GameObject CreateEmptyGameObject(string name)
		{
			return CreateEmptyGameObject(new GameObjectCreationParameters
			{
				Name = name
			}, null);
		}

		public GameObject CreateEmptyGameObject(GameObjectCreationParameters gameObjectBindInfo, InjectContext context)
		{
			Assert.That(!AssertOnNewGameObjects, "Given DiContainer does not support creating new game objects");
			FlushBindings();
			GameObject gameObj = new GameObject(gameObjectBindInfo.Name ?? "GameObject");
			Transform parent = GetTransformGroup(gameObjectBindInfo, context);
			if (parent == null)
			{
				gameObj.transform.SetParent(ContextTransform, false);
				gameObj.transform.SetParent(null, false);
			}
			else
			{
				gameObj.transform.SetParent(parent, false);
			}
			return gameObj;
		}

		private Transform GetTransformGroup(GameObjectCreationParameters gameObjectBindInfo, InjectContext context)
		{
			Assert.That(!AssertOnNewGameObjects, "Given DiContainer does not support creating new game objects");
			if (gameObjectBindInfo.ParentTransform != null)
			{
				Assert.IsNull(gameObjectBindInfo.GroupName);
				Assert.IsNull(gameObjectBindInfo.ParentTransformGetter);
				return gameObjectBindInfo.ParentTransform;
			}
			if (gameObjectBindInfo.ParentTransformGetter != null && !IsValidating)
			{
				Assert.IsNull(gameObjectBindInfo.GroupName);
				if (context == null)
				{
					context = new InjectContext
					{
						Container = this
					};
				}
				return gameObjectBindInfo.ParentTransformGetter(context);
			}
			string groupName = gameObjectBindInfo.GroupName;
			Transform defaultParent = (_hasExplicitDefaultParent ? _explicitDefaultParent : _inheritedDefaultParent);
			if (defaultParent == null)
			{
				if (groupName == null)
				{
					return null;
				}
				return (GameObject.Find("/" + groupName) ?? CreateTransformGroup(groupName)).transform;
			}
			if (groupName == null)
			{
				return defaultParent;
			}
			foreach (Transform child in defaultParent)
			{
				if (child.name == groupName)
				{
					return child;
				}
			}
			Transform group = new GameObject(groupName).transform;
			group.SetParent(defaultParent, false);
			return group;
		}

		private GameObject CreateTransformGroup(string groupName)
		{
			GameObject gameObj = new GameObject(groupName);
			gameObj.transform.SetParent(ContextTransform, false);
			gameObj.transform.SetParent(null, false);
			return gameObj;
		}

		public T Instantiate<T>()
		{
			return Instantiate<T>(new object[0]);
		}

		public T Instantiate<T>(IEnumerable<object> extraArgs)
		{
			object result = Instantiate(typeof(T), extraArgs);
			if (IsValidating && !(result is T))
			{
				Assert.That(result is ValidationMarker);
				return default(T);
			}
			return (T)result;
		}

		public object Instantiate(Type concreteType)
		{
			return Instantiate(concreteType, new object[0]);
		}

		public object Instantiate(Type concreteType, IEnumerable<object> extraArgs)
		{
			Assert.That(!extraArgs.ContainsItem(null), "Null value given to factory constructor arguments when instantiating object with type '{0}'. In order to use null use InstantiateExplicit", concreteType);
			return InstantiateExplicit(concreteType, InjectUtil.CreateArgList(extraArgs));
		}

		public TContract InstantiateComponent<TContract>(GameObject gameObject) where TContract : Component
		{
			return InstantiateComponent<TContract>(gameObject, new object[0]);
		}

		public TContract InstantiateComponent<TContract>(GameObject gameObject, IEnumerable<object> extraArgs) where TContract : Component
		{
			return (TContract)InstantiateComponent(typeof(TContract), gameObject, extraArgs);
		}

		public Component InstantiateComponent(Type componentType, GameObject gameObject)
		{
			return InstantiateComponent(componentType, gameObject, new object[0]);
		}

		public Component InstantiateComponent(Type componentType, GameObject gameObject, IEnumerable<object> extraArgs)
		{
			return InstantiateComponentExplicit(componentType, gameObject, InjectUtil.CreateArgList(extraArgs));
		}

		public T InstantiateComponentOnNewGameObject<T>() where T : Component
		{
			return InstantiateComponentOnNewGameObject<T>(typeof(T).Name);
		}

		public T InstantiateComponentOnNewGameObject<T>(IEnumerable<object> extraArgs) where T : Component
		{
			return InstantiateComponentOnNewGameObject<T>(typeof(T).Name, extraArgs);
		}

		public T InstantiateComponentOnNewGameObject<T>(string gameObjectName) where T : Component
		{
			return InstantiateComponentOnNewGameObject<T>(gameObjectName, new object[0]);
		}

		public T InstantiateComponentOnNewGameObject<T>(string gameObjectName, IEnumerable<object> extraArgs) where T : Component
		{
			return InstantiateComponent<T>(CreateEmptyGameObject(gameObjectName), extraArgs);
		}

		public GameObject InstantiatePrefab(UnityEngine.Object prefab)
		{
			return InstantiatePrefab(prefab, GameObjectCreationParameters.Default);
		}

		public GameObject InstantiatePrefab(UnityEngine.Object prefab, Transform parentTransform)
		{
			return InstantiatePrefab(prefab, new GameObjectCreationParameters
			{
				ParentTransform = parentTransform
			});
		}

		public GameObject InstantiatePrefab(UnityEngine.Object prefab, Vector3 position, Quaternion rotation, Transform parentTransform)
		{
			return InstantiatePrefab(prefab, new GameObjectCreationParameters
			{
				ParentTransform = parentTransform,
				Position = position,
				Rotation = rotation
			});
		}

		public GameObject InstantiatePrefab(UnityEngine.Object prefab, GameObjectCreationParameters gameObjectBindInfo)
		{
			FlushBindings();
			bool shouldMakeActive;
			GameObject gameObj = CreateAndParentPrefab(prefab, gameObjectBindInfo, null, out shouldMakeActive);
			InjectGameObject(gameObj);
			if (shouldMakeActive && !IsValidating)
			{
				gameObj.SetActive(true);
			}
			return gameObj;
		}

		public GameObject InstantiatePrefabResource(string resourcePath)
		{
			return InstantiatePrefabResource(resourcePath, GameObjectCreationParameters.Default);
		}

		public GameObject InstantiatePrefabResource(string resourcePath, Transform parentTransform)
		{
			return InstantiatePrefabResource(resourcePath, new GameObjectCreationParameters
			{
				ParentTransform = parentTransform
			});
		}

		public GameObject InstantiatePrefabResource(string resourcePath, Vector3 position, Quaternion rotation, Transform parentTransform)
		{
			return InstantiatePrefabResource(resourcePath, new GameObjectCreationParameters
			{
				ParentTransform = parentTransform,
				Position = position,
				Rotation = rotation
			});
		}

		public GameObject InstantiatePrefabResource(string resourcePath, GameObjectCreationParameters creationInfo)
		{
			GameObject prefab = (GameObject)Resources.Load(resourcePath);
			Assert.IsNotNull(prefab, "Could not find prefab at resource location '{0}'".Fmt(resourcePath));
			return InstantiatePrefab(prefab, creationInfo);
		}

		public T InstantiatePrefabForComponent<T>(UnityEngine.Object prefab)
		{
			return (T)InstantiatePrefabForComponent(typeof(T), prefab, null, new object[0]);
		}

		public T InstantiatePrefabForComponent<T>(UnityEngine.Object prefab, IEnumerable<object> extraArgs)
		{
			return (T)InstantiatePrefabForComponent(typeof(T), prefab, null, extraArgs);
		}

		public T InstantiatePrefabForComponent<T>(UnityEngine.Object prefab, Transform parentTransform)
		{
			return (T)InstantiatePrefabForComponent(typeof(T), prefab, parentTransform, new object[0]);
		}

		public T InstantiatePrefabForComponent<T>(UnityEngine.Object prefab, Transform parentTransform, IEnumerable<object> extraArgs)
		{
			return (T)InstantiatePrefabForComponent(typeof(T), prefab, parentTransform, extraArgs);
		}

		public T InstantiatePrefabForComponent<T>(UnityEngine.Object prefab, Vector3 position, Quaternion rotation, Transform parentTransform)
		{
			return (T)InstantiatePrefabForComponent(typeof(T), prefab, new object[0], new GameObjectCreationParameters
			{
				ParentTransform = parentTransform,
				Position = position,
				Rotation = rotation
			});
		}

		public T InstantiatePrefabForComponent<T>(UnityEngine.Object prefab, Vector3 position, Quaternion rotation, Transform parentTransform, IEnumerable<object> extraArgs)
		{
			return (T)InstantiatePrefabForComponent(typeof(T), prefab, extraArgs, new GameObjectCreationParameters
			{
				ParentTransform = parentTransform,
				Position = position,
				Rotation = rotation
			});
		}

		public object InstantiatePrefabForComponent(Type concreteType, UnityEngine.Object prefab, Transform parentTransform, IEnumerable<object> extraArgs)
		{
			return InstantiatePrefabForComponent(concreteType, prefab, extraArgs, new GameObjectCreationParameters
			{
				ParentTransform = parentTransform
			});
		}

		public object InstantiatePrefabForComponent(Type concreteType, UnityEngine.Object prefab, IEnumerable<object> extraArgs, GameObjectCreationParameters creationInfo)
		{
			return InstantiatePrefabForComponentExplicit(concreteType, prefab, InjectUtil.CreateArgList(extraArgs), creationInfo);
		}

		public T InstantiatePrefabResourceForComponent<T>(string resourcePath)
		{
			return (T)InstantiatePrefabResourceForComponent(typeof(T), resourcePath, null, new object[0]);
		}

		public T InstantiatePrefabResourceForComponent<T>(string resourcePath, IEnumerable<object> extraArgs)
		{
			return (T)InstantiatePrefabResourceForComponent(typeof(T), resourcePath, null, extraArgs);
		}

		public T InstantiatePrefabResourceForComponent<T>(string resourcePath, Transform parentTransform)
		{
			return (T)InstantiatePrefabResourceForComponent(typeof(T), resourcePath, parentTransform, new object[0]);
		}

		public T InstantiatePrefabResourceForComponent<T>(string resourcePath, Transform parentTransform, IEnumerable<object> extraArgs)
		{
			return (T)InstantiatePrefabResourceForComponent(typeof(T), resourcePath, parentTransform, extraArgs);
		}

		public T InstantiatePrefabResourceForComponent<T>(string resourcePath, Vector3 position, Quaternion rotation, Transform parentTransform)
		{
			return InstantiatePrefabResourceForComponent<T>(resourcePath, position, rotation, parentTransform, new object[0]);
		}

		public T InstantiatePrefabResourceForComponent<T>(string resourcePath, Vector3 position, Quaternion rotation, Transform parentTransform, IEnumerable<object> extraArgs)
		{
			List<TypeValuePair> argsList = InjectUtil.CreateArgList(extraArgs);
			GameObjectCreationParameters creationParameters = new GameObjectCreationParameters
			{
				ParentTransform = parentTransform,
				Position = position,
				Rotation = rotation
			};
			return (T)InstantiatePrefabResourceForComponentExplicit(typeof(T), resourcePath, argsList, creationParameters);
		}

		public object InstantiatePrefabResourceForComponent(Type concreteType, string resourcePath, Transform parentTransform, IEnumerable<object> extraArgs)
		{
			Assert.That(!extraArgs.ContainsItem(null), "Null value given to factory constructor arguments when instantiating object with type '{0}'. In order to use null use InstantiatePrefabForComponentExplicit", concreteType);
			return InstantiatePrefabResourceForComponentExplicit(concreteType, resourcePath, InjectUtil.CreateArgList(extraArgs), new GameObjectCreationParameters
			{
				ParentTransform = parentTransform
			});
		}

		public T InstantiateScriptableObjectResource<T>(string resourcePath) where T : ScriptableObject
		{
			return InstantiateScriptableObjectResource<T>(resourcePath, new object[0]);
		}

		public T InstantiateScriptableObjectResource<T>(string resourcePath, IEnumerable<object> extraArgs) where T : ScriptableObject
		{
			return (T)InstantiateScriptableObjectResource(typeof(T), resourcePath, extraArgs);
		}

		public object InstantiateScriptableObjectResource(Type scriptableObjectType, string resourcePath)
		{
			return InstantiateScriptableObjectResource(scriptableObjectType, resourcePath, new object[0]);
		}

		public object InstantiateScriptableObjectResource(Type scriptableObjectType, string resourcePath, IEnumerable<object> extraArgs)
		{
			Assert.DerivesFromOrEqual<ScriptableObject>(scriptableObjectType);
			return InstantiateScriptableObjectResourceExplicit(scriptableObjectType, resourcePath, InjectUtil.CreateArgList(extraArgs));
		}

		public void InjectGameObject(GameObject gameObject)
		{
			FlushBindings();
			ZenUtilInternal.AddStateMachineBehaviourAutoInjectersUnderGameObject(gameObject);
			List<MonoBehaviour> monoBehaviours = ZenPools.SpawnList<MonoBehaviour>();
			try
			{
				ZenUtilInternal.GetInjectableMonoBehavioursUnderGameObject(gameObject, monoBehaviours);
				for (int i = 0; i < monoBehaviours.Count; i++)
				{
					Inject(monoBehaviours[i]);
				}
			}
			finally
			{
				ZenPools.DespawnList(monoBehaviours);
			}
		}

		public T InjectGameObjectForComponent<T>(GameObject gameObject) where T : Component
		{
			return InjectGameObjectForComponent<T>(gameObject, new object[0]);
		}

		public T InjectGameObjectForComponent<T>(GameObject gameObject, IEnumerable<object> extraArgs) where T : Component
		{
			return (T)InjectGameObjectForComponent(gameObject, typeof(T), extraArgs);
		}

		public object InjectGameObjectForComponent(GameObject gameObject, Type componentType, IEnumerable<object> extraArgs)
		{
			return InjectGameObjectForComponentExplicit(gameObject, componentType, InjectUtil.CreateArgList(extraArgs), new InjectContext(this, componentType, null), null);
		}

		public Component InjectGameObjectForComponentExplicit(GameObject gameObject, Type componentType, List<TypeValuePair> extraArgs, InjectContext context, object concreteIdentifier)
		{
			if (!componentType.DerivesFrom<MonoBehaviour>() && extraArgs.Count > 0)
			{
				throw Assert.CreateException("Cannot inject into non-monobehaviours!  Argument list must be zero length");
			}
			ZenUtilInternal.AddStateMachineBehaviourAutoInjectersUnderGameObject(gameObject);
			List<MonoBehaviour> injectableMonoBehaviours = ZenPools.SpawnList<MonoBehaviour>();
			try
			{
				ZenUtilInternal.GetInjectableMonoBehavioursUnderGameObject(gameObject, injectableMonoBehaviours);
				for (int i = 0; i < injectableMonoBehaviours.Count; i++)
				{
					MonoBehaviour monoBehaviour = injectableMonoBehaviours[i];
					if (monoBehaviour.GetType().DerivesFromOrEqual(componentType))
					{
						InjectExplicit(monoBehaviour, monoBehaviour.GetType(), extraArgs, context, concreteIdentifier);
					}
					else
					{
						Inject(monoBehaviour);
					}
				}
			}
			finally
			{
				ZenPools.DespawnList(injectableMonoBehaviours);
			}
			Component[] matches = gameObject.GetComponentsInChildren(componentType, true);
			Assert.That(matches.Length != 0, "Expected to find component with type '{0}' when injecting into game object '{1}'", componentType, gameObject.name);
			Assert.That(matches.Length == 1, "Found multiple component with type '{0}' when injecting into game object '{1}'", componentType, gameObject.name);
			return matches[0];
		}

		public void Inject(object injectable)
		{
			Inject(injectable, new object[0]);
		}

		public void Inject(object injectable, IEnumerable<object> extraArgs)
		{
			InjectExplicit(injectable, InjectUtil.CreateArgList(extraArgs));
		}

		public TContract Resolve<TContract>()
		{
			return (TContract)Resolve(typeof(TContract));
		}

		public object Resolve(Type contractType)
		{
			return ResolveId(contractType, null);
		}

		public TContract ResolveId<TContract>(object identifier)
		{
			return (TContract)ResolveId(typeof(TContract), identifier);
		}

		public object ResolveId(Type contractType, object identifier)
		{
			using (InjectContext context = ZenPools.SpawnInjectContext(this, contractType))
			{
				context.Identifier = identifier;
				return Resolve(context);
			}
		}

		public TContract TryResolve<TContract>() where TContract : class
		{
			return (TContract)TryResolve(typeof(TContract));
		}

		public object TryResolve(Type contractType)
		{
			return TryResolveId(contractType, null);
		}

		public TContract TryResolveId<TContract>(object identifier) where TContract : class
		{
			return (TContract)TryResolveId(typeof(TContract), identifier);
		}

		public object TryResolveId(Type contractType, object identifier)
		{
			using (InjectContext context = ZenPools.SpawnInjectContext(this, contractType))
			{
				context.Identifier = identifier;
				context.Optional = true;
				return Resolve(context);
			}
		}

		public List<TContract> ResolveAll<TContract>()
		{
			return (List<TContract>)ResolveAll(typeof(TContract));
		}

		public IList ResolveAll(Type contractType)
		{
			return ResolveIdAll(contractType, null);
		}

		public List<TContract> ResolveIdAll<TContract>(object identifier)
		{
			return (List<TContract>)ResolveIdAll(typeof(TContract), identifier);
		}

		public IList ResolveIdAll(Type contractType, object identifier)
		{
			using (InjectContext context = ZenPools.SpawnInjectContext(this, contractType))
			{
				context.Identifier = identifier;
				context.Optional = true;
				return ResolveAll(context);
			}
		}

		public void UnbindAll()
		{
			FlushBindings();
			_providers.Clear();
		}

		public bool Unbind<TContract>()
		{
			return Unbind(typeof(TContract));
		}

		public bool Unbind(Type contractType)
		{
			return UnbindId(contractType, null);
		}

		public bool UnbindId<TContract>(object identifier)
		{
			return UnbindId(typeof(TContract), identifier);
		}

		public bool UnbindId(Type contractType, object identifier)
		{
			FlushBindings();
			BindingId bindingId = new BindingId(contractType, identifier);
			return _providers.Remove(bindingId);
		}

		public void UnbindInterfacesTo<TConcrete>()
		{
			UnbindInterfacesTo(typeof(TConcrete));
		}

		public void UnbindInterfacesTo(Type concreteType)
		{
			Type[] array = concreteType.Interfaces();
			foreach (Type i in array)
			{
				Unbind(i, concreteType);
			}
		}

		public bool Unbind<TContract, TConcrete>()
		{
			return Unbind(typeof(TContract), typeof(TConcrete));
		}

		public bool Unbind(Type contractType, Type concreteType)
		{
			return UnbindId(contractType, concreteType, null);
		}

		public bool UnbindId<TContract, TConcrete>(object identifier)
		{
			return UnbindId(typeof(TContract), typeof(TConcrete), identifier);
		}

		public bool UnbindId(Type contractType, Type concreteType, object identifier)
		{
			FlushBindings();
			BindingId bindingId = new BindingId(contractType, identifier);
			if (!_providers.TryGetValue(bindingId, out var providers))
			{
				return false;
			}
			List<ProviderInfo> matches = providers.Where((ProviderInfo x) => x.Provider.GetInstanceType(new InjectContext(this, contractType, identifier)).DerivesFromOrEqual(concreteType)).ToList();
			if (matches.Count == 0)
			{
				return false;
			}
			foreach (ProviderInfo info in matches)
			{
				bool success = providers.Remove(info);
				Assert.That(success);
			}
			return true;
		}

		public bool HasBinding<TContract>()
		{
			return HasBinding(typeof(TContract));
		}

		public bool HasBinding(Type contractType)
		{
			return HasBindingId(contractType, null);
		}

		public bool HasBindingId<TContract>(object identifier)
		{
			return HasBindingId(typeof(TContract), identifier);
		}

		public bool HasBindingId(Type contractType, object identifier)
		{
			return HasBindingId(contractType, identifier, InjectSources.Any);
		}

		public bool HasBindingId(Type contractType, object identifier, InjectSources sourceType)
		{
			using (InjectContext ctx = ZenPools.SpawnInjectContext(this, contractType))
			{
				ctx.Identifier = identifier;
				ctx.SourceType = sourceType;
				return HasBinding(ctx);
			}
		}

		public bool HasBinding(InjectContext context)
		{
			Assert.IsNotNull(context);
			FlushBindings();
			List<ProviderInfo> matches = ZenPools.SpawnList<ProviderInfo>();
			try
			{
				GetProviderMatches(context, matches);
				return matches.Count > 0;
			}
			finally
			{
				ZenPools.DespawnList(matches);
			}
		}

		public void FlushBindings()
		{
			while (_currentBindings.Count > 0)
			{
				BindStatement binding = _currentBindings.Dequeue();
				if (binding.BindingInheritanceMethod != BindingInheritanceMethods.MoveDirectOnly && binding.BindingInheritanceMethod != BindingInheritanceMethods.MoveIntoAll)
				{
					FinalizeBinding(binding);
				}
				if (binding.BindingInheritanceMethod != 0)
				{
					_childBindings.Add(binding);
				}
				else
				{
					binding.Dispose();
				}
			}
		}

		private void FinalizeBinding(BindStatement binding)
		{
			_isFinalizingBinding = true;
			try
			{
				binding.FinalizeBinding(this);
			}
			finally
			{
				_isFinalizingBinding = false;
			}
		}

		public BindStatement StartBinding(bool flush = true)
		{
			Assert.That(!_isFinalizingBinding, "Attempted to start a binding during a binding finalizer.  This is not allowed, since binding finalizers should directly use AddProvider instead, to allow for bindings to be inherited properly without duplicates");
			if (flush)
			{
				FlushBindings();
			}
			BindStatement bindStatement = ZenPools.SpawnStatement();
			_currentBindings.Enqueue(bindStatement);
			return bindStatement;
		}

		public ConcreteBinderGeneric<TContract> Rebind<TContract>()
		{
			return RebindId<TContract>(null);
		}

		public ConcreteBinderGeneric<TContract> RebindId<TContract>(object identifier)
		{
			UnbindId<TContract>(identifier);
			return Bind<TContract>().WithId(identifier);
		}

		public ConcreteBinderNonGeneric Rebind(Type contractType)
		{
			return RebindId(contractType, null);
		}

		public ConcreteBinderNonGeneric RebindId(Type contractType, object identifier)
		{
			UnbindId(contractType, identifier);
			return Bind(contractType).WithId(identifier);
		}

		public ConcreteIdBinderGeneric<TContract> Bind<TContract>()
		{
			return Bind<TContract>(StartBinding());
		}

		public ConcreteIdBinderGeneric<TContract> BindNoFlush<TContract>()
		{
			return Bind<TContract>(StartBinding(false));
		}

		private ConcreteIdBinderGeneric<TContract> Bind<TContract>(BindStatement bindStatement)
		{
			BindInfo bindInfo = bindStatement.SpawnBindInfo();
			Assert.That(!typeof(TContract).DerivesFrom<IPlaceholderFactory>(), "You should not use Container.Bind for factory classes.  Use Container.BindFactory instead.");
			Assert.That(!bindInfo.ContractTypes.Contains(typeof(TContract)));
			bindInfo.ContractTypes.Add(typeof(TContract));
			return new ConcreteIdBinderGeneric<TContract>(this, bindInfo, bindStatement);
		}

		public ConcreteIdBinderNonGeneric Bind(params Type[] contractTypes)
		{
			BindStatement statement = StartBinding();
			BindInfo bindInfo = statement.SpawnBindInfo();
			bindInfo.ContractTypes.AllocFreeAddRange(contractTypes);
			return BindInternal(bindInfo, statement);
		}

		public ConcreteIdBinderNonGeneric Bind(IEnumerable<Type> contractTypes)
		{
			BindStatement statement = StartBinding();
			BindInfo bindInfo = statement.SpawnBindInfo();
			bindInfo.ContractTypes.AddRange(contractTypes);
			return BindInternal(bindInfo, statement);
		}

		private ConcreteIdBinderNonGeneric BindInternal(BindInfo bindInfo, BindStatement bindingFinalizer)
		{
			Assert.That(bindInfo.ContractTypes.All((Type x) => !x.DerivesFrom<IPlaceholderFactory>()), "You should not use Container.Bind for factory classes.  Use Container.BindFactory instead.");
			return new ConcreteIdBinderNonGeneric(this, bindInfo, bindingFinalizer);
		}

		public ConcreteIdBinderNonGeneric Bind(Action<ConventionSelectTypesBinder> generator)
		{
			ConventionBindInfo conventionBindInfo = new ConventionBindInfo();
			generator(new ConventionSelectTypesBinder(conventionBindInfo));
			List<Type> contractTypesList = conventionBindInfo.ResolveTypes();
			Assert.That(contractTypesList.All((Type x) => !x.DerivesFrom<IPlaceholderFactory>()), "You should not use Container.Bind for factory classes.  Use Container.BindFactory instead.");
			BindStatement statement = StartBinding();
			BindInfo bindInfo = statement.SpawnBindInfo();
			bindInfo.ContractTypes.AllocFreeAddRange(contractTypesList);
			bindInfo.InvalidBindResponse = InvalidBindResponses.Skip;
			return new ConcreteIdBinderNonGeneric(this, bindInfo, statement);
		}

		public FromBinderNonGeneric BindInterfacesTo<T>()
		{
			return BindInterfacesTo(typeof(T));
		}

		public FromBinderNonGeneric BindInterfacesTo(Type type)
		{
			BindStatement statement = StartBinding();
			BindInfo bindInfo = statement.SpawnBindInfo();
			Type[] interfaces = type.Interfaces();
			if (interfaces.Length == 0)
			{
				Log.Warn("Called BindInterfacesTo for type {0} but no interfaces were found", type);
			}
			bindInfo.ContractTypes.AllocFreeAddRange(interfaces);
			bindInfo.RequireExplicitScope = true;
			return BindInternal(bindInfo, statement).To(type);
		}

		public FromBinderNonGeneric BindInterfacesAndSelfTo<T>()
		{
			return BindInterfacesAndSelfTo(typeof(T));
		}

		public FromBinderNonGeneric BindInterfacesAndSelfTo(Type type)
		{
			BindStatement statement = StartBinding();
			BindInfo bindInfo = statement.SpawnBindInfo();
			bindInfo.ContractTypes.AllocFreeAddRange(type.Interfaces());
			bindInfo.ContractTypes.Add(type);
			bindInfo.RequireExplicitScope = true;
			return BindInternal(bindInfo, statement).To(type);
		}

		public IdScopeConcreteIdArgConditionCopyNonLazyBinder BindInstance<TContract>(TContract instance)
		{
			BindStatement statement = StartBinding();
			BindInfo bindInfo = statement.SpawnBindInfo();
			bindInfo.ContractTypes.Add(typeof(TContract));
			statement.SetFinalizer(new ScopableBindingFinalizer(bindInfo, (DiContainer container, Type type) => new InstanceProvider(type, instance, container)));
			return new IdScopeConcreteIdArgConditionCopyNonLazyBinder(bindInfo);
		}

		public void BindInstances(params object[] instances)
		{
			foreach (object instance in instances)
			{
				Assert.That(!ZenUtilInternal.IsNull(instance), "Found null instance provided to BindInstances method");
				Bind(instance.GetType()).FromInstance(instance);
			}
		}

		private FactoryToChoiceIdBinder<TContract> BindFactoryInternal<TContract, TFactoryContract, TFactoryConcrete>() where TFactoryContract : IFactory where TFactoryConcrete : TFactoryContract, IFactory
		{
			BindStatement statement = StartBinding();
			BindInfo bindInfo = statement.SpawnBindInfo();
			bindInfo.ContractTypes.Add(typeof(TFactoryContract));
			FactoryBindInfo factoryBindInfo = new FactoryBindInfo(typeof(TFactoryConcrete));
			statement.SetFinalizer(new PlaceholderFactoryBindingFinalizer<TContract>(bindInfo, factoryBindInfo));
			return new FactoryToChoiceIdBinder<TContract>(this, bindInfo, factoryBindInfo);
		}

		public FactoryToChoiceIdBinder<TContract> BindIFactory<TContract>()
		{
			return BindFactoryInternal<TContract, IFactory<TContract>, PlaceholderFactory<TContract>>();
		}

		public FactoryToChoiceIdBinder<TContract> BindFactory<TContract, TFactory>() where TFactory : PlaceholderFactory<TContract>
		{
			return BindFactoryInternal<TContract, TFactory, TFactory>();
		}

		public FactoryToChoiceIdBinder<TContract> BindFactoryCustomInterface<TContract, TFactoryConcrete, TFactoryContract>() where TFactoryConcrete : PlaceholderFactory<TContract>, TFactoryContract where TFactoryContract : IFactory
		{
			return BindFactoryInternal<TContract, TFactoryContract, TFactoryConcrete>();
		}

		public MemoryPoolIdInitialSizeMaxSizeBinder<TItemContract> BindMemoryPool<TItemContract>()
		{
			return BindMemoryPool<TItemContract, MemoryPool<TItemContract>>();
		}

		public MemoryPoolIdInitialSizeMaxSizeBinder<TItemContract> BindMemoryPool<TItemContract, TPool>() where TPool : IMemoryPool
		{
			return BindMemoryPoolCustomInterface<TItemContract, TPool, TPool>();
		}

		public MemoryPoolIdInitialSizeMaxSizeBinder<TItemContract> BindMemoryPoolCustomInterface<TItemContract, TPoolConcrete, TPoolContract>(bool includeConcreteType = false) where TPoolConcrete : TPoolContract, IMemoryPool where TPoolContract : IMemoryPool
		{
			return BindMemoryPoolCustomInterfaceInternal<TItemContract, TPoolConcrete, TPoolContract>(includeConcreteType, StartBinding());
		}

		internal MemoryPoolIdInitialSizeMaxSizeBinder<TItemContract> BindMemoryPoolCustomInterfaceNoFlush<TItemContract, TPoolConcrete, TPoolContract>(bool includeConcreteType = false) where TPoolConcrete : TPoolContract, IMemoryPool where TPoolContract : IMemoryPool
		{
			return BindMemoryPoolCustomInterfaceInternal<TItemContract, TPoolConcrete, TPoolContract>(includeConcreteType, StartBinding(false));
		}

		private MemoryPoolIdInitialSizeMaxSizeBinder<TItemContract> BindMemoryPoolCustomInterfaceInternal<TItemContract, TPoolConcrete, TPoolContract>(bool includeConcreteType, BindStatement statement) where TPoolConcrete : TPoolContract, IMemoryPool where TPoolContract : IMemoryPool
		{
			List<Type> list = new List<Type>();
			list.Add(typeof(IDisposable));
			list.Add(typeof(TPoolContract));
			List<Type> contractTypes = list;
			if (includeConcreteType)
			{
				contractTypes.Add(typeof(TPoolConcrete));
			}
			BindInfo bindInfo = statement.SpawnBindInfo();
			bindInfo.ContractTypes.AllocFreeAddRange(contractTypes);
			bindInfo.ContractTypes.Add(typeof(IMemoryPool));
			FactoryBindInfo factoryBindInfo = new FactoryBindInfo(typeof(TPoolConcrete));
			MemoryPoolBindInfo poolBindInfo = new MemoryPoolBindInfo();
			statement.SetFinalizer(new MemoryPoolBindingFinalizer<TItemContract>(bindInfo, factoryBindInfo, poolBindInfo));
			return new MemoryPoolIdInitialSizeMaxSizeBinder<TItemContract>(this, bindInfo, factoryBindInfo, poolBindInfo);
		}

		private FactoryToChoiceIdBinder<TParam1, TContract> BindFactoryInternal<TParam1, TContract, TFactoryContract, TFactoryConcrete>() where TFactoryContract : IFactory where TFactoryConcrete : TFactoryContract, IFactory
		{
			BindStatement statement = StartBinding();
			BindInfo bindInfo = statement.SpawnBindInfo();
			bindInfo.ContractTypes.Add(typeof(TFactoryContract));
			FactoryBindInfo factoryBindInfo = new FactoryBindInfo(typeof(TFactoryConcrete));
			statement.SetFinalizer(new PlaceholderFactoryBindingFinalizer<TContract>(bindInfo, factoryBindInfo));
			return new FactoryToChoiceIdBinder<TParam1, TContract>(this, bindInfo, factoryBindInfo);
		}

		public FactoryToChoiceIdBinder<TParam1, TContract> BindIFactory<TParam1, TContract>()
		{
			return BindFactoryInternal<TParam1, TContract, IFactory<TParam1, TContract>, PlaceholderFactory<TParam1, TContract>>();
		}

		public FactoryToChoiceIdBinder<TParam1, TContract> BindFactory<TParam1, TContract, TFactory>() where TFactory : PlaceholderFactory<TParam1, TContract>
		{
			return BindFactoryInternal<TParam1, TContract, TFactory, TFactory>();
		}

		public FactoryToChoiceIdBinder<TParam1, TContract> BindFactoryCustomInterface<TParam1, TContract, TFactoryConcrete, TFactoryContract>() where TFactoryConcrete : PlaceholderFactory<TParam1, TContract>, TFactoryContract where TFactoryContract : IFactory
		{
			return BindFactoryInternal<TParam1, TContract, TFactoryContract, TFactoryConcrete>();
		}

		private FactoryToChoiceIdBinder<TParam1, TParam2, TContract> BindFactoryInternal<TParam1, TParam2, TContract, TFactoryContract, TFactoryConcrete>() where TFactoryContract : IFactory where TFactoryConcrete : TFactoryContract, IFactory
		{
			BindStatement statement = StartBinding();
			BindInfo bindInfo = statement.SpawnBindInfo();
			bindInfo.ContractTypes.Add(typeof(TFactoryContract));
			FactoryBindInfo factoryBindInfo = new FactoryBindInfo(typeof(TFactoryConcrete));
			statement.SetFinalizer(new PlaceholderFactoryBindingFinalizer<TContract>(bindInfo, factoryBindInfo));
			return new FactoryToChoiceIdBinder<TParam1, TParam2, TContract>(this, bindInfo, factoryBindInfo);
		}

		public FactoryToChoiceIdBinder<TParam1, TParam2, TContract> BindIFactory<TParam1, TParam2, TContract>()
		{
			return BindFactoryInternal<TParam1, TParam2, TContract, IFactory<TParam1, TParam2, TContract>, PlaceholderFactory<TParam1, TParam2, TContract>>();
		}

		public FactoryToChoiceIdBinder<TParam1, TParam2, TContract> BindFactory<TParam1, TParam2, TContract, TFactory>() where TFactory : PlaceholderFactory<TParam1, TParam2, TContract>
		{
			return BindFactoryInternal<TParam1, TParam2, TContract, TFactory, TFactory>();
		}

		public FactoryToChoiceIdBinder<TParam1, TParam2, TContract> BindFactoryCustomInterface<TParam1, TParam2, TContract, TFactoryConcrete, TFactoryContract>() where TFactoryConcrete : PlaceholderFactory<TParam1, TParam2, TContract>, TFactoryContract where TFactoryContract : IFactory
		{
			return BindFactoryInternal<TParam1, TParam2, TContract, TFactoryContract, TFactoryConcrete>();
		}

		private FactoryToChoiceIdBinder<TParam1, TParam2, TParam3, TContract> BindFactoryInternal<TParam1, TParam2, TParam3, TContract, TFactoryContract, TFactoryConcrete>() where TFactoryContract : IFactory where TFactoryConcrete : TFactoryContract, IFactory
		{
			BindStatement statement = StartBinding();
			BindInfo bindInfo = statement.SpawnBindInfo();
			bindInfo.ContractTypes.Add(typeof(TFactoryContract));
			FactoryBindInfo factoryBindInfo = new FactoryBindInfo(typeof(TFactoryConcrete));
			statement.SetFinalizer(new PlaceholderFactoryBindingFinalizer<TContract>(bindInfo, factoryBindInfo));
			return new FactoryToChoiceIdBinder<TParam1, TParam2, TParam3, TContract>(this, bindInfo, factoryBindInfo);
		}

		public FactoryToChoiceIdBinder<TParam1, TParam2, TParam3, TContract> BindIFactory<TParam1, TParam2, TParam3, TContract>()
		{
			return BindFactoryInternal<TParam1, TParam2, TParam3, TContract, IFactory<TParam1, TParam2, TParam3, TContract>, PlaceholderFactory<TParam1, TParam2, TParam3, TContract>>();
		}

		public FactoryToChoiceIdBinder<TParam1, TParam2, TParam3, TContract> BindFactory<TParam1, TParam2, TParam3, TContract, TFactory>() where TFactory : PlaceholderFactory<TParam1, TParam2, TParam3, TContract>
		{
			return BindFactoryInternal<TParam1, TParam2, TParam3, TContract, TFactory, TFactory>();
		}

		public FactoryToChoiceIdBinder<TParam1, TParam2, TParam3, TContract> BindFactoryCustomInterface<TParam1, TParam2, TParam3, TContract, TFactoryConcrete, TFactoryContract>() where TFactoryConcrete : PlaceholderFactory<TParam1, TParam2, TParam3, TContract>, TFactoryContract where TFactoryContract : IFactory
		{
			return BindFactoryInternal<TParam1, TParam2, TParam3, TContract, TFactoryContract, TFactoryConcrete>();
		}

		private FactoryToChoiceIdBinder<TParam1, TParam2, TParam3, TParam4, TContract> BindFactoryInternal<TParam1, TParam2, TParam3, TParam4, TContract, TFactoryContract, TFactoryConcrete>() where TFactoryContract : IFactory where TFactoryConcrete : TFactoryContract, IFactory
		{
			BindStatement statement = StartBinding();
			BindInfo bindInfo = statement.SpawnBindInfo();
			bindInfo.ContractTypes.Add(typeof(TFactoryContract));
			FactoryBindInfo factoryBindInfo = new FactoryBindInfo(typeof(TFactoryConcrete));
			statement.SetFinalizer(new PlaceholderFactoryBindingFinalizer<TContract>(bindInfo, factoryBindInfo));
			return new FactoryToChoiceIdBinder<TParam1, TParam2, TParam3, TParam4, TContract>(this, bindInfo, factoryBindInfo);
		}

		public FactoryToChoiceIdBinder<TParam1, TParam2, TParam3, TParam4, TContract> BindIFactory<TParam1, TParam2, TParam3, TParam4, TContract>()
		{
			return BindFactoryInternal<TParam1, TParam2, TParam3, TParam4, TContract, IFactory<TParam1, TParam2, TParam3, TParam4, TContract>, PlaceholderFactory<TParam1, TParam2, TParam3, TParam4, TContract>>();
		}

		public FactoryToChoiceIdBinder<TParam1, TParam2, TParam3, TParam4, TContract> BindFactory<TParam1, TParam2, TParam3, TParam4, TContract, TFactory>() where TFactory : PlaceholderFactory<TParam1, TParam2, TParam3, TParam4, TContract>
		{
			return BindFactoryInternal<TParam1, TParam2, TParam3, TParam4, TContract, TFactory, TFactory>();
		}

		public FactoryToChoiceIdBinder<TParam1, TParam2, TParam3, TParam4, TContract> BindFactoryCustomInterface<TParam1, TParam2, TParam3, TParam4, TContract, TFactoryConcrete, TFactoryContract>() where TFactoryConcrete : PlaceholderFactory<TParam1, TParam2, TParam3, TParam4, TContract>, TFactoryContract where TFactoryContract : IFactory
		{
			return BindFactoryInternal<TParam1, TParam2, TParam3, TParam4, TContract, TFactoryContract, TFactoryConcrete>();
		}

		private FactoryToChoiceIdBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TContract> BindFactoryInternal<TParam1, TParam2, TParam3, TParam4, TParam5, TContract, TFactoryContract, TFactoryConcrete>() where TFactoryContract : IFactory where TFactoryConcrete : TFactoryContract, IFactory
		{
			BindStatement statement = StartBinding();
			BindInfo bindInfo = statement.SpawnBindInfo();
			bindInfo.ContractTypes.Add(typeof(TFactoryContract));
			FactoryBindInfo factoryBindInfo = new FactoryBindInfo(typeof(TFactoryConcrete));
			statement.SetFinalizer(new PlaceholderFactoryBindingFinalizer<TContract>(bindInfo, factoryBindInfo));
			return new FactoryToChoiceIdBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TContract>(this, bindInfo, factoryBindInfo);
		}

		public FactoryToChoiceIdBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TContract> BindIFactory<TParam1, TParam2, TParam3, TParam4, TParam5, TContract>()
		{
			return BindFactoryInternal<TParam1, TParam2, TParam3, TParam4, TParam5, TContract, IFactory<TParam1, TParam2, TParam3, TParam4, TParam5, TContract>, PlaceholderFactory<TParam1, TParam2, TParam3, TParam4, TParam5, TContract>>();
		}

		public FactoryToChoiceIdBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TContract> BindFactory<TParam1, TParam2, TParam3, TParam4, TParam5, TContract, TFactory>() where TFactory : PlaceholderFactory<TParam1, TParam2, TParam3, TParam4, TParam5, TContract>
		{
			return BindFactoryInternal<TParam1, TParam2, TParam3, TParam4, TParam5, TContract, TFactory, TFactory>();
		}

		public FactoryToChoiceIdBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TContract> BindFactoryCustomInterface<TParam1, TParam2, TParam3, TParam4, TParam5, TContract, TFactoryConcrete, TFactoryContract>() where TFactoryConcrete : PlaceholderFactory<TParam1, TParam2, TParam3, TParam4, TParam5, TContract>, TFactoryContract where TFactoryContract : IFactory
		{
			return BindFactoryInternal<TParam1, TParam2, TParam3, TParam4, TParam5, TContract, TFactoryContract, TFactoryConcrete>();
		}

		private FactoryToChoiceIdBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TContract> BindFactoryInternal<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TContract, TFactoryContract, TFactoryConcrete>() where TFactoryContract : IFactory where TFactoryConcrete : TFactoryContract, IFactory
		{
			BindStatement statement = StartBinding();
			BindInfo bindInfo = statement.SpawnBindInfo();
			bindInfo.ContractTypes.Add(typeof(TFactoryContract));
			FactoryBindInfo factoryBindInfo = new FactoryBindInfo(typeof(TFactoryConcrete));
			statement.SetFinalizer(new PlaceholderFactoryBindingFinalizer<TContract>(bindInfo, factoryBindInfo));
			return new FactoryToChoiceIdBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TContract>(this, bindInfo, factoryBindInfo);
		}

		public FactoryToChoiceIdBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TContract> BindIFactory<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TContract>()
		{
			return BindFactoryInternal<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TContract, IFactory<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TContract>, PlaceholderFactory<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TContract>>();
		}

		public FactoryToChoiceIdBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TContract> BindFactory<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TContract, TFactory>() where TFactory : PlaceholderFactory<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TContract>
		{
			return BindFactoryInternal<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TContract, TFactory, TFactory>();
		}

		public FactoryToChoiceIdBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TContract> BindFactoryCustomInterface<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TContract, TFactoryConcrete, TFactoryContract>() where TFactoryConcrete : PlaceholderFactory<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TContract>, TFactoryContract where TFactoryContract : IFactory
		{
			return BindFactoryInternal<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TContract, TFactoryContract, TFactoryConcrete>();
		}

		private FactoryToChoiceIdBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TParam9, TParam10, TContract> BindFactoryInternal<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TParam9, TParam10, TContract, TFactoryContract, TFactoryConcrete>() where TFactoryContract : IFactory where TFactoryConcrete : TFactoryContract, IFactory
		{
			BindStatement statement = StartBinding();
			BindInfo bindInfo = statement.SpawnBindInfo();
			bindInfo.ContractTypes.Add(typeof(TFactoryContract));
			FactoryBindInfo factoryBindInfo = new FactoryBindInfo(typeof(TFactoryConcrete));
			statement.SetFinalizer(new PlaceholderFactoryBindingFinalizer<TContract>(bindInfo, factoryBindInfo));
			return new FactoryToChoiceIdBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TParam9, TParam10, TContract>(this, bindInfo, factoryBindInfo);
		}

		public FactoryToChoiceIdBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TParam9, TParam10, TContract> BindIFactory<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TParam9, TParam10, TContract>()
		{
			return BindFactoryInternal<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TParam9, TParam10, TContract, IFactory<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TParam9, TParam10, TContract>, PlaceholderFactory<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TParam9, TParam10, TContract>>();
		}

		public FactoryToChoiceIdBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TParam9, TParam10, TContract> BindFactory<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TParam9, TParam10, TContract, TFactory>() where TFactory : PlaceholderFactory<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TParam9, TParam10, TContract>
		{
			return BindFactoryInternal<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TParam9, TParam10, TContract, TFactory, TFactory>();
		}

		public FactoryToChoiceIdBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TParam9, TParam10, TContract> BindFactoryCustomInterface<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TParam9, TParam10, TContract, TFactoryConcrete, TFactoryContract>() where TFactoryConcrete : PlaceholderFactory<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TParam9, TParam10, TContract>, TFactoryContract where TFactoryContract : IFactory
		{
			return BindFactoryInternal<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TParam9, TParam10, TContract, TFactoryContract, TFactoryConcrete>();
		}

		public T InstantiateExplicit<T>(List<TypeValuePair> extraArgs)
		{
			return (T)InstantiateExplicit(typeof(T), extraArgs);
		}

		public object InstantiateExplicit(Type concreteType, List<TypeValuePair> extraArgs)
		{
			bool autoInject = true;
			return InstantiateExplicit(concreteType, autoInject, extraArgs, new InjectContext(this, concreteType, null), null);
		}

		public object InstantiateExplicit(Type concreteType, bool autoInject, List<TypeValuePair> extraArgs, InjectContext context, object concreteIdentifier)
		{
			if (IsValidating)
			{
				if (_settings.ValidationErrorResponse == ValidationErrorResponses.Throw)
				{
					return InstantiateInternal(concreteType, autoInject, extraArgs, context, concreteIdentifier);
				}
				try
				{
					return InstantiateInternal(concreteType, autoInject, extraArgs, context, concreteIdentifier);
				}
				catch (Exception e)
				{
					Log.ErrorException(e);
					return new ValidationMarker(concreteType, true);
				}
			}
			return InstantiateInternal(concreteType, autoInject, extraArgs, context, concreteIdentifier);
		}

		public Component InstantiateComponentExplicit(Type componentType, GameObject gameObject, List<TypeValuePair> extraArgs)
		{
			Assert.That(componentType.DerivesFrom<Component>());
			FlushBindings();
			Component monoBehaviour = gameObject.AddComponent(componentType);
			InjectExplicit(monoBehaviour, extraArgs);
			return monoBehaviour;
		}

		public object InstantiateScriptableObjectResourceExplicit(Type scriptableObjectType, string resourcePath, List<TypeValuePair> extraArgs)
		{
			UnityEngine.Object[] objects = Resources.LoadAll(resourcePath, scriptableObjectType);
			Assert.That(objects.Length != 0, "Could not find resource at path '{0}' with type '{1}'", resourcePath, scriptableObjectType);
			Assert.That(objects.Length == 1, "Found multiple scriptable objects at path '{0}' when only 1 was expected with type '{1}'", resourcePath, scriptableObjectType);
			UnityEngine.Object newObj = UnityEngine.Object.Instantiate(objects.Single());
			InjectExplicit(newObj, extraArgs);
			return newObj;
		}

		public object InstantiatePrefabResourceForComponentExplicit(Type componentType, string resourcePath, List<TypeValuePair> extraArgs, GameObjectCreationParameters creationInfo)
		{
			return InstantiatePrefabResourceForComponentExplicit(componentType, resourcePath, extraArgs, new InjectContext(this, componentType, null), null, creationInfo);
		}

		public object InstantiatePrefabResourceForComponentExplicit(Type componentType, string resourcePath, List<TypeValuePair> extraArgs, InjectContext context, object concreteIdentifier, GameObjectCreationParameters creationInfo)
		{
			GameObject prefab = (GameObject)Resources.Load(resourcePath);
			Assert.IsNotNull(prefab, "Could not find prefab at resource location '{0}'".Fmt(resourcePath));
			return InstantiatePrefabForComponentExplicit(componentType, prefab, extraArgs, context, concreteIdentifier, creationInfo);
		}

		public object InstantiatePrefabForComponentExplicit(Type componentType, UnityEngine.Object prefab, List<TypeValuePair> extraArgs)
		{
			return InstantiatePrefabForComponentExplicit(componentType, prefab, extraArgs, GameObjectCreationParameters.Default);
		}

		public object InstantiatePrefabForComponentExplicit(Type componentType, UnityEngine.Object prefab, List<TypeValuePair> extraArgs, GameObjectCreationParameters gameObjectBindInfo)
		{
			return InstantiatePrefabForComponentExplicit(componentType, prefab, extraArgs, new InjectContext(this, componentType, null), null, gameObjectBindInfo);
		}

		public object InstantiatePrefabForComponentExplicit(Type componentType, UnityEngine.Object prefab, List<TypeValuePair> extraArgs, InjectContext context, object concreteIdentifier, GameObjectCreationParameters gameObjectBindInfo)
		{
			Assert.That(!AssertOnNewGameObjects, "Given DiContainer does not support creating new game objects");
			FlushBindings();
			Assert.That(componentType.IsInterface() || componentType.DerivesFrom<Component>(), "Expected type '{0}' to derive from UnityEngine.Component", componentType);
			bool shouldMakeActive;
			GameObject gameObj = CreateAndParentPrefab(prefab, gameObjectBindInfo, context, out shouldMakeActive);
			Component component = InjectGameObjectForComponentExplicit(gameObj, componentType, extraArgs, context, concreteIdentifier);
			if (shouldMakeActive && !IsValidating)
			{
				gameObj.SetActive(true);
			}
			return component;
		}

		public void BindExecutionOrder<T>(int order)
		{
			BindExecutionOrder(typeof(T), order);
		}

		public void BindExecutionOrder(Type type, int order)
		{
			Assert.That(type.DerivesFrom<ITickable>() || type.DerivesFrom<IInitializable>() || type.DerivesFrom<IDisposable>() || type.DerivesFrom<ILateDisposable>() || type.DerivesFrom<IFixedTickable>() || type.DerivesFrom<ILateTickable>() || type.DerivesFrom<IPoolable>(), "Expected type '{0}' to derive from one or more of the following interfaces: ITickable, IInitializable, ILateTickable, IFixedTickable, IDisposable, ILateDisposable", type);
			if (type.DerivesFrom<ITickable>())
			{
				BindTickableExecutionOrder(type, order);
			}
			if (type.DerivesFrom<IInitializable>())
			{
				BindInitializableExecutionOrder(type, order);
			}
			if (type.DerivesFrom<IDisposable>())
			{
				BindDisposableExecutionOrder(type, order);
			}
			if (type.DerivesFrom<ILateDisposable>())
			{
				BindLateDisposableExecutionOrder(type, order);
			}
			if (type.DerivesFrom<IFixedTickable>())
			{
				BindFixedTickableExecutionOrder(type, order);
			}
			if (type.DerivesFrom<ILateTickable>())
			{
				BindLateTickableExecutionOrder(type, order);
			}
			if (type.DerivesFrom<IPoolable>())
			{
				BindPoolableExecutionOrder(type, order);
			}
		}

		public CopyNonLazyBinder BindTickableExecutionOrder<T>(int order) where T : ITickable
		{
			return BindTickableExecutionOrder(typeof(T), order);
		}

		public CopyNonLazyBinder BindTickableExecutionOrder(Type type, int order)
		{
			Assert.That(type.DerivesFrom<ITickable>(), "Expected type '{0}' to derive from ITickable", type);
			return BindInstance(ValuePair.New(type, order)).WhenInjectedInto<TickableManager>();
		}

		public CopyNonLazyBinder BindInitializableExecutionOrder<T>(int order) where T : IInitializable
		{
			return BindInitializableExecutionOrder(typeof(T), order);
		}

		public CopyNonLazyBinder BindInitializableExecutionOrder(Type type, int order)
		{
			Assert.That(type.DerivesFrom<IInitializable>(), "Expected type '{0}' to derive from IInitializable", type);
			return BindInstance(ValuePair.New(type, order)).WhenInjectedInto<InitializableManager>();
		}

		public CopyNonLazyBinder BindDisposableExecutionOrder<T>(int order) where T : IDisposable
		{
			return BindDisposableExecutionOrder(typeof(T), order);
		}

		public CopyNonLazyBinder BindLateDisposableExecutionOrder<T>(int order) where T : ILateDisposable
		{
			return BindLateDisposableExecutionOrder(typeof(T), order);
		}

		public CopyNonLazyBinder BindDisposableExecutionOrder(Type type, int order)
		{
			Assert.That(type.DerivesFrom<IDisposable>(), "Expected type '{0}' to derive from IDisposable", type);
			return BindInstance(ValuePair.New(type, order)).WhenInjectedInto<DisposableManager>();
		}

		public CopyNonLazyBinder BindLateDisposableExecutionOrder(Type type, int order)
		{
			Assert.That(type.DerivesFrom<ILateDisposable>(), "Expected type '{0}' to derive from ILateDisposable", type);
			return BindInstance(ValuePair.New(type, order)).WithId("Late").WhenInjectedInto<DisposableManager>();
		}

		public CopyNonLazyBinder BindFixedTickableExecutionOrder<T>(int order) where T : IFixedTickable
		{
			return BindFixedTickableExecutionOrder(typeof(T), order);
		}

		public CopyNonLazyBinder BindFixedTickableExecutionOrder(Type type, int order)
		{
			Assert.That(type.DerivesFrom<IFixedTickable>(), "Expected type '{0}' to derive from IFixedTickable", type);
			return Bind<ValuePair<Type, int>>().WithId("Fixed").FromInstance(ValuePair.New(type, order)).WhenInjectedInto<TickableManager>();
		}

		public CopyNonLazyBinder BindLateTickableExecutionOrder<T>(int order) where T : ILateTickable
		{
			return BindLateTickableExecutionOrder(typeof(T), order);
		}

		public CopyNonLazyBinder BindLateTickableExecutionOrder(Type type, int order)
		{
			Assert.That(type.DerivesFrom<ILateTickable>(), "Expected type '{0}' to derive from ILateTickable", type);
			return Bind<ValuePair<Type, int>>().WithId("Late").FromInstance(ValuePair.New(type, order)).WhenInjectedInto<TickableManager>();
		}

		public CopyNonLazyBinder BindPoolableExecutionOrder<T>(int order) where T : IPoolable
		{
			return BindPoolableExecutionOrder(typeof(T), order);
		}

		public CopyNonLazyBinder BindPoolableExecutionOrder(Type type, int order)
		{
			Assert.That(type.DerivesFrom<IPoolable>(), "Expected type '{0}' to derive from IPoolable", type);
			return Bind<ValuePair<Type, int>>().FromInstance(ValuePair.New(type, order)).WhenInjectedInto<PoolableManager>();
		}
	}
}
