namespace Zenject
{
	public class ScriptableObjectInstaller : ScriptableObjectInstallerBase
	{
	}
	public class ScriptableObjectInstaller<TDerived> : ScriptableObjectInstaller where TDerived : ScriptableObjectInstaller<TDerived>
	{
		public static TDerived InstallFromResource(DiContainer container)
		{
			return InstallFromResource(ScriptableObjectInstallerUtil.GetDefaultResourcePath<TDerived>(), container);
		}

		public static TDerived InstallFromResource(string resourcePath, DiContainer container)
		{
			TDerived installer = ScriptableObjectInstallerUtil.CreateInstaller<TDerived>(resourcePath, container);
			container.Inject(installer);
			installer.InstallBindings();
			return installer;
		}
	}
	public class ScriptableObjectInstaller<TParam1, TDerived> : ScriptableObjectInstallerBase where TDerived : ScriptableObjectInstaller<TParam1, TDerived>
	{
		public static TDerived InstallFromResource(DiContainer container, TParam1 p1)
		{
			return InstallFromResource(ScriptableObjectInstallerUtil.GetDefaultResourcePath<TDerived>(), container, p1);
		}

		public static TDerived InstallFromResource(string resourcePath, DiContainer container, TParam1 p1)
		{
			TDerived installer = ScriptableObjectInstallerUtil.CreateInstaller<TDerived>(resourcePath, container);
			container.InjectExplicit(installer, InjectUtil.CreateArgListExplicit(p1));
			installer.InstallBindings();
			return installer;
		}
	}
	public class ScriptableObjectInstaller<TParam1, TParam2, TDerived> : ScriptableObjectInstallerBase where TDerived : ScriptableObjectInstaller<TParam1, TParam2, TDerived>
	{
		public static TDerived InstallFromResource(DiContainer container, TParam1 p1, TParam2 p2)
		{
			return InstallFromResource(ScriptableObjectInstallerUtil.GetDefaultResourcePath<TDerived>(), container, p1, p2);
		}

		public static TDerived InstallFromResource(string resourcePath, DiContainer container, TParam1 p1, TParam2 p2)
		{
			TDerived installer = ScriptableObjectInstallerUtil.CreateInstaller<TDerived>(resourcePath, container);
			container.InjectExplicit(installer, InjectUtil.CreateArgListExplicit(p1, p2));
			installer.InstallBindings();
			return installer;
		}
	}
	public class ScriptableObjectInstaller<TParam1, TParam2, TParam3, TDerived> : ScriptableObjectInstallerBase where TDerived : ScriptableObjectInstaller<TParam1, TParam2, TParam3, TDerived>
	{
		public static TDerived InstallFromResource(DiContainer container, TParam1 p1, TParam2 p2, TParam3 p3)
		{
			return InstallFromResource(ScriptableObjectInstallerUtil.GetDefaultResourcePath<TDerived>(), container, p1, p2, p3);
		}

		public static TDerived InstallFromResource(string resourcePath, DiContainer container, TParam1 p1, TParam2 p2, TParam3 p3)
		{
			TDerived installer = ScriptableObjectInstallerUtil.CreateInstaller<TDerived>(resourcePath, container);
			container.InjectExplicit(installer, InjectUtil.CreateArgListExplicit(p1, p2, p3));
			installer.InstallBindings();
			return installer;
		}
	}
	public class ScriptableObjectInstaller<TParam1, TParam2, TParam3, TParam4, TDerived> : ScriptableObjectInstallerBase where TDerived : ScriptableObjectInstaller<TParam1, TParam2, TParam3, TParam4, TDerived>
	{
		public static TDerived InstallFromResource(DiContainer container, TParam1 p1, TParam2 p2, TParam3 p3, TParam4 p4)
		{
			return InstallFromResource(ScriptableObjectInstallerUtil.GetDefaultResourcePath<TDerived>(), container, p1, p2, p3, p4);
		}

		public static TDerived InstallFromResource(string resourcePath, DiContainer container, TParam1 p1, TParam2 p2, TParam3 p3, TParam4 p4)
		{
			TDerived installer = ScriptableObjectInstallerUtil.CreateInstaller<TDerived>(resourcePath, container);
			container.InjectExplicit(installer, InjectUtil.CreateArgListExplicit(p1, p2, p3, p4));
			installer.InstallBindings();
			return installer;
		}
	}
}
