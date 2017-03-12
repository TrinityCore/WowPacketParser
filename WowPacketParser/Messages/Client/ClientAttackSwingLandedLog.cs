using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientAttackSwingLandedLog
    {
        public SpellCastLogData LogData;
        public Data AttackRoundInfo;
    }
}
