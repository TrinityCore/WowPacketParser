using System.Collections.Concurrent;
using System.Collections.Generic;

namespace WowPacketParser.Misc
{
    public class ConcurrentMultiDictionary<TKey, TValue> : ConcurrentDictionary<TKey, List<TValue>>
    {
        public bool TryAdd(TKey key, TValue value)
        {
            List<TValue> container;
            if (!TryGetValue(key, out container))
            {
                container = new List<TValue>();
                if (!TryAdd(key, container))
                    return false;
            }

            container.Add(value);
            return true;
        }

        public bool ContainsValue(TKey key, TValue value)
        {
            List<TValue> container;
            return TryGetValue(key, out container) && container.Contains(value);
        }

        public List<TValue> GetValues(TKey key)
        {
            List<TValue> container;
            return TryGetValue(key, out container) ? container : new List<TValue>();
        }
    }
}