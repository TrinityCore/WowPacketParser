using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliItemUpgradeCheat
    {
        public int ItemID;
        public int ItemUpgradeID;
    }
}
