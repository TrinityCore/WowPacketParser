using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientAuctionReplicateResponse
    {
        public uint ChangeNumberCursor;
        public uint ChangeNumberGlobal;
        public uint DesiredDelay;
        public uint ChangeNumberTombstone;
        public uint Result;
        public List<CliAuctionItem> Items;
    }
}
