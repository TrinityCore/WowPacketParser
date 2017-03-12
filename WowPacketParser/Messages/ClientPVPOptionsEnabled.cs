using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientPVPOptionsEnabled
    {
        public bool WargameArenas;
        public bool RatedArenas;
        public bool WargameBattlegrounds;
        public bool ArenaSkirmish;
        public bool PugBattlegrounds;
        public bool RatedBattlegrounds;
    }
}
