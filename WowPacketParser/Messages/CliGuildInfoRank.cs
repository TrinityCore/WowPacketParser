using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct CliGuildInfoRank
    {
        public int RankID;
        public int RankOrder;
        public string RankName;
    }
}
