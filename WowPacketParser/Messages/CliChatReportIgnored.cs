using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct CliChatReportIgnored
    {
        public ulong IgnoredGUID;
        public byte Reason;
    }
}
