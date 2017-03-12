using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliMakeMonsterAttackGUID
    {
        public ulong Victim;
        public ulong Attacker;
    }
}
