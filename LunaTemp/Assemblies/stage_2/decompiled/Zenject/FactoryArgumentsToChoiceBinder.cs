using System.Collections.Generic;
using System.Linq;

namespace Zenject
{
	[NoReflectionBaking]
	public class FactoryArgumentsToChoiceBinder<TContract> : FactoryToChoiceBinder<TContract>
	{
		public FactoryArgumentsToChoiceBinder(DiContainer bindContainer, BindInfo bindInfo, FactoryBindInfo factoryBindInfo)
			: base(bindContainer, bindInfo, factoryBindInfo)
		{
		}

		public FactoryToChoiceBinder<TContract> WithFactoryArguments<T>(T param)
		{
			base.FactoryBindInfo.Arguments = InjectUtil.CreateArgListExplicit(param);
			return this;
		}

		public FactoryToChoiceBinder<TContract> WithFactoryArguments<TParam1, TParam2>(TParam1 param1, TParam2 param2)
		{
			base.FactoryBindInfo.Arguments = InjectUtil.CreateArgListExplicit(param1, param2);
			return this;
		}

		public FactoryToChoiceBinder<TContract> WithFactoryArguments<TParam1, TParam2, TParam3>(TParam1 param1, TParam2 param2, TParam3 param3)
		{
			base.FactoryBindInfo.Arguments = InjectUtil.CreateArgListExplicit(param1, param2, param3);
			return this;
		}

		public FactoryToChoiceBinder<TContract> WithFactoryArguments<TParam1, TParam2, TParam3, TParam4>(TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4)
		{
			base.FactoryBindInfo.Arguments = InjectUtil.CreateArgListExplicit(param1, param2, param3, param4);
			return this;
		}

		public FactoryToChoiceBinder<TContract> WithFactoryArguments<TParam1, TParam2, TParam3, TParam4, TParam5>(TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5)
		{
			base.FactoryBindInfo.Arguments = InjectUtil.CreateArgListExplicit(param1, param2, param3, param4, param5);
			return this;
		}

		public FactoryToChoiceBinder<TContract> WithFactoryArguments<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>(TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6)
		{
			base.FactoryBindInfo.Arguments = InjectUtil.CreateArgListExplicit(param1, param2, param3, param4, param5, param6);
			return this;
		}

		public FactoryToChoiceBinder<TContract> WithFactoryArguments(object[] args)
		{
			base.FactoryBindInfo.Arguments = InjectUtil.CreateArgList(args);
			return this;
		}

		public FactoryToChoiceBinder<TContract> WithFactoryArgumentsExplicit(IEnumerable<TypeValuePair> extraArgs)
		{
			base.FactoryBindInfo.Arguments = extraArgs.ToList();
			return this;
		}
	}
	[NoReflectionBaking]
	public class FactoryArgumentsToChoiceBinder<TParam1, TContract> : FactoryToChoiceBinder<TParam1, TContract>
	{
		public FactoryArgumentsToChoiceBinder(DiContainer bindContainer, BindInfo bindInfo, FactoryBindInfo factoryBindInfo)
			: base(bindContainer, bindInfo, factoryBindInfo)
		{
		}

		public FactoryToChoiceBinder<TParam1, TContract> WithFactoryArguments<T>(T param)
		{
			base.FactoryBindInfo.Arguments = InjectUtil.CreateArgListExplicit(param);
			return this;
		}

		public FactoryToChoiceBinder<TParam1, TContract> WithFactoryArguments<TFactoryParam1, TFactoryParam2>(TFactoryParam1 param1, TFactoryParam2 param2)
		{
			base.FactoryBindInfo.Arguments = InjectUtil.CreateArgListExplicit(param1, param2);
			return this;
		}

		public FactoryToChoiceBinder<TParam1, TContract> WithFactoryArguments<TFactoryParam1, TFactoryParam2, TFactoryParam3>(TFactoryParam1 param1, TFactoryParam2 param2, TFactoryParam3 param3)
		{
			base.FactoryBindInfo.Arguments = InjectUtil.CreateArgListExplicit(param1, param2, param3);
			return this;
		}

		public FactoryToChoiceBinder<TParam1, TContract> WithFactoryArguments<TFactoryParam1, TFactoryParam2, TFactoryParam3, TFactoryParam4>(TFactoryParam1 param1, TFactoryParam2 param2, TFactoryParam3 param3, TFactoryParam4 param4)
		{
			base.FactoryBindInfo.Arguments = InjectUtil.CreateArgListExplicit(param1, param2, param3, param4);
			return this;
		}

		public FactoryToChoiceBinder<TParam1, TContract> WithFactoryArguments<TFactoryParam1, TFactoryParam2, TFactoryParam3, TFactoryParam4, TFactoryParam5>(TFactoryParam1 param1, TFactoryParam2 param2, TFactoryParam3 param3, TFactoryParam4 param4, TFactoryParam5 param5)
		{
			base.FactoryBindInfo.Arguments = InjectUtil.CreateArgListExplicit(param1, param2, param3, param4, param5);
			return this;
		}

		public FactoryToChoiceBinder<TParam1, TContract> WithFactoryArguments<TFactoryParam1, TFactoryParam2, TFactoryParam3, TFactoryParam4, TFactoryParam5, TFactoryParam6>(TFactoryParam1 param1, TFactoryParam2 param2, TFactoryParam3 param3, TFactoryParam4 param4, TFactoryParam5 param5, TFactoryParam6 param6)
		{
			base.FactoryBindInfo.Arguments = InjectUtil.CreateArgListExplicit(param1, param2, param3, param4, param5, param6);
			return this;
		}

		public FactoryToChoiceBinder<TParam1, TContract> WithFactoryArguments(object[] args)
		{
			base.FactoryBindInfo.Arguments = InjectUtil.CreateArgList(args);
			return this;
		}

		public FactoryToChoiceBinder<TParam1, TContract> WithFactoryArgumentsExplicit(IEnumerable<TypeValuePair> extraArgs)
		{
			base.FactoryBindInfo.Arguments = extraArgs.ToList();
			return this;
		}
	}
	[NoReflectionBaking]
	public class FactoryArgumentsToChoiceBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TParam9, TParam10, TContract> : FactoryToChoiceBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TParam9, TParam10, TContract>
	{
		public FactoryArgumentsToChoiceBinder(DiContainer bindContainer, BindInfo bindInfo, FactoryBindInfo factoryBindInfo)
			: base(bindContainer, bindInfo, factoryBindInfo)
		{
		}

		public FactoryToChoiceBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TParam9, TParam10, TContract> WithFactoryArguments<T>(T param)
		{
			base.FactoryBindInfo.Arguments = InjectUtil.CreateArgListExplicit(param);
			return this;
		}

		public FactoryToChoiceBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TParam9, TParam10, TContract> WithFactoryArguments<TFactoryParam1, TFactoryParam2>(TFactoryParam1 param1, TFactoryParam2 param2)
		{
			base.FactoryBindInfo.Arguments = InjectUtil.CreateArgListExplicit(param1, param2);
			return this;
		}

		public FactoryToChoiceBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TParam9, TParam10, TContract> WithFactoryArguments<TFactoryParam1, TFactoryParam2, TFactoryParam3>(TFactoryParam1 param1, TFactoryParam2 param2, TFactoryParam3 param3)
		{
			base.FactoryBindInfo.Arguments = InjectUtil.CreateArgListExplicit(param1, param2, param3);
			return this;
		}

		public FactoryToChoiceBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TParam9, TParam10, TContract> WithFactoryArguments<TFactoryParam1, TFactoryParam2, TFactoryParam3, TFactoryParam4>(TFactoryParam1 param1, TFactoryParam2 param2, TFactoryParam3 param3, TFactoryParam4 param4)
		{
			base.FactoryBindInfo.Arguments = InjectUtil.CreateArgListExplicit(param1, param2, param3, param4);
			return this;
		}

		public FactoryToChoiceBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TParam9, TParam10, TContract> WithFactoryArguments<TFactoryParam1, TFactoryParam2, TFactoryParam3, TFactoryParam4, TFactoryParam5>(TFactoryParam1 param1, TFactoryParam2 param2, TFactoryParam3 param3, TFactoryParam4 param4, TFactoryParam5 param5)
		{
			base.FactoryBindInfo.Arguments = InjectUtil.CreateArgListExplicit(param1, param2, param3, param4, param5);
			return this;
		}

		public FactoryToChoiceBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TParam9, TParam10, TContract> WithFactoryArguments<TFactoryParam1, TFactoryParam2, TFactoryParam3, TFactoryParam4, TFactoryParam5, TFactoryParam6>(TFactoryParam1 param1, TFactoryParam2 param2, TFactoryParam3 param3, TFactoryParam4 param4, TFactoryParam5 param5, TFactoryParam6 param6)
		{
			base.FactoryBindInfo.Arguments = InjectUtil.CreateArgListExplicit(param1, param2, param3, param4, param5, param6);
			return this;
		}

		public FactoryToChoiceBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TParam9, TParam10, TContract> WithFactoryArguments(object[] args)
		{
			base.FactoryBindInfo.Arguments = InjectUtil.CreateArgList(args);
			return this;
		}

		public FactoryToChoiceBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TParam9, TParam10, TContract> WithFactoryArgumentsExplicit(IEnumerable<TypeValuePair> extraArgs)
		{
			base.FactoryBindInfo.Arguments = extraArgs.ToList();
			return this;
		}
	}
	[NoReflectionBaking]
	public class FactoryArgumentsToChoiceBinder<TParam1, TParam2, TContract> : FactoryToChoiceBinder<TParam1, TParam2, TContract>
	{
		public FactoryArgumentsToChoiceBinder(DiContainer bindContainer, BindInfo bindInfo, FactoryBindInfo factoryBindInfo)
			: base(bindContainer, bindInfo, factoryBindInfo)
		{
		}

		public FactoryToChoiceBinder<TParam1, TParam2, TContract> WithFactoryArguments<T>(T param)
		{
			base.FactoryBindInfo.Arguments = InjectUtil.CreateArgListExplicit(param);
			return this;
		}

		public FactoryToChoiceBinder<TParam1, TParam2, TContract> WithFactoryArguments<TFactoryParam1, TFactoryParam2>(TFactoryParam1 param1, TFactoryParam2 param2)
		{
			base.FactoryBindInfo.Arguments = InjectUtil.CreateArgListExplicit(param1, param2);
			return this;
		}

		public FactoryToChoiceBinder<TParam1, TParam2, TContract> WithFactoryArguments<TFactoryParam1, TFactoryParam2, TFactoryParam3>(TFactoryParam1 param1, TFactoryParam2 param2, TFactoryParam3 param3)
		{
			base.FactoryBindInfo.Arguments = InjectUtil.CreateArgListExplicit(param1, param2, param3);
			return this;
		}

		public FactoryToChoiceBinder<TParam1, TParam2, TContract> WithFactoryArguments<TFactoryParam1, TFactoryParam2, TFactoryParam3, TFactoryParam4>(TFactoryParam1 param1, TFactoryParam2 param2, TFactoryParam3 param3, TFactoryParam4 param4)
		{
			base.FactoryBindInfo.Arguments = InjectUtil.CreateArgListExplicit(param1, param2, param3, param4);
			return this;
		}

		public FactoryToChoiceBinder<TParam1, TParam2, TContract> WithFactoryArguments<TFactoryParam1, TFactoryParam2, TFactoryParam3, TFactoryParam4, TFactoryParam5>(TFactoryParam1 param1, TFactoryParam2 param2, TFactoryParam3 param3, TFactoryParam4 param4, TFactoryParam5 param5)
		{
			base.FactoryBindInfo.Arguments = InjectUtil.CreateArgListExplicit(param1, param2, param3, param4, param5);
			return this;
		}

		public FactoryToChoiceBinder<TParam1, TParam2, TContract> WithFactoryArguments<TFactoryParam1, TFactoryParam2, TFactoryParam3, TFactoryParam4, TFactoryParam5, TFactoryParam6>(TFactoryParam1 param1, TFactoryParam2 param2, TFactoryParam3 param3, TFactoryParam4 param4, TFactoryParam5 param5, TFactoryParam6 param6)
		{
			base.FactoryBindInfo.Arguments = InjectUtil.CreateArgListExplicit(param1, param2, param3, param4, param5, param6);
			return this;
		}

		public FactoryToChoiceBinder<TParam1, TParam2, TContract> WithFactoryArguments(object[] args)
		{
			base.FactoryBindInfo.Arguments = InjectUtil.CreateArgList(args);
			return this;
		}

		public FactoryToChoiceBinder<TParam1, TParam2, TContract> WithFactoryArgumentsExplicit(IEnumerable<TypeValuePair> extraArgs)
		{
			base.FactoryBindInfo.Arguments = extraArgs.ToList();
			return this;
		}
	}
	[NoReflectionBaking]
	public class FactoryArgumentsToChoiceBinder<TParam1, TParam2, TParam3, TContract> : FactoryToChoiceBinder<TParam1, TParam2, TParam3, TContract>
	{
		public FactoryArgumentsToChoiceBinder(DiContainer bindContainer, BindInfo bindInfo, FactoryBindInfo factoryBindInfo)
			: base(bindContainer, bindInfo, factoryBindInfo)
		{
		}

		public FactoryToChoiceBinder<TParam1, TParam2, TParam3, TContract> WithFactoryArguments<T>(T param)
		{
			base.FactoryBindInfo.Arguments = InjectUtil.CreateArgListExplicit(param);
			return this;
		}

		public FactoryToChoiceBinder<TParam1, TParam2, TParam3, TContract> WithFactoryArguments<TFactoryParam1, TFactoryParam2>(TFactoryParam1 param1, TFactoryParam2 param2)
		{
			base.FactoryBindInfo.Arguments = InjectUtil.CreateArgListExplicit(param1, param2);
			return this;
		}

		public FactoryToChoiceBinder<TParam1, TParam2, TParam3, TContract> WithFactoryArguments<TFactoryParam1, TFactoryParam2, TFactoryParam3>(TFactoryParam1 param1, TFactoryParam2 param2, TFactoryParam3 param3)
		{
			base.FactoryBindInfo.Arguments = InjectUtil.CreateArgListExplicit(param1, param2, param3);
			return this;
		}

		public FactoryToChoiceBinder<TParam1, TParam2, TParam3, TContract> WithFactoryArguments<TFactoryParam1, TFactoryParam2, TFactoryParam3, TFactoryParam4>(TFactoryParam1 param1, TFactoryParam2 param2, TFactoryParam3 param3, TFactoryParam4 param4)
		{
			base.FactoryBindInfo.Arguments = InjectUtil.CreateArgListExplicit(param1, param2, param3, param4);
			return this;
		}

		public FactoryToChoiceBinder<TParam1, TParam2, TParam3, TContract> WithFactoryArguments<TFactoryParam1, TFactoryParam2, TFactoryParam3, TFactoryParam4, TFactoryParam5>(TFactoryParam1 param1, TFactoryParam2 param2, TFactoryParam3 param3, TFactoryParam4 param4, TFactoryParam5 param5)
		{
			base.FactoryBindInfo.Arguments = InjectUtil.CreateArgListExplicit(param1, param2, param3, param4, param5);
			return this;
		}

		public FactoryToChoiceBinder<TParam1, TParam2, TParam3, TContract> WithFactoryArguments<TFactoryParam1, TFactoryParam2, TFactoryParam3, TFactoryParam4, TFactoryParam5, TFactoryParam6>(TFactoryParam1 param1, TFactoryParam2 param2, TFactoryParam3 param3, TFactoryParam4 param4, TFactoryParam5 param5, TFactoryParam6 param6)
		{
			base.FactoryBindInfo.Arguments = InjectUtil.CreateArgListExplicit(param1, param2, param3, param4, param5, param6);
			return this;
		}

		public FactoryToChoiceBinder<TParam1, TParam2, TParam3, TContract> WithFactoryArguments(object[] args)
		{
			base.FactoryBindInfo.Arguments = InjectUtil.CreateArgList(args);
			return this;
		}

		public FactoryToChoiceBinder<TParam1, TParam2, TParam3, TContract> WithFactoryArgumentsExplicit(IEnumerable<TypeValuePair> extraArgs)
		{
			base.FactoryBindInfo.Arguments = extraArgs.ToList();
			return this;
		}
	}
	[NoReflectionBaking]
	public class FactoryArgumentsToChoiceBinder<TParam1, TParam2, TParam3, TParam4, TContract> : FactoryToChoiceBinder<TParam1, TParam2, TParam3, TParam4, TContract>
	{
		public FactoryArgumentsToChoiceBinder(DiContainer bindContainer, BindInfo bindInfo, FactoryBindInfo factoryBindInfo)
			: base(bindContainer, bindInfo, factoryBindInfo)
		{
		}

		public FactoryToChoiceBinder<TParam1, TParam2, TParam3, TParam4, TContract> WithFactoryArguments<T>(T param)
		{
			base.FactoryBindInfo.Arguments = InjectUtil.CreateArgListExplicit(param);
			return this;
		}

		public FactoryToChoiceBinder<TParam1, TParam2, TParam3, TParam4, TContract> WithFactoryArguments<TFactoryParam1, TFactoryParam2>(TFactoryParam1 param1, TFactoryParam2 param2)
		{
			base.FactoryBindInfo.Arguments = InjectUtil.CreateArgListExplicit(param1, param2);
			return this;
		}

		public FactoryToChoiceBinder<TParam1, TParam2, TParam3, TParam4, TContract> WithFactoryArguments<TFactoryParam1, TFactoryParam2, TFactoryParam3>(TFactoryParam1 param1, TFactoryParam2 param2, TFactoryParam3 param3)
		{
			base.FactoryBindInfo.Arguments = InjectUtil.CreateArgListExplicit(param1, param2, param3);
			return this;
		}

		public FactoryToChoiceBinder<TParam1, TParam2, TParam3, TParam4, TContract> WithFactoryArguments<TFactoryParam1, TFactoryParam2, TFactoryParam3, TFactoryParam4>(TFactoryParam1 param1, TFactoryParam2 param2, TFactoryParam3 param3, TFactoryParam4 param4)
		{
			base.FactoryBindInfo.Arguments = InjectUtil.CreateArgListExplicit(param1, param2, param3, param4);
			return this;
		}

		public FactoryToChoiceBinder<TParam1, TParam2, TParam3, TParam4, TContract> WithFactoryArguments<TFactoryParam1, TFactoryParam2, TFactoryParam3, TFactoryParam4, TFactoryParam5>(TFactoryParam1 param1, TFactoryParam2 param2, TFactoryParam3 param3, TFactoryParam4 param4, TFactoryParam5 param5)
		{
			base.FactoryBindInfo.Arguments = InjectUtil.CreateArgListExplicit(param1, param2, param3, param4, param5);
			return this;
		}

		public FactoryToChoiceBinder<TParam1, TParam2, TParam3, TParam4, TContract> WithFactoryArguments<TFactoryParam1, TFactoryParam2, TFactoryParam3, TFactoryParam4, TFactoryParam5, TFactoryParam6>(TFactoryParam1 param1, TFactoryParam2 param2, TFactoryParam3 param3, TFactoryParam4 param4, TFactoryParam5 param5, TFactoryParam6 param6)
		{
			base.FactoryBindInfo.Arguments = InjectUtil.CreateArgListExplicit(param1, param2, param3, param4, param5, param6);
			return this;
		}

		public FactoryToChoiceBinder<TParam1, TParam2, TParam3, TParam4, TContract> WithFactoryArguments(object[] args)
		{
			base.FactoryBindInfo.Arguments = InjectUtil.CreateArgList(args);
			return this;
		}

		public FactoryToChoiceBinder<TParam1, TParam2, TParam3, TParam4, TContract> WithFactoryArgumentsExplicit(IEnumerable<TypeValuePair> extraArgs)
		{
			base.FactoryBindInfo.Arguments = extraArgs.ToList();
			return this;
		}
	}
	[NoReflectionBaking]
	public class FactoryArgumentsToChoiceBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TContract> : FactoryToChoiceBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TContract>
	{
		public FactoryArgumentsToChoiceBinder(DiContainer bindContainer, BindInfo bindInfo, FactoryBindInfo factoryBindInfo)
			: base(bindContainer, bindInfo, factoryBindInfo)
		{
		}

		public FactoryToChoiceBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TContract> WithFactoryArguments<T>(T param)
		{
			base.FactoryBindInfo.Arguments = InjectUtil.CreateArgListExplicit(param);
			return this;
		}

		public FactoryToChoiceBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TContract> WithFactoryArguments<TFactoryParam1, TFactoryParam2>(TFactoryParam1 param1, TFactoryParam2 param2)
		{
			base.FactoryBindInfo.Arguments = InjectUtil.CreateArgListExplicit(param1, param2);
			return this;
		}

		public FactoryToChoiceBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TContract> WithFactoryArguments<TFactoryParam1, TFactoryParam2, TFactoryParam3>(TFactoryParam1 param1, TFactoryParam2 param2, TFactoryParam3 param3)
		{
			base.FactoryBindInfo.Arguments = InjectUtil.CreateArgListExplicit(param1, param2, param3);
			return this;
		}

		public FactoryToChoiceBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TContract> WithFactoryArguments<TFactoryParam1, TFactoryParam2, TFactoryParam3, TFactoryParam4>(TFactoryParam1 param1, TFactoryParam2 param2, TFactoryParam3 param3, TFactoryParam4 param4)
		{
			base.FactoryBindInfo.Arguments = InjectUtil.CreateArgListExplicit(param1, param2, param3, param4);
			return this;
		}

		public FactoryToChoiceBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TContract> WithFactoryArguments<TFactoryParam1, TFactoryParam2, TFactoryParam3, TFactoryParam4, TFactoryParam5>(TFactoryParam1 param1, TFactoryParam2 param2, TFactoryParam3 param3, TFactoryParam4 param4, TFactoryParam5 param5)
		{
			base.FactoryBindInfo.Arguments = InjectUtil.CreateArgListExplicit(param1, param2, param3, param4, param5);
			return this;
		}

		public FactoryToChoiceBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TContract> WithFactoryArguments<TFactoryParam1, TFactoryParam2, TFactoryParam3, TFactoryParam4, TFactoryParam5, TFactoryParam6>(TFactoryParam1 param1, TFactoryParam2 param2, TFactoryParam3 param3, TFactoryParam4 param4, TFactoryParam5 param5, TFactoryParam6 param6)
		{
			base.FactoryBindInfo.Arguments = InjectUtil.CreateArgListExplicit(param1, param2, param3, param4, param5, param6);
			return this;
		}

		public FactoryToChoiceBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TContract> WithFactoryArguments(object[] args)
		{
			base.FactoryBindInfo.Arguments = InjectUtil.CreateArgList(args);
			return this;
		}

		public FactoryToChoiceBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TContract> WithFactoryArgumentsExplicit(IEnumerable<TypeValuePair> extraArgs)
		{
			base.FactoryBindInfo.Arguments = extraArgs.ToList();
			return this;
		}
	}
	[NoReflectionBaking]
	public class FactoryArgumentsToChoiceBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TContract> : FactoryToChoiceBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TContract>
	{
		public FactoryArgumentsToChoiceBinder(DiContainer bindContainer, BindInfo bindInfo, FactoryBindInfo factoryBindInfo)
			: base(bindContainer, bindInfo, factoryBindInfo)
		{
		}

		public FactoryToChoiceBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TContract> WithFactoryArguments<T>(T param)
		{
			base.FactoryBindInfo.Arguments = InjectUtil.CreateArgListExplicit(param);
			return this;
		}

		public FactoryToChoiceBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TContract> WithFactoryArguments<TFactoryParam1, TFactoryParam2>(TFactoryParam1 param1, TFactoryParam2 param2)
		{
			base.FactoryBindInfo.Arguments = InjectUtil.CreateArgListExplicit(param1, param2);
			return this;
		}

		public FactoryToChoiceBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TContract> WithFactoryArguments<TFactoryParam1, TFactoryParam2, TFactoryParam3>(TFactoryParam1 param1, TFactoryParam2 param2, TFactoryParam3 param3)
		{
			base.FactoryBindInfo.Arguments = InjectUtil.CreateArgListExplicit(param1, param2, param3);
			return this;
		}

		public FactoryToChoiceBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TContract> WithFactoryArguments<TFactoryParam1, TFactoryParam2, TFactoryParam3, TFactoryParam4>(TFactoryParam1 param1, TFactoryParam2 param2, TFactoryParam3 param3, TFactoryParam4 param4)
		{
			base.FactoryBindInfo.Arguments = InjectUtil.CreateArgListExplicit(param1, param2, param3, param4);
			return this;
		}

		public FactoryToChoiceBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TContract> WithFactoryArguments<TFactoryParam1, TFactoryParam2, TFactoryParam3, TFactoryParam4, TFactoryParam5>(TFactoryParam1 param1, TFactoryParam2 param2, TFactoryParam3 param3, TFactoryParam4 param4, TFactoryParam5 param5)
		{
			base.FactoryBindInfo.Arguments = InjectUtil.CreateArgListExplicit(param1, param2, param3, param4, param5);
			return this;
		}

		public FactoryToChoiceBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TContract> WithFactoryArguments<TFactoryParam1, TFactoryParam2, TFactoryParam3, TFactoryParam4, TFactoryParam5, TFactoryParam6>(TFactoryParam1 param1, TFactoryParam2 param2, TFactoryParam3 param3, TFactoryParam4 param4, TFactoryParam5 param5, TFactoryParam6 param6)
		{
			base.FactoryBindInfo.Arguments = InjectUtil.CreateArgListExplicit(param1, param2, param3, param4, param5, param6);
			return this;
		}

		public FactoryToChoiceBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TContract> WithFactoryArguments(object[] args)
		{
			base.FactoryBindInfo.Arguments = InjectUtil.CreateArgList(args);
			return this;
		}

		public FactoryToChoiceBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TContract> WithFactoryArgumentsExplicit(IEnumerable<TypeValuePair> extraArgs)
		{
			base.FactoryBindInfo.Arguments = extraArgs.ToList();
			return this;
		}
	}
}
