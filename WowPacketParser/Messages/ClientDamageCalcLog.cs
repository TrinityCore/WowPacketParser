using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientDamageCalcLog
    {
        public uint SourceType;
        public ulong Attacker;
        public List<ClientDamageCalcLogEntry> Entries;
        public uint SpellID;
        public ulong Defender;
    }
}
