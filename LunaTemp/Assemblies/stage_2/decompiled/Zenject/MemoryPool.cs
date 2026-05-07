namespace Zenject
{
	public class MemoryPool<TValue> : MemoryPoolBase<TValue>, IMemoryPool<TValue>, IDespawnableMemoryPool<TValue>, IMemoryPool, IFactory<TValue>, IFactory
	{
		public TValue Spawn()
		{
			TValue item = GetInternal();
			if (!base.Container.IsValidating)
			{
				Reinitialize(item);
			}
			return item;
		}

		protected virtual void Reinitialize(TValue item)
		{
		}

		TValue IFactory<TValue>.Create()
		{
			return Spawn();
		}
	}
	public class MemoryPool<TParam1, TValue> : MemoryPoolBase<TValue>, IMemoryPool<TParam1, TValue>, IDespawnableMemoryPool<TValue>, IMemoryPool, IFactory<TParam1, TValue>, IFactory
	{
		public TValue Spawn(TParam1 param)
		{
			TValue item = GetInternal();
			if (!base.Container.IsValidating)
			{
				Reinitialize(param, item);
			}
			return item;
		}

		protected virtual void Reinitialize(TParam1 p1, TValue item)
		{
		}

		TValue IFactory<TParam1, TValue>.Create(TParam1 p1)
		{
			return Spawn(p1);
		}
	}
	public class MemoryPool<TParam1, TParam2, TValue> : MemoryPoolBase<TValue>, IMemoryPool<TParam1, TParam2, TValue>, IDespawnableMemoryPool<TValue>, IMemoryPool, IFactory<TParam1, TParam2, TValue>, IFactory
	{
		public TValue Spawn(TParam1 param1, TParam2 param2)
		{
			TValue item = GetInternal();
			if (!base.Container.IsValidating)
			{
				Reinitialize(param1, param2, item);
			}
			return item;
		}

		protected virtual void Reinitialize(TParam1 p1, TParam2 p2, TValue item)
		{
		}

		TValue IFactory<TParam1, TParam2, TValue>.Create(TParam1 p1, TParam2 p2)
		{
			return Spawn(p1, p2);
		}
	}
	public class MemoryPool<TParam1, TParam2, TParam3, TValue> : MemoryPoolBase<TValue>, IMemoryPool<TParam1, TParam2, TParam3, TValue>, IDespawnableMemoryPool<TValue>, IMemoryPool, IFactory<TParam1, TParam2, TParam3, TValue>, IFactory
	{
		public TValue Spawn(TParam1 param1, TParam2 param2, TParam3 param3)
		{
			TValue item = GetInternal();
			if (!base.Container.IsValidating)
			{
				Reinitialize(param1, param2, param3, item);
			}
			return item;
		}

		protected virtual void Reinitialize(TParam1 p1, TParam2 p2, TParam3 p3, TValue item)
		{
		}

		TValue IFactory<TParam1, TParam2, TParam3, TValue>.Create(TParam1 p1, TParam2 p2, TParam3 p3)
		{
			return Spawn(p1, p2, p3);
		}
	}
	public class MemoryPool<TParam1, TParam2, TParam3, TParam4, TValue> : MemoryPoolBase<TValue>, IMemoryPool<TParam1, TParam2, TParam3, TParam4, TValue>, IDespawnableMemoryPool<TValue>, IMemoryPool, IFactory<TParam1, TParam2, TParam3, TParam4, TValue>, IFactory
	{
		public TValue Spawn(TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4)
		{
			TValue item = GetInternal();
			if (!base.Container.IsValidating)
			{
				Reinitialize(param1, param2, param3, param4, item);
			}
			return item;
		}

		protected virtual void Reinitialize(TParam1 p1, TParam2 p2, TParam3 p3, TParam4 p4, TValue item)
		{
		}

		TValue IFactory<TParam1, TParam2, TParam3, TParam4, TValue>.Create(TParam1 p1, TParam2 p2, TParam3 p3, TParam4 p4)
		{
			return Spawn(p1, p2, p3, p4);
		}
	}
	public class MemoryPool<TParam1, TParam2, TParam3, TParam4, TParam5, TValue> : MemoryPoolBase<TValue>, IMemoryPool<TParam1, TParam2, TParam3, TParam4, TParam5, TValue>, IDespawnableMemoryPool<TValue>, IMemoryPool, IFactory<TParam1, TParam2, TParam3, TParam4, TParam5, TValue>, IFactory
	{
		public TValue Spawn(TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5)
		{
			TValue item = GetInternal();
			if (!base.Container.IsValidating)
			{
				Reinitialize(param1, param2, param3, param4, param5, item);
			}
			return item;
		}

		protected virtual void Reinitialize(TParam1 p1, TParam2 p2, TParam3 p3, TParam4 p4, TParam5 p5, TValue item)
		{
		}

		TValue IFactory<TParam1, TParam2, TParam3, TParam4, TParam5, TValue>.Create(TParam1 p1, TParam2 p2, TParam3 p3, TParam4 p4, TParam5 p5)
		{
			return Spawn(p1, p2, p3, p4, p5);
		}
	}
	public class MemoryPool<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TValue> : MemoryPoolBase<TValue>, IMemoryPool<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TValue>, IDespawnableMemoryPool<TValue>, IMemoryPool, IFactory<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TValue>, IFactory
	{
		public TValue Spawn(TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6)
		{
			TValue item = GetInternal();
			if (!base.Container.IsValidating)
			{
				Reinitialize(param1, param2, param3, param4, param5, param6, item);
			}
			return item;
		}

		protected virtual void Reinitialize(TParam1 p1, TParam2 p2, TParam3 p3, TParam4 p4, TParam5 p5, TParam6 p6, TValue item)
		{
		}

		TValue IFactory<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TValue>.Create(TParam1 p1, TParam2 p2, TParam3 p3, TParam4 p4, TParam5 p5, TParam6 p6)
		{
			return Spawn(p1, p2, p3, p4, p5, p6);
		}
	}
	public class MemoryPool<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TValue> : MemoryPoolBase<TValue>, IMemoryPool<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TValue>, IDespawnableMemoryPool<TValue>, IMemoryPool, IFactory<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TValue>, IFactory
	{
		public TValue Spawn(TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7)
		{
			TValue item = GetInternal();
			if (!base.Container.IsValidating)
			{
				Reinitialize(param1, param2, param3, param4, param5, param6, param7, item);
			}
			return item;
		}

		protected virtual void Reinitialize(TParam1 p1, TParam2 p2, TParam3 p3, TParam4 p4, TParam5 p5, TParam6 p6, TParam7 p7, TValue item)
		{
		}

		TValue IFactory<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TValue>.Create(TParam1 p1, TParam2 p2, TParam3 p3, TParam4 p4, TParam5 p5, TParam6 p6, TParam7 p7)
		{
			return Spawn(p1, p2, p3, p4, p5, p6, p7);
		}
	}
	public class MemoryPool<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TValue> : MemoryPoolBase<TValue>, IMemoryPool<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TValue>, IDespawnableMemoryPool<TValue>, IMemoryPool, IFactory<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TValue>, IFactory
	{
		public TValue Spawn(TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, TParam8 param8)
		{
			TValue item = GetInternal();
			if (!base.Container.IsValidating)
			{
				Reinitialize(param1, param2, param3, param4, param5, param6, param7, param8, item);
			}
			return item;
		}

		protected virtual void Reinitialize(TParam1 p1, TParam2 p2, TParam3 p3, TParam4 p4, TParam5 p5, TParam6 p6, TParam7 p7, TParam8 p8, TValue item)
		{
		}

		TValue IFactory<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TValue>.Create(TParam1 p1, TParam2 p2, TParam3 p3, TParam4 p4, TParam5 p5, TParam6 p6, TParam7 p7, TParam8 p8)
		{
			return Spawn(p1, p2, p3, p4, p5, p6, p7, p8);
		}
	}
}
