using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientPartyKillLog
    {
        public ulong Victim;
        public ulong Player;
    }
}
