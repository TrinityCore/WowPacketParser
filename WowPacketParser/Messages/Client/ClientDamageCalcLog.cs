using System.Collections.Generic;

namespace WowPacketParser.Messages.Client
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
