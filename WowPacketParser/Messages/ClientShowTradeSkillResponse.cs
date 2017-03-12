using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientShowTradeSkillResponse
    {
        public SkillLineData SkillLineData;
        public ulong PlayerGUID;
    }
}
