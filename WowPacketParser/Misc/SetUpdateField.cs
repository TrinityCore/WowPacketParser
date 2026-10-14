using System;
using System.Collections.Generic;

namespace WowPacketParser.Misc
{
    public class SetUpdateField<TValue> : HashSet<TValue>
    {
        public void ReadCreate(Packet packet, string name, object[] indexes,
            Func<Packet, string, object[], uint, TValue> readValue)
        {
            var size = packet.ReadUInt32();
            for (var i = 0u; i < size; ++i)
                Add(readValue(packet, name, indexes, i));
        }

        public void ReadUpdate(Packet packet, string name, object[] indexes,
            Func<Packet, string, object[], uint, TValue> readValueCreate,
            Func<Packet, string, object[], uint, TValue> readValueUpdate)
        {
            var updateType = packet.ReadByte();
            if (updateType != 0)
            {
                ReadCreate(packet, name, indexes, readValueCreate);
                return;
            }

            var changesCount = packet.ReadUInt16();
            for (var i = 0u; i < changesCount; ++i)
            {
                var value = readValueUpdate(packet, name, indexes, i);
                var changeType = packet.ReadByte("ChangeType", indexes, name, i);
                if (changeType == 2)
                    continue;

                Add(value);
            }
        }
    }
}
