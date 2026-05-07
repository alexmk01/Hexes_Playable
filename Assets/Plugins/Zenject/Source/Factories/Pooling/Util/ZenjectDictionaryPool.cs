using System.Collections.Generic;
using ModestTree;

namespace Zenject
{
    public class ZenjectDictionaryPool<TKey, TValue> : StaticMemoryPool<Dictionary<TKey, TValue>>
    {
        static ZenjectDictionaryPool<TKey, TValue> _instance = new ZenjectDictionaryPool<TKey, TValue>();

        public ZenjectDictionaryPool()
        {
            OnSpawnMethod = OnSpawned;
            OnDespawnedMethod = OnDespawned;
        }

        public static ZenjectDictionaryPool<TKey, TValue> Instance
        {
            get { return _instance; }
        }

        static void OnSpawned(Dictionary<TKey, TValue> items)
        {
            Assert.That(items.IsEmpty());
        }

        static void OnDespawned(Dictionary<TKey, TValue> items)
        {
            items.Clear();
        }
    }
}

