using System;
using System.Collections.Generic;
using System.Linq;
using ModestTree;
using ModestTree.Util;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Zenject.Internal
{
	public static class ZenUtilInternal
	{
		public static bool IsNull(object obj)
		{
			return obj?.Equals(null) ?? true;
		}

		public static bool AreFunctionsEqual(Delegate left, Delegate right)
		{
			return left.Target == right.Target && left.Method() == right.Method();
		}

		public static int GetInheritanceDelta(Type derived, Type parent)
		{
			Assert.That(derived.DerivesFromOrEqual(parent));
			if (parent.IsInterface())
			{
				return 1;
			}
			if (derived == parent)
			{
				return 0;
			}
			int distance = 1;
			Type child = derived;
			while ((child = child.BaseType()) != parent)
			{
				distance++;
			}
			return distance;
		}

		public static IEnumerable<SceneContext> GetAllSceneContexts()
		{
			foreach (Scene scene in UnityUtil.AllLoadedScenes)
			{
				List<SceneContext> contexts = scene.GetRootGameObjects().SelectMany((GameObject root) => root.GetComponentsInChildren<SceneContext>()).ToList();
				if (!contexts.IsEmpty())
				{
					Assert.That(contexts.Count == 1, "Found multiple scene contexts in scene '{0}'", scene.name);
					yield return contexts[0];
				}
			}
		}

		public static void AddStateMachineBehaviourAutoInjectersInScene(Scene scene)
		{
			foreach (GameObject rootObj in GetRootGameObjects(scene))
			{
				if (rootObj != null)
				{
					AddStateMachineBehaviourAutoInjectersUnderGameObject(rootObj);
				}
			}
		}

		public static void AddStateMachineBehaviourAutoInjectersUnderGameObject(GameObject root)
		{
			Animator[] animators = root.GetComponentsInChildren<Animator>(true);
			Animator[] array = animators;
			foreach (Animator animator in array)
			{
				if (animator.gameObject.GetComponent<ZenjectStateMachineBehaviourAutoInjecter>() == null)
				{
					animator.gameObject.AddComponent<ZenjectStateMachineBehaviourAutoInjecter>();
				}
			}
		}

		public static void GetInjectableMonoBehavioursInScene(Scene scene, List<MonoBehaviour> monoBehaviours)
		{
			foreach (GameObject rootObj in GetRootGameObjects(scene))
			{
				if (rootObj != null)
				{
					GetInjectableMonoBehavioursUnderGameObjectInternal(rootObj, monoBehaviours);
				}
			}
		}

		public static void GetInjectableMonoBehavioursUnderGameObject(GameObject gameObject, List<MonoBehaviour> injectableComponents)
		{
			GetInjectableMonoBehavioursUnderGameObjectInternal(gameObject, injectableComponents);
		}

		private static void GetInjectableMonoBehavioursUnderGameObjectInternal(GameObject gameObject, List<MonoBehaviour> injectableComponents)
		{
			if (gameObject == null)
			{
				return;
			}
			MonoBehaviour[] monoBehaviours = gameObject.GetComponents<MonoBehaviour>();
			foreach (MonoBehaviour monoBehaviour in monoBehaviours)
			{
				if (monoBehaviour != null && monoBehaviour.GetType().DerivesFromOrEqual<GameObjectContext>())
				{
					injectableComponents.Add(monoBehaviour);
					return;
				}
			}
			for (int k = 0; k < gameObject.transform.childCount; k++)
			{
				Transform child = gameObject.transform.GetChild(k);
				if (child != null)
				{
					GetInjectableMonoBehavioursUnderGameObjectInternal(child.gameObject, injectableComponents);
				}
			}
			foreach (MonoBehaviour monoBehaviour2 in monoBehaviours)
			{
				if (monoBehaviour2 != null && IsInjectableMonoBehaviourType(monoBehaviour2.GetType()))
				{
					injectableComponents.Add(monoBehaviour2);
				}
			}
		}

		public static bool IsInjectableMonoBehaviourType(Type type)
		{
			return type != null && !type.DerivesFrom<MonoInstaller>() && TypeAnalyzer.HasInfo(type);
		}

		public static IEnumerable<GameObject> GetRootGameObjects(Scene scene)
		{
			if (scene.isLoaded)
			{
				return from x in scene.GetRootGameObjects()
					where x.GetComponent<ProjectContext>() == null
					select x;
			}
			return from x in Resources.FindObjectsOfTypeAll<GameObject>()
				where x.transform.parent == null && x.GetComponent<ProjectContext>() == null && x.scene == scene
				select x;
		}
	}
}
