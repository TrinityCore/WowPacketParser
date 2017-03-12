using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientAuraCastLog
    {
        public ulong Victim;
        public ulong Attacker;
        public float ApplyRoll;
        public int SpellID;
        public float ApplyRollNeeded;
    }
}
