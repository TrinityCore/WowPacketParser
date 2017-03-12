using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientCalendarEventInitialInvites
    {
        public List<CalendarEventInitialInviteInfo> Invites;
    }
}
