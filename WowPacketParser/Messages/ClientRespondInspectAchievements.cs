using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientRespondInspectAchievements
    {
        public ulong Player;
        public AllAchievements Data;
    }
}
