using System;
using System.Collections.Generic;
using System.Linq;
using ModestTree;
using ModestTree.Util;

namespace Zenject
{
	public class DisposableManager : IDisposable
	{
		private struct DisposableInfo
		{
			public IDisposable Disposable;

			public int Priority;

			public DisposableInfo(IDisposable disposable, int priority)
			{
				Disposable = disposable;
				Priority = priority;
			}
		}

		private class LateDisposableInfo
		{
			public ILateDisposable LateDisposable;

			public int Priority;

			public LateDisposableInfo(ILateDisposable lateDisposable, int priority)
			{
				LateDisposable = lateDisposable;
				Priority = priority;
			}
		}

		private readonly List<DisposableInfo> _disposables = new List<DisposableInfo>();

		private readonly List<LateDisposableInfo> _lateDisposables = new List<LateDisposableInfo>();

		private bool _disposed;

		private bool _lateDisposed;

		[Inject]
		public DisposableManager([Inject(Optional = true, Source = InjectSources.Local)] List<IDisposable> disposables, [Inject(Optional = true, Source = InjectSources.Local)] List<ValuePair<Type, int>> priorities, [Inject(Optional = true, Source = InjectSources.Local)] List<ILateDisposable> lateDisposables, [Inject(Id = "Late", Optional = true, Source = InjectSources.Local)] List<ValuePair<Type, int>> latePriorities)
		{
			foreach (IDisposable disposable in disposables)
			{
				int? match2 = priorities.Where((ValuePair<Type, int> x) => disposable.GetType().DerivesFromOrEqual(x.First)).Select((Func<ValuePair<Type, int>, int?>)((ValuePair<Type, int> x) => x.Second)).SingleOrDefault();
				int priority2 = (match2.HasValue ? match2.Value : 0);
				_disposables.Add(new DisposableInfo(disposable, priority2));
			}
			foreach (ILateDisposable lateDisposable in lateDisposables)
			{
				int? match = latePriorities.Where((ValuePair<Type, int> x) => lateDisposable.GetType().DerivesFromOrEqual(x.First)).Select((Func<ValuePair<Type, int>, int?>)((ValuePair<Type, int> x) => x.Second)).SingleOrDefault();
				int priority = (match.HasValue ? match.Value : 0);
				_lateDisposables.Add(new LateDisposableInfo(lateDisposable, priority));
			}
		}

		public void Add(IDisposable disposable)
		{
			Add(disposable, 0);
		}

		public void Add(IDisposable disposable, int priority)
		{
			_disposables.Add(new DisposableInfo(disposable, priority));
		}

		public void AddLate(ILateDisposable disposable)
		{
			AddLate(disposable, 0);
		}

		public void AddLate(ILateDisposable disposable, int priority)
		{
			_lateDisposables.Add(new LateDisposableInfo(disposable, priority));
		}

		public void Remove(IDisposable disposable)
		{
			_disposables.RemoveWithConfirm(_disposables.Where((DisposableInfo x) => x.Disposable == disposable).Single());
		}

		public void LateDispose()
		{
			Assert.That(!_lateDisposed, "Tried to late dispose DisposableManager twice!");
			_lateDisposed = true;
			List<LateDisposableInfo> disposablesOrdered = _lateDisposables.OrderBy((LateDisposableInfo x) => x.Priority).Reverse().ToList();
			foreach (LateDisposableInfo disposable in disposablesOrdered)
			{
				try
				{
					disposable.LateDisposable.LateDispose();
				}
				catch (Exception e)
				{
					throw Assert.CreateException(e, "Error occurred while late disposing ILateDisposable with type '{0}'", disposable.LateDisposable.GetType());
				}
			}
		}

		public void Dispose()
		{
			Assert.That(!_disposed, "Tried to dispose DisposableManager twice!");
			_disposed = true;
			List<DisposableInfo> disposablesOrdered = _disposables.OrderBy((DisposableInfo x) => x.Priority).Reverse().ToList();
			foreach (DisposableInfo disposable in disposablesOrdered)
			{
				try
				{
					disposable.Disposable.Dispose();
				}
				catch (Exception e)
				{
					throw Assert.CreateException(e, "Error occurred while disposing IDisposable with type '{0}'", disposable.Disposable.GetType());
				}
			}
		}
	}
}
