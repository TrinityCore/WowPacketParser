using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientAttackStop
    {
        public ulong Attacker;
        public ulong Victim;
        public bool NowDead;
    }
}
