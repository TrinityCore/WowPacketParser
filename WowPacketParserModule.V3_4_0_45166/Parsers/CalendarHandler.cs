using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V3_4_0_45166.Parsers
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
        }

        public static void ReadCalendarSendCalendarRaidLockoutInfo(Packet packet, params object[] indexes)
        {
            packet.ReadInt64("InstanceID", indexes);

            packet.ReadInt32("MapID", indexes);
            packet.ReadUInt32("DifficultyID", indexes);
            packet.ReadInt32("ExpireTime", indexes);
        }

        public static void ReadCalendarSendCalendarRaidResetInfo(Packet packet, DateTime time, params object[] indexes)
        {
            packet.ReadInt32<MapId>("MapID", indexes);
            packet.ReadUInt32("Unk340_1", indexes);
            var interval = packet.ReadInt32("IntervalSeconds", indexes);
            var offset = packet.ReadInt32("OffsetSeconds", indexes);
            packet.ReadInt32<DifficultyId>("Difficulty", indexes);

            var diff = time.Subtract(new DateTime(2022, 8, 10, 9, 0, 0)); // last total reset before 3.4. release
            var diffSeconds = diff.Days * 86400 + diff.Hours * 3600 + diff.Minutes * 60;
            float multiplierF = ((float)diffSeconds + (float)offset) / (float)interval;
            packet.AddValue("NextReset", new DateTime(2022, 8, 10, 9, 0, 0).AddSeconds(interval * Math.Ceiling(multiplierF) + offset), indexes);
        }

        public static void ReadCalendarSendCalendarEventInfo(Packet packet, params object[] indexes)
        {
            packet.ReadInt64("EventID", indexes);

            packet.ReadByte("EventType", indexes);

            packet.ReadPackedTime("Date", indexes);
            packet.ReadInt32("Flags", indexes);
            packet.ReadInt32("TextureID", indexes);

            packet.ReadUInt64("CommunityID", indexes);
            packet.ReadPackedGuid128("OwnerGUID", indexes);

            packet.ResetBitReader();

            var len = packet.ReadBits(8);
            packet.ReadWoWString("EventName", len, indexes);
        }

        public static void ReadCalendarSendCalendarInviteInfo344(Packet packet, params object[] indexes)
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

        public static void ReadCalendarSendCalendarEventInfo344(Packet packet, params object[] indexes)
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

        public static void ReadCalendarSendCalendarRaidLockoutInfo344(Packet packet, params object[] indexes)
        {
            packet.ReadInt64("InstanceID", indexes);

            packet.ReadInt32("MapID", indexes);
            packet.ReadUInt32("DifficultyID", indexes);
            packet.ReadInt32("ExpireTime", indexes);
        }

        public static void ReadCalendarSendCalendarRaidResetInfo344(Packet packet, params object[] indexes)
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

        public static void ReadCalendarAddEventInviteInfo(Packet packet, params object[] index)
        {
            packet.ReadPackedGuid128("Guid", index);
            packet.ReadByteE<CalendarEventStatus>("Status", index);
            packet.ReadByteE<CalendarModerationRank>("Moderator", index);

            var unused_801_1 = packet.ReadBit();
            var unused_801_2 = packet.ReadBit();
            var unused_801_3 = packet.ReadBit();

            if (unused_801_1)
                packet.ReadPackedGuid128("Unused_801_1");

            if (unused_801_2)
                packet.ReadUInt64("Unused_801_2");

            if (unused_801_3)
                packet.ReadUInt64("Unused_801_3");

        }

        public static void ReadCalendarEventInfo(Packet packet, params object[] index)
        {
            packet.ReadUInt64("ClubID", index);
            packet.ReadByteE<CalendarEventType>("EventType", index);
            packet.ReadInt32("TextureID", index);
            packet.ReadTime("Time", index);
            packet.ReadInt32E<CalendarFlag>("Flags", index);

            var inviteInfoCount = packet.ReadInt32();

            packet.ResetBitReader();
            var TitleLen = packet.ReadBits(8);
            var DescriptionLen = packet.ReadBits(11);

            for (int i = 0; i < inviteInfoCount; i++)
                ReadCalendarAddEventInviteInfo(packet, index, i);

            packet.ReadWoWString("Title", TitleLen, index);
            packet.ReadWoWString("Description", DescriptionLen, index);
        }

        public static void ReadCalendarUpdateEventInfo(Packet packet, params object[] index)
        {
            packet.ReadUInt64("ClubID", index);
            packet.ReadUInt64("EventID", index);
            packet.ReadUInt64("ModeratorID", index);
            packet.ReadByteE<CalendarEventType>("EventType", index);
            packet.ReadInt32("TextureID", index);
            packet.ReadTime("Time", index);
            packet.ReadUInt16E<CalendarFlag>("Flags", index);

            packet.ResetBitReader();
            var TitleLen = packet.ReadBits(8);
            var DescriptionLen = packet.ReadBits(11);

            packet.ReadWoWString("Title", TitleLen, index);
            packet.ReadWoWString("Description", DescriptionLen, index);
        }

        [Parser(Opcode.SMSG_CALENDAR_SEND_CALENDAR, ClientVersionBuild.V3_4_0_44832, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleCalendarSendCalendar(Packet packet)
        {
            var time = packet.ReadPackedTime("ServerTime");
            var invitesCount = packet.ReadUInt32("InvitesCount");
            var eventsCount = packet.ReadUInt32("EventsCount");
            var raidLockoutsCount = packet.ReadUInt32("RaidLockoutsCount");
            var raidResetsCount = packet.ReadUInt32("RaidResetsCount");

            for (int i = 0; i < invitesCount; i++)
                ReadCalendarSendCalendarInviteInfo(packet, "Invites", i);

            for (int i = 0; i < raidLockoutsCount; i++)
                ReadCalendarSendCalendarRaidLockoutInfo(packet, "RaidLockouts", i);

            for (int i = 0; i < raidResetsCount; i++)
                ReadCalendarSendCalendarRaidResetInfo(packet, time, "RaidResets", i);

            for (int i = 0; i < eventsCount; i++)
                ReadCalendarSendCalendarEventInfo(packet, "Events", i);
        }

        [Parser(Opcode.SMSG_CALENDAR_COMMAND_RESULT, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleCalendarCommandResult(Packet packet)
        {
            packet.ReadByte("Command");
            packet.ReadByte("Result");
            var nameLen = packet.ReadBits(9);
            packet.ReadWoWString("Name", nameLen);
        }

        [Parser(Opcode.SMSG_CALENDAR_COMMUNITY_INVITE, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleCalendarCommunityInvite(Packet packet)
        {
            var inviteInfoCount = packet.ReadUInt32();

            for (int i = 0; i < inviteInfoCount; i++)
            {
                packet.ReadPackedGuid128("InviteGUID", i);
                packet.ReadByte("Level", i);
            }
        }

        [Parser(Opcode.SMSG_CALENDAR_EVENT_REMOVED_ALERT, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleCalendarEventRemovedAlert(Packet packet)
        {
            packet.ReadUInt64("EventID");
            packet.ReadPackedTime("Date");
            packet.ResetBitReader();
            packet.ReadBit("ClearPending");
        }

        [Parser(Opcode.SMSG_CALENDAR_EVENT_UPDATED_ALERT, ClientVersionBuild.V3_4_4_59817)]
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

        [Parser(Opcode.SMSG_CALENDAR_INVITE_ADDED, ClientVersionBuild.V3_4_4_59817)]
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

        [Parser(Opcode.SMSG_CALENDAR_INVITE_ALERT, ClientVersionBuild.V3_4_4_59817)]
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

        [Parser(Opcode.SMSG_CALENDAR_INVITE_NOTES, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleCalendarInviteNotes(Packet packet)
        {
            packet.ReadPackedGuid128("InviteGUID");
            packet.ReadUInt64("EventID");
            packet.ReadBit("ClearPending");
            var notesLength = packet.ReadBits(8);
            packet.ResetBitReader();

            packet.ReadWoWString("Notes", notesLength);
        }

        [Parser(Opcode.SMSG_CALENDAR_INVITE_NOTES_ALERT, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleCalendarInviteNotesAlert(Packet packet)
        {
            packet.ReadUInt64("EventID");

            var notesLength = packet.ReadBits(8);
            packet.ResetBitReader();
            packet.ReadWoWString("Notes", notesLength);
        }

        [Parser(Opcode.SMSG_CALENDAR_INVITE_REMOVED, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleCalendarInviteRemoved(Packet packet)
        {
            packet.ReadPackedGuid128("InviteGUID");
            packet.ReadUInt64("EventID");
            packet.ReadUInt32E<CalendarFlag>("Flags");
            packet.ReadBit("ClearPending");
        }

        [Parser(Opcode.SMSG_CALENDAR_INVITE_REMOVED_ALERT, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleCalendarInviteRemovedAlert(Packet packet)
        {
            packet.ReadInt64("EventID");
            packet.ReadPackedTime("Date");
            packet.ReadUInt32E<CalendarFlag>("Flags");
            packet.ReadByteE<CalendarEventStatus>("Status");
        }

        [Parser(Opcode.SMSG_CALENDAR_INVITE_STATUS, ClientVersionBuild.V3_4_4_59817)]
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

        [Parser(Opcode.SMSG_CALENDAR_INVITE_STATUS_ALERT, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleCalendarInviteStatusAlert(Packet packet)
        {
            packet.ReadInt64("EventID");
            packet.ReadPackedTime("Date");
            packet.ReadUInt32E<CalendarFlag>("Flags");
            packet.ReadByteE<CalendarEventStatus>("Status");
        }

        [Parser(Opcode.SMSG_CALENDAR_MODERATOR_STATUS, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleCalendarModeratorStatus(Packet packet)
        {
            packet.ReadPackedGuid128("InviteGUID");
            packet.ReadUInt64("EventID");
            packet.ReadByteE<CalendarModerationRank>("Status");
            packet.ReadBit("ClearPending");
        }

        [Parser(Opcode.SMSG_CALENDAR_RAID_LOCKOUT_ADDED, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleRaidLockoutAdded(Packet packet)
        {
            packet.ReadUInt64("InstanceID");
            packet.ReadUInt32("ServerTime");
            packet.ReadInt32("MapID");
            packet.ReadUInt32("DifficultyID");
            packet.ReadInt32("TimeRemaining");
        }

        [Parser(Opcode.SMSG_CALENDAR_RAID_LOCKOUT_REMOVED, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleRaidLockoutRemoved(Packet packet)
        {
            packet.ReadUInt64("InstanceID");
            packet.ReadInt32("MapID");
            packet.ReadUInt32("DifficultyID");
        }

        [Parser(Opcode.SMSG_CALENDAR_RAID_LOCKOUT_UPDATED, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleCalendarRaidLockoutUpdated(Packet packet)
        {
            packet.ReadUInt32("ServerTime");
            packet.ReadInt32("MapID");
            packet.ReadUInt32("DifficultyID");
            packet.ReadInt32("NewTimeRemaining");
            packet.ReadInt32("OldTimeRemaining");
        }

        [Parser(Opcode.SMSG_CALENDAR_SEND_CALENDAR, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleCalendarSendCalendar344(Packet packet)
        {
            packet.ReadPackedTime("ServerTime");

            var invitesCount = packet.ReadUInt32("InvitesCount");
            var eventsCount = packet.ReadUInt32("EventsCount");
            var raidLockoutsCount = packet.ReadUInt32("RaidLockoutsCount");
            var raidResetsCount = packet.ReadUInt32("RaidResetsCount");

            for (int i = 0; i < raidLockoutsCount; i++)
                ReadCalendarSendCalendarRaidLockoutInfo344(packet, "RaidLockouts", i);

            for (int i = 0; i < raidResetsCount; i++)
                ReadCalendarSendCalendarRaidResetInfo344(packet, "RaidResets", i);

            for (int i = 0; i < invitesCount; i++)
                ReadCalendarSendCalendarInviteInfo344(packet, "Invites", i);

            for (int i = 0; i < eventsCount; i++)
                ReadCalendarSendCalendarEventInfo344(packet, "Events", i);
        }

        [Parser(Opcode.SMSG_CALENDAR_SEND_EVENT, ClientVersionBuild.V3_4_4_59817)]
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

        [Parser(Opcode.SMSG_CALENDAR_SEND_NUM_PENDING, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleSendCalendarNumPending(Packet packet)
        {
            packet.ReadUInt32("NumPending");
        }

        [Parser(Opcode.CMSG_CALENDAR_ADD_EVENT, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleCalendarAddEvent(Packet packet)
        {
            ReadCalendarEventInfo(packet);
            packet.ReadInt32("MaxSize");
        }

        [Parser(Opcode.CMSG_CALENDAR_COMMUNITY_INVITE, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleUserClientCalendarCommunityInvite(Packet packet)
        {
            packet.ReadUInt64("ClubID");
            packet.ReadByte("MinLevel");
            packet.ReadByte("MaxLevel");
            packet.ReadByte("MaxRankOrder");
        }

        [Parser(Opcode.CMSG_CALENDAR_COMPLAIN, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleCalenderComplain(Packet packet)
        {
            packet.ReadPackedGuid128("InvitedByGUID");
            packet.ReadUInt64("EventID");
            packet.ReadUInt64("InviteID");
        }

        [Parser(Opcode.CMSG_CALENDAR_COPY_EVENT, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleCalenderCopyEvent(Packet packet)
        {
            packet.ReadUInt64("EventID");
            packet.ReadUInt64("ModeratorID");
            packet.ReadUInt64("EventClubID");
            packet.ReadPackedTime("Date");
        }

        [Parser(Opcode.CMSG_CALENDAR_EVENT_SIGN_UP, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleCalendarEventSignup(Packet packet)
        {
            packet.ReadInt64("EventID");
            packet.ReadInt64("ClubID");
            packet.ResetBitReader();
            packet.ReadBit("Tentative");
        }

        [Parser(Opcode.CMSG_CALENDAR_GET_EVENT, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleGetCalendarEvent(Packet packet)
        {
            packet.ReadUInt64("EventID");
        }

        [Parser(Opcode.CMSG_CALENDAR_INVITE, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleCalendarInvite(Packet packet)
        {
            packet.ReadUInt64("EventID");
            packet.ReadUInt64("ModeratorID");
            packet.ReadUInt64("ClubID");

            var nameLen = packet.ReadBits(9);
            packet.ReadBit("Creating");
            packet.ReadBit("IsSignUp");
            packet.ResetBitReader();

            packet.ReadWoWString("Name", nameLen);
        }

        [Parser(Opcode.CMSG_CALENDAR_MODERATOR_STATUS, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.CMSG_CALENDAR_STATUS, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleUserClientCalendarModeratorStatus(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");
            packet.ReadUInt64("EventID");
            packet.ReadUInt64("InviteID");
            packet.ReadUInt64("ModeratorID");
            packet.ReadByteE<CalendarEventStatus>("Status");
        }

        [Parser(Opcode.CMSG_CALENDAR_REMOVE_EVENT, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleRemoveCalendarEvent(Packet packet)
        {
            packet.ReadUInt64("EventID");
            packet.ReadUInt64("ModeratorID");
            packet.ReadUInt64("ClubID");
            packet.ReadInt32E<CalendarFlag>("Flags");
        }

        [Parser(Opcode.CMSG_CALENDAR_REMOVE_INVITE, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleCalendarRemoveInvite(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");
            packet.ReadUInt64("InviteID");
            packet.ReadUInt64("ModeratorID");
            packet.ReadUInt64("EventID");
        }

        [Parser(Opcode.CMSG_CALENDAR_RSVP, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleCalendarRSVP(Packet packet)
        {
            packet.ReadUInt64("EventID");
            packet.ReadUInt64("InviteID");
            packet.ReadByte("Status");
        }

        [Parser(Opcode.CMSG_CALENDAR_UPDATE_EVENT, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleUpdateCalendarEvent(Packet packet)
        {
            ReadCalendarUpdateEventInfo(packet);
            packet.ReadUInt32("MaxSize");
        }

        [Parser(Opcode.SMSG_CALENDAR_CLEAR_PENDING_ACTION, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.CMSG_CALENDAR_GET_NUM_PENDING, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.CMSG_CALENDAR_GET, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleCalenderZero(Packet packet)
        {
        }
    }
}
