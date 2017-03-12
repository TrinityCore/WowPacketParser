using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientSpellProcsPerMinuteLog
    {
        public SpellProcsPerMinuteLogData LogData;
        public ulong Guid;
        public ulong TargetGUID;
    }
}
