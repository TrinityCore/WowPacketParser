using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientSpellGo
    {
        public SpellCastLogData? LogData; // Optional
        public SpellCastData Cast;
    }
}
