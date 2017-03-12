using WowPacketParser.Messages.Cli;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UserClientSupportTicketSubmitSuggestion
    {
        public CliSupportTicketHeader Header;
        public string Note;
    }
}
