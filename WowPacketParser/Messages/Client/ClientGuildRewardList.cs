using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientGuildRewardList
    {
        public List<GuildRewardItem> RewardItems;
        public UnixTime Version;
    }
}
