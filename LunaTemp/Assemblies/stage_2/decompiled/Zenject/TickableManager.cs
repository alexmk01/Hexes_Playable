using System;
using System.Collections.Generic;
using System.Linq;
using ModestTree;
using ModestTree.Util;

namespace Zenject
{
	public class TickableManager
	{
		[Inject(Optional = true, Source = InjectSources.Local)]
		private readonly List<ITickable> _tickables = null;

		[Inject(Optional = true, Source = InjectSources.Local)]
		private readonly List<IFixedTickable> _fixedTickables = null;

		[Inject(Optional = true, Source = InjectSources.Local)]
		private readonly List<ILateTickable> _lateTickables = null;

		[Inject(Optional = true, Source = InjectSources.Local)]
		private readonly List<ValuePair<Type, int>> _priorities = null;

		[Inject(Optional = true, Id = "Fixed", Source = InjectSources.Local)]
		private readonly List<ValuePair<Type, int>> _fixedPriorities = null;

		[Inject(Optional = true, Id = "Late", Source = InjectSources.Local)]
		private readonly List<ValuePair<Type, int>> _latePriorities = null;

		private readonly TickablesTaskUpdater _updater = new TickablesTaskUpdater();

		private readonly FixedTickablesTaskUpdater _fixedUpdater = new FixedTickablesTaskUpdater();

		private readonly LateTickablesTaskUpdater _lateUpdater = new LateTickablesTaskUpdater();

		private bool _isPaused;

		public IEnumerable<ITickable> Tickables => _tickables;

		public bool IsPaused
		{
			get
			{
				return _isPaused;
			}
			set
			{
				_isPaused = value;
			}
		}

		[Inject]
		public TickableManager()
		{
		}

		[Inject]
		public void Initialize()
		{
			InitTickables();
			InitFixedTickables();
			InitLateTickables();
		}

		private void InitFixedTickables()
		{
			foreach (Type type in _fixedPriorities.Select((ValuePair<Type, int> x) => x.First))
			{
				Assert.That(type.DerivesFrom<IFixedTickable>(), "Expected type '{0}' to drive from IFixedTickable while checking priorities in TickableHandler", type);
			}
			foreach (IFixedTickable tickable in _fixedTickables)
			{
				List<int> matches = (from x in _fixedPriorities
					where tickable.GetType().DerivesFromOrEqual(x.First)
					select x.Second).ToList();
				int priority = ((!matches.IsEmpty()) ? matches.Distinct().Single() : 0);
				_fixedUpdater.AddTask(tickable, priority);
			}
		}

		private void InitTickables()
		{
			foreach (Type type in _priorities.Select((ValuePair<Type, int> x) => x.First))
			{
				Assert.That(type.DerivesFrom<ITickable>(), "Expected type '{0}' to drive from ITickable while checking priorities in TickableHandler", type);
			}
			foreach (ITickable tickable in _tickables)
			{
				List<int> matches = (from x in _priorities
					where tickable.GetType().DerivesFromOrEqual(x.First)
					select x.Second).ToList();
				int priority = ((!matches.IsEmpty()) ? matches.Distinct().Single() : 0);
				_updater.AddTask(tickable, priority);
			}
		}

		private void InitLateTickables()
		{
			foreach (Type type in _latePriorities.Select((ValuePair<Type, int> x) => x.First))
			{
				Assert.That(type.DerivesFrom<ILateTickable>(), "Expected type '{0}' to drive from ILateTickable while checking priorities in TickableHandler", type);
			}
			foreach (ILateTickable tickable in _lateTickables)
			{
				List<int> matches = (from x in _latePriorities
					where tickable.GetType().DerivesFromOrEqual(x.First)
					select x.Second).ToList();
				int priority = ((!matches.IsEmpty()) ? matches.Distinct().Single() : 0);
				_lateUpdater.AddTask(tickable, priority);
			}
		}

		public void Add(ITickable tickable, int priority)
		{
			_updater.AddTask(tickable, priority);
		}

		public void Add(ITickable tickable)
		{
			Add(tickable, 0);
		}

		public void AddLate(ILateTickable tickable, int priority)
		{
			_lateUpdater.AddTask(tickable, priority);
		}

		public void AddLate(ILateTickable tickable)
		{
			AddLate(tickable, 0);
		}

		public void AddFixed(IFixedTickable tickable, int priority)
		{
			_fixedUpdater.AddTask(tickable, priority);
		}

		public void AddFixed(IFixedTickable tickable)
		{
			_fixedUpdater.AddTask(tickable, 0);
		}

		public void Remove(ITickable tickable)
		{
			_updater.RemoveTask(tickable);
		}

		public void RemoveLate(ILateTickable tickable)
		{
			_lateUpdater.RemoveTask(tickable);
		}

		public void RemoveFixed(IFixedTickable tickable)
		{
			_fixedUpdater.RemoveTask(tickable);
		}

		public void Update()
		{
			if (!IsPaused)
			{
				_updater.OnFrameStart();
				_updater.UpdateAll();
			}
		}

		public void FixedUpdate()
		{
			if (!IsPaused)
			{
				_fixedUpdater.OnFrameStart();
				_fixedUpdater.UpdateAll();
			}
		}

		public void LateUpdate()
		{
			if (!IsPaused)
			{
				_lateUpdater.OnFrameStart();
				_lateUpdater.UpdateAll();
			}
		}
	}
}
