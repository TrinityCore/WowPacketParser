using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliBlackMarketRequestItems
    {
        public ulong NpcGUID;
        public UnixTime LastUpdateID;
    }
}
