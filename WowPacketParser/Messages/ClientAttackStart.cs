using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientAttackStart
    {
        public ulong Attacker;
        public ulong Victim;
    }
}
