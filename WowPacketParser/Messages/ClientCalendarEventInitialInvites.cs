using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientCalendarEventInitialInvites
    {
        public List<CalendarEventInitialInviteInfo> Invites;
    }
}
