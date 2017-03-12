using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
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
