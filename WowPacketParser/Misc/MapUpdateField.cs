using System.Collections.Generic;

namespace WowPacketParser.Misc
{
    public class MapUpdateField<TKey, TValue> where TKey : notnull
    {
        private readonly Dictionary<TKey, TValue> _values = new();

        public TValue this[TKey index]
        {
            get => _values[index];
            set => _values[index] = value;
        }
    }
}
