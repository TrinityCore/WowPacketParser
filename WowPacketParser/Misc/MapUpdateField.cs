using System;
using System.Collections.Generic;

namespace WowPacketParser.Misc
{
    public class MapUpdateField<TKey, TValue> : Dictionary<TKey, TValue> where TKey : notnull
    {
        public void ReadCreate(Packet packet, string name, object[] indexes,
            Func<Packet, string, object[], uint, TKey> readKey,
            Func<Packet, string, object[], uint, TValue> readValue)
        {
            var size = packet.ReadUInt32();
            for (var i = 0u; i < size; ++i)
            {
                var key = readKey(packet, name, indexes, i);
                this[key] = readValue(packet, name, indexes, i);
            }
        }

        public void ReadUpdate(Packet packet, string name, object[] indexes,
            Func<Packet, string, object[], uint, TKey> readKey,
            Func<Packet, string, object[], uint, TValue> readValueCreate,
            Func<Packet, string, object[], uint, TValue> readValueUpdate)
        {
            var updateType = packet.ReadByte();
            if (updateType != 0)
            {
                ReadCreate(packet, name, indexes, readKey, readValueCreate);
                return;
            }

            var changesCount = packet.ReadUInt16();
            for (var i = 0u; i < changesCount; ++i)
            {
                var key = readKey(packet, name, indexes, i);
                var changeType = packet.ReadByte("ChangeType", indexes, name, i);
                if (changeType == 2)
                    continue;

                this[key] = readValueUpdate(packet, name, indexes, i);
            }
        }
    }
}
