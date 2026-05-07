using System;
using System.Collections.Generic;
using System.Linq;
using ModestTree;
using ModestTree.Util;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Zenject.Internal;

namespace Zenject
{
	public class SceneContext : RunnableContext
	{
		public UnityEvent OnPreInstall;

		public UnityEvent OnPostInstall;

		public UnityEvent OnPreResolve;

		public UnityEvent OnPostResolve;

		public static Action<DiContainer> ExtraBindingsInstallMethod;

		public static Action<DiContainer> ExtraBindingsLateInstallMethod;

		public static IEnumerable<DiContainer> ParentContainers;

		[FormerlySerializedAs("ParentNewObjectsUnderRoot")]
		[FormerlySerializedAs("_parentNewObjectsUnderRoot")]
		[Tooltip("When true, objects that are created at runtime will be parented to the SceneContext")]
		[SerializeField]
		private bool _parentNewObjectsUnderSceneContext;

		[Tooltip("Optional contract names for this SceneContext, allowing contexts in subsequently loaded scenes to depend on it and be parented to it, and also for previously loaded decorators to be included")]
		[SerializeField]
		private List<string> _contractNames = new List<string>();

		[Tooltip("Optional contract names of SceneContexts in previously loaded scenes that this context depends on and to which it should be parented")]
		[SerializeField]
		private List<string> _parentContractNames = new List<string>();

		private DiContainer _container;

		private readonly List<SceneDecoratorContext> _decoratorContexts = new List<SceneDecoratorContext>();

		private bool _hasInstalled;

		private bool _hasResolved;

		public override DiContainer Container => _container;

		public bool HasResolved => _hasResolved;

		public bool HasInstalled => _hasInstalled;

		public bool IsValidating => ProjectContext.Instance.Container.IsValidating;

		public IEnumerable<string> ContractNames
		{
			get
			{
				return _contractNames;
			}
			set
			{
				_contractNames.Clear();
				_contractNames.AddRange(value);
			}
		}

		public IEnumerable<string> ParentContractNames
		{
			get
			{
				List<string> result = new List<string>();
				result.AddRange(_parentContractNames);
				return result;
			}
			set
			{
				_parentContractNames = value.ToList();
			}
		}

		public bool ParentNewObjectsUnderSceneContext
		{
			get
			{
				return _parentNewObjectsUnderSceneContext;
			}
			set
			{
				_parentNewObjectsUnderSceneContext = value;
			}
		}

		public event Action PreInstall;

		public event Action PostInstall;

		public event Action PreResolve;

		public event Action PostResolve;

		public void Awake()
		{
			Initialize();
		}

		public void Validate()
		{
			Assert.That(IsValidating);
			Install();
			Resolve();
		}

		protected override void RunInternal()
		{
			ProjectContext.Instance.EnsureIsInitialized();
			Install();
			Resolve();
		}

		public override IEnumerable<GameObject> GetRootGameObjects()
		{
			return ZenUtilInternal.GetRootGameObjects(base.gameObject.scene);
		}

		private IEnumerable<DiContainer> GetParentContainers()
		{
			IEnumerable<string> parentContractNames = ParentContractNames;
			if (parentContractNames.IsEmpty())
			{
				if (ParentContainers != null)
				{
					IEnumerable<DiContainer> tempParentContainer = ParentContainers;
					ParentContainers = null;
					return tempParentContainer;
				}
				return new DiContainer[1] { ProjectContext.Instance.Container };
			}
			Assert.IsNull(ParentContainers, "Scene cannot have both a parent scene context name set and also an explicit parent container given");
			List<DiContainer> parentContainers = (from sceneContext in UnityUtil.AllLoadedScenes.Except(base.gameObject.scene).SelectMany((Scene scene) => scene.GetRootGameObjects()).SelectMany((GameObject root) => root.GetComponentsInChildren<SceneContext>())
				where sceneContext.ContractNames.Where((string x) => parentContractNames.Contains(x)).Any()
				select sceneContext into x
				select x.Container).ToList();
			if (!parentContainers.Any())
			{
				throw Assert.CreateException("SceneContext on object {0} of scene {1} requires at least one of contracts '{2}', but none of the loaded SceneContexts implements that contract.", base.gameObject.name, base.gameObject.scene.name, parentContractNames.Join(", "));
			}
			return parentContainers;
		}

		private List<SceneDecoratorContext> LookupDecoratorContexts()
		{
			if (_contractNames.IsEmpty())
			{
				return new List<SceneDecoratorContext>();
			}
			return (from decoratorContext in UnityUtil.AllLoadedScenes.Except(base.gameObject.scene).SelectMany((Scene scene) => scene.GetRootGameObjects()).SelectMany((GameObject root) => root.GetComponentsInChildren<SceneDecoratorContext>())
				where _contractNames.Contains(decoratorContext.DecoratedContractName)
				select decoratorContext).ToList();
		}

		public void Install()
		{
			Assert.That(!_hasInstalled);
			_hasInstalled = true;
			Assert.IsNull(_container);
			IEnumerable<DiContainer> parents = GetParentContainers();
			Assert.That(!parents.IsEmpty());
			Assert.That(parents.All((DiContainer x) => x.IsValidating == parents.First().IsValidating));
			_container = new DiContainer(parents, parents.First().IsValidating);
			if (this.PreInstall != null)
			{
				this.PreInstall();
			}
			if (OnPreInstall != null)
			{
				OnPreInstall.Invoke();
			}
			Assert.That(_decoratorContexts.IsEmpty());
			_decoratorContexts.AddRange(LookupDecoratorContexts());
			if (_parentNewObjectsUnderSceneContext)
			{
				_container.DefaultParent = base.transform;
			}
			else
			{
				_container.DefaultParent = null;
			}
			List<MonoBehaviour> injectableMonoBehaviours = new List<MonoBehaviour>();
			GetInjectableMonoBehaviours(injectableMonoBehaviours);
			foreach (MonoBehaviour instance in injectableMonoBehaviours)
			{
				_container.QueueForInject(instance);
			}
			foreach (SceneDecoratorContext decoratorContext in _decoratorContexts)
			{
				decoratorContext.Initialize(_container);
			}
			_container.IsInstalling = true;
			try
			{
				InstallBindings(injectableMonoBehaviours);
			}
			finally
			{
				_container.IsInstalling = false;
			}
			if (this.PostInstall != null)
			{
				this.PostInstall();
			}
			if (OnPostInstall != null)
			{
				OnPostInstall.Invoke();
			}
		}

		public void Resolve()
		{
			if (this.PreResolve != null)
			{
				this.PreResolve();
			}
			if (OnPreResolve != null)
			{
				OnPreResolve.Invoke();
			}
			Assert.That(_hasInstalled);
			Assert.That(!_hasResolved);
			_hasResolved = true;
			_container.ResolveRoots();
			if (this.PostResolve != null)
			{
				this.PostResolve();
			}
			if (OnPostResolve != null)
			{
				OnPostResolve.Invoke();
			}
		}

		private void InstallBindings(List<MonoBehaviour> injectableMonoBehaviours)
		{
			_container.Bind(typeof(Context), typeof(SceneContext)).To<SceneContext>().FromInstance(this);
			_container.BindInterfacesTo<SceneContextRegistryAdderAndRemover>().AsSingle();
			_container.BindExecutionOrder<SceneContextRegistryAdderAndRemover>(-1);
			foreach (SceneDecoratorContext decoratorContext3 in _decoratorContexts)
			{
				decoratorContext3.InstallDecoratorSceneBindings();
			}
			InstallSceneBindings(injectableMonoBehaviours);
			_container.Bind(typeof(SceneKernel), typeof(MonoKernel)).To<SceneKernel>().FromNewComponentOn(base.gameObject)
				.AsSingle()
				.NonLazy();
			_container.Bind<ZenjectSceneLoader>().AsSingle();
			if (ExtraBindingsInstallMethod != null)
			{
				ExtraBindingsInstallMethod(_container);
				ExtraBindingsInstallMethod = null;
			}
			foreach (SceneDecoratorContext decoratorContext2 in _decoratorContexts)
			{
				decoratorContext2.InstallDecoratorInstallers();
			}
			InstallInstallers();
			foreach (SceneDecoratorContext decoratorContext in _decoratorContexts)
			{
				decoratorContext.InstallLateDecoratorInstallers();
			}
			if (ExtraBindingsLateInstallMethod != null)
			{
				ExtraBindingsLateInstallMethod(_container);
				ExtraBindingsLateInstallMethod = null;
			}
		}

		protected override void GetInjectableMonoBehaviours(List<MonoBehaviour> monoBehaviours)
		{
			Scene scene = base.gameObject.scene;
			ZenUtilInternal.AddStateMachineBehaviourAutoInjectersInScene(scene);
			ZenUtilInternal.GetInjectableMonoBehavioursInScene(scene, monoBehaviours);
		}

		public static SceneContext Create()
		{
			return RunnableContext.CreateComponent<SceneContext>(new GameObject("SceneContext"));
		}
	}
}
