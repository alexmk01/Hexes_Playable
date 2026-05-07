using System;
using System.Diagnostics;
using UnityEngine;

namespace Zenject
{
	[DebuggerStepThrough]
	public class MonoInstallerBase : MonoBehaviour, IInstaller
	{
		[Inject]
		protected DiContainer Container { get; set; }

		public virtual bool IsEnabled => base.enabled;

		public virtual void Start()
		{
		}

		public virtual void InstallBindings()
		{
			throw new NotImplementedException();
		}
	}
}
