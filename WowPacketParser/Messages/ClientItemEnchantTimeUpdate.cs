using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientItemEnchantTimeUpdate
    {
        public ulong OwnerGuid;
        public ulong ItemGuid;
        public uint DurationLeft;
        public int Slot;
    }
}
