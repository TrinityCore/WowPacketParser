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
            packet.Translator.ReadInt32("ServerTime");

            var invitesCount = packet.Translator.ReadInt32("InvitesCount");
            var eventsCount = packet.Translator.ReadInt32("EventsCount");
            var raidLockoutsCount = packet.Translator.ReadInt32("RaidLockoutsCount");

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
            packet.Translator.ReadByte("EventType");
            packet.Translator.ReadPackedGuid128("OwnerGUID");
            packet.Translator.ReadInt64("EventID");
            packet.Translator.ReadByte("GetEventType");
            packet.Translator.ReadInt32("TextureID");
            packet.Translator.ReadUInt32("Flags");
            packet.Translator.ReadUInt32("Date");
            packet.Translator.ReadUInt32("LockDate");
            packet.Translator.ReadPackedGuid128("EventGuildID");

            var inviteCount = packet.Translator.ReadInt32("InviteCount");

            packet.Translator.ResetBitReader();

            var lenEventName = packet.Translator.ReadBits(8);
            var lenDescription = packet.Translator.ReadBits(11);

            packet.Translator.ResetBitReader();

            for (int i = 0; i < inviteCount; i++)
                V6_0_2_19033.Parsers.CalendarHandler.ReadCalendarEventInviteInfo(packet, "Invites", i);

            packet.Translator.ReadWoWString("EventName", lenEventName);
            packet.Translator.ReadWoWString("Description", lenDescription);
        }
    }
}
