using System;
using System.Collections.Generic;
using System.Linq;
using ModestTree;
using ModestTree.Util;

namespace Zenject
{
	public class PoolableManager
	{
		private struct PoolableInfo
		{
			public IPoolable Poolable;

			public int Priority;

			public PoolableInfo(IPoolable poolable, int priority)
			{
				Poolable = poolable;
				Priority = priority;
			}
		}

		private readonly List<IPoolable> _poolables;

		private bool _isSpawned;

		public PoolableManager([InjectLocal] List<IPoolable> poolables, [Inject(Optional = true, Source = InjectSources.Local)] List<ValuePair<Type, int>> priorities)
		{
			PoolableManager poolableManager = this;
			_poolables = (from x in poolables
				select poolableManager.CreatePoolableInfo(x, priorities) into x
				orderby x.Priority
				select x.Poolable).ToList();
		}

		private PoolableInfo CreatePoolableInfo(IPoolable poolable, List<ValuePair<Type, int>> priorities)
		{
			int? match = priorities.Where((ValuePair<Type, int> x) => poolable.GetType().DerivesFromOrEqual(x.First)).Select((Func<ValuePair<Type, int>, int?>)((ValuePair<Type, int> x) => x.Second)).SingleOrDefault();
			int priority = (match.HasValue ? match.Value : 0);
			return new PoolableInfo(poolable, priority);
		}

		public void TriggerOnSpawned()
		{
			Assert.That(!_isSpawned);
			_isSpawned = true;
			for (int i = 0; i < _poolables.Count; i++)
			{
				_poolables[i].OnSpawned();
			}
		}

		public void TriggerOnDespawned()
		{
			Assert.That(_isSpawned);
			_isSpawned = false;
			for (int i = _poolables.Count - 1; i >= 0; i--)
			{
				_poolables[i].OnDespawned();
			}
		}
	}
}
