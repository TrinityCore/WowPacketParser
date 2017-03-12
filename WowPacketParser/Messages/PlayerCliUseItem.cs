using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliUseItem
    {
        public byte PackSlot;
        public SpellCastRequest Cast;
        public byte Slot;
        public ulong CastItem;
    }
}
