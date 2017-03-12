using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientAchievementDeleted
    {
        public int Immunities;
        public int AchievementID;
    }
}
