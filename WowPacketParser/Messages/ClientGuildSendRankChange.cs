using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientGuildSendRankChange
    {
        public ulong Other;
        public bool Promote;
        public ulong Officer;
        public uint RankID;
    }
}
