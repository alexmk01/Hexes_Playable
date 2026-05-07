using System.Collections.Generic;
using ModestTree;

namespace Zenject
{
	[NoReflectionBaking]
	public class ArgConditionCopyNonLazyBinder : InstantiateCallbackConditionCopyNonLazyBinder
	{
		public ArgConditionCopyNonLazyBinder(BindInfo bindInfo)
			: base(bindInfo)
		{
		}

		public InstantiateCallbackConditionCopyNonLazyBinder WithArguments<T>(T param)
		{
			base.BindInfo.Arguments.Clear();
			base.BindInfo.Arguments.Add(InjectUtil.CreateTypePair(param));
			return this;
		}

		public InstantiateCallbackConditionCopyNonLazyBinder WithArguments<TParam1, TParam2>(TParam1 param1, TParam2 param2)
		{
			base.BindInfo.Arguments.Clear();
			base.BindInfo.Arguments.Add(InjectUtil.CreateTypePair(param1));
			base.BindInfo.Arguments.Add(InjectUtil.CreateTypePair(param2));
			return this;
		}

		public InstantiateCallbackConditionCopyNonLazyBinder WithArguments<TParam1, TParam2, TParam3>(TParam1 param1, TParam2 param2, TParam3 param3)
		{
			base.BindInfo.Arguments.Clear();
			base.BindInfo.Arguments.Add(InjectUtil.CreateTypePair(param1));
			base.BindInfo.Arguments.Add(InjectUtil.CreateTypePair(param2));
			base.BindInfo.Arguments.Add(InjectUtil.CreateTypePair(param3));
			return this;
		}

		public InstantiateCallbackConditionCopyNonLazyBinder WithArguments<TParam1, TParam2, TParam3, TParam4>(TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4)
		{
			base.BindInfo.Arguments.Clear();
			base.BindInfo.Arguments.Add(InjectUtil.CreateTypePair(param1));
			base.BindInfo.Arguments.Add(InjectUtil.CreateTypePair(param2));
			base.BindInfo.Arguments.Add(InjectUtil.CreateTypePair(param3));
			base.BindInfo.Arguments.Add(InjectUtil.CreateTypePair(param4));
			return this;
		}

		public InstantiateCallbackConditionCopyNonLazyBinder WithArguments<TParam1, TParam2, TParam3, TParam4, TParam5>(TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5)
		{
			base.BindInfo.Arguments.Clear();
			base.BindInfo.Arguments.Add(InjectUtil.CreateTypePair(param1));
			base.BindInfo.Arguments.Add(InjectUtil.CreateTypePair(param2));
			base.BindInfo.Arguments.Add(InjectUtil.CreateTypePair(param3));
			base.BindInfo.Arguments.Add(InjectUtil.CreateTypePair(param4));
			base.BindInfo.Arguments.Add(InjectUtil.CreateTypePair(param5));
			return this;
		}

		public InstantiateCallbackConditionCopyNonLazyBinder WithArguments<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>(TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6)
		{
			base.BindInfo.Arguments.Clear();
			base.BindInfo.Arguments.Add(InjectUtil.CreateTypePair(param1));
			base.BindInfo.Arguments.Add(InjectUtil.CreateTypePair(param2));
			base.BindInfo.Arguments.Add(InjectUtil.CreateTypePair(param3));
			base.BindInfo.Arguments.Add(InjectUtil.CreateTypePair(param4));
			base.BindInfo.Arguments.Add(InjectUtil.CreateTypePair(param5));
			base.BindInfo.Arguments.Add(InjectUtil.CreateTypePair(param6));
			return this;
		}

		public InstantiateCallbackConditionCopyNonLazyBinder WithArguments(object[] args)
		{
			base.BindInfo.Arguments.Clear();
			foreach (object arg in args)
			{
				Assert.IsNotNull(arg, "Cannot include null values when creating a zenject argument list because zenject has no way of deducing the type from a null value.  If you want to allow null, use the Explicit form.");
				base.BindInfo.Arguments.Add(new TypeValuePair(arg.GetType(), arg));
			}
			return this;
		}

		public InstantiateCallbackConditionCopyNonLazyBinder WithArgumentsExplicit(IEnumerable<TypeValuePair> extraArgs)
		{
			base.BindInfo.Arguments.Clear();
			foreach (TypeValuePair arg in extraArgs)
			{
				base.BindInfo.Arguments.Add(arg);
			}
			return this;
		}
	}
}
