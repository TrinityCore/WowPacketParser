using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientSpellNonMeleeDamageLog
    {
        public int Absorbed;
        public int ShieldBlock;
        public ulong Me;
        public int SpellID;
        public int Resisted;
        public bool Periodic;
        public byte SchoolMask;
        public ulong CasterGUID;
        public SpellCastLogData? LogData; // Optional
        public int Damage;
        public ClientSpellNonMeleeDamageLogDebugInfo? DebugInfo; // Optional
        public int Flags;
        public int OverKill;
    }
}
