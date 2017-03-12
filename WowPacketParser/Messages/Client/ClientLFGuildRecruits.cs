using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientLFGuildRecruits
    {
        public List<LFGuildRecruitData> Recruits;
        public UnixTime UpdateTime;
    }
}
