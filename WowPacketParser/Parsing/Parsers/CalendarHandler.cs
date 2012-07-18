using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Parsing.Parsers
{
    public static class CalendarHandler
    {
        [Parser(Opcode.SMSG_CALENDAR_SEND_CALENDAR)]
        public static void HandleSendCalendar(Packet packet)
        {
            var invCount = packet.ReadInt32("Invite Count");

            for (var i = 0; i < invCount; i++)
            {
                packet.ReadInt64("Event ID", i);
                packet.ReadInt64("Invite ID", i);
                packet.ReadEnum<CalendarEventStatus>("Status", TypeCode.Byte, i);
                packet.ReadEnum<CalendarModerationRank>("Moderation Rank", TypeCode.Byte, i);
                packet.ReadBoolean("Guild Event", i);
                packet.ReadPackedGuid("Creator GUID", i);
            }

            var eventCount = packet.ReadInt32("Event Count");

            for (var i = 0; i < eventCount; i++)
            {
                packet.ReadInt64("Event ID", i);
                packet.ReadCString("Event Title ", i);
                packet.ReadEnum<CalendarEventType>("Event Type", TypeCode.Int32, i);
                packet.ReadPackedTime("Event Time", i);
                packet.ReadEnum<CalendarFlag>("Event Flags", TypeCode.Int32, i);
                packet.ReadEntryWithName<Int32>(StoreNameType.LFGDungeon, "Dungeon ID", i);

                if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_2_2_14545))
                    packet.ReadInt64("Unk int64");

                packet.ReadPackedGuid("Creator GUID", i);
            }

            packet.ReadTime("Current Time");
            packet.ReadPackedTime("Zone Time?");

            var instanceResetCount = packet.ReadInt32("Instance Reset Count");

            for (var i = 0; i < instanceResetCount; i++)
            {
                packet.ReadEntryWithName<Int32>(StoreNameType.Map, "Map ID", i);
                packet.ReadEnum<MapDifficulty>("Difficulty", TypeCode.Int32, i);
                packet.ReadInt32("Time left", i);
                packet.ReadGuid("Instance ID", i);
            }

            packet.ReadTime("Constant Date");

            var raidResetCount = packet.ReadInt32("Raid Reset Count");

            for (var i = 0; i < raidResetCount; i++)
            {
                packet.ReadEntryWithName<Int32>(StoreNameType.Map, "Map ID", i);
                packet.ReadInt32("Time left", i);
                packet.ReadInt32("Unk Time", i);
            }

            var holidayCount = packet.ReadInt32("Holiday Count");

            for (var i = 0; i < holidayCount; i++)
            {
                packet.ReadInt32("ID", i);
                packet.ReadInt32("Region (Looping?)", i);
                packet.ReadInt32("Looping (Region?)", i);
                packet.ReadInt32("Priority", i);
                packet.ReadInt32("Calendar FilterType", i);
                for (var j = 0; j < 26; j++)
                    packet.ReadPackedTime("Start Date", i, j);
                for (var j = 0; j < 10; j++)
                    packet.ReadInt32("Duration", i, j);
                for (var j = 0; j < 10; j++)
                    packet.ReadEnum<CalendarFlag>("Calendar Flags", TypeCode.Int32, i, j);
                packet.ReadCString("Holiday Name", i);
            }
        }

        [Parser(Opcode.CMSG_CALENDAR_GET_EVENT)]
        public static void HandleGetCalendarEvent(Packet packet)
        {
            packet.ReadInt64("Event ID");
        }

        [Parser(Opcode.SMSG_CALENDAR_SEND_EVENT)]
        public static void HandleSendCalendarEvent(Packet packet)
        {
            packet.ReadEnum<CalendarSendEventType>("Send Event Type", TypeCode.Byte);
            packet.ReadPackedGuid("Creator GUID");
            packet.ReadInt64("Event ID");
            packet.ReadCString("Title");
            packet.ReadCString("Description");
            packet.ReadEnum<CalendarEventType>("Event Type", TypeCode.Byte);
            packet.ReadEnum<CalendarRepeatType>("Repeat Type", TypeCode.Byte);
            packet.ReadInt32("Max Invites");
            packet.ReadEntryWithName<Int32>(StoreNameType.LFGDungeon, "Dungeon ID");
            packet.ReadEnum<CalendarFlag>("Event Flags", TypeCode.Int32);
            packet.ReadPackedTime("Event Time");
            packet.ReadPackedTime("Unk PackedTime");
            packet.ReadInt32("Guild");

            var invCount = packet.ReadInt32("Invite Count");

            for (var i = 0; i < invCount; i++)
            {
                packet.ReadPackedGuid("Invitee GUID", i);
                packet.ReadByte("Player Level", i);
                packet.ReadEnum<CalendarEventStatus>("[" + i + "] Status", TypeCode.Byte, i);
                packet.ReadEnum<CalendarModerationRank>("[" + i + "] Moderation Rank", TypeCode.Byte, i);
                packet.ReadBoolean("Guild Member", i);
                packet.ReadInt64("Invite ID", i);
                packet.ReadPackedTime("Status Time", i);
                packet.ReadCString("Invite Text", i);
            }
        }

        [Parser(Opcode.CMSG_CALENDAR_GUILD_FILTER)]
        public static void HandleCalendarGuildFilter(Packet packet)
        {
            packet.ReadInt32("Min Level");
            packet.ReadInt32("Max Level");
            packet.ReadInt32("Min Rank");
        }

        [Parser(Opcode.CMSG_CALENDAR_ARENA_TEAM)]
        public static void HandleCalendarArenaTeam(Packet packet)
        {
            packet.ReadInt32("Unk Int32 1");
        }

        [Parser(Opcode.SMSG_CALENDAR_ARENA_TEAM)]
        [Parser(Opcode.SMSG_CALENDAR_FILTER_GUILD)]
        public static void HandleCalendarFilters(Packet packet)
        {
            var count = packet.ReadInt32("Count");

            for (var i = 0; i < count; i++)
            {
                packet.ReadPackedGuid("GUID", i);
                packet.ReadByte("Unk Byte", i);
            }
        }

        [Parser(Opcode.CMSG_CALENDAR_ADD_EVENT)]
        public static void HandleAddCalendarEvent(Packet packet)
        {
            packet.ReadCString("Title");
            packet.ReadCString("Description");
            packet.ReadEnum<CalendarEventType>("Event Type", TypeCode.Byte);
            packet.ReadEnum<CalendarRepeatType>("Repeat Type", TypeCode.Byte);
            packet.ReadInt32("Max Invites");
            packet.ReadEntryWithName<Int32>(StoreNameType.LFGDungeon, "Dungeon ID");
            packet.ReadPackedTime("Event Time");
            packet.ReadPackedTime("Unk PackedTime");

            var flags = packet.ReadEnum<CalendarFlag>("Event Flags", TypeCode.Int32);

            if ((flags & CalendarFlag.GuildAnnouncement) != 0)
                return;

            var count = packet.ReadInt32("Invite Count");

            for (var i = 0; i < count; i++)
            {
                packet.ReadPackedGuid("Creator GUID");
                packet.ReadEnum<CalendarEventStatus>("Status", TypeCode.Byte);
                packet.ReadEnum<CalendarModerationRank>("Moderation Rank", TypeCode.Byte);
            }
        }

        [Parser(Opcode.CMSG_CALENDAR_UPDATE_EVENT)]
        public static void HandleUpdateCalendarEvent(Packet packet)
        {
            packet.ReadInt64("Event ID");
            packet.ReadInt64("Invite ID");
            packet.ReadCString("Title");
            packet.ReadCString("Description");
            packet.ReadEnum<CalendarEventType>("Event Type", TypeCode.Byte);
            packet.ReadEnum<CalendarRepeatType>("Repeat Type", TypeCode.Byte);
            packet.ReadInt32("Max Invites");
            packet.ReadEntryWithName<Int32>(StoreNameType.LFGDungeon, "Dungeon ID");
            packet.ReadPackedTime("Event Time");
            packet.ReadPackedTime("Unk PackedTime");
            packet.ReadEnum<CalendarFlag>("Event Flags", TypeCode.Int32);
        }

        [Parser(Opcode.CMSG_CALENDAR_REMOVE_EVENT)]
        public static void HandleRemoveCalendarEvent(Packet packet)
        {
            packet.ReadInt64("Event ID");
            packet.ReadInt64("Invite ID");
            packet.ReadEnum<CalendarFlag>("Event Flags", TypeCode.Int32);
        }

        [Parser(Opcode.CMSG_CALENDAR_COPY_EVENT)]
        public static void HandleCopyCalendarEvent(Packet packet)
        {
            packet.ReadInt64("Event ID");
            packet.ReadInt64("Invite ID");
            packet.ReadPackedTime("Event Time");
        }

        [Parser(Opcode.CMSG_CALENDAR_EVENT_INVITE)]
        public static void HandleAddCalendarEventInvite(Packet packet)
        {
            packet.ReadInt64("Event ID");
            packet.ReadInt64("Invite ID");
            packet.ReadCString("Name");
            packet.ReadEnum<CalendarEventStatus>("Status", TypeCode.Byte);
            packet.ReadEnum<CalendarModerationRank>("Moderation Rank", TypeCode.Byte);
        }

        [Parser(Opcode.SMSG_CALENDAR_EVENT_INVITE)]
        public static void HandleSendCalendarEventInvite(Packet packet)
        {
            packet.ReadPackedGuid("Invitee GUID");
            packet.ReadInt64("Event ID");
            packet.ReadInt64("Invite ID");
            packet.ReadByte("Player Level");
            packet.ReadEnum<CalendarEventStatus>("Status", TypeCode.Byte);

            if (packet.ReadBoolean("Has Confirm Time"))
                packet.ReadPackedTime("Confirm Time");

            packet.ReadBoolean("Guild Event");
        }

        [Parser(Opcode.CMSG_CALENDAR_EVENT_INVITE_NOTES)]
        public static void HandleCalendarUpdateInviteNotes(Packet packet)
        {
            packet.ReadPackedGuid("Invitee GUID");
            packet.ReadInt64("Invite ID");
            packet.ReadCString("Invite Text");
            packet.ReadBoolean("Unk Boolean");
        }

        [Parser(Opcode.SMSG_CALENDAR_EVENT_INVITE_NOTES_ALERT)]
        public static void HandleCalendarUpdateInviteNotesAlert(Packet packet)
        {
            packet.ReadInt64("Invite ID");
            packet.ReadCString("Invite Text");
        }

        [Parser(Opcode.SMSG_CALENDAR_EVENT_INVITE_NOTES)]
        public static void HandleCalendarSendUpdatedInviteNotes(Packet packet)
        {
            packet.ReadPackedGuid("Invitee GUID");
            packet.ReadInt64("Invite ID");
            packet.ReadCString("Invite Text");
            packet.ReadBoolean("Unk bool");
        }

        [Parser(Opcode.CMSG_CALENDAR_EVENT_REMOVE_INVITE)]
        public static void HandleRemoveCalendarEventInvite(Packet packet)
        {
            packet.ReadPackedGuid("GUID");
            packet.ReadInt64("Invite ID");
            packet.ReadInt64("Unk int64 1");
            packet.ReadInt64("Event ID");
        }

        [Parser(Opcode.SMSG_CALENDAR_EVENT_INVITE_REMOVED)]
        public static void HandleSendCalendarEventInviteRemoved(Packet packet)
        {
            packet.ReadPackedGuid("GUID");
            packet.ReadInt64("Invite ID");
            packet.ReadEnum<CalendarFlag>("Event Flags", TypeCode.Int32);
            packet.ReadByte("Unk Byte");
        }

        [Parser(Opcode.CMSG_CALENDAR_EVENT_STATUS)]
        [Parser(Opcode.CMSG_CALENDAR_EVENT_MODERATOR_STATUS)]
        public static void HandleCalendarEventStatus(Packet packet)
        {
            packet.ReadPackedGuid("Invitee GUID");
            packet.ReadInt64("Event ID");
            packet.ReadInt64("Invitee ID");
            packet.ReadInt64("Invite ID");
            packet.ReadEnum<CalendarEventStatus>("Status", TypeCode.Int32);
        }

        [Parser(Opcode.SMSG_CALENDAR_EVENT_STATUS)]
        public static void HandleSendCalendarEventStatus(Packet packet)
        {
            packet.ReadPackedGuid("GUID");
            packet.ReadInt64("Event ID");
            packet.ReadPackedTime("Event Time");
            packet.ReadEnum<CalendarFlag>("Event Flags", TypeCode.Int32);
            packet.ReadEnum<CalendarEventStatus>("Status", TypeCode.Byte);
            packet.ReadEnum<CalendarModerationRank>("Moderation Rank", TypeCode.Byte);
            packet.ReadPackedTime("Event status change time");
        }

        [Parser(Opcode.SMSG_CALENDAR_EVENT_MODERATOR_STATUS_ALERT)]
        public static void HandleSendCalendarEventModeratorStatus(Packet packet)
        {
            packet.ReadPackedGuid("Invitee GUID");
            packet.ReadInt64("Event ID");
            packet.ReadEnum<CalendarEventStatus>("Status", TypeCode.Byte);
            packet.ReadBoolean("Unk Boolean");
        }

        [Parser(Opcode.CMSG_CALENDAR_COMPLAIN)]
        public static void HandleCalendarComplain(Packet packet)
        {
            packet.ReadInt64("Event ID");
            packet.ReadGuid("GUID");
        }

        [Parser(Opcode.SMSG_CALENDAR_SEND_NUM_PENDING)]
        public static void HandleSendCalendarNumPending(Packet packet)
        {
            packet.ReadInt32("Pending Invites");
        }

        [Parser(Opcode.CMSG_CALENDAR_EVENT_RSVP)]
        public static void HandleCalendarRsvp(Packet packet)
        {
            packet.ReadInt64("Event ID");
            packet.ReadInt64("Invite ID");
            packet.ReadEnum<CalendarEventStatus>("Status", TypeCode.Int32);
        }

        [Parser(Opcode.SMSG_CALENDAR_RAID_LOCKOUT_ADDED)]
        public static void HandleRaidLockoutAdded(Packet packet)
        {
            packet.ReadPackedTime("Time");
            packet.ReadEntryWithName<Int32>(StoreNameType.Map, "Map ID");
            packet.ReadEnum<MapDifficulty>("Difficulty", TypeCode.Int32);
            packet.ReadInt32("Reset Time");
            packet.ReadGuid("Instance ID");
        }

        [Parser(Opcode.SMSG_CALENDAR_RAID_LOCKOUT_REMOVED)]
        public static void HandleRaidLockoutRemoved(Packet packet)
        {
            packet.ReadEntryWithName<Int32>(StoreNameType.Map, "Map ID");
            packet.ReadEnum<MapDifficulty>("Difficulty", TypeCode.Int32);
            packet.ReadInt32("Reset Time");
            packet.ReadGuid("Instance ID");
        }

        [Parser(Opcode.SMSG_CALENDAR_RAID_LOCKOUT_UPDATED)]
        public static void HandleCalendarRaidLockoutUpdated(Packet packet)
        {
            packet.ReadPackedTime("Time");
            packet.ReadInt32("Map ID");
            packet.ReadInt32("Difficulty");
            packet.ReadInt32("Time changed (in seconds)");
            packet.ReadInt32("Reset time");
        }

        [Parser(Opcode.SMSG_CALENDAR_EVENT_INVITE_ALERT)]
        public static void HandleCalendarEventInviteAlert(Packet packet)
        {
            packet.ReadInt64("Event ID");
            packet.ReadCString("Title");
            packet.ReadPackedTime("Time");
            packet.ReadEnum<CalendarFlag>("Event Flags", TypeCode.Int32);
            packet.ReadEnum<CalendarEventType>("Type", TypeCode.Int32);
            packet.ReadEntryWithName<Int32>(StoreNameType.LFGDungeon, "Dungeon ID");
            packet.ReadInt64("Invite ID");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_3_4_15595))
            {
                packet.ReadInt64("Unk Int64");
            }
            packet.ReadEnum<CalendarEventStatus>("Status", TypeCode.Byte);
            packet.ReadEnum<CalendarModerationRank>("Moderation Rank", TypeCode.Byte);
            packet.ReadPackedGuid("Creator GUID");
            packet.ReadPackedGuid("Sender GUID");
        }

        [Parser(Opcode.SMSG_CALENDAR_EVENT_REMOVED_ALERT)]
        public static void HandleCalendarEventRemoveAlert(Packet packet)
        {
            packet.ReadByte("Unk (RemovedAlert)");
            packet.ReadInt64("Event ID");
            packet.ReadPackedTime("Event Time");
        }

        [Parser(Opcode.SMSG_CALENDAR_EVENT_UPDATED_ALERT)]
        public static void HandleCalendarEventUpdateAlert(Packet packet)
        {
            packet.ReadByte("Unk byte (UpdatedAlert)");
            packet.ReadInt64("Event ID");
            packet.ReadPackedTime("Event Time");
            packet.ReadEnum<CalendarFlag>("Event Flags", TypeCode.Int32);
            packet.ReadPackedTime("Time2");
            packet.ReadEnum<CalendarEventType>("Event Type", TypeCode.Byte);
            packet.ReadEntryWithName<Int32>(StoreNameType.LFGDungeon, "Dungeon ID");
            packet.ReadCString("Title");
            packet.ReadCString("Description");
            packet.ReadEnum<CalendarRepeatType>("Repeat Type", TypeCode.Byte);
            packet.ReadInt32("Max Invites");
            packet.ReadInt32("Unk int32 (UpdatedAlert)");
        }

        [Parser(Opcode.SMSG_CALENDAR_COMMAND_RESULT)]
        public static void HandleCalendarCommandResult(Packet packet)
        {
            packet.ReadInt32("UnkInt1");
            packet.ReadCString("UnkString1");
            packet.ReadCString("Param 1"); // if %s is used in the error message
            packet.ReadEnum<CalendarError>("Error", TypeCode.Int32);
        }

        [Parser(Opcode.SMSG_CALENDAR_EVENT_INVITE_REMOVED_ALERT)]
        public static void HandleCalendarEventInviteRemoveAlert(Packet packet)
        {
            packet.ReadInt64("Event ID");
            packet.ReadPackedTime("Event Time");
            packet.ReadEnum<CalendarFlag>("Event Flags", TypeCode.Int32);
            packet.ReadEnum<CalendarEventStatus>("Status", TypeCode.Byte);
        }

        [Parser(Opcode.SMSG_CALENDAR_EVENT_INVITE_STATUS_ALERT)]
        public static void HandleCalendarEventInviteStatusAlert(Packet packet)
        {
            packet.ReadInt64("Event ID");
            packet.ReadPackedTime("Event Time");
            packet.ReadInt32("Unk flag"); // (v38 & 0x440) != 0
            packet.ReadBoolean("DeletePendingInvite");
        }

        [Parser(Opcode.CMSG_CALENDAR_EVENT_SIGNUP)]
        public static void HandleCalendarEventSignup(Packet packet)
        {
            packet.ReadInt64("Event ID");
            packet.ReadEnum<CalendarEventStatus>("Status", TypeCode.Byte);
        }
    }
}
