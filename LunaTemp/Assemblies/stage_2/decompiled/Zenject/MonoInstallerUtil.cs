using ModestTree;
using UnityEngine;

namespace Zenject
{
	public static class MonoInstallerUtil
	{
		public static string GetDefaultResourcePath<TInstaller>() where TInstaller : MonoInstallerBase
		{
			return "Installers/" + typeof(TInstaller).PrettyName();
		}

		public static TInstaller CreateInstaller<TInstaller>(string resourcePath, DiContainer container) where TInstaller : MonoInstallerBase
		{
			bool shouldMakeActive;
			GameObject gameObj = container.CreateAndParentPrefabResource(resourcePath, GameObjectCreationParameters.Default, null, out shouldMakeActive);
			if (shouldMakeActive && !container.IsValidating)
			{
				gameObj.SetActive(true);
			}
			TInstaller[] installers = gameObj.GetComponentsInChildren<TInstaller>();
			Assert.That(installers.Length == 1, "Could not find unique MonoInstaller with type '{0}' on prefab '{1}'", typeof(TInstaller), gameObj.name);
			return installers[0];
		}
	}
}
