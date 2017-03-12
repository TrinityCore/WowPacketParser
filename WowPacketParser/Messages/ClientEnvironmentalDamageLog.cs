using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientEnvironmentalDamageLog
    {
        public SpellCastLogData? LogData; // Optional
        public int Absorbed;
        public ulong Victim;
        public byte Type;
        public int Resisted;
        public int Amount;
    }
}
