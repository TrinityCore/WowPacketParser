using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientCalendarSendCalendar
    {
        public List<ClientCalendarSendCalendarEventInfo> Events;
        public List<ClientCalendarSendCalendarRaidResetInfo> RaidResets;
        public List<ClientCalendarSendCalendarRaidLockoutInfo> RaidLockouts;
        public List<ClientCalendarSendCalendarHolidayInfo> Holidays;
        public UnixTime RaidOrigin;
        public List<ClientCalendarSendCalendarInviteInfo> Invites;
        public uint ServerTime;
        public UnixTime ServerNow;
    }
}
