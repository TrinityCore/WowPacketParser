using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientSpellProcsPerMinuteLog
    {
        public SpellProcsPerMinuteLogData LogData;
        public ulong Guid;
        public ulong TargetGUID;
    }
}
