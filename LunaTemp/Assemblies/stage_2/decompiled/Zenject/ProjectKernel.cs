using System.Collections.Generic;
using System.Linq;
using ModestTree;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Zenject
{
	public class ProjectKernel : MonoKernel
	{
		[Inject]
		private ZenjectSettings _settings = null;

		[Inject]
		private SceneContextRegistry _contextRegistry = null;

		public void OnApplicationQuit()
		{
			if (_settings.EnsureDeterministicDestructionOrderOnApplicationQuit)
			{
				DestroyEverythingInOrder();
			}
		}

		public void DestroyEverythingInOrder()
		{
			ForceUnloadAllScenes(true);
			Assert.That(!base.IsDestroyed);
			Object.DestroyImmediate(base.gameObject);
			Assert.That(base.IsDestroyed);
		}

		public void ForceUnloadAllScenes(bool immediate = false)
		{
			Assert.That(!base.IsDestroyed);
			List<Scene> sceneOrder = new List<Scene>();
			for (int i = 0; i < SceneManager.sceneCount; i++)
			{
				sceneOrder.Add(SceneManager.GetSceneAt(i));
			}
			foreach (SceneContext sceneContext in _contextRegistry.SceneContexts.OrderByDescending((SceneContext x) => sceneOrder.IndexOf(x.gameObject.scene)).ToList())
			{
				if (immediate)
				{
					Object.DestroyImmediate(sceneContext.gameObject);
				}
				else
				{
					Object.Destroy(sceneContext.gameObject);
				}
			}
		}
	}
}
