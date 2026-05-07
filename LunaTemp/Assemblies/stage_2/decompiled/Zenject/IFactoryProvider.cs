using System;
using System.Collections.Generic;
using ModestTree;

namespace Zenject
{
	[NoReflectionBaking]
	public class IFactoryProvider<TContract> : IFactoryProviderBase<TContract>
	{
		public IFactoryProvider(DiContainer container, Guid factoryId)
			: base(container, factoryId)
		{
		}

		public override void GetAllInstancesWithInjectSplit(InjectContext context, List<TypeValuePair> args, out Action injectAction, List<object> buffer)
		{
			Assert.That(args.IsEmpty());
			Assert.IsNotNull(context);
			Assert.That(typeof(TContract).DerivesFromOrEqual(context.MemberType));
			object factory = base.Container.ResolveId(typeof(IFactory<TContract>), base.FactoryId);
			injectAction = null;
			if (base.Container.IsValidating)
			{
				buffer.Add(new ValidationMarker(typeof(TContract)));
			}
			else
			{
				buffer.Add(((IFactory<TContract>)factory).Create());
			}
		}
	}
	[NoReflectionBaking]
	public class IFactoryProvider<TParam1, TContract> : IFactoryProviderBase<TContract>
	{
		public IFactoryProvider(DiContainer container, Guid factoryId)
			: base(container, factoryId)
		{
		}

		public override void GetAllInstancesWithInjectSplit(InjectContext context, List<TypeValuePair> args, out Action injectAction, List<object> buffer)
		{
			Assert.IsEqual(args.Count, 1);
			Assert.IsNotNull(context);
			Assert.That(typeof(TContract).DerivesFromOrEqual(context.MemberType));
			Assert.That(args[0].Type.DerivesFromOrEqual<TParam1>());
			object factory = base.Container.ResolveId(typeof(IFactory<TParam1, TContract>), base.FactoryId);
			injectAction = null;
			if (base.Container.IsValidating)
			{
				buffer.Add(new ValidationMarker(typeof(TContract)));
			}
			else
			{
				buffer.Add(((IFactory<TParam1, TContract>)factory).Create((TParam1)args[0].Value));
			}
		}
	}
	[NoReflectionBaking]
	public class IFactoryProvider<TParam1, TParam2, TContract> : IFactoryProviderBase<TContract>
	{
		public IFactoryProvider(DiContainer container, Guid factoryId)
			: base(container, factoryId)
		{
		}

		public override void GetAllInstancesWithInjectSplit(InjectContext context, List<TypeValuePair> args, out Action injectAction, List<object> buffer)
		{
			Assert.IsEqual(args.Count, 2);
			Assert.IsNotNull(context);
			Assert.That(typeof(TContract).DerivesFromOrEqual(context.MemberType));
			Assert.That(args[0].Type.DerivesFromOrEqual<TParam1>());
			Assert.That(args[1].Type.DerivesFromOrEqual<TParam2>());
			object factory = base.Container.ResolveId(typeof(IFactory<TParam1, TParam2, TContract>), base.FactoryId);
			injectAction = null;
			if (base.Container.IsValidating)
			{
				buffer.Add(new ValidationMarker(typeof(TContract)));
			}
			else
			{
				buffer.Add(((IFactory<TParam1, TParam2, TContract>)factory).Create((TParam1)args[0].Value, (TParam2)args[1].Value));
			}
		}
	}
	[NoReflectionBaking]
	public class IFactoryProvider<TParam1, TParam2, TParam3, TContract> : IFactoryProviderBase<TContract>
	{
		public IFactoryProvider(DiContainer container, Guid factoryId)
			: base(container, factoryId)
		{
		}

		public override void GetAllInstancesWithInjectSplit(InjectContext context, List<TypeValuePair> args, out Action injectAction, List<object> buffer)
		{
			Assert.IsEqual(args.Count, 3);
			Assert.IsNotNull(context);
			Assert.That(typeof(TContract).DerivesFromOrEqual(context.MemberType));
			Assert.That(args[0].Type.DerivesFromOrEqual<TParam1>());
			Assert.That(args[1].Type.DerivesFromOrEqual<TParam2>());
			Assert.That(args[2].Type.DerivesFromOrEqual<TParam3>());
			object factory = base.Container.ResolveId(typeof(IFactory<TParam1, TParam2, TParam3, TContract>), base.FactoryId);
			injectAction = null;
			if (base.Container.IsValidating)
			{
				buffer.Add(new ValidationMarker(typeof(TContract)));
			}
			else
			{
				buffer.Add(((IFactory<TParam1, TParam2, TParam3, TContract>)factory).Create((TParam1)args[0].Value, (TParam2)args[1].Value, (TParam3)args[2].Value));
			}
		}
	}
	[NoReflectionBaking]
	public class IFactoryProvider<TParam1, TParam2, TParam3, TParam4, TContract> : IFactoryProviderBase<TContract>
	{
		public IFactoryProvider(DiContainer container, Guid factoryId)
			: base(container, factoryId)
		{
		}

		public override void GetAllInstancesWithInjectSplit(InjectContext context, List<TypeValuePair> args, out Action injectAction, List<object> buffer)
		{
			Assert.IsEqual(args.Count, 4);
			Assert.IsNotNull(context);
			Assert.That(typeof(TContract).DerivesFromOrEqual(context.MemberType));
			Assert.That(args[0].Type.DerivesFromOrEqual<TParam1>());
			Assert.That(args[1].Type.DerivesFromOrEqual<TParam2>());
			Assert.That(args[2].Type.DerivesFromOrEqual<TParam3>());
			Assert.That(args[3].Type.DerivesFromOrEqual<TParam4>());
			object factory = base.Container.ResolveId(typeof(IFactory<TParam1, TParam2, TParam3, TParam4, TContract>), base.FactoryId);
			injectAction = null;
			if (base.Container.IsValidating)
			{
				buffer.Add(new ValidationMarker(typeof(TContract)));
			}
			else
			{
				buffer.Add(((IFactory<TParam1, TParam2, TParam3, TParam4, TContract>)factory).Create((TParam1)args[0].Value, (TParam2)args[1].Value, (TParam3)args[2].Value, (TParam4)args[3].Value));
			}
		}
	}
	[NoReflectionBaking]
	public class IFactoryProvider<TParam1, TParam2, TParam3, TParam4, TParam5, TContract> : IFactoryProviderBase<TContract>
	{
		public IFactoryProvider(DiContainer container, Guid factoryId)
			: base(container, factoryId)
		{
		}

		public override void GetAllInstancesWithInjectSplit(InjectContext context, List<TypeValuePair> args, out Action injectAction, List<object> buffer)
		{
			Assert.IsEqual(args.Count, 5);
			Assert.IsNotNull(context);
			Assert.That(typeof(TContract).DerivesFromOrEqual(context.MemberType));
			Assert.That(args[0].Type.DerivesFromOrEqual<TParam1>());
			Assert.That(args[1].Type.DerivesFromOrEqual<TParam2>());
			Assert.That(args[2].Type.DerivesFromOrEqual<TParam3>());
			Assert.That(args[3].Type.DerivesFromOrEqual<TParam4>());
			Assert.That(args[4].Type.DerivesFromOrEqual<TParam5>());
			object factory = base.Container.ResolveId(typeof(IFactory<TParam1, TParam2, TParam3, TParam4, TParam5, TContract>), base.FactoryId);
			injectAction = null;
			if (base.Container.IsValidating)
			{
				buffer.Add(new ValidationMarker(typeof(TContract)));
			}
			else
			{
				buffer.Add(((IFactory<TParam1, TParam2, TParam3, TParam4, TParam5, TContract>)factory).Create((TParam1)args[0].Value, (TParam2)args[1].Value, (TParam3)args[2].Value, (TParam4)args[3].Value, (TParam5)args[4].Value));
			}
		}
	}
	[NoReflectionBaking]
	public class IFactoryProvider<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TContract> : IFactoryProviderBase<TContract>
	{
		public IFactoryProvider(DiContainer container, Guid factoryId)
			: base(container, factoryId)
		{
		}

		public override void GetAllInstancesWithInjectSplit(InjectContext context, List<TypeValuePair> args, out Action injectAction, List<object> buffer)
		{
			Assert.IsEqual(args.Count, 6);
			Assert.IsNotNull(context);
			Assert.That(typeof(TContract).DerivesFromOrEqual(context.MemberType));
			Assert.That(args[0].Type.DerivesFromOrEqual<TParam1>());
			Assert.That(args[1].Type.DerivesFromOrEqual<TParam2>());
			Assert.That(args[2].Type.DerivesFromOrEqual<TParam3>());
			Assert.That(args[3].Type.DerivesFromOrEqual<TParam4>());
			Assert.That(args[4].Type.DerivesFromOrEqual<TParam5>());
			Assert.That(args[5].Type.DerivesFromOrEqual<TParam6>());
			object factory = base.Container.ResolveId(typeof(IFactory<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TContract>), base.FactoryId);
			injectAction = null;
			if (base.Container.IsValidating)
			{
				buffer.Add(new ValidationMarker(typeof(TContract)));
			}
			else
			{
				buffer.Add(((IFactory<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TContract>)factory).Create((TParam1)args[0].Value, (TParam2)args[1].Value, (TParam3)args[2].Value, (TParam4)args[3].Value, (TParam5)args[4].Value, (TParam6)args[5].Value));
			}
		}
	}
	[NoReflectionBaking]
	public class IFactoryProvider<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TParam9, TParam10, TContract> : IFactoryProviderBase<TContract>
	{
		public IFactoryProvider(DiContainer container, Guid factoryId)
			: base(container, factoryId)
		{
		}

		public override void GetAllInstancesWithInjectSplit(InjectContext context, List<TypeValuePair> args, out Action injectAction, List<object> buffer)
		{
			Assert.IsEqual(args.Count, 10);
			Assert.IsNotNull(context);
			Assert.That(typeof(TContract).DerivesFromOrEqual(context.MemberType));
			Assert.That(args[0].Type.DerivesFromOrEqual<TParam1>());
			Assert.That(args[1].Type.DerivesFromOrEqual<TParam2>());
			Assert.That(args[2].Type.DerivesFromOrEqual<TParam3>());
			Assert.That(args[3].Type.DerivesFromOrEqual<TParam4>());
			Assert.That(args[4].Type.DerivesFromOrEqual<TParam5>());
			Assert.That(args[5].Type.DerivesFromOrEqual<TParam6>());
			Assert.That(args[6].Type.DerivesFromOrEqual<TParam7>());
			Assert.That(args[7].Type.DerivesFromOrEqual<TParam8>());
			Assert.That(args[8].Type.DerivesFromOrEqual<TParam9>());
			Assert.That(args[9].Type.DerivesFromOrEqual<TParam10>());
			object factory = base.Container.ResolveId(typeof(IFactory<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TParam9, TParam10, TContract>), base.FactoryId);
			injectAction = null;
			if (base.Container.IsValidating)
			{
				buffer.Add(new ValidationMarker(typeof(TContract)));
			}
			else
			{
				buffer.Add(((IFactory<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TParam9, TParam10, TContract>)factory).Create((TParam1)args[0].Value, (TParam2)args[1].Value, (TParam3)args[2].Value, (TParam4)args[3].Value, (TParam5)args[4].Value, (TParam6)args[5].Value, (TParam7)args[6].Value, (TParam8)args[7].Value, (TParam9)args[8].Value, (TParam10)args[9].Value));
			}
		}
	}
}
