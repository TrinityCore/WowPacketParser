using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V7_0_3_22248.Parsers
{
    public static class CalendarHandler
    {
        [Parser(Opcode.SMSG_CALENDAR_SEND_CALENDAR)]
        public static void HandleCalendarSendCalendar(Packet packet)
        {
            packet.ReadInt32("ServerTime");

            var invitesCount = packet.ReadInt32("InvitesCount");
            var eventsCount = packet.ReadInt32("EventsCount");
            var raidLockoutsCount = packet.ReadInt32("RaidLockoutsCount");

            for (int i = 0; i < invitesCount; i++)
                V6_0_2_19033.Parsers.CalendarHandler.ReadCalendarSendCalendarInviteInfo(packet, "Invites", i);

            for (int i = 0; i < raidLockoutsCount; i++)
                V6_0_2_19033.Parsers.CalendarHandler.ReadCalendarSendCalendarRaidLockoutInfo(packet, "RaidLockouts", i);

            for (int i = 0; i < eventsCount; i++)
                V6_0_2_19033.Parsers.CalendarHandler.ReadCalendarSendCalendarEventInfo(packet, "Events", i);
        }

        [Parser(Opcode.SMSG_CALENDAR_SEND_EVENT)]
        public static void HandleCalendarSendEvent(Packet packet)
        {
            packet.ReadByte("EventType");
            packet.ReadPackedGuid128("OwnerGUID");
            packet.ReadInt64("EventID");
            packet.ReadByte("GetEventType");
            packet.ReadInt32("TextureID");
            packet.ReadUInt32("Flags");
            packet.ReadUInt32("Date");
            packet.ReadUInt32("LockDate");
            packet.ReadPackedGuid128("EventGuildID");

            var inviteCount = packet.ReadInt32("InviteCount");

            packet.ResetBitReader();

            var lenEventName = packet.ReadBits(8);
            var lenDescription = packet.ReadBits(11);

            packet.ResetBitReader();

            for (int i = 0; i < inviteCount; i++)
                V6_0_2_19033.Parsers.CalendarHandler.ReadCalendarEventInviteInfo(packet, "Invites", i);

            packet.ReadWoWString("EventName", lenEventName);
            packet.ReadWoWString("Description", lenDescription);
        }
    }
}
