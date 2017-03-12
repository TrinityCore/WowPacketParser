using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientChangePlayerDifficultyResult
    {
        public SetPlayerDifficultyResults Result;
        public int DifficultyRecID;
        public int MapID;
        public int InstanceDifficultyID;
        public DifficultyCooldownReason CooldownReason;
        public UnixTime Cooldown;
        public ulong Guid;
    }
}
