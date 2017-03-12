using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientSetFactionStanding
    {
        public List<FactionStandingData> Faction;
        public float BonusFromAchievementSystem;
        public float ReferAFriendBonus;
        public bool ShowVisual;
    }
}
