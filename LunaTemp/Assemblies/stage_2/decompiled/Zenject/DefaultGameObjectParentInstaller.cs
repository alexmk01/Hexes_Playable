using System;
using UnityEngine;

namespace Zenject
{
	public class DefaultGameObjectParentInstaller : Installer<string, DefaultGameObjectParentInstaller>
	{
		private class DefaultParentObjectDestroyer : IDisposable
		{
			private readonly GameObject _gameObject;

			public DefaultParentObjectDestroyer(GameObject gameObject)
			{
				_gameObject = gameObject;
			}

			public void Dispose()
			{
				UnityEngine.Object.Destroy(_gameObject);
			}
		}

		private readonly string _name;

		public DefaultGameObjectParentInstaller(string name)
		{
			_name = name;
		}

		public override void InstallBindings()
		{
			GameObject defaultParent = new GameObject(_name);
			defaultParent.transform.SetParent(base.Container.InheritedDefaultParent, false);
			base.Container.DefaultParent = defaultParent.transform;
			base.Container.Bind<IDisposable>().To<DefaultParentObjectDestroyer>().AsCached()
				.WithArguments(defaultParent);
			base.Container.BindDisposableExecutionOrder<DefaultParentObjectDestroyer>(int.MinValue);
		}
	}
}
