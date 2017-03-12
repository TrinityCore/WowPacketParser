using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientLfgPartyInfo
    {
        public List<LFGBlackList> Player;
    }
}
