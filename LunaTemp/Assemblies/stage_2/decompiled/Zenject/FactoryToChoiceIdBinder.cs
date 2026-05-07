namespace Zenject
{
	[NoReflectionBaking]
	public class FactoryToChoiceIdBinder<TContract> : FactoryArgumentsToChoiceBinder<TContract>
	{
		public FactoryToChoiceIdBinder(DiContainer container, BindInfo bindInfo, FactoryBindInfo factoryBindInfo)
			: base(container, bindInfo, factoryBindInfo)
		{
		}

		public FactoryArgumentsToChoiceBinder<TContract> WithId(object identifier)
		{
			base.BindInfo.Identifier = identifier;
			return this;
		}
	}
	[NoReflectionBaking]
	public class FactoryToChoiceIdBinder<TParam1, TContract> : FactoryArgumentsToChoiceBinder<TParam1, TContract>
	{
		public FactoryToChoiceIdBinder(DiContainer bindContainer, BindInfo bindInfo, FactoryBindInfo factoryBindInfo)
			: base(bindContainer, bindInfo, factoryBindInfo)
		{
		}

		public FactoryArgumentsToChoiceBinder<TParam1, TContract> WithId(object identifier)
		{
			base.BindInfo.Identifier = identifier;
			return this;
		}
	}
	[NoReflectionBaking]
	public class FactoryToChoiceIdBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TParam9, TParam10, TContract> : FactoryArgumentsToChoiceBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TParam9, TParam10, TContract>
	{
		public FactoryToChoiceIdBinder(DiContainer bindContainer, BindInfo bindInfo, FactoryBindInfo factoryBindInfo)
			: base(bindContainer, bindInfo, factoryBindInfo)
		{
		}

		public FactoryArgumentsToChoiceBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TParam9, TParam10, TContract> WithId(object identifier)
		{
			base.BindInfo.Identifier = identifier;
			return this;
		}
	}
	[NoReflectionBaking]
	public class FactoryToChoiceIdBinder<TParam1, TParam2, TContract> : FactoryArgumentsToChoiceBinder<TParam1, TParam2, TContract>
	{
		public FactoryToChoiceIdBinder(DiContainer bindContainer, BindInfo bindInfo, FactoryBindInfo factoryBindInfo)
			: base(bindContainer, bindInfo, factoryBindInfo)
		{
		}

		public FactoryArgumentsToChoiceBinder<TParam1, TParam2, TContract> WithId(object identifier)
		{
			base.BindInfo.Identifier = identifier;
			return this;
		}
	}
	[NoReflectionBaking]
	public class FactoryToChoiceIdBinder<TParam1, TParam2, TParam3, TContract> : FactoryArgumentsToChoiceBinder<TParam1, TParam2, TParam3, TContract>
	{
		public FactoryToChoiceIdBinder(DiContainer bindContainer, BindInfo bindInfo, FactoryBindInfo factoryBindInfo)
			: base(bindContainer, bindInfo, factoryBindInfo)
		{
		}

		public FactoryArgumentsToChoiceBinder<TParam1, TParam2, TParam3, TContract> WithId(object identifier)
		{
			base.BindInfo.Identifier = identifier;
			return this;
		}
	}
	[NoReflectionBaking]
	public class FactoryToChoiceIdBinder<TParam1, TParam2, TParam3, TParam4, TContract> : FactoryArgumentsToChoiceBinder<TParam1, TParam2, TParam3, TParam4, TContract>
	{
		public FactoryToChoiceIdBinder(DiContainer bindContainer, BindInfo bindInfo, FactoryBindInfo factoryBindInfo)
			: base(bindContainer, bindInfo, factoryBindInfo)
		{
		}

		public FactoryArgumentsToChoiceBinder<TParam1, TParam2, TParam3, TParam4, TContract> WithId(object identifier)
		{
			base.BindInfo.Identifier = identifier;
			return this;
		}
	}
	[NoReflectionBaking]
	public class FactoryToChoiceIdBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TContract> : FactoryArgumentsToChoiceBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TContract>
	{
		public FactoryToChoiceIdBinder(DiContainer bindContainer, BindInfo bindInfo, FactoryBindInfo factoryBindInfo)
			: base(bindContainer, bindInfo, factoryBindInfo)
		{
		}

		public FactoryArgumentsToChoiceBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TContract> WithId(object identifier)
		{
			base.BindInfo.Identifier = identifier;
			return this;
		}
	}
	[NoReflectionBaking]
	public class FactoryToChoiceIdBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TContract> : FactoryArgumentsToChoiceBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TContract>
	{
		public FactoryToChoiceIdBinder(DiContainer bindContainer, BindInfo bindInfo, FactoryBindInfo factoryBindInfo)
			: base(bindContainer, bindInfo, factoryBindInfo)
		{
		}

		public FactoryArgumentsToChoiceBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TContract> WithId(object identifier)
		{
			base.BindInfo.Identifier = identifier;
			return this;
		}
	}
}
