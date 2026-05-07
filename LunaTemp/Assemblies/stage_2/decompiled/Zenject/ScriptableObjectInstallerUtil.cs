using ModestTree;
using UnityEngine;

namespace Zenject
{
	public static class ScriptableObjectInstallerUtil
	{
		public static string GetDefaultResourcePath<TInstaller>() where TInstaller : ScriptableObjectInstallerBase
		{
			return "Installers/" + typeof(TInstaller).PrettyName();
		}

		public static TInstaller CreateInstaller<TInstaller>(string resourcePath, DiContainer container) where TInstaller : ScriptableObjectInstallerBase
		{
			Object[] installers = Resources.LoadAll(resourcePath);
			Assert.That(installers.Length == 1, "Could not find unique ScriptableObjectInstaller with type '{0}' at resource path '{1}'", typeof(TInstaller), resourcePath);
			Object installer = installers[0];
			Assert.That(installer is TInstaller, "Expected to find installer with type '{0}' at resource path '{1}'", typeof(TInstaller), resourcePath);
			return (TInstaller)installer;
		}
	}
}
