using System;
using System.Collections.Generic;
using ModestTree;

namespace Zenject
{
	[NoReflectionBaking]
	public abstract class StaticMemoryPoolBaseBase<TValue> : IDespawnableMemoryPool<TValue>, IMemoryPool, IDisposable where TValue : class
	{
		private readonly Stack<TValue> _stack = new Stack<TValue>();

		private Action<TValue> _onDespawnedMethod;

		private int _activeCount;

		public Action<TValue> OnDespawnedMethod
		{
			set
			{
				_onDespawnedMethod = value;
			}
		}

		public int NumTotal => NumInactive + NumActive;

		public int NumActive => _activeCount;

		public int NumInactive => _stack.Count;

		public Type ItemType => typeof(TValue);

		public StaticMemoryPoolBaseBase(Action<TValue> onDespawnedMethod)
		{
			_onDespawnedMethod = onDespawnedMethod;
		}

		public void Resize(int desiredPoolSize)
		{
			ResizeInternal(desiredPoolSize);
		}

		private void ResizeInternal(int desiredPoolSize)
		{
			Assert.That(desiredPoolSize >= 0, "Attempted to resize the pool to a negative amount");
			while (_stack.Count > desiredPoolSize)
			{
				_stack.Pop();
			}
			while (desiredPoolSize > _stack.Count)
			{
				_stack.Push(Alloc());
			}
			Assert.IsEqual(_stack.Count, desiredPoolSize);
		}

		public void Dispose()
		{
		}

		public void ClearActiveCount()
		{
			_activeCount = 0;
		}

		public void Clear()
		{
			Resize(0);
		}

		public void ShrinkBy(int numToRemove)
		{
			ResizeInternal(_stack.Count - numToRemove);
		}

		public void ExpandBy(int numToAdd)
		{
			ResizeInternal(_stack.Count + numToAdd);
		}

		protected TValue SpawnInternal()
		{
			TValue element = ((_stack.Count != 0) ? _stack.Pop() : Alloc());
			_activeCount++;
			return element;
		}

		void IMemoryPool.Despawn(object item)
		{
			Despawn((TValue)item);
		}

		public void Despawn(TValue element)
		{
			if (_onDespawnedMethod != null)
			{
				_onDespawnedMethod(element);
			}
			Assert.That(!_stack.Contains(element), "Attempted to despawn element twice!");
			_activeCount--;
			_stack.Push(element);
		}

		protected abstract TValue Alloc();
	}
}
