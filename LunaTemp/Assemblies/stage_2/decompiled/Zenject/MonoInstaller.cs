namespace Zenject
{
	public class MonoInstaller : MonoInstallerBase
	{
	}
	public class MonoInstaller<TDerived> : MonoInstaller where TDerived : MonoInstaller<TDerived>
	{
		public static TDerived InstallFromResource(DiContainer container)
		{
			return InstallFromResource(MonoInstallerUtil.GetDefaultResourcePath<TDerived>(), container);
		}

		public static TDerived InstallFromResource(string resourcePath, DiContainer container)
		{
			return InstallFromResource(resourcePath, container, new object[0]);
		}

		public static TDerived InstallFromResource(DiContainer container, object[] extraArgs)
		{
			return InstallFromResource(MonoInstallerUtil.GetDefaultResourcePath<TDerived>(), container, extraArgs);
		}

		public static TDerived InstallFromResource(string resourcePath, DiContainer container, object[] extraArgs)
		{
			TDerived installer = MonoInstallerUtil.CreateInstaller<TDerived>(resourcePath, container);
			container.Inject(installer, extraArgs);
			installer.InstallBindings();
			return installer;
		}
	}
	public class MonoInstaller<TParam1, TDerived> : MonoInstallerBase where TDerived : MonoInstaller<TParam1, TDerived>
	{
		public static TDerived InstallFromResource(DiContainer container, TParam1 p1)
		{
			return InstallFromResource(MonoInstallerUtil.GetDefaultResourcePath<TDerived>(), container, p1);
		}

		public static TDerived InstallFromResource(string resourcePath, DiContainer container, TParam1 p1)
		{
			TDerived installer = MonoInstallerUtil.CreateInstaller<TDerived>(resourcePath, container);
			container.InjectExplicit(installer, InjectUtil.CreateArgListExplicit(p1));
			installer.InstallBindings();
			return installer;
		}
	}
	public class MonoInstaller<TParam1, TParam2, TDerived> : MonoInstallerBase where TDerived : MonoInstaller<TParam1, TParam2, TDerived>
	{
		public static TDerived InstallFromResource(DiContainer container, TParam1 p1, TParam2 p2)
		{
			return InstallFromResource(MonoInstallerUtil.GetDefaultResourcePath<TDerived>(), container, p1, p2);
		}

		public static TDerived InstallFromResource(string resourcePath, DiContainer container, TParam1 p1, TParam2 p2)
		{
			TDerived installer = MonoInstallerUtil.CreateInstaller<TDerived>(resourcePath, container);
			container.InjectExplicit(installer, InjectUtil.CreateArgListExplicit(p1, p2));
			installer.InstallBindings();
			return installer;
		}
	}
	public class MonoInstaller<TParam1, TParam2, TParam3, TDerived> : MonoInstallerBase where TDerived : MonoInstaller<TParam1, TParam2, TParam3, TDerived>
	{
		public static TDerived InstallFromResource(DiContainer container, TParam1 p1, TParam2 p2, TParam3 p3)
		{
			return InstallFromResource(MonoInstallerUtil.GetDefaultResourcePath<TDerived>(), container, p1, p2, p3);
		}

		public static TDerived InstallFromResource(string resourcePath, DiContainer container, TParam1 p1, TParam2 p2, TParam3 p3)
		{
			TDerived installer = MonoInstallerUtil.CreateInstaller<TDerived>(resourcePath, container);
			container.InjectExplicit(installer, InjectUtil.CreateArgListExplicit(p1, p2, p3));
			installer.InstallBindings();
			return installer;
		}
	}
	public class MonoInstaller<TParam1, TParam2, TParam3, TParam4, TDerived> : MonoInstallerBase where TDerived : MonoInstaller<TParam1, TParam2, TParam3, TParam4, TDerived>
	{
		public static TDerived InstallFromResource(DiContainer container, TParam1 p1, TParam2 p2, TParam3 p3, TParam4 p4)
		{
			return InstallFromResource(MonoInstallerUtil.GetDefaultResourcePath<TDerived>(), container, p1, p2, p3, p4);
		}

		public static TDerived InstallFromResource(string resourcePath, DiContainer container, TParam1 p1, TParam2 p2, TParam3 p3, TParam4 p4)
		{
			TDerived installer = MonoInstallerUtil.CreateInstaller<TDerived>(resourcePath, container);
			container.InjectExplicit(installer, InjectUtil.CreateArgListExplicit(p1, p2, p3, p4));
			installer.InstallBindings();
			return installer;
		}
	}
	public class MonoInstaller<TParam1, TParam2, TParam3, TParam4, TParam5, TDerived> : MonoInstallerBase where TDerived : MonoInstaller<TParam1, TParam2, TParam3, TParam4, TParam5, TDerived>
	{
		public static TDerived InstallFromResource(DiContainer container, TParam1 p1, TParam2 p2, TParam3 p3, TParam4 p4, TParam5 p5)
		{
			return InstallFromResource(MonoInstallerUtil.GetDefaultResourcePath<TDerived>(), container, p1, p2, p3, p4, p5);
		}

		public static TDerived InstallFromResource(string resourcePath, DiContainer container, TParam1 p1, TParam2 p2, TParam3 p3, TParam4 p4, TParam5 p5)
		{
			TDerived installer = MonoInstallerUtil.CreateInstaller<TDerived>(resourcePath, container);
			container.InjectExplicit(installer, InjectUtil.CreateArgListExplicit(p1, p2, p3, p4, p5));
			installer.InstallBindings();
			return installer;
		}
	}
}
