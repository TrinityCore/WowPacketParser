using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Cli
{
    public unsafe struct CliSupportTicketChatLine
    {
        public UnixTime Timestamp;
        public string Text;
    }
}
