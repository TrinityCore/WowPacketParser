using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientLFGuildRecruits
    {
        public List<LFGuildRecruitData> Recruits;
        public UnixTime UpdateTime;
    }
}
