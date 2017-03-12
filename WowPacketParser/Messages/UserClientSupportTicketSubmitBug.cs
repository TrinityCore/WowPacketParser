using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientSupportTicketSubmitBug
    {
        public string Note;
        public CliSupportTicketHeader Header;
    }
}
