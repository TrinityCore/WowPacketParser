using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliAuctionReplicateItems
    {
        public ulong Auctioneer;
        public uint ChangeNumberCursor;
        public uint Count;
        public uint ChangeNumberGlobal;
        public uint ChangeNumberTombstone;
    }
}
