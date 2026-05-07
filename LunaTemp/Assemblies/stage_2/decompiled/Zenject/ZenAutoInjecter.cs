using ModestTree;
using UnityEngine;

namespace Zenject
{
	public class ZenAutoInjecter : MonoBehaviour
	{
		public enum ContainerSources
		{
			SceneContext,
			ProjectContext,
			SearchHierarchy
		}

		[SerializeField]
		private ContainerSources _containerSource = ContainerSources.SearchHierarchy;

		private bool _hasInjected;

		public ContainerSources ContainerSource
		{
			get
			{
				return _containerSource;
			}
			set
			{
				_containerSource = value;
			}
		}

		[Inject]
		public void Construct()
		{
			if (!_hasInjected)
			{
				throw Assert.CreateException("ZenAutoInjecter was injected!  Do not use ZenAutoInjecter for objects that are instantiated through zenject or which exist in the initial scene hierarchy");
			}
		}

		public void Awake()
		{
			_hasInjected = true;
			LookupContainer().InjectGameObject(base.gameObject);
		}

		private DiContainer LookupContainer()
		{
			if (_containerSource == ContainerSources.ProjectContext)
			{
				return ProjectContext.Instance.Container;
			}
			if (_containerSource == ContainerSources.SceneContext)
			{
				return GetContainerForCurrentScene();
			}
			Assert.IsEqual(_containerSource, ContainerSources.SearchHierarchy);
			Context parentContext = base.transform.GetComponentInParent<Context>();
			if (parentContext != null)
			{
				return parentContext.Container;
			}
			return GetContainerForCurrentScene();
		}

		private DiContainer GetContainerForCurrentScene()
		{
			return ProjectContext.Instance.Container.Resolve<SceneContextRegistry>().GetContainerForScene(base.gameObject.scene);
		}
	}
}
