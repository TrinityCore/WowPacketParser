using WowPacketParser.Messages.Cli;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UserClientSupportTicketSubmitBug
    {
        public string Note;
        public CliSupportTicketHeader Header;
    }
}
