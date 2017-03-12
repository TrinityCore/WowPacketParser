using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
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
