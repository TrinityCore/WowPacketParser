using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
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
