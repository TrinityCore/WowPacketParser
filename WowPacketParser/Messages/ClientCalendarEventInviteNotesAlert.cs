using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientCalendarEventInviteNotesAlert
    {
        public ulong EventID;
        public string Notes;
    }
}
