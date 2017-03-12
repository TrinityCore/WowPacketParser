using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientGuildCriteriaUpdate
    {
        public List<GuildCriteriaProgress> Progress;
    }
}
