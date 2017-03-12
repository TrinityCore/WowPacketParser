using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientGuildCriteriaDeleted
    {
        public ulong GuildGUID;
        public int CriteriaID;
    }
}
