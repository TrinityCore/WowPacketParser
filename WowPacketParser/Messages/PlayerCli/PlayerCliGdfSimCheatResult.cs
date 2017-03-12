using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.PlayerCli
{
    public unsafe struct PlayerCliGdfSimCheatResult
    {
        public List<GdfSimPlayerResult> Players;
        public fixed int PreMMR[2];
        public fixed int PostMMR[2];
    }
}
