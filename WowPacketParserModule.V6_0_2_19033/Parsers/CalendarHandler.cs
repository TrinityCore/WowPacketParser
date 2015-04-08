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

        [Parser(Opcode.CMSG_CALENDAR_ADD_EVENT)]
        public static void HandleUserClientCalendarAddEvent(Packet packet)
        {
            packet.ResetBitReader();
            var TitleLen = packet.ReadBits(8);
            var DescriptionLen = packet.ReadBits(11);

            packet.ReadByteE<CalendarEventType>("EventType");
            packet.ReadInt32("TextureID");
            packet.ReadTime("Time");
            packet.ReadInt32E<CalendarFlag>("Flags");

            var inviteInfoCount = packet.ReadInt32();

            packet.ReadWoWString("Title", TitleLen);
            packet.ReadWoWString("Description", DescriptionLen);

            for (int i = 0; i < inviteInfoCount; i++)
            {
                packet.ReadPackedGuid128("Guid");
                packet.ReadByteE<CalendarEventStatus>("Status");
                packet.ReadByteE<CalendarModerationRank>("Moderator");
            }

            packet.ReadInt32("MaxSize");
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

        [Parser(Opcode.SMSG_CALENDAR_EVENT_INVITE_ALERT)]
        public static void HandleCalendarEventInviteAlert(Packet packet)
        {
            // TODO: find unks

            packet.ReadUInt64("EventID");
            packet.ReadPackedTime("Date");
            packet.ReadInt32E<CalendarFlag>("Flags");

            packet.ReadByteE<CalendarEventType>("EventType");

            packet.ReadInt32("TextureID");

            packet.ReadPackedGuid128("EventGuildID");

            packet.ReadUInt64("InviteID");

            packet.ReadByteE<CalendarEventStatus>("Status");
            packet.ReadByteE<CalendarModerationRank>("ModeratorStatus");

            packet.ReadPackedGuid128("OwnerGUID | InvitedByGUID");
            packet.ReadPackedGuid128("OwnerGUID | InvitedByGUID");

            var eventNameLength = packet.ReadBits("EventNameLength", 8);
            packet.ResetBitReader();

            packet.ReadWoWString("EventName", eventNameLength);
        }

        [Parser(Opcode.SMSG_CALENDAR_EVENT_INVITE)]
        public static void HandleCalendarEventInvite(Packet packet)
        {
            packet.ReadPackedGuid128("InviteGUID");
            packet.ReadUInt64("EventID");
            packet.ReadUInt64("InviteID");
            packet.ReadByte("Level");
            packet.ReadByteE<CalendarEventStatus>("Status");
            packet.ReadByteE<CalendarEventType>("Type");
            packet.ReadPackedTime("ResponseTime");
            packet.ReadBit("ClearPending");
        }

        [Parser(Opcode.SMSG_CALENDAR_EVENT_INVITE_MODERATOR_STATUS)]
        public static void HandleCalendarEventInviteModeratorStatus(Packet packet)
        {
            packet.ReadPackedGuid128("InviteGUID");
            packet.ReadUInt64("EventID");
            packet.ReadByteE<CalendarModerationRank>("Status"); // enum NC
            packet.ReadBit("ClearPending");
        }

        [Parser(Opcode.SMSG_CALENDAR_EVENT_INVITE_NOTES)]
        public static void HandleCalendarEventInviteNotes(Packet packet)
        {
            packet.ReadPackedGuid128("InviteGUID");
            packet.ReadUInt64("EventID");

            var notesLength = packet.ReadBits(8);
            packet.ReadBit("ClearPending");
            packet.ResetBitReader();

            packet.ReadWoWString("Notes", notesLength);
        }

        [Parser(Opcode.SMSG_CALENDAR_EVENT_INVITE_NOTES_ALERT)]
        public static void HandleCalendarEventInviteNotesAlert(Packet packet)
        {
            packet.ReadUInt64("EventID");

            var notesLength = packet.ReadBits(8);
            packet.ResetBitReader();

            packet.ReadWoWString("Notes", notesLength);
        }

        [Parser(Opcode.SMSG_CALENDAR_EVENT_INVITE_REMOVED)]
        public static void HandleCalendarEventInviteRemoved(Packet packet)
        {
            packet.ReadPackedGuid128("InviteGUID");
            packet.ReadUInt64("EventID");
            packet.ReadUInt32E<CalendarFlag>("Flags");
            packet.ReadBit("ClearPending");
        }

        [Parser(Opcode.SMSG_CALENDAR_EVENT_INVITE_REMOVED_ALERT)]
        public static void HandleCalendarEventInviteRemovedAlert(Packet packet)
        {
            packet.ReadInt64("EventID");
            packet.ReadPackedTime("Date");
            packet.ReadUInt32E<CalendarFlag>("Flags");
            packet.ReadByteE<CalendarEventStatus>("Status");
        }

        [Parser(Opcode.SMSG_CALENDAR_EVENT_INVITE_STATUS)]
        public static void HandleCalendarEventInviteStatus(Packet packet)
        {
            packet.ReadPackedGuid128("InviteGUID");
            packet.ReadInt64("EventID");
            packet.ReadPackedTime("Date");
            packet.ReadUInt32E<CalendarFlag>("Flags");
            packet.ReadByteE<CalendarEventStatus>("Status");
            packet.ReadPackedTime("ResponseTime");
            packet.ReadBit("ClearPending");
        }

        [Parser(Opcode.SMSG_CALENDAR_EVENT_INVITE_STATUS_ALERT)]
        public static void HandleCalendarEventInviteStatusAlert(Packet packet)
        {
            packet.ReadInt64("EventID");
            packet.ReadPackedTime("Date");
            packet.ReadUInt32E<CalendarFlag>("Flags");
            packet.ReadByteE<CalendarEventStatus>("Status");
        }
    }
}
