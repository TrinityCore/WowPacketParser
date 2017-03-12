using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientSetItemChallengeModeData
    {
        public ulong ItemGUID;
        public fixed int Stats[6];
    }
}
