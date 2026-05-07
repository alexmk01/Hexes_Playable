using System;
using ModestTree;

namespace Zenject
{
	[NoReflectionBaking]
	public class FactoryToChoiceBinder<TContract> : FactoryFromBinder<TContract>
	{
		public FactoryToChoiceBinder(DiContainer container, BindInfo bindInfo, FactoryBindInfo factoryBindInfo)
			: base(container, bindInfo, factoryBindInfo)
		{
		}

		public FactoryFromBinder<TContract> ToSelf()
		{
			Assert.IsEqual(base.BindInfo.ToChoice, ToChoices.Self);
			return this;
		}

		public FactoryFromBinderUntyped To(Type concreteType)
		{
			base.BindInfo.ToChoice = ToChoices.Concrete;
			base.BindInfo.ToTypes.Clear();
			base.BindInfo.ToTypes.Add(concreteType);
			return new FactoryFromBinderUntyped(base.BindContainer, concreteType, base.BindInfo, base.FactoryBindInfo);
		}

		public FactoryFromBinder<TConcrete> To<TConcrete>() where TConcrete : TContract
		{
			base.BindInfo.ToChoice = ToChoices.Concrete;
			base.BindInfo.ToTypes.Clear();
			base.BindInfo.ToTypes.Add(typeof(TConcrete));
			return new FactoryFromBinder<TConcrete>(base.BindContainer, base.BindInfo, base.FactoryBindInfo);
		}
	}
	[NoReflectionBaking]
	public class FactoryToChoiceBinder<TParam1, TContract> : FactoryFromBinder<TParam1, TContract>
	{
		public FactoryToChoiceBinder(DiContainer bindContainer, BindInfo bindInfo, FactoryBindInfo factoryBindInfo)
			: base(bindContainer, bindInfo, factoryBindInfo)
		{
		}

		public FactoryFromBinder<TParam1, TContract> ToSelf()
		{
			Assert.IsEqual(base.BindInfo.ToChoice, ToChoices.Self);
			return this;
		}

		public FactoryFromBinder<TParam1, TConcrete> To<TConcrete>() where TConcrete : TContract
		{
			base.BindInfo.ToChoice = ToChoices.Concrete;
			base.BindInfo.ToTypes.Clear();
			base.BindInfo.ToTypes.Add(typeof(TConcrete));
			return new FactoryFromBinder<TParam1, TConcrete>(base.BindContainer, base.BindInfo, base.FactoryBindInfo);
		}
	}
	[NoReflectionBaking]
	public class FactoryToChoiceBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TParam9, TParam10, TContract> : FactoryFromBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TParam9, TParam10, TContract>
	{
		public FactoryToChoiceBinder(DiContainer bindContainer, BindInfo bindInfo, FactoryBindInfo factoryBindInfo)
			: base(bindContainer, bindInfo, factoryBindInfo)
		{
		}

		public FactoryFromBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TParam9, TParam10, TContract> ToSelf()
		{
			Assert.IsEqual(base.BindInfo.ToChoice, ToChoices.Self);
			return this;
		}

		public FactoryFromBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TParam9, TParam10, TConcrete> To<TConcrete>() where TConcrete : TContract
		{
			base.BindInfo.ToChoice = ToChoices.Concrete;
			base.BindInfo.ToTypes.Clear();
			base.BindInfo.ToTypes.Add(typeof(TConcrete));
			return new FactoryFromBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TParam9, TParam10, TConcrete>(base.BindContainer, base.BindInfo, base.FactoryBindInfo);
		}
	}
	[NoReflectionBaking]
	public class FactoryToChoiceBinder<TParam1, TParam2, TContract> : FactoryFromBinder<TParam1, TParam2, TContract>
	{
		public FactoryToChoiceBinder(DiContainer bindContainer, BindInfo bindInfo, FactoryBindInfo factoryBindInfo)
			: base(bindContainer, bindInfo, factoryBindInfo)
		{
		}

		public FactoryFromBinder<TParam1, TParam2, TContract> ToSelf()
		{
			Assert.IsEqual(base.BindInfo.ToChoice, ToChoices.Self);
			return this;
		}

		public FactoryFromBinder<TParam1, TParam2, TConcrete> To<TConcrete>() where TConcrete : TContract
		{
			base.BindInfo.ToChoice = ToChoices.Concrete;
			base.BindInfo.ToTypes.Clear();
			base.BindInfo.ToTypes.Add(typeof(TConcrete));
			return new FactoryFromBinder<TParam1, TParam2, TConcrete>(base.BindContainer, base.BindInfo, base.FactoryBindInfo);
		}
	}
	[NoReflectionBaking]
	public class FactoryToChoiceBinder<TParam1, TParam2, TParam3, TContract> : FactoryFromBinder<TParam1, TParam2, TParam3, TContract>
	{
		public FactoryToChoiceBinder(DiContainer bindContainer, BindInfo bindInfo, FactoryBindInfo factoryBindInfo)
			: base(bindContainer, bindInfo, factoryBindInfo)
		{
		}

		public FactoryFromBinder<TParam1, TParam2, TParam3, TContract> ToSelf()
		{
			Assert.IsEqual(base.BindInfo.ToChoice, ToChoices.Self);
			return this;
		}

		public FactoryFromBinder<TParam1, TParam2, TParam3, TConcrete> To<TConcrete>() where TConcrete : TContract
		{
			base.BindInfo.ToChoice = ToChoices.Concrete;
			base.BindInfo.ToTypes.Clear();
			base.BindInfo.ToTypes.Add(typeof(TConcrete));
			return new FactoryFromBinder<TParam1, TParam2, TParam3, TConcrete>(base.BindContainer, base.BindInfo, base.FactoryBindInfo);
		}
	}
	[NoReflectionBaking]
	public class FactoryToChoiceBinder<TParam1, TParam2, TParam3, TParam4, TContract> : FactoryFromBinder<TParam1, TParam2, TParam3, TParam4, TContract>
	{
		public FactoryToChoiceBinder(DiContainer bindContainer, BindInfo bindInfo, FactoryBindInfo factoryBindInfo)
			: base(bindContainer, bindInfo, factoryBindInfo)
		{
		}

		public FactoryFromBinder<TParam1, TParam2, TParam3, TParam4, TContract> ToSelf()
		{
			Assert.IsEqual(base.BindInfo.ToChoice, ToChoices.Self);
			return this;
		}

		public FactoryFromBinder<TParam1, TParam2, TParam3, TParam4, TConcrete> To<TConcrete>() where TConcrete : TContract
		{
			base.BindInfo.ToChoice = ToChoices.Concrete;
			base.BindInfo.ToTypes.Clear();
			base.BindInfo.ToTypes.Add(typeof(TConcrete));
			return new FactoryFromBinder<TParam1, TParam2, TParam3, TParam4, TConcrete>(base.BindContainer, base.BindInfo, base.FactoryBindInfo);
		}
	}
	[NoReflectionBaking]
	public class FactoryToChoiceBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TContract> : FactoryFromBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TContract>
	{
		public FactoryToChoiceBinder(DiContainer bindContainer, BindInfo bindInfo, FactoryBindInfo factoryBindInfo)
			: base(bindContainer, bindInfo, factoryBindInfo)
		{
		}

		public FactoryFromBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TContract> ToSelf()
		{
			Assert.IsEqual(base.BindInfo.ToChoice, ToChoices.Self);
			return this;
		}

		public FactoryFromBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TConcrete> To<TConcrete>() where TConcrete : TContract
		{
			base.BindInfo.ToChoice = ToChoices.Concrete;
			base.BindInfo.ToTypes.Clear();
			base.BindInfo.ToTypes.Add(typeof(TConcrete));
			return new FactoryFromBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TConcrete>(base.BindContainer, base.BindInfo, base.FactoryBindInfo);
		}
	}
	[NoReflectionBaking]
	public class FactoryToChoiceBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TContract> : FactoryFromBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TContract>
	{
		public FactoryToChoiceBinder(DiContainer bindContainer, BindInfo bindInfo, FactoryBindInfo factoryBindInfo)
			: base(bindContainer, bindInfo, factoryBindInfo)
		{
		}

		public FactoryFromBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TContract> ToSelf()
		{
			Assert.IsEqual(base.BindInfo.ToChoice, ToChoices.Self);
			return this;
		}

		public FactoryFromBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TConcrete> To<TConcrete>() where TConcrete : TContract
		{
			base.BindInfo.ToChoice = ToChoices.Concrete;
			base.BindInfo.ToTypes.Clear();
			base.BindInfo.ToTypes.Add(typeof(TConcrete));
			return new FactoryFromBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TConcrete>(base.BindContainer, base.BindInfo, base.FactoryBindInfo);
		}
	}
}
