using WowPacketParser.Messages.Cli;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UserClientSupportTicketSubmitSuggestion
    {
        public CliSupportTicketHeader Header;
        public string Note;
    }
}
