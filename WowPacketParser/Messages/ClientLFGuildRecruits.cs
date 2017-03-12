using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientLFGuildRecruits
    {
        public List<LFGuildRecruitData> Recruits;
        public UnixTime UpdateTime;
    }
}
