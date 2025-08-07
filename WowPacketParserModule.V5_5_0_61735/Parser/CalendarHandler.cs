using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_5_0_61735.Parsers
{
    public static class CalendarHandler
    {
        public static void ReadCalendarSendCalendarInviteInfo(Packet packet, params object[] indexes)
        {
            packet.ReadInt64("EventID", indexes);
            packet.ReadInt64("InviteID", indexes);

            packet.ReadByte("Status", indexes);
            packet.ReadByte("Moderator", indexes);
            packet.ReadByte("InviteType", indexes);

            packet.ReadPackedGuid128("InviterGUID", indexes);

            packet.ResetBitReader();
            packet.ReadBit("IgnoreFriendAndGuildRestriction");
        }

        public static void ReadCalendarSendCalendarEventInfo(Packet packet, params object[] indexes)
        {
            packet.ReadInt64("EventID", indexes);
            packet.ReadByte("EventType", indexes);
            packet.ReadInt32("Date", indexes);
            packet.ReadUInt16E<CalendarFlag>("Flags");
            packet.ReadInt32("TextureID", indexes);
            packet.ReadUInt64("EventClubID");
            packet.ReadPackedGuid128("OwnerGUID", indexes);

            packet.ResetBitReader();

            var len = packet.ReadBits(8);
            packet.ReadWoWString("EventName", len, indexes);
        }

        public static void ReadCalendarSendCalendarRaidLockoutInfo(Packet packet, params object[] indexes)
        {
            packet.ReadInt64("InstanceID", indexes);

            packet.ReadInt32("MapID", indexes);
            packet.ReadUInt32("DifficultyID", indexes);
            packet.ReadInt32("ExpireTime", indexes);
        }

        public static void ReadCalendarSendCalendarRaidResetInfo(Packet packet, params object[] indexes)
        {
            packet.ReadInt32<MapId>("MapID", indexes);
            packet.ReadUInt32("Unk340_1", indexes);
            packet.ReadInt32("IntervalSeconds", indexes);
            packet.ReadInt32("OffsetSeconds", indexes);
            packet.ReadInt32<DifficultyId>("Difficulty", indexes);
        }

        public static void ReadCalendarEventInviteInfo(Packet packet, params object[] indexes)
        {
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

        [Parser(Opcode.SMSG_CALENDAR_SEND_CALENDAR)]
        public static void HandleCalendarSendCalendar(Packet packet)
        {
            packet.ReadPackedTime("ServerTime");

            var invitesCount = packet.ReadUInt32("InvitesCount");
            var eventsCount = packet.ReadUInt32("EventsCount");
            var raidLockoutsCount = packet.ReadUInt32("RaidLockoutsCount");
            var raidResetsCount = packet.ReadUInt32("RaidResetsCount");

            for (int i = 0; i < raidLockoutsCount; i++)
                ReadCalendarSendCalendarRaidLockoutInfo(packet, "RaidLockouts", i);

            for (int i = 0; i < raidResetsCount; i++)
                ReadCalendarSendCalendarRaidResetInfo(packet, "RaidResets", i);

            for (int i = 0; i < invitesCount; i++)
                ReadCalendarSendCalendarInviteInfo(packet, "Invites", i);

            for (int i = 0; i < eventsCount; i++)
                ReadCalendarSendCalendarEventInfo(packet, "Events", i);
        }

        [Parser(Opcode.SMSG_CALENDAR_SEND_EVENT)]
        public static void HandleCalendarSendEvent(Packet packet)
        {
            packet.ReadByte("EventType");
            packet.ReadPackedGuid128("OwnerGUID");
            packet.ReadUInt64("EventID");
            packet.ReadByte("GetEventType");
            packet.ReadInt32("TextureID");
            packet.ReadUInt16E<CalendarFlag>("Flags");
            packet.ReadPackedTime("Date");
            packet.ReadUInt32("LockDate");
            packet.ReadUInt64("EventClubID");

            var inviteCount = packet.ReadInt32("InviteCount");

            packet.ResetBitReader();

            var lenEventName = packet.ReadBits(8);
            var lenDescription = packet.ReadBits(11);

            packet.ResetBitReader();

            for (int i = 0; i < inviteCount; i++)
                ReadCalendarEventInviteInfo(packet, "Invites", i);

            packet.ReadWoWString("EventName", lenEventName);
            packet.ReadWoWString("Description", lenDescription);
        }

        [Parser(Opcode.SMSG_CALENDAR_COMMUNITY_INVITE)]
        public static void HandleCalendarCommunityInvite(Packet packet)
        {
            var inviteInfoCount = packet.ReadUInt32();

            for (int i = 0; i < inviteInfoCount; i++)
            {
                packet.ReadPackedGuid128("InviteGUID", i);
                packet.ReadByte("Level", i);
            }
        }

        [Parser(Opcode.SMSG_CALENDAR_INVITE_ADDED)]
        public static void HandleCalendarInviteAdded(Packet packet)
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

        [Parser(Opcode.SMSG_CALENDAR_INVITE_REMOVED)]
        public static void HandleCalendarInviteRemoved(Packet packet)
        {
            packet.ReadPackedGuid128("InviteGUID");
            packet.ReadUInt64("EventID");
            packet.ReadUInt32E<CalendarFlag>("Flags");
            packet.ReadBit("ClearPending");
        }

        [Parser(Opcode.SMSG_CALENDAR_INVITE_STATUS)]
        public static void HandleCalendarInviteStatus(Packet packet)
        {
            packet.ReadPackedGuid128("InviteGUID");
            packet.ReadUInt64("EventID");
            packet.ReadPackedTime("Date");
            packet.ReadUInt32E<CalendarFlag>("Flags");
            packet.ReadByteE<CalendarEventStatus>("Status");
            packet.ReadPackedTime("ResponseTime");
            packet.ReadBit("ClearPending");
        }

        [Parser(Opcode.SMSG_CALENDAR_MODERATOR_STATUS)]
        public static void HandleCalendarModeratorStatus(Packet packet)
        {
            packet.ReadPackedGuid128("InviteGUID");
            packet.ReadUInt64("EventID");
            packet.ReadByteE<CalendarModerationRank>("Status");
            packet.ReadBit("ClearPending");
        }

        [Parser(Opcode.SMSG_CALENDAR_INVITE_ALERT)]
        public static void HandleCalendarEventInviteAlert(Packet packet)
        {
            packet.ReadUInt64("EventID");
            packet.ReadPackedTime("Date");
            packet.ReadUInt16E<CalendarFlag>("Flags");
            packet.ReadByteE<CalendarEventType>("EventType");
            packet.ReadInt32("TextureID");
            packet.ReadUInt64("EventClubID");
            packet.ReadUInt64("InviteID");
            packet.ReadByteE<CalendarEventStatus>("Status");
            packet.ReadByteE<CalendarModerationRank>("ModeratorStatus");

            // TODO: check order
            packet.ReadPackedGuid128("OwnerGUID | InvitedByGUID");
            packet.ReadPackedGuid128("OwnerGUID | InvitedByGUID");

            var eventNameLength = packet.ReadBits("EventNameLength", 8);
            packet.ReadBit("Unknown_1100");
            packet.ResetBitReader();

            packet.ReadWoWString("EventName", eventNameLength);
        }

        [Parser(Opcode.SMSG_CALENDAR_INVITE_STATUS_ALERT)]
        public static void HandleCalendarInviteStatusAlert(Packet packet)
        {
            packet.ReadInt64("EventID");
            packet.ReadPackedTime("Date");
            packet.ReadUInt32E<CalendarFlag>("Flags");
            packet.ReadByteE<CalendarEventStatus>("Status");
        }

        [Parser(Opcode.SMSG_CALENDAR_INVITE_REMOVED_ALERT)]
        public static void HandleCalendarInviteRemovedAlert(Packet packet)
        {
            packet.ReadInt64("EventID");
            packet.ReadPackedTime("Date");
            packet.ReadUInt32E<CalendarFlag>("Flags");
            packet.ReadByteE<CalendarEventStatus>("Status");
        }

        [Parser(Opcode.SMSG_CALENDAR_EVENT_REMOVED_ALERT)]
        public static void HandleCalendarEventRemovedAlert(Packet packet)
        {
            packet.ReadUInt64("EventID");
            packet.ReadPackedTime("Date");
            packet.ResetBitReader();
            packet.ReadBit("ClearPending");
        }

        [Parser(Opcode.SMSG_CALENDAR_EVENT_UPDATED_ALERT)]
        public static void HandleCalendarEventUpdateAlert(Packet packet)
        {
            packet.ReadUInt64("EventClubID");
            packet.ReadUInt64("EventID");
            packet.ReadPackedTime("OriginalDate");
            packet.ReadPackedTime("Date");
            packet.ReadUInt32("LockDate");
            packet.ReadUInt16E<CalendarFlag>("Flags");

            packet.ReadUInt32("TextureID");
            packet.ReadByte("EventType");

            packet.ResetBitReader();
            var eventNameLen = packet.ReadBits(8);
            var descLen = packet.ReadBits(11);
            packet.ReadBit("ClearPending");

            packet.ReadWoWString("EventName", eventNameLen);
            packet.ReadWoWString("Description", descLen);
        }

        [Parser(Opcode.SMSG_CALENDAR_INVITE_NOTES)]
        public static void HandleCalendarInviteNotes(Packet packet)
        {
            packet.ReadPackedGuid128("InviteGUID");
            packet.ReadUInt64("EventID");
            packet.ReadBit("ClearPending");
            var notesLength = packet.ReadBits(8);
            packet.ResetBitReader();

            packet.ReadWoWString("Notes", notesLength);
        }

        [Parser(Opcode.SMSG_CALENDAR_INVITE_NOTES_ALERT)]
        public static void HandleCalendarInviteNotesAlert(Packet packet)
        {
            packet.ReadUInt64("EventID");

            var notesLength = packet.ReadBits(8);
            packet.ResetBitReader();
            packet.ReadWoWString("Notes", notesLength);
        }

        [Parser(Opcode.SMSG_CALENDAR_RAID_LOCKOUT_ADDED)]
        public static void HandleRaidLockoutAdded(Packet packet)
        {
            packet.ReadUInt64("InstanceID");
            packet.ReadUInt32("ServerTime");
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
            packet.ReadUInt32("ServerTime");
            packet.ReadInt32("MapID");
            packet.ReadUInt32("DifficultyID");
            packet.ReadInt32("NewTimeRemaining");
            packet.ReadInt32("OldTimeRemaining");
        }

        [Parser(Opcode.SMSG_CALENDAR_SEND_NUM_PENDING)]
        public static void HandleSendCalendarNumPending(Packet packet)
        {
            packet.ReadUInt32("NumPending");
        }

        [Parser(Opcode.SMSG_CALENDAR_COMMAND_RESULT)]
        public static void HandleCalendarCommandResult(Packet packet)
        {
            packet.ReadByte("Command");
            packet.ReadByte("Result");
            var nameLen = packet.ReadBits(9);
            packet.ReadWoWString("Name", nameLen);
        }

        [Parser(Opcode.SMSG_CALENDAR_CLEAR_PENDING_ACTION)]
        public static void HandleCalenderZero(Packet packet)
        {
        }
    }
}
