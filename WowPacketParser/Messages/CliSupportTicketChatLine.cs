using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct CliSupportTicketChatLine
    {
        public UnixTime Timestamp;
        public string Text;
    }
}
