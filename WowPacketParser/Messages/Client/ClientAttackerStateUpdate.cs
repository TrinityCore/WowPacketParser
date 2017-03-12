using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientAttackerStateUpdate
    {
        public SpellCastLogData? LogData; // Optional
        public Data AttackRoundInfo;
    }
}
