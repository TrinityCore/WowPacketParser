using System.Collections.Generic;

namespace WowPacketParser.Misc
{
    public class MapUpdateField<TKey, TValue> : Dictionary<TKey, TValue> where TKey : notnull
    {
    }
}
