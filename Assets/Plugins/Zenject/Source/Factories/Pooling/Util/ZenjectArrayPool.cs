using System.Collections.Generic;

namespace Zenject
{
    public class ZenjectArrayPool<T> : StaticMemoryPoolBaseBase<T[]>
    {
        readonly int _length;

        public ZenjectArrayPool(int length)
            : base(OnDespawned)
        {
            _length = length;
        }

        static void OnDespawned(T[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = default(T);
            }
        }

        public T[] Spawn()
        {
#if ZEN_MULTITHREADING
            lock (_locker)
#endif
            {
                return SpawnInternal();
            }
        }

        protected override T[] Alloc()
        {
            return new T[_length];
        }

        static readonly Dictionary<int, ZenjectArrayPool<T>> _pools =
            new Dictionary<int, ZenjectArrayPool<T>>();

        public static ZenjectArrayPool<T> GetPool(int length)
        {
            ZenjectArrayPool<T> pool;

            if (!_pools.TryGetValue(length, out pool))
            {
                pool = new ZenjectArrayPool<T>(length);
                _pools.Add(length, pool);
            }

            return pool;
        }
    }
}
