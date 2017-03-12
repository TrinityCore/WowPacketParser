using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientGuildCriteriaUpdate
    {
        public List<GuildCriteriaProgress> Progress;
    }
}
