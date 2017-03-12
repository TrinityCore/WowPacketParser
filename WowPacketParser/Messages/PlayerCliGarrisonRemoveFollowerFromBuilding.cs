using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliGarrisonRemoveFollowerFromBuilding
    {
        public ulong NpcGUID;
        public ulong FollowerDBID;
    }
}
