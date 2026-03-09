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

        public static void ReadCalendarAddEventInviteInfo(Packet packet, params object[] index)
        {
            packet.ReadPackedGuid128("Guid", index);
            packet.ReadByteE<CalendarEventStatus>("Status", index);
            packet.ReadByteE<CalendarModerationRank>("Moderator", index);
        }

        public static void ReadCalendarEventInfo(Packet packet, params object[] index)
        {
            packet.ResetBitReader();
            var TitleLen = packet.ReadBits(8);
            var DescriptionLen = packet.ReadBits(11);

            packet.ReadByteE<CalendarEventType>("EventType", index);
            packet.ReadInt32("TextureID", index);
            packet.ReadTime("Time", index);
            packet.ReadInt32E<CalendarFlag>("Flags", index);

            var inviteInfoCount = packet.ReadInt32();

            packet.ReadWoWString("Title", TitleLen, index);
            packet.ReadWoWString("Description", DescriptionLen, index);

            for (int i = 0; i < inviteInfoCount; i++)
                ReadCalendarAddEventInviteInfo(packet, index, i);
        }

        [Parser(Opcode.CMSG_CALENDAR_ADD_EVENT)]
        public static void HandleUserClientCalendarAddEvent(Packet packet)
        {
            ReadCalendarEventInfo(packet);
            packet.ReadInt32("MaxSize");
        }

        [Parser(Opcode.CMSG_CALENDAR_COMPLAIN)]
        public static void HandleCalenderComplain(Packet packet)
        {
            packet.ReadPackedGuid128("InvitedByGUID");
            packet.ReadUInt64("EventID");
            packet.ReadUInt64("InviteID");
        }

        [Parser(Opcode.CMSG_CALENDAR_COPY_EVENT)]
        public static void HandleCalenderCopyEvent(Packet packet)
        {
            packet.ReadUInt64("ModeratorID");
            packet.ReadUInt64("EventID");
            packet.ReadPackedTime("Date");
        }

        [Parser(Opcode.SMSG_CALENDAR_SEND_NUM_PENDING)]
        public static void HandleSendCalendarNumPending(Packet packet)
        {
            packet.ReadUInt32("NumPending");
        }

        [Parser(Opcode.SMSG_CALENDAR_SEND_CALENDAR)]
        public static void HandleCalendarSendCalendar(Packet packet)
        {
            packet.ReadTime("ServerNow");
            packet.ReadInt32("ServerTime");
            packet.ReadTime("RaidOrigin");

            var invitesCount = packet.ReadInt32("InvitesCount");
            var eventsCount = packet.ReadInt32("EventsCount");
            var raidLockoutsCount = packet.ReadInt32("RaidLockoutsCount");
            var raidResetsCount = packet.ReadInt32("RaidResetsCount");

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
                ReadCalendarEventInviteInfo(packet, "Invites", i);

            packet.ResetBitReader();

            var lenEventName = packet.ReadBits(8);
            var lenDescription = packet.ReadBits(11);

            packet.ReadWoWString("EventName", lenEventName);
            packet.ReadWoWString("Description", lenDescription);
        }

        [Parser(Opcode.SMSG_CALENDAR_EVENT_INVITE_ALERT)]
        [Parser(Opcode.SMSG_CALENDAR_INVITE_ALERT)]
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
        [Parser(Opcode.SMSG_CALENDAR_INVITE_ADDED)]
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

        [Parser(Opcode.SMSG_CALENDAR_EVENT_INITIAL_INVITES)]
        [Parser(Opcode.SMSG_CALENDAR_COMMUNITY_INVITE)]
        public static void HandleCalendarEventInitialInvites(Packet packet)
        {
            var inviteInfoCount = packet.ReadUInt32();

            for (int i = 0; i < inviteInfoCount; i++)
            {
                packet.ReadPackedGuid128("InviteGUID", i);
                packet.ReadByte("Level", i);
            }
        }

        [Parser(Opcode.SMSG_CALENDAR_EVENT_REMOVED_ALERT)]
        public static void HandleCalendarEventRemovedAlert(Packet packet)
        {
            packet.ReadUInt64("EventID");
            packet.ReadPackedTime("Date");
            packet.ResetBitReader();
            packet.ReadBit("ClearPending");
        }

        [Parser(Opcode.SMSG_CALENDAR_EVENT_INVITE_MODERATOR_STATUS)]
        [Parser(Opcode.SMSG_CALENDAR_MODERATOR_STATUS)]
        public static void HandleCalendarEventInviteModeratorStatus(Packet packet)
        {
            packet.ReadPackedGuid128("InviteGUID");
            packet.ReadUInt64("EventID");
            packet.ReadByteE<CalendarModerationRank>("Status"); // enum NC
            packet.ReadBit("ClearPending");
        }

        [Parser(Opcode.SMSG_CALENDAR_EVENT_INVITE_NOTES)]
        [Parser(Opcode.SMSG_CALENDAR_INVITE_NOTES)]
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
        [Parser(Opcode.SMSG_CALENDAR_INVITE_NOTES_ALERT)]
        public static void HandleCalendarEventInviteNotesAlert(Packet packet)
        {
            packet.ReadUInt64("EventID");

            packet.ResetBitReader();
            var notesLength = packet.ReadBits(8);

            packet.ReadWoWString("Notes", notesLength);
        }

        [Parser(Opcode.SMSG_CALENDAR_EVENT_INVITE_REMOVED)]
        [Parser(Opcode.SMSG_CALENDAR_INVITE_REMOVED)]
        public static void HandleCalendarEventInviteRemoved(Packet packet)
        {
            packet.ReadPackedGuid128("InviteGUID");
            packet.ReadUInt64("EventID");
            packet.ReadUInt32E<CalendarFlag>("Flags");
            packet.ReadBit("ClearPending");
        }

        [Parser(Opcode.SMSG_CALENDAR_EVENT_INVITE_REMOVED_ALERT)]
        [Parser(Opcode.SMSG_CALENDAR_INVITE_REMOVED_ALERT)]
        public static void HandleCalendarEventInviteRemovedAlert(Packet packet)
        {
            packet.ReadInt64("EventID");
            packet.ReadPackedTime("Date");
            packet.ReadUInt32E<CalendarFlag>("Flags");
            packet.ReadByteE<CalendarEventStatus>("Status");
        }

        [Parser(Opcode.SMSG_CALENDAR_EVENT_INVITE_STATUS)]
        [Parser(Opcode.SMSG_CALENDAR_INVITE_STATUS)]
        public static void HandleCalendarEventInviteStatus(Packet packet)
        {
            packet.ReadPackedGuid128("InviteGUID");
            packet.ReadUInt64("EventID");
            packet.ReadPackedTime("Date");
            packet.ReadUInt32E<CalendarFlag>("Flags");
            packet.ReadByteE<CalendarEventStatus>("Status");
            packet.ReadPackedTime("ResponseTime");
            packet.ReadBit("ClearPending");
        }

        [Parser(Opcode.SMSG_CALENDAR_EVENT_INVITE_STATUS_ALERT)]
        [Parser(Opcode.SMSG_CALENDAR_INVITE_STATUS_ALERT)]
        public static void HandleCalendarEventInviteStatusAlert(Packet packet)
        {
            packet.ReadInt64("EventID");
            packet.ReadPackedTime("Date");
            packet.ReadUInt32E<CalendarFlag>("Flags");
            packet.ReadByteE<CalendarEventStatus>("Status");
        }

        [Parser(Opcode.CMSG_CALENDAR_UPDATE_EVENT)]
        public static void HandleUpdateCalendarEvent(Packet packet)
        {
            packet.ReadInt64("EventID");
            packet.ReadInt64("ModeratorID");
            packet.ReadByteE<CalendarEventType>("EventType");
            packet.ReadUInt32("TextureID");
            packet.ReadPackedTime("Time");
            packet.ReadInt32E<CalendarFlag>("Flags");

            var titleLen = packet.ReadBits(8);
            var descLen = packet.ReadBits(11);
            packet.ReadWoWString("Title", titleLen);
            packet.ReadWoWString("Description", descLen);
        }

        [Parser(Opcode.CMSG_CALENDAR_EVENT_INVITE)]
        [Parser(Opcode.CMSG_CALENDAR_INVITE)]
        public static void HandleAddCalendarEventInvite(Packet packet)
        {
            packet.ReadInt64("EventID");
            packet.ReadInt64("ModeratorID");
            var nameLen = packet.ReadBits(9);
            packet.ReadByteE<CalendarEventStatus>("Status");
            packet.ReadByteE<CalendarModerationRank>("Moderation Rank");
            packet.ReadWoWString("Name", nameLen);
        }

        [Parser(Opcode.CMSG_CALENDAR_EVENT_SIGN_UP)]
        public static void HandleCalendarEventSignup(Packet packet)
        {
            packet.ReadInt64("EventID");
            packet.ResetBitReader();
            packet.ReadBit("Tentative");
        }

        [Parser(Opcode.CMSG_CALENDAR_REMOVE_INVITE)]
        public static void HandleCalendarRemoveInvite(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");
            packet.ReadUInt64("InviteID");
            packet.ReadUInt64("ModeratorID");
            packet.ReadUInt64("EventID");
        }

        [Parser(Opcode.CMSG_CALENDAR_EVENT_STATUS)]
        [Parser(Opcode.CMSG_CALENDAR_STATUS)]
        [Parser(Opcode.CMSG_CALENDAR_EVENT_MODERATOR_STATUS)]
        [Parser(Opcode.CMSG_CALENDAR_MODERATOR_STATUS)]
        public static void HandleCalendarEventStatus(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");
            packet.ReadInt64("EventID");
            packet.ReadInt64("InviteID");
            packet.ReadInt64("ModeratorID");
            packet.ReadByteE<CalendarEventStatus>("Status");
        }

        [Parser(Opcode.SMSG_CALENDAR_EVENT_UPDATED_ALERT)]
        public static void HandleCalendarEventUpdateAlert(Packet packet)
        {
            packet.ReadUInt64("EventID");
            packet.ReadPackedTime("OriginalDate");
            packet.ReadPackedTime("Date");
            packet.ReadUInt32("LockDate");
            packet.ReadUInt32E<CalendarFlag>("Flags");
            packet.ReadUInt32("TextureID");
            packet.ReadByte("EventType");

            packet.ResetBitReader();
            var eventNameLen = packet.ReadBits(8);
            var descLen = packet.ReadBits(11);
            packet.ReadBit("ClearPending");

            packet.ReadWoWString("EventName", eventNameLen);
            packet.ReadWoWString("Description", descLen);
        }

        [Parser(Opcode.SMSG_CALENDAR_COMMAND_RESULT)]
        public static void HandleCalendarCommandResult(Packet packet)
        {
            packet.ReadByte("Command");
            packet.ReadByte("Result");
            var nameLen = packet.ReadBits(9);
            packet.ReadWoWString("Name", nameLen);
        }

        [Parser(Opcode.SMSG_CALENDAR_RAID_LOCKOUT_ADDED)]
        public static void HandleRaidLockoutAdded(Packet packet)
        {
            packet.ReadUInt64("InstanceID");
            packet.ReadPackedTime("ServerTime");
            packet.ReadInt32("MapID");
            packet.ReadUInt32("DifficultyID");
            packet.ReadInt32("TimeRemaining");
        }

        [Parser(Opcode.SMSG_CALENDAR_RAID_LOCKOUT_REMOVED)]
        public static void HandleRaidLockoutRemoved(Packet packet)
        {
            packet.ReadUInt64("InstanceID");
            packet.ReadInt32("MapID");
            packet.ReadUInt32("DifficultyID");
        }

        [Parser(Opcode.SMSG_CALENDAR_RAID_LOCKOUT_UPDATED)]
        public static void HandleCalendarRaidLockoutUpdated(Packet packet)
        {
            packet.ReadPackedTime("ServerTime");
            packet.ReadInt32("MapID");
            packet.ReadUInt32("DifficultyID");
            packet.ReadInt32("NewTimeRemaining");
            packet.ReadInt32("OldTimeRemaining");
        }
    }
}
