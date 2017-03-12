using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientSpellEnergizeLog
    {
        public int SpellID;
        public ulong TargetGUID;
        public ulong CasterGUID;
        public SpellCastLogData? LogData; // Optional
        public int Amount;
        public int Type;
    }
}
