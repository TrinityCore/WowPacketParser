using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientStartBattlegroundCheat
    {
        public int MapID;
        public bool Rated;
    }
}
