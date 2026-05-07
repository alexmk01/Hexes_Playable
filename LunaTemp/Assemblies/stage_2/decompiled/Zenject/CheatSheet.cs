using System.Collections.Generic;
using System.Linq;
using ModestTree;
using UnityEngine;

namespace Zenject
{
	public class CheatSheet : Installer<CheatSheet>
	{
		public class Norf
		{
			[Inject(Id = "FooA")]
			public string Foo;
		}

		public class Qux
		{
			[Inject(Id = "FooB")]
			public string Foo;
		}

		public class Norf2
		{
			[Inject]
			public Foo Foo;
		}

		public class Qux2
		{
			[Inject]
			public Foo Foo;

			[Inject(Id = "FooA")]
			public Foo Foo2;
		}

		public class FooInstaller : Installer<FooInstaller>
		{
			public FooInstaller(string foo)
			{
			}

			public override void InstallBindings()
			{
			}
		}

		public class FooInstallerWithArgs : Installer<string, FooInstallerWithArgs>
		{
			public FooInstallerWithArgs(string foo)
			{
			}

			public override void InstallBindings()
			{
			}
		}

		public interface IFoo2
		{
		}

		public interface IFoo
		{
		}

		public interface IBar : IFoo
		{
		}

		public class Foo : MonoBehaviour, IFoo, IFoo2, IBar
		{
			public Bar GetBar()
			{
				return new Bar();
			}

			public string GetTitle()
			{
				return "title";
			}
		}

		public class Foo1 : IFoo
		{
		}

		public class Foo2 : IFoo
		{
		}

		public class Foo3 : IFoo
		{
		}

		public class Baz
		{
		}

		public class Gui
		{
		}

		public class Bar : IBar, IFoo
		{
			public Foo Foo => null;
		}

		public override void InstallBindings()
		{
			base.Container.Bind<Foo>().AsTransient();
			base.Container.Bind<IFoo>().To<Foo>().AsTransient();
			base.Container.Bind(typeof(IFoo)).To(typeof(Foo)).AsTransient();
			base.Container.Bind<Foo>().AsSingle();
			base.Container.Bind<IFoo>().To<Foo>().AsSingle();
			base.Container.Bind(typeof(Foo), typeof(IFoo), typeof(IFoo2)).To<Foo>().AsSingle();
			base.Container.BindInterfacesAndSelfTo<Foo>().AsSingle();
			base.Container.BindInterfacesTo<Foo>().AsSingle();
			base.Container.Bind<Foo>().FromInstance(new Foo());
			base.Container.BindInstance(new Foo());
			base.Container.BindInstances(new Foo(), new Bar());
			base.Container.Bind<int>().FromInstance(10);
			base.Container.Bind<bool>().FromInstance(false);
			base.Container.BindInstance(10);
			base.Container.BindInstance(false);
			base.Container.BindInstance(10).WhenInjectedInto<Foo>();
			base.Container.Bind<Foo>().FromMethod(GetFoo);
			base.Container.Bind<IFoo>().FromMethod(GetRandomFoo);
			base.Container.Bind<Foo>().FromMethod((InjectContext ctx) => new Foo());
			base.Container.Bind<Foo>().FromMethod((InjectContext ctx) => ctx.Container.Instantiate<Foo>());
			InstallMore();
		}

		private Foo GetFoo(InjectContext ctx)
		{
			return new Foo();
		}

		private IFoo GetRandomFoo(InjectContext ctx)
		{
			switch (Random.Range(0, 3))
			{
			case 0:
				return ctx.Container.Instantiate<Foo1>();
			case 1:
				return ctx.Container.Instantiate<Foo2>();
			default:
				return ctx.Container.Instantiate<Foo3>();
			}
		}

		private void InstallMore()
		{
			base.Container.Bind<Foo>().AsSingle();
			base.Container.Bind<Bar>().FromResolveGetter((Foo foo) => foo.GetBar());
			base.Container.Bind<string>().FromResolveGetter((Foo foo) => foo.GetTitle());
			base.Container.Bind<Foo>().FromNewComponentOnNewGameObject().AsSingle();
			base.Container.Bind<Foo>().FromNewComponentOnNewGameObject().WithGameObjectName("Foo1")
				.AsSingle();
			base.Container.Bind<IFoo>().To<Foo>().FromNewComponentOnNewGameObject()
				.AsSingle();
			GameObject prefab = null;
			base.Container.Bind<Foo>().FromComponentInNewPrefab(prefab).AsSingle();
			base.Container.Bind<IFoo>().To<Foo>().FromComponentInNewPrefab(prefab)
				.AsSingle();
			base.Container.Bind(typeof(Foo), typeof(Bar)).FromComponentInNewPrefab(prefab).AsSingle();
			base.Container.Bind<Foo>().FromComponentInNewPrefab(prefab).AsTransient();
			base.Container.Bind<IFoo>().To<Foo>().FromComponentInNewPrefab(prefab);
			base.Container.Bind<string>().WithId("PlayerName").FromInstance("name of the player");
			base.Container.BindInstance("name of the player").WithId("PlayerName");
			base.Container.BindInstance("foo").WithId("FooA");
			base.Container.BindInstance("asdf").WithId("FooB");
			InstallMore2();
		}

		public void InstallMore2()
		{
			base.Container.Bind<Foo>().AsCached();
			base.Container.Bind<Foo>().WithId("FooA").AsCached();
			base.Container.Bind<Foo>().WithId("FooA").AsCached();
			InstallMore3();
		}

		public void InstallMore3()
		{
			base.Container.Bind<Foo>().AsSingle().WhenInjectedInto<Bar>();
			base.Container.Bind<IFoo>().To<Foo1>().AsSingle()
				.WhenInjectedInto<Bar>();
			base.Container.Bind<IFoo>().To<Foo2>().AsSingle()
				.WhenInjectedInto<Qux>();
			base.Container.Bind<IFoo>().To<Foo1>().AsSingle();
			base.Container.Bind<IFoo>().To<Foo2>().AsSingle()
				.WhenInjectedInto<Qux>();
			base.Container.Bind<Foo>().AsSingle().WhenInjectedInto(typeof(Bar), typeof(Qux), typeof(Baz));
			base.Container.BindInstance("my game").WithId("Title").WhenInjectedInto<Gui>();
			base.Container.BindInstance(5).WhenInjectedInto<Gui>();
			base.Container.BindInstance(5f).When((InjectContext ctx) => ctx.ObjectType == typeof(Gui) && ctx.MemberName == "width");
			base.Container.Bind<IFoo>().To<Foo>().AsTransient()
				.When((InjectContext ctx) => ctx.AllObjectTypes.Contains(typeof(Bar)));
			Foo foo1 = new Foo();
			Foo foo2 = new Foo();
			base.Container.Bind<Bar>().WithId("Bar1").AsCached();
			base.Container.Bind<Bar>().WithId("Bar2").AsCached();
			base.Container.BindInstance(foo1).When((InjectContext c) => c.ParentContexts.Where((InjectContext x) => x.MemberType == typeof(Bar) && object.Equals(x.Identifier, "Bar1")).Any());
			base.Container.BindInstance(foo2).When((InjectContext c) => c.ParentContexts.Where((InjectContext x) => x.MemberType == typeof(Bar) && object.Equals(x.Identifier, "Bar2")).Any());
			Assert.That(base.Container.ResolveId<Bar>("Bar1").Foo == foo1);
			Assert.That(base.Container.ResolveId<Bar>("Bar2").Foo == foo2);
			GameObject fooPrefab = null;
			base.Container.Bind<Foo>().FromComponentInNewPrefab(fooPrefab).AsSingle();
			base.Container.Bind<IBar>().To<Foo>().FromResolve();
			base.Container.Bind<IFoo>().To<IBar>().FromResolve();
			base.Container.Bind(typeof(Foo), typeof(IBar), typeof(IFoo)).To<Foo>().FromComponentInNewPrefab(fooPrefab)
				.AsSingle();
			InstallMore4();
		}

		private void InstallMore4()
		{
			Installer<FooInstaller>.Install(base.Container);
			base.Container.BindInstance("foo").WhenInjectedInto<FooInstaller>();
			Installer<FooInstaller>.Install(base.Container);
			Installer<string, FooInstallerWithArgs>.Install(base.Container, "foo");
			Foo foo = new Foo();
			base.Container.Inject(foo);
			base.Container.Resolve<IFoo>();
			base.Container.TryResolve<IFoo>();
			base.Container.BindInstance(new Foo());
			base.Container.BindInstance(new Foo());
			List<IFoo> foos = base.Container.ResolveAll<IFoo>();
			base.Container.Instantiate<Foo>();
			GameObject prefab1 = null;
			GameObject prefab2 = null;
			GameObject go = base.Container.InstantiatePrefab(prefab1);
			Foo foo2 = base.Container.InstantiatePrefabForComponent<Foo>(prefab2);
			Foo foo3 = base.Container.InstantiateComponent<Foo>(go);
		}
	}
}
