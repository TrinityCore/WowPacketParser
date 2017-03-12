using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientSupportTicketSubmitSuggestion
    {
        public CliSupportTicketHeader Header;
        public string Note;
    }
}
