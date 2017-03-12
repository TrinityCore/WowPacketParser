using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct CliSupportTicketChatLog
    {
        public List<CliSupportTicketChatLine> Lines;
        public uint ReportLineIndex; // Optional
    }
}
