using System;
using System.Collections.Generic;

namespace Zenject
{
	public class KeyedFactory<TBase, TKey> : KeyedFactoryBase<TBase, TKey>
	{
		protected override IEnumerable<Type> ProvidedTypes => new Type[0];

		public virtual TBase Create(TKey key)
		{
			Type type = GetTypeForKey(key);
			return (TBase)base.Container.Instantiate(type);
		}
	}
	public class KeyedFactory<TBase, TKey, TParam1> : KeyedFactoryBase<TBase, TKey>
	{
		protected override IEnumerable<Type> ProvidedTypes => new Type[1] { typeof(TParam1) };

		public virtual TBase Create(TKey key, TParam1 param1)
		{
			return (TBase)base.Container.InstantiateExplicit(GetTypeForKey(key), new List<TypeValuePair> { InjectUtil.CreateTypePair(param1) });
		}
	}
	public class KeyedFactory<TBase, TKey, TParam1, TParam2> : KeyedFactoryBase<TBase, TKey>
	{
		protected override IEnumerable<Type> ProvidedTypes => new Type[2]
		{
			typeof(TParam1),
			typeof(TParam2)
		};

		public virtual TBase Create(TKey key, TParam1 param1, TParam2 param2)
		{
			return (TBase)base.Container.InstantiateExplicit(GetTypeForKey(key), new List<TypeValuePair>
			{
				InjectUtil.CreateTypePair(param1),
				InjectUtil.CreateTypePair(param2)
			});
		}
	}
	public class KeyedFactory<TBase, TKey, TParam1, TParam2, TParam3> : KeyedFactoryBase<TBase, TKey>
	{
		protected override IEnumerable<Type> ProvidedTypes => new Type[3]
		{
			typeof(TParam1),
			typeof(TParam2),
			typeof(TParam3)
		};

		public virtual TBase Create(TKey key, TParam1 param1, TParam2 param2, TParam3 param3)
		{
			return (TBase)base.Container.InstantiateExplicit(GetTypeForKey(key), new List<TypeValuePair>
			{
				InjectUtil.CreateTypePair(param1),
				InjectUtil.CreateTypePair(param2),
				InjectUtil.CreateTypePair(param3)
			});
		}
	}
	public class KeyedFactory<TBase, TKey, TParam1, TParam2, TParam3, TParam4> : KeyedFactoryBase<TBase, TKey>
	{
		protected override IEnumerable<Type> ProvidedTypes => new Type[4]
		{
			typeof(TParam1),
			typeof(TParam2),
			typeof(TParam3),
			typeof(TParam4)
		};

		public virtual TBase Create(TKey key, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4)
		{
			return (TBase)base.Container.InstantiateExplicit(GetTypeForKey(key), new List<TypeValuePair>
			{
				InjectUtil.CreateTypePair(param1),
				InjectUtil.CreateTypePair(param2),
				InjectUtil.CreateTypePair(param3),
				InjectUtil.CreateTypePair(param4)
			});
		}
	}
}
