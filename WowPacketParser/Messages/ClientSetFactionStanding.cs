using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientSetFactionStanding
    {
        public List<FactionStandingData> Faction;
        public float BonusFromAchievementSystem;
        public float ReferAFriendBonus;
        public bool ShowVisual;
    }
}
