using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientSpellPeriodicAuraLog
    {
        public ulong TargetGUID;
        public List<PeriodicAuraLogEffect> Entries;
        public SpellCastLogData? LogData; // Optional
        public ulong CasterGUID;
        public int SpellID;
    }
}
