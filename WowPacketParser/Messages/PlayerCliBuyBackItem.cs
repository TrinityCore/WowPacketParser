using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliBuyBackItem
    {
        public ulong VendorGUID;
        public uint Slot;
    }
}
