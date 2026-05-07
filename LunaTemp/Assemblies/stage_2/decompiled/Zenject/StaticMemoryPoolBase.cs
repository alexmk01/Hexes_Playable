using System;

namespace Zenject
{
	[NoReflectionBaking]
	public abstract class StaticMemoryPoolBase<TValue> : StaticMemoryPoolBaseBase<TValue> where TValue : class, new()
	{
		public StaticMemoryPoolBase(Action<TValue> onDespawnedMethod)
			: base(onDespawnedMethod)
		{
		}

		protected override TValue Alloc()
		{
			return new TValue();
		}
	}
}
