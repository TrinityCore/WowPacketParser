using System.Collections.Generic;

namespace WowPacketParser.Messages.Cli
{
    public unsafe struct CliSupportTicketChatLog
    {
        public List<CliSupportTicketChatLine> Lines;
        public uint? ReportLineIndex; // Optional
    }
}
