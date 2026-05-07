using System;
using System.Collections.Generic;
using System.Linq;
using ModestTree;

namespace Zenject
{
	[NoReflectionBaking]
	public class ConcreteBinderGeneric<TContract> : FromBinderGeneric<TContract>
	{
		public ConcreteBinderGeneric(DiContainer bindContainer, BindInfo bindInfo, BindStatement bindStatement)
			: base(bindContainer, bindInfo, bindStatement)
		{
			ToSelf();
		}

		public FromBinderGeneric<TContract> ToSelf()
		{
			Assert.IsEqual(base.BindInfo.ToChoice, ToChoices.Self);
			base.BindInfo.RequireExplicitScope = true;
			base.SubFinalizer = new ScopableBindingFinalizer(base.BindInfo, (DiContainer container, Type type) => new TransientProvider(type, container, base.BindInfo.Arguments, base.BindInfo.ContextInfo, base.BindInfo.ConcreteIdentifier, base.BindInfo.InstantiatedCallback));
			return this;
		}

		public FromBinderGeneric<TConcrete> To<TConcrete>() where TConcrete : TContract
		{
			base.BindInfo.ToChoice = ToChoices.Concrete;
			base.BindInfo.ToTypes.Clear();
			base.BindInfo.ToTypes.Add(typeof(TConcrete));
			return new FromBinderGeneric<TConcrete>(base.BindContainer, base.BindInfo, base.BindStatement);
		}

		public FromBinderNonGeneric To(params Type[] concreteTypes)
		{
			return To((IEnumerable<Type>)concreteTypes);
		}

		public FromBinderNonGeneric To(IEnumerable<Type> concreteTypes)
		{
			BindingUtil.AssertIsDerivedFromTypes(concreteTypes, base.BindInfo.ContractTypes, base.BindInfo.InvalidBindResponse);
			base.BindInfo.ToChoice = ToChoices.Concrete;
			base.BindInfo.ToTypes.Clear();
			base.BindInfo.ToTypes.AddRange(concreteTypes);
			return new FromBinderNonGeneric(base.BindContainer, base.BindInfo, base.BindStatement);
		}

		public FromBinderNonGeneric To(Action<ConventionSelectTypesBinder> generator)
		{
			ConventionBindInfo bindInfo = new ConventionBindInfo();
			bindInfo.AddTypeFilter((Type concreteType) => base.BindInfo.ContractTypes.All((Type contractType) => concreteType.DerivesFromOrEqual(contractType)));
			generator(new ConventionSelectTypesBinder(bindInfo));
			return To(bindInfo.ResolveTypes());
		}
	}
}
