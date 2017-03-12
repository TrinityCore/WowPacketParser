using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientAuctionCommandResult
    {
        public ulong Guid;
        public ulong MinIncrement;
        public ulong Money;
        public int ErrorCode;
        public int AuctionItemID;
        public int BagResult;
        public int Command;
    }
}
