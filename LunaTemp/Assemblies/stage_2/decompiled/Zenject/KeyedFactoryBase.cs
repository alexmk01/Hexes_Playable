using System;
using System.Collections.Generic;
using System.Linq;
using ModestTree;
using ModestTree.Util;

namespace Zenject
{
	public abstract class KeyedFactoryBase<TBase, TKey> : IValidatable
	{
		[Inject]
		private readonly DiContainer _container = null;

		[InjectOptional]
		private readonly List<ValuePair<TKey, Type>> _typePairs = null;

		private Dictionary<TKey, Type> _typeMap = null;

		[InjectOptional]
		private readonly Type _fallbackType = null;

		protected DiContainer Container => _container;

		protected abstract IEnumerable<Type> ProvidedTypes { get; }

		public ICollection<TKey> Keys => _typeMap.Keys;

		protected Dictionary<TKey, Type> TypeMap => _typeMap;

		[Inject]
		public void Initialize()
		{
			Assert.That(_fallbackType == null || _fallbackType.DerivesFromOrEqual<TBase>(), "Expected fallback type '{0}' to derive from '{1}'", _fallbackType, typeof(TBase));
			_typeMap = _typePairs.ToDictionary((ValuePair<TKey, Type> x) => x.First, (ValuePair<TKey, Type> x) => x.Second);
			_typePairs.Clear();
		}

		public bool HasKey(TKey key)
		{
			return _typeMap.ContainsKey(key);
		}

		protected Type GetTypeForKey(TKey key)
		{
			if (!_typeMap.TryGetValue(key, out var keyedType))
			{
				Assert.IsNotNull(_fallbackType, "Could not find instance for key '{0}'", key);
				return _fallbackType;
			}
			return keyedType;
		}

		public virtual void Validate()
		{
			foreach (Type constructType in _typeMap.Values)
			{
				Container.InstantiateExplicit(constructType, ValidationUtil.CreateDefaultArgs(ProvidedTypes.ToArray()));
			}
		}

		protected static ConditionCopyNonLazyBinder AddBindingInternal<TDerived>(DiContainer container, TKey key) where TDerived : TBase
		{
			return container.Bind<ValuePair<TKey, Type>>().FromInstance(ValuePair.New(key, typeof(TDerived)));
		}
	}
}
