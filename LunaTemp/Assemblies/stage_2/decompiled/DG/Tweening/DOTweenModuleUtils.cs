using System;
using System.Reflection;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Core.PathCore;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.Scripting;

namespace DG.Tweening
{
	public static class DOTweenModuleUtils
	{
		public static class Physics
		{
			public static void SetOrientationOnPath(PathOptions options, Tween t, Quaternion newRot, Transform trans)
			{
				trans.rotation = newRot;
			}

			public static bool HasRigidbody2D(Component target)
			{
				return false;
			}

			
			public static bool HasRigidbody(Component target)
			{
				return false;
			}

			
			public static TweenerCore<Vector3, Path, PathOptions> CreateDOTweenPathTween(MonoBehaviour target, bool tweenRigidbody, bool isLocal, Path path, float duration, PathMode pathMode)
			{
				TweenerCore<Vector3, Path, PathOptions> t = null;
				if (0 == 0)
				{
					t = (isLocal ? target.transform.DOLocalPath(path, duration, pathMode) : target.transform.DOPath(path, duration, pathMode));
				}
				return t;
			}
		}

		private static bool _initialized;

		
		public static void Init()
		{
			if (!_initialized)
			{
				_initialized = true;
				DOTweenExternalCommand.SetOrientationOnPath += Physics.SetOrientationOnPath;
			}
		}

		
		private static void Preserver()
		{
			Assembly[] loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies();
			MethodInfo mi = typeof(MonoBehaviour).GetMethod("Stub");
		}
	}
}
