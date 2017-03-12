using System.Collections.Generic;

namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct CliSupportTicketChatLog
    {
        public List<CliSupportTicketChatLine> Lines;
        public uint? ReportLineIndex; // Optional
    }
}
