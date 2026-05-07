using System;
using ModestTree;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Zenject
{
	public class ZenjectSceneLoader
	{
		private readonly ProjectKernel _projectKernel;

		private readonly DiContainer _sceneContainer;

		public ZenjectSceneLoader([InjectOptional] SceneContext sceneRoot, ProjectKernel projectKernel)
		{
			_projectKernel = projectKernel;
			_sceneContainer = ((sceneRoot == null) ? null : sceneRoot.Container);
		}

		public void LoadScene(string sceneName)
		{
			LoadScene(sceneName, LoadSceneMode.Single);
		}

		public void LoadScene(string sceneName, LoadSceneMode loadMode)
		{
			LoadScene(sceneName, loadMode, null);
		}

		public void LoadScene(string sceneName, LoadSceneMode loadMode, Action<DiContainer> extraBindings)
		{
			LoadScene(sceneName, loadMode, extraBindings, LoadSceneRelationship.None);
		}

		public void LoadScene(string sceneName, LoadSceneMode loadMode, Action<DiContainer> extraBindings, LoadSceneRelationship containerMode)
		{
			LoadScene(sceneName, loadMode, extraBindings, containerMode, null);
		}

		public void LoadScene(string sceneName, LoadSceneMode loadMode, Action<DiContainer> extraBindings, LoadSceneRelationship containerMode, Action<DiContainer> extraBindingsLate)
		{
			PrepareForLoadScene(loadMode, extraBindings, extraBindingsLate, containerMode);
			Assert.That(Application.CanStreamedLevelBeLoaded(sceneName), "Unable to load scene '{0}'", sceneName);
			SceneManager.LoadScene(sceneName, loadMode);
		}

		public AsyncOperation LoadSceneAsync(string sceneName)
		{
			return LoadSceneAsync(sceneName, LoadSceneMode.Single);
		}

		public AsyncOperation LoadSceneAsync(string sceneName, LoadSceneMode loadMode)
		{
			return LoadSceneAsync(sceneName, loadMode, null);
		}

		public AsyncOperation LoadSceneAsync(string sceneName, LoadSceneMode loadMode, Action<DiContainer> extraBindings)
		{
			return LoadSceneAsync(sceneName, loadMode, extraBindings, LoadSceneRelationship.None);
		}

		public AsyncOperation LoadSceneAsync(string sceneName, LoadSceneMode loadMode, Action<DiContainer> extraBindings, LoadSceneRelationship containerMode)
		{
			return LoadSceneAsync(sceneName, loadMode, extraBindings, containerMode, null);
		}

		public AsyncOperation LoadSceneAsync(string sceneName, LoadSceneMode loadMode, Action<DiContainer> extraBindings, LoadSceneRelationship containerMode, Action<DiContainer> extraBindingsLate)
		{
			PrepareForLoadScene(loadMode, extraBindings, extraBindingsLate, containerMode);
			Assert.That(Application.CanStreamedLevelBeLoaded(sceneName), "Unable to load scene '{0}'", sceneName);
			return SceneManager.LoadSceneAsync(sceneName, loadMode);
		}

		private void PrepareForLoadScene(LoadSceneMode loadMode, Action<DiContainer> extraBindings, Action<DiContainer> extraBindingsLate, LoadSceneRelationship containerMode)
		{
			if (loadMode == LoadSceneMode.Single)
			{
				Assert.IsEqual(containerMode, LoadSceneRelationship.None);
				_projectKernel.ForceUnloadAllScenes();
			}
			switch (containerMode)
			{
			case LoadSceneRelationship.None:
				SceneContext.ParentContainers = null;
				break;
			case LoadSceneRelationship.Child:
				if (_sceneContainer == null)
				{
					SceneContext.ParentContainers = null;
					break;
				}
				SceneContext.ParentContainers = new DiContainer[1] { _sceneContainer };
				break;
			default:
				Assert.IsNotNull(_sceneContainer, "Cannot use LoadSceneRelationship.Sibling when loading scenes from ProjectContext");
				Assert.IsEqual(containerMode, LoadSceneRelationship.Sibling);
				SceneContext.ParentContainers = _sceneContainer.ParentContainers;
				break;
			}
			SceneContext.ExtraBindingsInstallMethod = extraBindings;
			SceneContext.ExtraBindingsLateInstallMethod = extraBindingsLate;
		}

		public void LoadScene(int sceneIndex)
		{
			LoadScene(sceneIndex, LoadSceneMode.Single);
		}

		public void LoadScene(int sceneIndex, LoadSceneMode loadMode)
		{
			LoadScene(sceneIndex, loadMode, null);
		}

		public void LoadScene(int sceneIndex, LoadSceneMode loadMode, Action<DiContainer> extraBindings)
		{
			LoadScene(sceneIndex, loadMode, extraBindings, LoadSceneRelationship.None);
		}

		public void LoadScene(int sceneIndex, LoadSceneMode loadMode, Action<DiContainer> extraBindings, LoadSceneRelationship containerMode)
		{
			LoadScene(sceneIndex, loadMode, extraBindings, containerMode, null);
		}

		public void LoadScene(int sceneIndex, LoadSceneMode loadMode, Action<DiContainer> extraBindings, LoadSceneRelationship containerMode, Action<DiContainer> extraBindingsLate)
		{
			PrepareForLoadScene(loadMode, extraBindings, extraBindingsLate, containerMode);
			Assert.That(Application.CanStreamedLevelBeLoaded(sceneIndex), "Unable to load scene '{0}'", sceneIndex);
			SceneManager.LoadScene(sceneIndex, loadMode);
		}

		public AsyncOperation LoadSceneAsync(int sceneIndex)
		{
			return LoadSceneAsync(sceneIndex, LoadSceneMode.Single);
		}

		public AsyncOperation LoadSceneAsync(int sceneIndex, LoadSceneMode loadMode)
		{
			return LoadSceneAsync(sceneIndex, loadMode, null);
		}

		public AsyncOperation LoadSceneAsync(int sceneIndex, LoadSceneMode loadMode, Action<DiContainer> extraBindings)
		{
			return LoadSceneAsync(sceneIndex, loadMode, extraBindings, LoadSceneRelationship.None);
		}

		public AsyncOperation LoadSceneAsync(int sceneIndex, LoadSceneMode loadMode, Action<DiContainer> extraBindings, LoadSceneRelationship containerMode)
		{
			return LoadSceneAsync(sceneIndex, loadMode, extraBindings, containerMode, null);
		}

		public AsyncOperation LoadSceneAsync(int sceneIndex, LoadSceneMode loadMode, Action<DiContainer> extraBindings, LoadSceneRelationship containerMode, Action<DiContainer> extraBindingsLate)
		{
			PrepareForLoadScene(loadMode, extraBindings, extraBindingsLate, containerMode);
			Assert.That(Application.CanStreamedLevelBeLoaded(sceneIndex), "Unable to load scene '{0}'", sceneIndex);
			return SceneManager.LoadSceneAsync(sceneIndex, loadMode);
		}
	}
}
