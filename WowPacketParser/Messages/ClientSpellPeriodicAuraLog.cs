using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
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
