using System;
using System.Collections.Generic;

namespace Zenject.Internal
{
	public static class ZenPools
	{
		private static readonly StaticMemoryPool<InjectContext> _contextPool = new StaticMemoryPool<InjectContext>();

		private static readonly StaticMemoryPool<LookupId> _lookupIdPool = new StaticMemoryPool<LookupId>();

		private static readonly StaticMemoryPool<BindInfo> _bindInfoPool = new StaticMemoryPool<BindInfo>();

		private static readonly StaticMemoryPool<BindStatement> _bindStatementPool = new StaticMemoryPool<BindStatement>();

		public static HashSet<T> SpawnHashSet<T>()
		{
			return ZenjectHashSetPool<T>.Instance.Spawn();
		}

		public static Dictionary<TKey, TValue> SpawnDictionary<TKey, TValue>()
		{
			return ZenjectDictionaryPool<TKey, TValue>.Instance.Spawn();
		}

		public static BindStatement SpawnStatement()
		{
			return _bindStatementPool.Spawn();
		}

		public static void DespawnStatement(BindStatement statement)
		{
			statement.Reset();
			_bindStatementPool.Despawn(statement);
		}

		public static BindInfo SpawnBindInfo()
		{
			return _bindInfoPool.Spawn();
		}

		public static void DespawnBindInfo(BindInfo bindInfo)
		{
			bindInfo.Reset();
			_bindInfoPool.Despawn(bindInfo);
		}

		public static void DespawnDictionary<TKey, TValue>(Dictionary<TKey, TValue> dictionary)
		{
			ZenjectDictionaryPool<TKey, TValue>.Instance.Despawn(dictionary);
		}

		public static void DespawnHashSet<T>(HashSet<T> set)
		{
			ZenjectHashSetPool<T>.Instance.Despawn(set);
		}

		public static LookupId SpawnLookupId(IProvider provider, BindingId bindingId)
		{
			LookupId lookupId = _lookupIdPool.Spawn();
			lookupId.Provider = provider;
			lookupId.BindingId = bindingId;
			return lookupId;
		}

		public static void DespawnLookupId(LookupId lookupId)
		{
			_lookupIdPool.Despawn(lookupId);
		}

		public static List<T> SpawnList<T>()
		{
			return ZenjectListPool<T>.Instance.Spawn();
		}

		public static void DespawnList<T>(List<T> list)
		{
			ZenjectListPool<T>.Instance.Despawn(list);
		}

		public static void DespawnArray<T>(T[] arr)
		{
			ZenjectArrayPool<T>.GetPool(arr.Length).Despawn(arr);
		}

		public static T[] SpawnArray<T>(int length)
		{
			return ZenjectArrayPool<T>.GetPool(length).Spawn();
		}

		public static InjectContext SpawnInjectContext(DiContainer container, Type memberType)
		{
			InjectContext context = _contextPool.Spawn();
			context.Container = container;
			context.MemberType = memberType;
			return context;
		}

		public static void DespawnInjectContext(InjectContext context)
		{
			context.Reset();
			_contextPool.Despawn(context);
		}

		public static InjectContext SpawnInjectContext(DiContainer container, InjectableInfo injectableInfo, InjectContext currentContext, object targetInstance, Type targetType, object concreteIdentifier)
		{
			InjectContext context = SpawnInjectContext(container, injectableInfo.MemberType);
			context.ObjectType = targetType;
			context.ParentContext = currentContext;
			context.ObjectInstance = targetInstance;
			context.Identifier = injectableInfo.Identifier;
			context.MemberName = injectableInfo.MemberName;
			context.Optional = injectableInfo.Optional;
			context.SourceType = injectableInfo.SourceType;
			context.FallBackValue = injectableInfo.DefaultValue;
			context.ConcreteIdentifier = concreteIdentifier;
			return context;
		}
	}
}
