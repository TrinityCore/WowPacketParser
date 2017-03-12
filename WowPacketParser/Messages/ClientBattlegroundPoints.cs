using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientBattlegroundPoints
    {
        public ushort Points;
        public byte Team;
    }
}
