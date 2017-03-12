using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliGarrisonMissionBonusRoll
    {
        public ulong NpcGUID;
        public int MissionRecID;
        public int BonusIndex;
    }
}
