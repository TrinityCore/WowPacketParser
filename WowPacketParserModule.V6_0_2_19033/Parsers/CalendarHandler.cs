using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class CalendarHandler
    {
        public static void ReadCalendarSendCalendarInviteInfo(Packet packet, params object[] indexes)
        {
            // sub_5F9CFB
            packet.ReadInt64("EventID", indexes);
            packet.ReadInt64("InviteID", indexes);

            packet.ReadByte("Status", indexes);
            packet.ReadByte("Moderator", indexes);
            packet.ReadByte("InviteType", indexes);

            packet.ReadPackedGuid128("InviterGUID", indexes);
        }

        public static void ReadCalendarSendCalendarEventInfo(Packet packet, params object[] indexes)
        {
            // sub_6073D9
            packet.ReadInt64("EventID", indexes);

            packet.ReadByte("EventType", indexes);

            packet.ReadInt32("Date", indexes);
            packet.ReadInt32("Flags", indexes);
            packet.ReadInt32("TextureID", indexes);

            packet.ReadPackedGuid128("EventGuildID", indexes);
            packet.ReadPackedGuid128("OwnerGUID", indexes);

            packet.ResetBitReader();

            var len = packet.ReadBits(8);
            packet.ReadWoWString("EventName", len, indexes);
        }

        public static void ReadCalendarSendCalendarRaidLockoutInfo(Packet packet, params object[] indexes)
        {
            // sub_5F9975
            packet.ReadInt64("InstanceID", indexes);

            packet.ReadInt32("MapID", indexes);
            packet.ReadUInt32("DifficultyID", indexes);
            packet.ReadInt32("ExpireTime", indexes);
        }

        public static void ReadCalendarSendCalendarRaidResetInfo(Packet packet, params object[] indexes)
        {
            // sub_5F9A7E
            packet.ReadInt32("MapID", indexes);
            packet.ReadInt32("Duration", indexes);
            packet.ReadInt32("Offset", indexes);
        }

        public static void ReadCalendarEventInviteInfo(Packet packet, params object[] indexes)
        {
            // sub_602F66
            packet.ReadPackedGuid128("Guid", indexes);
            packet.ReadInt64("InviteID", indexes);

            packet.ReadByte("Level", indexes);
            packet.ReadByte("Status", indexes);
            packet.ReadByte("Moderator", indexes);
            packet.ReadByte("InviteType", indexes);

            packet.ReadInt32("ResponseTime", indexes);

            packet.ResetBitReader();

            var lenNotes = packet.ReadBits(8);
            packet.ReadWoWString("Notes", lenNotes, indexes);
        }

        [Parser(Opcode.CMSG_CALENDAR_GET)]
        public static void HandleCalendarZero(Packet packet)
        {
        }

        [Parser(Opcode.SMSG_CALENDAR_SEND_NUM_PENDING)]
        public static void HandleSendCalendarNumPending(Packet packet)
        {
            packet.ReadInt32("NumPending");
        }

        [Parser(Opcode.SMSG_CALENDAR_SEND_CALENDAR)]
        public static void HandleCalendarSendCalendar(Packet packet)
        {
            packet.ReadTime("ServerNow");
            packet.ReadInt32("ServerTime");
            packet.ReadTime("RaidOrigin");

            var int52 = packet.ReadInt32("CalendarSendCalendarInviteInfoCount");
            var int68 = packet.ReadInt32("CalendarSendCalendarEventInfoCount");
            var int36 = packet.ReadInt32("CalendarSendCalendarRaidLockoutInfoCount");
            var int16 = packet.ReadInt32("CalendarSendCalendarRaidResetInfoCount");

            for (int i = 0; i < int52; i++)
                ReadCalendarSendCalendarInviteInfo(packet, i, "CalendarSendCalendarInviteInfo");

            for (int i = 0; i < int68; i++)
                ReadCalendarSendCalendarEventInfo(packet, i, "CalendarSendCalendarEventInfo");

            for (int i = 0; i < int36; i++)
                ReadCalendarSendCalendarRaidLockoutInfo(packet, i, "CalendarSendCalendarRaidLockoutInfo");

            for (int i = 0; i < int16; i++)
                ReadCalendarSendCalendarRaidResetInfo(packet, i, "CalendarSendCalendarRaidResetInfo");
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
            for (int i = 0; i < inviteCount; i++)
                ReadCalendarEventInviteInfo(packet, i, "CalendarEventInviteInfo");

            packet.ResetBitReader();

            var lenEventName = packet.ReadBits(8);
            var lenDescription = packet.ReadBits(11);

            packet.ReadWoWString("EventName", lenEventName);
            packet.ReadWoWString("Description", lenDescription);
        }
    }
}
