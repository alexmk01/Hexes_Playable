using System.Collections.Generic;
using ModestTree;

namespace Zenject
{
    public class ZenjectHashSetPool<T> : StaticMemoryPool<HashSet<T>>
    {
        static ZenjectHashSetPool<T> _instance = new ZenjectHashSetPool<T>();

        public ZenjectHashSetPool()
        {
            OnSpawnMethod = OnSpawned;
            OnDespawnedMethod = OnDespawned;
        }

        public static ZenjectHashSetPool<T> Instance
        {
            get { return _instance; }
        }
        
        static void OnSpawned(HashSet<T> items)
        {
            Assert.That(items.IsEmpty());
        }

        static void OnDespawned(HashSet<T> items)
        {
            items.Clear();
        }
    }
}
