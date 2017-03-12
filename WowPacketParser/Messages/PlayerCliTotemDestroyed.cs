using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliTotemDestroyed
    {
        public ulong TotemGUID;
        public byte Slot;
    }
}
