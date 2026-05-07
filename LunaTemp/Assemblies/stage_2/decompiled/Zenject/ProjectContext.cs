using System;
using System.Collections.Generic;
using ModestTree;
using UnityEngine;
using Zenject.Internal;

namespace Zenject
{
	public class ProjectContext : Context
	{
		public const string ProjectContextResourcePath = "ProjectContext";

		public const string ProjectContextResourcePathOld = "ProjectCompositionRoot";

		private static ProjectContext _instance;

		[Tooltip("When true, objects that are created at runtime will be parented to the ProjectContext")]
		[SerializeField]
		private bool _parentNewObjectsUnderContext = true;

		[SerializeField]
		private ReflectionBakingCoverageModes _editorReflectionBakingCoverageMode = ReflectionBakingCoverageModes.FallbackToDirectReflection;

		[SerializeField]
		private ReflectionBakingCoverageModes _buildsReflectionBakingCoverageMode = ReflectionBakingCoverageModes.FallbackToDirectReflection;

		[SerializeField]
		private ZenjectSettings _settings = null;

		private DiContainer _container;

		public override DiContainer Container => _container;

		public static bool HasInstance => _instance != null;

		public static ProjectContext Instance
		{
			get
			{
				if (_instance == null)
				{
					InstantiateAndInitialize();
					Assert.IsNotNull(_instance);
				}
				return _instance;
			}
		}

		public static bool ValidateOnNextRun { get; set; }

		public bool ParentNewObjectsUnderContext
		{
			get
			{
				return _parentNewObjectsUnderContext;
			}
			set
			{
				_parentNewObjectsUnderContext = value;
			}
		}

		public event Action PreInstall;

		public event Action PostInstall;

		public event Action PreResolve;

		public event Action PostResolve;

		public override IEnumerable<GameObject> GetRootGameObjects()
		{
			return new GameObject[1] { base.gameObject };
		}

		public static GameObject TryGetPrefab()
		{
			UnityEngine.Object[] prefabs = Resources.LoadAll("ProjectContext", typeof(GameObject));
			if (prefabs.Length != 0)
			{
				Assert.That(prefabs.Length == 1, "Found multiple project context prefabs at resource path '{0}'", "ProjectContext");
				return (GameObject)prefabs[0];
			}
			prefabs = Resources.LoadAll("ProjectCompositionRoot", typeof(GameObject));
			if (prefabs.Length != 0)
			{
				Assert.That(prefabs.Length == 1, "Found multiple project context prefabs at resource path '{0}'", "ProjectCompositionRoot");
				return (GameObject)prefabs[0];
			}
			return null;
		}

		private static void InstantiateAndInitialize()
		{
			Assert.That(UnityEngine.Object.FindObjectsOfType<ProjectContext>().IsEmpty(), "Tried to create multiple instances of ProjectContext!");
			GameObject prefab = TryGetPrefab();
			bool prefabWasActive = false;
			if (prefab == null)
			{
				_instance = new GameObject("ProjectContext").AddComponent<ProjectContext>();
			}
			else
			{
				prefabWasActive = prefab.activeSelf;
				GameObject gameObjectInstance;
				if (prefabWasActive)
				{
					prefab.SetActive(false);
					gameObjectInstance = UnityEngine.Object.Instantiate(prefab);
					prefab.SetActive(true);
				}
				else
				{
					gameObjectInstance = UnityEngine.Object.Instantiate(prefab);
				}
				_instance = gameObjectInstance.GetComponent<ProjectContext>();
				Assert.IsNotNull(_instance, "Could not find ProjectContext component on prefab 'Resources/{0}.prefab'", "ProjectContext");
			}
			_instance.Initialize();
			if (prefabWasActive)
			{
				_instance.gameObject.SetActive(true);
			}
		}

		public void EnsureIsInitialized()
		{
		}

		public void Awake()
		{
			if (Application.isPlaying)
			{
				UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
			}
		}

		private void Initialize()
		{
			Assert.IsNull(_container);
			if (Application.isEditor)
			{
				TypeAnalyzer.ReflectionBakingCoverageMode = _editorReflectionBakingCoverageMode;
			}
			else
			{
				TypeAnalyzer.ReflectionBakingCoverageMode = _buildsReflectionBakingCoverageMode;
			}
			bool isValidating = ValidateOnNextRun;
			ValidateOnNextRun = false;
			_container = new DiContainer(new DiContainer[1] { StaticContext.Container }, isValidating);
			if (this.PreInstall != null)
			{
				this.PreInstall();
			}
			List<MonoBehaviour> injectableMonoBehaviours = new List<MonoBehaviour>();
			GetInjectableMonoBehaviours(injectableMonoBehaviours);
			foreach (MonoBehaviour instance in injectableMonoBehaviours)
			{
				_container.QueueForInject(instance);
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
			if (this.PreResolve != null)
			{
				this.PreResolve();
			}
			_container.ResolveRoots();
			if (this.PostResolve != null)
			{
				this.PostResolve();
			}
		}

		protected override void GetInjectableMonoBehaviours(List<MonoBehaviour> monoBehaviours)
		{
			ZenUtilInternal.AddStateMachineBehaviourAutoInjectersUnderGameObject(base.gameObject);
			ZenUtilInternal.GetInjectableMonoBehavioursUnderGameObject(base.gameObject, monoBehaviours);
		}

		private void InstallBindings(List<MonoBehaviour> injectableMonoBehaviours)
		{
			if (_parentNewObjectsUnderContext)
			{
				_container.DefaultParent = base.transform;
			}
			else
			{
				_container.DefaultParent = null;
			}
			_container.Settings = _settings ?? ZenjectSettings.Default;
			_container.Bind<ZenjectSceneLoader>().AsSingle();
			Installer<ZenjectManagersInstaller>.Install(_container);
			_container.Bind<Context>().FromInstance(this);
			_container.Bind(typeof(ProjectKernel), typeof(MonoKernel)).To<ProjectKernel>().FromNewComponentOn(base.gameObject)
				.AsSingle()
				.NonLazy();
			_container.Bind<SceneContextRegistry>().AsSingle();
			InstallSceneBindings(injectableMonoBehaviours);
			InstallInstallers();
		}
	}
}
