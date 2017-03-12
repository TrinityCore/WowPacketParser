using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliGdfSimCheatResult
    {
        public List<GdfSimPlayerResult> Players;
        public fixed int PreMMR[2];
        public fixed int PostMMR[2];
    }
}
