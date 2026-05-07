using System;
using UnityEngine;

namespace Zenject
{
	public class ScriptableObjectInstallerBase : ScriptableObject, IInstaller
	{
		[Inject]
		private DiContainer _container = null;

		protected DiContainer Container => _container;

		bool IInstaller.IsEnabled => true;

		public virtual void InstallBindings()
		{
			throw new NotImplementedException();
		}
	}
}
