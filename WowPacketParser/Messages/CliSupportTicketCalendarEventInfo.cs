using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct CliSupportTicketCalendarEventInfo
    {
        public ulong EventID;
        public ulong InviteID;
        public string EventTitle;
    }
}
