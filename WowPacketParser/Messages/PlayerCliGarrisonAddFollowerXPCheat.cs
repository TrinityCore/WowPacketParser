using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliGarrisonAddFollowerXPCheat
    {
        public ulong FollowerDBID;
        public int XpAmount;
    }
}
