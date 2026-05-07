using System;
using System.Collections.Generic;
using ModestTree;
using Zenject.Internal;

namespace Zenject
{
	[NoReflectionBaking]
	public class SubContainerCreatorByNewPrefabInstaller : SubContainerCreatorByNewPrefabDynamicContext
	{
		private readonly Type _installerType;

		private readonly List<TypeValuePair> _extraArgs;

		public SubContainerCreatorByNewPrefabInstaller(DiContainer container, IPrefabProvider prefabProvider, GameObjectCreationParameters gameObjectBindInfo, Type installerType, List<TypeValuePair> extraArgs)
			: base(container, prefabProvider, gameObjectBindInfo)
		{
			_installerType = installerType;
			_extraArgs = extraArgs;
			Assert.That(installerType.DerivesFrom<InstallerBase>(), "Invalid installer type given during bind command.  Expected type '{0}' to derive from 'Installer<>'", installerType);
		}

		protected override void AddInstallers(List<TypeValuePair> args, GameObjectContext context)
		{
			context.AddNormalInstaller(new ActionInstaller(delegate(DiContainer subContainer)
			{
				List<TypeValuePair> list = ZenPools.SpawnList<TypeValuePair>();
				list.AllocFreeAddRange(_extraArgs);
				list.AllocFreeAddRange(args);
				InstallerBase installerBase = (InstallerBase)subContainer.InstantiateExplicit(_installerType, list);
				ZenPools.DespawnList(list);
				installerBase.InstallBindings();
			}));
		}
	}
}
