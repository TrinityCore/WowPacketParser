using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliGarrisonSetFollowerItemLevelCheat
    {
        public ulong FollowerDBID;
        public int ItemLevelArmor;
        public int ItemLevelWeapon;
    }
}
