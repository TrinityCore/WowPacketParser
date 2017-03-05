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
            packet.Translator.ReadInt64("EventID", indexes);
            packet.Translator.ReadInt64("InviteID", indexes);

            packet.Translator.ReadByte("Status", indexes);
            packet.Translator.ReadByte("Moderator", indexes);
            packet.Translator.ReadByte("InviteType", indexes);

            packet.Translator.ReadPackedGuid128("InviterGUID", indexes);
        }

        public static void ReadCalendarSendCalendarEventInfo(Packet packet, params object[] indexes)
        {
            // sub_6073D9
            packet.Translator.ReadInt64("EventID", indexes);

            packet.Translator.ReadByte("EventType", indexes);

            packet.Translator.ReadInt32("Date", indexes);
            packet.Translator.ReadInt32("Flags", indexes);
            packet.Translator.ReadInt32("TextureID", indexes);

            packet.Translator.ReadPackedGuid128("EventGuildID", indexes);
            packet.Translator.ReadPackedGuid128("OwnerGUID", indexes);

            packet.Translator.ResetBitReader();

            var len = packet.Translator.ReadBits(8);
            packet.Translator.ReadWoWString("EventName", len, indexes);
        }

        public static void ReadCalendarSendCalendarRaidLockoutInfo(Packet packet, params object[] indexes)
        {
            // sub_5F9975
            packet.Translator.ReadInt64("InstanceID", indexes);

            packet.Translator.ReadInt32("MapID", indexes);
            packet.Translator.ReadUInt32("DifficultyID", indexes);
            packet.Translator.ReadInt32("ExpireTime", indexes);
        }

        public static void ReadCalendarSendCalendarRaidResetInfo(Packet packet, params object[] indexes)
        {
            // sub_5F9A7E
            packet.Translator.ReadInt32("MapID", indexes);
            packet.Translator.ReadInt32("Duration", indexes);
            packet.Translator.ReadInt32("Offset", indexes);
        }

        public static void ReadCalendarEventInviteInfo(Packet packet, params object[] indexes)
        {
            // sub_602F66
            packet.Translator.ReadPackedGuid128("Guid", indexes);
            packet.Translator.ReadInt64("InviteID", indexes);

            packet.Translator.ReadByte("Level", indexes);
            packet.Translator.ReadByte("Status", indexes);
            packet.Translator.ReadByte("Moderator", indexes);
            packet.Translator.ReadByte("InviteType", indexes);

            packet.Translator.ReadInt32("ResponseTime", indexes);

            packet.Translator.ResetBitReader();

            var lenNotes = packet.Translator.ReadBits(8);
            packet.Translator.ReadWoWString("Notes", lenNotes, indexes);
        }

        [Parser(Opcode.CMSG_CALENDAR_GET)]
        public static void HandleCalendarZero(Packet packet)
        {
        }

        [Parser(Opcode.CMSG_CALENDAR_ADD_EVENT)]
        public static void HandleUserClientCalendarAddEvent(Packet packet)
        {
            packet.Translator.ResetBitReader();
            var TitleLen = packet.Translator.ReadBits(8);
            var DescriptionLen = packet.Translator.ReadBits(11);

            packet.Translator.ReadByteE<CalendarEventType>("EventType");
            packet.Translator.ReadInt32("TextureID");
            packet.Translator.ReadTime("Time");
            packet.Translator.ReadInt32E<CalendarFlag>("Flags");

            var inviteInfoCount = packet.Translator.ReadInt32();

            packet.Translator.ReadWoWString("Title", TitleLen);
            packet.Translator.ReadWoWString("Description", DescriptionLen);

            for (int i = 0; i < inviteInfoCount; i++)
            {
                packet.Translator.ReadPackedGuid128("Guid");
                packet.Translator.ReadByteE<CalendarEventStatus>("Status");
                packet.Translator.ReadByteE<CalendarModerationRank>("Moderator");
            }

            packet.Translator.ReadInt32("MaxSize");
        }

        [Parser(Opcode.CMSG_CALENDAR_COMPLAIN)]
        public static void HandleCalenderComplain(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("InvitedByGUID");
            packet.Translator.ReadUInt64("EventID");
            packet.Translator.ReadUInt64("InviteID");
        }

        [Parser(Opcode.CMSG_CALENDAR_COPY_EVENT)]
        public static void HandleCalenderCopyEvent(Packet packet)
        {
            packet.Translator.ReadUInt64("ModeratorID");
            packet.Translator.ReadUInt64("EventID");
            packet.Translator.ReadPackedTime("Date");
        }

        [Parser(Opcode.SMSG_CALENDAR_SEND_NUM_PENDING)]
        public static void HandleSendCalendarNumPending(Packet packet)
        {
            packet.Translator.ReadInt32("NumPending");
        }

        [Parser(Opcode.SMSG_CALENDAR_SEND_CALENDAR)]
        public static void HandleCalendarSendCalendar(Packet packet)
        {
            packet.Translator.ReadTime("ServerNow");
            packet.Translator.ReadInt32("ServerTime");
            packet.Translator.ReadTime("RaidOrigin");

            var invitesCount = packet.Translator.ReadInt32("InvitesCount");
            var eventsCount = packet.Translator.ReadInt32("EventsCount");
            var raidLockoutsCount = packet.Translator.ReadInt32("RaidLockoutsCount");
            var raidResetsCount = packet.Translator.ReadInt32("RaidResetsCount");

            for (int i = 0; i < invitesCount; i++)
                ReadCalendarSendCalendarInviteInfo(packet, "Invites", i);

            for (int i = 0; i < eventsCount; i++)
                ReadCalendarSendCalendarEventInfo(packet, "Events", i);

            for (int i = 0; i < raidLockoutsCount; i++)
                ReadCalendarSendCalendarRaidLockoutInfo(packet, "RaidLockouts", i);

            for (int i = 0; i < raidResetsCount; i++)
                ReadCalendarSendCalendarRaidResetInfo(packet, "RaidResets", i);
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
            for (int i = 0; i < inviteCount; i++)
                ReadCalendarEventInviteInfo(packet, "Invites", i);

            packet.Translator.ResetBitReader();

            var lenEventName = packet.Translator.ReadBits(8);
            var lenDescription = packet.Translator.ReadBits(11);

            packet.Translator.ReadWoWString("EventName", lenEventName);
            packet.Translator.ReadWoWString("Description", lenDescription);
        }

        [Parser(Opcode.SMSG_CALENDAR_EVENT_INVITE_ALERT)]
        public static void HandleCalendarEventInviteAlert(Packet packet)
        {
            // TODO: find unks

            packet.Translator.ReadUInt64("EventID");
            packet.Translator.ReadPackedTime("Date");
            packet.Translator.ReadInt32E<CalendarFlag>("Flags");

            packet.Translator.ReadByteE<CalendarEventType>("EventType");

            packet.Translator.ReadInt32("TextureID");

            packet.Translator.ReadPackedGuid128("EventGuildID");

            packet.Translator.ReadUInt64("InviteID");

            packet.Translator.ReadByteE<CalendarEventStatus>("Status");
            packet.Translator.ReadByteE<CalendarModerationRank>("ModeratorStatus");

            packet.Translator.ReadPackedGuid128("OwnerGUID | InvitedByGUID");
            packet.Translator.ReadPackedGuid128("OwnerGUID | InvitedByGUID");

            var eventNameLength = packet.Translator.ReadBits("EventNameLength", 8);
            packet.Translator.ResetBitReader();

            packet.Translator.ReadWoWString("EventName", eventNameLength);
        }

        [Parser(Opcode.SMSG_CALENDAR_EVENT_INVITE)]
        public static void HandleCalendarEventInvite(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("InviteGUID");
            packet.Translator.ReadUInt64("EventID");
            packet.Translator.ReadUInt64("InviteID");
            packet.Translator.ReadByte("Level");
            packet.Translator.ReadByteE<CalendarEventStatus>("Status");
            packet.Translator.ReadByteE<CalendarEventType>("Type");
            packet.Translator.ReadPackedTime("ResponseTime");
            packet.Translator.ReadBit("ClearPending");
        }

        [Parser(Opcode.SMSG_CALENDAR_EVENT_INITIAL_INVITES)]
        public static void HandleCalendarEventInitialInvites(Packet packet)
        {
            var inviteInfoCount = packet.Translator.ReadUInt32();

            for (int i = 0; i < inviteInfoCount; i++)
            {
                packet.Translator.ReadPackedGuid128("InviteGUID", i);
                packet.Translator.ReadByte("Level", i);
            }
        }

        [Parser(Opcode.SMSG_CALENDAR_EVENT_REMOVED_ALERT)]
        public static void HandleCalendarEventRemovedAlert(Packet packet)
        {
            packet.Translator.ReadUInt64("EventID");
            packet.Translator.ReadPackedTime("Date");
            packet.Translator.ResetBitReader();
            packet.Translator.ReadBit("ClearPending");
        }

        [Parser(Opcode.SMSG_CALENDAR_EVENT_INVITE_MODERATOR_STATUS)]
        public static void HandleCalendarEventInviteModeratorStatus(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("InviteGUID");
            packet.Translator.ReadUInt64("EventID");
            packet.Translator.ReadByteE<CalendarModerationRank>("Status"); // enum NC
            packet.Translator.ReadBit("ClearPending");
        }

        [Parser(Opcode.SMSG_CALENDAR_EVENT_INVITE_NOTES)]
        public static void HandleCalendarEventInviteNotes(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("InviteGUID");
            packet.Translator.ReadUInt64("EventID");

            var notesLength = packet.Translator.ReadBits(8);
            packet.Translator.ReadBit("ClearPending");
            packet.Translator.ResetBitReader();

            packet.Translator.ReadWoWString("Notes", notesLength);
        }

        [Parser(Opcode.SMSG_CALENDAR_EVENT_INVITE_NOTES_ALERT)]
        public static void HandleCalendarEventInviteNotesAlert(Packet packet)
        {
            packet.Translator.ReadUInt64("EventID");

            var notesLength = packet.Translator.ReadBits(8);
            packet.Translator.ResetBitReader();

            packet.Translator.ReadWoWString("Notes", notesLength);
        }

        [Parser(Opcode.SMSG_CALENDAR_EVENT_INVITE_REMOVED)]
        public static void HandleCalendarEventInviteRemoved(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("InviteGUID");
            packet.Translator.ReadUInt64("EventID");
            packet.Translator.ReadUInt32E<CalendarFlag>("Flags");
            packet.Translator.ReadBit("ClearPending");
        }

        [Parser(Opcode.SMSG_CALENDAR_EVENT_INVITE_REMOVED_ALERT)]
        public static void HandleCalendarEventInviteRemovedAlert(Packet packet)
        {
            packet.Translator.ReadInt64("EventID");
            packet.Translator.ReadPackedTime("Date");
            packet.Translator.ReadUInt32E<CalendarFlag>("Flags");
            packet.Translator.ReadByteE<CalendarEventStatus>("Status");
        }

        [Parser(Opcode.SMSG_CALENDAR_EVENT_INVITE_STATUS)]
        public static void HandleCalendarEventInviteStatus(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("InviteGUID");
            packet.Translator.ReadInt64("EventID");
            packet.Translator.ReadPackedTime("Date");
            packet.Translator.ReadUInt32E<CalendarFlag>("Flags");
            packet.Translator.ReadByteE<CalendarEventStatus>("Status");
            packet.Translator.ReadPackedTime("ResponseTime");
            packet.Translator.ReadBit("ClearPending");
        }

        [Parser(Opcode.SMSG_CALENDAR_EVENT_INVITE_STATUS_ALERT)]
        public static void HandleCalendarEventInviteStatusAlert(Packet packet)
        {
            packet.Translator.ReadInt64("EventID");
            packet.Translator.ReadPackedTime("Date");
            packet.Translator.ReadUInt32E<CalendarFlag>("Flags");
            packet.Translator.ReadByteE<CalendarEventStatus>("Status");
        }
    }
}
