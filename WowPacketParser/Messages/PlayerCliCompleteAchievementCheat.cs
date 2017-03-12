using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliCompleteAchievementCheat
    {
        public int AchievementID;
        public bool Complete;
    }
}
