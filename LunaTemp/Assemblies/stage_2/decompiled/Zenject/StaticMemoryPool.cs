using System;
using ModestTree;
using ModestTree.Util;

namespace Zenject
{
	[NoReflectionBaking]
	public class StaticMemoryPool<TValue> : StaticMemoryPoolBase<TValue>, IMemoryPool<TValue>, IDespawnableMemoryPool<TValue>, IMemoryPool where TValue : class, new()
	{
		private Action<TValue> _onSpawnMethod;

		public Action<TValue> OnSpawnMethod
		{
			set
			{
				_onSpawnMethod = value;
			}
		}

		public StaticMemoryPool(Action<TValue> onSpawnMethod = null, Action<TValue> onDespawnedMethod = null)
			: base(onDespawnedMethod)
		{
			_onSpawnMethod = onSpawnMethod;
		}

		public TValue Spawn()
		{
			TValue item = SpawnInternal();
			if (_onSpawnMethod != null)
			{
				_onSpawnMethod(item);
			}
			return item;
		}
	}
	[NoReflectionBaking]
	public class StaticMemoryPool<TParam1, TValue> : StaticMemoryPoolBase<TValue>, IMemoryPool<TParam1, TValue>, IDespawnableMemoryPool<TValue>, IMemoryPool where TValue : class, new()
	{
		private Action<TParam1, TValue> _onSpawnMethod;

		public Action<TParam1, TValue> OnSpawnMethod
		{
			set
			{
				_onSpawnMethod = value;
			}
		}

		public StaticMemoryPool(Action<TParam1, TValue> onSpawnMethod, Action<TValue> onDespawnedMethod = null)
			: base(onDespawnedMethod)
		{
			Assert.IsNotNull(onSpawnMethod);
			_onSpawnMethod = onSpawnMethod;
		}

		public TValue Spawn(TParam1 param)
		{
			TValue item = SpawnInternal();
			if (_onSpawnMethod != null)
			{
				_onSpawnMethod(param, item);
			}
			return item;
		}
	}
	[NoReflectionBaking]
	public class StaticMemoryPool<TParam1, TParam2, TValue> : StaticMemoryPoolBase<TValue>, IMemoryPool<TParam1, TParam2, TValue>, IDespawnableMemoryPool<TValue>, IMemoryPool where TValue : class, new()
	{
		private Action<TParam1, TParam2, TValue> _onSpawnMethod;

		public Action<TParam1, TParam2, TValue> OnSpawnMethod
		{
			set
			{
				_onSpawnMethod = value;
			}
		}

		public StaticMemoryPool(Action<TParam1, TParam2, TValue> onSpawnMethod, Action<TValue> onDespawnedMethod = null)
			: base(onDespawnedMethod)
		{
			Assert.IsNotNull(onSpawnMethod);
			_onSpawnMethod = onSpawnMethod;
		}

		public TValue Spawn(TParam1 p1, TParam2 p2)
		{
			TValue item = SpawnInternal();
			if (_onSpawnMethod != null)
			{
				_onSpawnMethod(p1, p2, item);
			}
			return item;
		}
	}
	[NoReflectionBaking]
	public class StaticMemoryPool<TParam1, TParam2, TParam3, TValue> : StaticMemoryPoolBase<TValue>, IMemoryPool<TParam1, TParam2, TParam3, TValue>, IDespawnableMemoryPool<TValue>, IMemoryPool where TValue : class, new()
	{
		private Action<TParam1, TParam2, TParam3, TValue> _onSpawnMethod;

		public Action<TParam1, TParam2, TParam3, TValue> OnSpawnMethod
		{
			set
			{
				_onSpawnMethod = value;
			}
		}

		public StaticMemoryPool(Action<TParam1, TParam2, TParam3, TValue> onSpawnMethod, Action<TValue> onDespawnedMethod = null)
			: base(onDespawnedMethod)
		{
			Assert.IsNotNull(onSpawnMethod);
			_onSpawnMethod = onSpawnMethod;
		}

		public TValue Spawn(TParam1 p1, TParam2 p2, TParam3 p3)
		{
			TValue item = SpawnInternal();
			if (_onSpawnMethod != null)
			{
				_onSpawnMethod(p1, p2, p3, item);
			}
			return item;
		}
	}
	[NoReflectionBaking]
	public class StaticMemoryPool<TParam1, TParam2, TParam3, TParam4, TValue> : StaticMemoryPoolBase<TValue>, IMemoryPool<TParam1, TParam2, TParam3, TParam4, TValue>, IDespawnableMemoryPool<TValue>, IMemoryPool where TValue : class, new()
	{
		private ModestTree.Util.Action<TParam1, TParam2, TParam3, TParam4, TValue> _onSpawnMethod;

		public ModestTree.Util.Action<TParam1, TParam2, TParam3, TParam4, TValue> OnSpawnMethod
		{
			set
			{
				_onSpawnMethod = value;
			}
		}

		public StaticMemoryPool(ModestTree.Util.Action<TParam1, TParam2, TParam3, TParam4, TValue> onSpawnMethod, Action<TValue> onDespawnedMethod = null)
			: base(onDespawnedMethod)
		{
			Assert.IsNotNull(onSpawnMethod);
			_onSpawnMethod = onSpawnMethod;
		}

		public TValue Spawn(TParam1 p1, TParam2 p2, TParam3 p3, TParam4 p4)
		{
			TValue item = SpawnInternal();
			if (_onSpawnMethod != null)
			{
				_onSpawnMethod(p1, p2, p3, p4, item);
			}
			return item;
		}
	}
	[NoReflectionBaking]
	public class StaticMemoryPool<TParam1, TParam2, TParam3, TParam4, TParam5, TValue> : StaticMemoryPoolBase<TValue>, IMemoryPool<TParam1, TParam2, TParam3, TParam4, TParam5, TValue>, IDespawnableMemoryPool<TValue>, IMemoryPool where TValue : class, new()
	{
		private ModestTree.Util.Action<TParam1, TParam2, TParam3, TParam4, TParam5, TValue> _onSpawnMethod;

		public ModestTree.Util.Action<TParam1, TParam2, TParam3, TParam4, TParam5, TValue> OnSpawnMethod
		{
			set
			{
				_onSpawnMethod = value;
			}
		}

		public StaticMemoryPool(ModestTree.Util.Action<TParam1, TParam2, TParam3, TParam4, TParam5, TValue> onSpawnMethod, Action<TValue> onDespawnedMethod = null)
			: base(onDespawnedMethod)
		{
			Assert.IsNotNull(onSpawnMethod);
			_onSpawnMethod = onSpawnMethod;
		}

		public TValue Spawn(TParam1 p1, TParam2 p2, TParam3 p3, TParam4 p4, TParam5 p5)
		{
			TValue item = SpawnInternal();
			if (_onSpawnMethod != null)
			{
				_onSpawnMethod(p1, p2, p3, p4, p5, item);
			}
			return item;
		}
	}
	[NoReflectionBaking]
	public class StaticMemoryPool<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TValue> : StaticMemoryPoolBase<TValue>, IMemoryPool<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TValue>, IDespawnableMemoryPool<TValue>, IMemoryPool where TValue : class, new()
	{
		private ModestTree.Util.Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TValue> _onSpawnMethod;

		public ModestTree.Util.Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TValue> OnSpawnMethod
		{
			set
			{
				_onSpawnMethod = value;
			}
		}

		public StaticMemoryPool(ModestTree.Util.Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TValue> onSpawnMethod, Action<TValue> onDespawnedMethod = null)
			: base(onDespawnedMethod)
		{
			Assert.IsNotNull(onSpawnMethod);
			_onSpawnMethod = onSpawnMethod;
		}

		public TValue Spawn(TParam1 p1, TParam2 p2, TParam3 p3, TParam4 p4, TParam5 p5, TParam6 p6)
		{
			TValue item = SpawnInternal();
			if (_onSpawnMethod != null)
			{
				_onSpawnMethod(p1, p2, p3, p4, p5, p6, item);
			}
			return item;
		}
	}
	[NoReflectionBaking]
	public class StaticMemoryPool<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TValue> : StaticMemoryPoolBase<TValue>, IMemoryPool<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TValue>, IDespawnableMemoryPool<TValue>, IMemoryPool where TValue : class, new()
	{
		private ModestTree.Util.Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TValue> _onSpawnMethod;

		public ModestTree.Util.Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TValue> OnSpawnMethod
		{
			set
			{
				_onSpawnMethod = value;
			}
		}

		public StaticMemoryPool(ModestTree.Util.Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TValue> onSpawnMethod, Action<TValue> onDespawnedMethod = null)
			: base(onDespawnedMethod)
		{
			Assert.IsNotNull(onSpawnMethod);
			_onSpawnMethod = onSpawnMethod;
		}

		public TValue Spawn(TParam1 p1, TParam2 p2, TParam3 p3, TParam4 p4, TParam5 p5, TParam6 p6, TParam7 p7)
		{
			TValue item = SpawnInternal();
			if (_onSpawnMethod != null)
			{
				_onSpawnMethod(p1, p2, p3, p4, p5, p6, p7, item);
			}
			return item;
		}
	}
}
