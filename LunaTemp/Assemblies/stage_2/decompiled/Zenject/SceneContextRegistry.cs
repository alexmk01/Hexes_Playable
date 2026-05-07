using System.Collections.Generic;
using ModestTree;
using UnityEngine.SceneManagement;

namespace Zenject
{
	public class SceneContextRegistry
	{
		private readonly Dictionary<Scene, SceneContext> _map = new Dictionary<Scene, SceneContext>();

		public IEnumerable<SceneContext> SceneContexts => _map.Values;

		public void Add(SceneContext context)
		{
			Assert.That(!_map.ContainsKey(context.gameObject.scene));
			_map.Add(context.gameObject.scene, context);
		}

		public SceneContext GetSceneContextForScene(string name)
		{
			Scene scene = SceneManager.GetSceneByName(name);
			Assert.That(scene.IsValid(), "Could not find scene with name '{0}'", name);
			return GetSceneContextForScene(scene);
		}

		public SceneContext GetSceneContextForScene(Scene scene)
		{
			return _map[scene];
		}

		public SceneContext TryGetSceneContextForScene(string name)
		{
			Scene scene = SceneManager.GetSceneByName(name);
			Assert.That(scene.IsValid(), "Could not find scene with name '{0}'", name);
			return TryGetSceneContextForScene(scene);
		}

		public SceneContext TryGetSceneContextForScene(Scene scene)
		{
			if (_map.TryGetValue(scene, out var context))
			{
				return context;
			}
			return null;
		}

		public DiContainer GetContainerForScene(Scene scene)
		{
			DiContainer container = TryGetContainerForScene(scene);
			if (container != null)
			{
				return container;
			}
			throw Assert.CreateException("Unable to find DiContainer for scene '{0}'", scene.name);
		}

		public DiContainer TryGetContainerForScene(Scene scene)
		{
			if (scene == ProjectContext.Instance.gameObject.scene)
			{
				return ProjectContext.Instance.Container;
			}
			SceneContext sceneContext = TryGetSceneContextForScene(scene);
			if (sceneContext != null)
			{
				return sceneContext.Container;
			}
			return null;
		}

		public void Remove(SceneContext context)
		{
			if (!_map.Remove(context.gameObject.scene))
			{
				Log.Warn("Failed to remove SceneContext from SceneContextRegistry");
			}
		}
	}
}
