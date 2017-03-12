using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
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
