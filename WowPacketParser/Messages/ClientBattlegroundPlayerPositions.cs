using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientBattlegroundPlayerPositions
    {
        public List<BattlegroundPlayerPosition> FlagCarriers;
    }
}
