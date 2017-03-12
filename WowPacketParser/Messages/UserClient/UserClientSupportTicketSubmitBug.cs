using WowPacketParser.Messages.Cli;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UserClientSupportTicketSubmitBug
    {
        public string Note;
        public CliSupportTicketHeader Header;
    }
}
