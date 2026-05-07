using System;
using System.Collections.Generic;
using System.Linq;
using ModestTree;
using ModestTree.Util;

namespace Zenject
{
	public class InitializableManager
	{
		private class InitializableInfo
		{
			public IInitializable Initializable;

			public int Priority;

			public InitializableInfo(IInitializable initializable, int priority)
			{
				Initializable = initializable;
				Priority = priority;
			}
		}

		private List<InitializableInfo> _initializables;

		private bool _hasInitialized;

		[Inject]
		public InitializableManager([Inject(Optional = true, Source = InjectSources.Local)] List<IInitializable> initializables, [Inject(Optional = true, Source = InjectSources.Local)] List<ValuePair<Type, int>> priorities)
		{
			_initializables = new List<InitializableInfo>();
			for (int i = 0; i < initializables.Count; i++)
			{
				IInitializable initializable = initializables[i];
				List<int> matches = (from x in priorities
					where initializable.GetType().DerivesFromOrEqual(x.First)
					select x.Second).ToList();
				int priority = ((!matches.IsEmpty()) ? matches.Distinct().Single() : 0);
				_initializables.Add(new InitializableInfo(initializable, priority));
			}
		}

		public void Add(IInitializable initializable)
		{
			Add(initializable, 0);
		}

		public void Add(IInitializable initializable, int priority)
		{
			Assert.That(!_hasInitialized);
			_initializables.Add(new InitializableInfo(initializable, priority));
		}

		public void Initialize()
		{
			Assert.That(!_hasInitialized);
			_hasInitialized = true;
			_initializables = _initializables.OrderBy((InitializableInfo x) => x.Priority).ToList();
			foreach (InitializableInfo initializable in _initializables)
			{
				try
				{
					initializable.Initializable.Initialize();
				}
				catch (Exception e)
				{
					throw Assert.CreateException(e, "Error occurred while initializing IInitializable with type '{0}'", initializable.Initializable.GetType());
				}
			}
		}
	}
}
