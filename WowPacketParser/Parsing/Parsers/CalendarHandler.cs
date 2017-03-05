using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Parsing.Parsers
{
    public static class CalendarHandler
    {
        [Parser(Opcode.SMSG_CALENDAR_SEND_CALENDAR)]
        public static void HandleSendCalendar(Packet packet)
        {
            var invCount = packet.Translator.ReadInt32("Invite Count");

            for (var i = 0; i < invCount; i++)
            {
                packet.Translator.ReadInt64("Event ID", i);
                packet.Translator.ReadInt64("Invite ID", i);
                packet.Translator.ReadByteE<CalendarEventStatus>("Status", i);
                packet.Translator.ReadByteE<CalendarModerationRank>("Moderation Rank", i);
                packet.Translator.ReadBool("Guild Event", i);
                packet.Translator.ReadPackedGuid("Creator GUID", i);
            }

            var eventCount = packet.Translator.ReadInt32("Event Count");

            for (var i = 0; i < eventCount; i++)
            {
                packet.Translator.ReadInt64("Event ID", i);
                packet.Translator.ReadCString("Event Title", i);
                packet.Translator.ReadInt32E<CalendarEventType>("Event Type", i);
                packet.Translator.ReadPackedTime("Event Time", i);
                packet.Translator.ReadInt32E<CalendarFlag>("Event Flags", i);
                packet.Translator.ReadInt32<LFGDungeonId>("Dungeon ID", i);

                if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_2_2_14545))
                    packet.Translator.ReadGuid("Guild GUID", i);

                packet.Translator.ReadPackedGuid("Creator GUID", i);
            }

            packet.Translator.ReadTime("Current Time");
            packet.Translator.ReadPackedTime("Zone Time?");

            var instanceResetCount = packet.Translator.ReadInt32("Instance Reset Count");

            for (var i = 0; i < instanceResetCount; i++)
            {
                packet.Translator.ReadInt32<MapId>("Map ID", i);
                packet.Translator.ReadInt32E<MapDifficulty>("Difficulty", i);
                packet.Translator.ReadInt32("Time left", i);
                packet.Translator.ReadGuid("Instance ID", i);
            }

            packet.Translator.ReadTime("Constant Date");

            var raidResetCount = packet.Translator.ReadInt32("Raid Reset Count");

            for (var i = 0; i < raidResetCount; i++)
            {
                packet.Translator.ReadInt32<MapId>("Map ID", i);
                packet.Translator.ReadInt32("Time left", i);
                packet.Translator.ReadInt32("Unk Time", i);
            }

            var holidayCount = packet.Translator.ReadInt32("Holiday Count");

            for (var i = 0; i < holidayCount; i++)
            {
                packet.Translator.ReadInt32("ID", i);
                packet.Translator.ReadInt32("Region (Looping?)", i);
                packet.Translator.ReadInt32("Looping (Region?)", i);
                packet.Translator.ReadInt32("Priority", i);
                packet.Translator.ReadInt32("Calendar FilterType", i);
                for (var j = 0; j < 26; j++)
                    packet.Translator.ReadPackedTime("Start Date", i, j);
                for (var j = 0; j < 10; j++)
                    packet.Translator.ReadInt32("Duration", i, j);
                for (var j = 0; j < 10; j++)
                    packet.Translator.ReadInt32E<CalendarFlag>("Calendar Flags", i, j);
                packet.Translator.ReadCString("Holiday Name", i);
            }
        }

        [Parser(Opcode.CMSG_CALENDAR_GET_EVENT)]
        public static void HandleGetCalendarEvent(Packet packet)
        {
            packet.Translator.ReadInt64("Event ID");
        }

        [Parser(Opcode.SMSG_CALENDAR_SEND_EVENT)]
        public static void HandleSendCalendarEvent(Packet packet)
        {
            packet.Translator.ReadByteE<CalendarSendEventType>("Send Event Type");
            packet.Translator.ReadPackedGuid("Creator GUID");
            packet.Translator.ReadInt64("Event ID");
            packet.Translator.ReadCString("Title");
            packet.Translator.ReadCString("Description");
            packet.Translator.ReadByteE<CalendarEventType>("Event Type");
            packet.Translator.ReadByteE<CalendarRepeatType>("Repeat Type");
            packet.Translator.ReadInt32("Max Invites");
            packet.Translator.ReadInt32<LFGDungeonId>("Dungeon ID");
            packet.Translator.ReadInt32E<CalendarFlag>("Event Flags");
            packet.Translator.ReadPackedTime("Event Time");
            packet.Translator.ReadPackedTime("Unk PackedTime");
            if (ClientVersion.AddedInVersion(ClientType.Cataclysm))
                packet.Translator.ReadGuid("Guild Guid");
            else
                packet.Translator.ReadInt32("Guild");

            var invCount = packet.Translator.ReadInt32("Invite Count");

            for (var i = 0; i < invCount; i++)
            {
                packet.Translator.ReadPackedGuid("Invitee GUID", i);
                packet.Translator.ReadByte("Player Level", i);
                packet.Translator.ReadByteE<CalendarEventStatus>("Status", i);
                packet.Translator.ReadByteE<CalendarModerationRank>("Moderation Rank", i);
                packet.Translator.ReadBool("Guild Member", i);
                packet.Translator.ReadInt64("Invite ID", i);
                packet.Translator.ReadPackedTime("Status Time", i);
                packet.Translator.ReadCString("Invite Text", i);
            }
        }

        [Parser(Opcode.CMSG_CALENDAR_GUILD_FILTER)]
        public static void HandleCalendarGuildFilter(Packet packet)
        {
            packet.Translator.ReadInt32("Min Level");
            packet.Translator.ReadInt32("Max Level");
            packet.Translator.ReadInt32("Min Rank");
        }

        [Parser(Opcode.CMSG_CALENDAR_ARENA_TEAM)]
        public static void HandleCalendarArenaTeam(Packet packet)
        {
            packet.Translator.ReadInt32("Unk Int32 1");
        }

        [Parser(Opcode.SMSG_CALENDAR_ARENA_TEAM)]
        [Parser(Opcode.SMSG_CALENDAR_FILTER_GUILD)]
        public static void HandleCalendarFilters(Packet packet)
        {
            var count = packet.Translator.ReadInt32("Count");

            for (var i = 0; i < count; i++)
            {
                packet.Translator.ReadPackedGuid("GUID", i);
                packet.Translator.ReadByte("Unk Byte", i);
            }
        }

        [Parser(Opcode.CMSG_CALENDAR_ADD_EVENT)]
        public static void HandleAddCalendarEvent(Packet packet)
        {
            packet.Translator.ReadCString("Title");
            packet.Translator.ReadCString("Description");
            packet.Translator.ReadByteE<CalendarEventType>("Event Type");
            packet.Translator.ReadByteE<CalendarRepeatType>("Repeat Type");
            packet.Translator.ReadInt32("Max Invites");
            packet.Translator.ReadInt32<LFGDungeonId>("Dungeon ID");
            packet.Translator.ReadPackedTime("Event Time");
            packet.Translator.ReadPackedTime("Unk PackedTime");

            var flags = packet.Translator.ReadInt32E<CalendarFlag>("Event Flags");

            if ((flags & CalendarFlag.GuildAnnouncement) != 0)
                return;

            var count = packet.Translator.ReadInt32("Invite Count");

            for (var i = 0; i < count; i++)
            {
                packet.Translator.ReadPackedGuid("Creator GUID");
                packet.Translator.ReadByteE<CalendarEventStatus>("Status");
                packet.Translator.ReadByteE<CalendarModerationRank>("Moderation Rank");
            }
        }

        [Parser(Opcode.CMSG_CALENDAR_UPDATE_EVENT)]
        public static void HandleUpdateCalendarEvent(Packet packet)
        {
            packet.Translator.ReadInt64("Event ID");
            packet.Translator.ReadInt64("Invite ID");
            packet.Translator.ReadCString("Title");
            packet.Translator.ReadCString("Description");
            packet.Translator.ReadByteE<CalendarEventType>("Event Type");
            packet.Translator.ReadByteE<CalendarRepeatType>("Repeat Type");
            packet.Translator.ReadInt32("Max Invites");
            packet.Translator.ReadInt32<LFGDungeonId>("Dungeon ID");
            packet.Translator.ReadPackedTime("Event Time");
            packet.Translator.ReadPackedTime("Unk PackedTime");
            packet.Translator.ReadInt32E<CalendarFlag>("Event Flags");
        }

        [Parser(Opcode.CMSG_CALENDAR_REMOVE_EVENT)]
        public static void HandleRemoveCalendarEvent(Packet packet)
        {
            packet.Translator.ReadInt64("Event ID");
            packet.Translator.ReadInt64("Invite ID");
            packet.Translator.ReadInt32E<CalendarFlag>("Event Flags");
        }

        [Parser(Opcode.CMSG_CALENDAR_COPY_EVENT)]
        public static void HandleCopyCalendarEvent(Packet packet)
        {
            packet.Translator.ReadInt64("Event ID");
            packet.Translator.ReadInt64("Invite ID");
            packet.Translator.ReadPackedTime("Event Time");
        }

        [Parser(Opcode.CMSG_CALENDAR_EVENT_INVITE)]
        public static void HandleAddCalendarEventInvite(Packet packet)
        {
            packet.Translator.ReadInt64("Event ID");
            packet.Translator.ReadInt64("Invite ID");
            packet.Translator.ReadCString("Name");
            packet.Translator.ReadByteE<CalendarEventStatus>("Status");
            packet.Translator.ReadByteE<CalendarModerationRank>("Moderation Rank");
        }

        [Parser(Opcode.SMSG_CALENDAR_EVENT_INVITE)]
        public static void HandleSendCalendarEventInvite(Packet packet)
        {
            packet.Translator.ReadPackedGuid("Invitee GUID");
            packet.Translator.ReadInt64("Event ID");
            packet.Translator.ReadInt64("Invite ID");
            packet.Translator.ReadByte("Player Level");
            packet.Translator.ReadByteE<CalendarEventStatus>("Status");

            if (packet.Translator.ReadBool("Has Confirm Time"))
                packet.Translator.ReadPackedTime("Confirm Time");

            packet.Translator.ReadBool("Guild Event");
        }

        [Parser(Opcode.CMSG_CALENDAR_EVENT_INVITE_NOTES)]
        public static void HandleCalendarUpdateInviteNotes(Packet packet)
        {
            packet.Translator.ReadPackedGuid("Invitee GUID");
            packet.Translator.ReadInt64("Invite ID");
            packet.Translator.ReadCString("Invite Text");
            packet.Translator.ReadBool("Unk Boolean");
        }

        [Parser(Opcode.SMSG_CALENDAR_EVENT_INVITE_NOTES_ALERT)]
        public static void HandleCalendarUpdateInviteNotesAlert(Packet packet)
        {
            packet.Translator.ReadInt64("Invite ID");
            packet.Translator.ReadCString("Invite Text");
        }

        [Parser(Opcode.SMSG_CALENDAR_EVENT_INVITE_NOTES)]
        public static void HandleCalendarSendUpdatedInviteNotes(Packet packet)
        {
            packet.Translator.ReadPackedGuid("Invitee GUID");
            packet.Translator.ReadInt64("Invite ID");
            packet.Translator.ReadCString("Invite Text");
            packet.Translator.ReadBool("Unk bool");
        }

        [Parser(Opcode.CMSG_CALENDAR_EVENT_REMOVE_INVITE)]
        public static void HandleRemoveCalendarEventInvite(Packet packet)
        {
            packet.Translator.ReadPackedGuid("GUID");
            packet.Translator.ReadInt64("Invite ID");
            packet.Translator.ReadInt64("Unk int64 1");
            packet.Translator.ReadInt64("Event ID");
        }

        [Parser(Opcode.SMSG_CALENDAR_EVENT_INVITE_REMOVED)]
        public static void HandleSendCalendarEventInviteRemoved(Packet packet)
        {
            packet.Translator.ReadPackedGuid("GUID");
            packet.Translator.ReadInt64("Invite ID");
            packet.Translator.ReadInt32E<CalendarFlag>("Event Flags");
            packet.Translator.ReadByte("Unk Byte");
        }

        [Parser(Opcode.CMSG_CALENDAR_EVENT_STATUS)]
        public static void HandleCalendarEventStatus(Packet packet)
        {
            packet.Translator.ReadPackedGuid("Invitee GUID");
            packet.Translator.ReadInt64("Event ID");
            packet.Translator.ReadInt64("Invite ID");
            packet.Translator.ReadInt64("Owner Invite ID"); // sender's invite id?
            packet.Translator.ReadInt32E<CalendarEventStatus>("Status");
        }

        [Parser(Opcode.CMSG_CALENDAR_EVENT_MODERATOR_STATUS)]
        public static void HandleCalendarEventModeratorStatus(Packet packet)
        {
            packet.Translator.ReadPackedGuid("Invitee GUID");
            packet.Translator.ReadInt64("Event ID");
            packet.Translator.ReadInt64("Invite ID");
            packet.Translator.ReadInt64("Owner Invite ID"); // sender's invite id?
            packet.Translator.ReadInt32E<CalendarModerationRank>("Rank");
        }

        [Parser(Opcode.SMSG_CALENDAR_EVENT_STATUS)]
        public static void HandleSendCalendarEventStatus(Packet packet)
        {
            packet.Translator.ReadPackedGuid("GUID");
            packet.Translator.ReadInt64("Event ID");
            packet.Translator.ReadPackedTime("Event Time");
            packet.Translator.ReadInt32E<CalendarFlag>("Event Flags");
            packet.Translator.ReadByteE<CalendarEventStatus>("Status");
            packet.Translator.ReadByteE<CalendarModerationRank>("Moderation Rank");
            packet.Translator.ReadPackedTime("Event status change time");
        }

        [Parser(Opcode.SMSG_CALENDAR_EVENT_MODERATOR_STATUS_ALERT)]
        public static void HandleSendCalendarEventModeratorStatus(Packet packet)
        {
            packet.Translator.ReadPackedGuid("Invitee GUID");
            packet.Translator.ReadInt64("Event ID");
            packet.Translator.ReadByteE<CalendarModerationRank>("Rank");
            packet.Translator.ReadBool("Unk Boolean");
        }

        [Parser(Opcode.CMSG_CALENDAR_COMPLAIN, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleCalendarComplain(Packet packet)
        {
            packet.Translator.ReadInt64("Event ID");
            packet.Translator.ReadGuid("GUID");
        }

        [Parser(Opcode.CMSG_CALENDAR_COMPLAIN, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleCalendarComplain434(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
            packet.Translator.ReadInt64("Event ID");
            packet.Translator.ReadInt64("Invite ID");
        }

        [Parser(Opcode.SMSG_CALENDAR_SEND_NUM_PENDING)]
        public static void HandleSendCalendarNumPending(Packet packet)
        {
            packet.Translator.ReadInt32("Pending Invites");
        }

        [Parser(Opcode.CMSG_CALENDAR_EVENT_RSVP)]
        public static void HandleCalendarRsvp(Packet packet)
        {
            packet.Translator.ReadInt64("Event ID");
            packet.Translator.ReadInt64("Invite ID");
            packet.Translator.ReadInt32E<CalendarEventStatus>("Status");
        }

        [Parser(Opcode.SMSG_CALENDAR_RAID_LOCKOUT_ADDED)]
        public static void HandleRaidLockoutAdded(Packet packet)
        {
            packet.Translator.ReadPackedTime("Time");
            packet.Translator.ReadInt32<MapId>("Map ID");
            packet.Translator.ReadInt32E<MapDifficulty>("Difficulty");
            packet.Translator.ReadInt32("Reset Time");
            packet.Translator.ReadGuid("Instance ID");
        }

        [Parser(Opcode.SMSG_CALENDAR_RAID_LOCKOUT_REMOVED)]
        public static void HandleRaidLockoutRemoved(Packet packet)
        {
            packet.Translator.ReadInt32<MapId>("Map ID");
            packet.Translator.ReadInt32E<MapDifficulty>("Difficulty");
            packet.Translator.ReadInt32("Reset Time");
            packet.Translator.ReadGuid("Instance ID");
        }

        [Parser(Opcode.SMSG_CALENDAR_RAID_LOCKOUT_UPDATED)]
        public static void HandleCalendarRaidLockoutUpdated(Packet packet)
        {
            packet.Translator.ReadPackedTime("Time");
            packet.Translator.ReadInt32("Map ID");
            packet.Translator.ReadInt32("Difficulty");
            packet.Translator.ReadInt32("Time changed (in seconds)");
            packet.Translator.ReadInt32("Reset time");
        }

        [Parser(Opcode.SMSG_CALENDAR_EVENT_INVITE_ALERT)]
        public static void HandleCalendarEventInviteAlert(Packet packet)
        {
            packet.Translator.ReadInt64("Event ID");
            packet.Translator.ReadCString("Title");
            packet.Translator.ReadPackedTime("Time");
            packet.Translator.ReadInt32E<CalendarFlag>("Event Flags");
            packet.Translator.ReadInt32E<CalendarEventType>("Type");
            packet.Translator.ReadInt32<LFGDungeonId>("Dungeon ID");
            packet.Translator.ReadInt64("Invite ID");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_3_0a_15050))
            {
                packet.Translator.ReadGuid("Guild GUID");
            }
            packet.Translator.ReadByteE<CalendarEventStatus>("Status");
            packet.Translator.ReadByteE<CalendarModerationRank>("Moderation Rank");
            packet.Translator.ReadPackedGuid("Creator GUID");
            packet.Translator.ReadPackedGuid("Sender GUID");
        }

        [Parser(Opcode.SMSG_CALENDAR_EVENT_REMOVED_ALERT)]
        public static void HandleCalendarEventRemoveAlert(Packet packet)
        {
            packet.Translator.ReadByte("Unk (RemovedAlert)");
            packet.Translator.ReadInt64("Event ID");
            packet.Translator.ReadPackedTime("Event Time");
        }

        [Parser(Opcode.SMSG_CALENDAR_EVENT_UPDATED_ALERT)]
        public static void HandleCalendarEventUpdateAlert(Packet packet)
        {
            packet.Translator.ReadByte("Unk byte (UpdatedAlert)");
            packet.Translator.ReadInt64("Event ID");
            packet.Translator.ReadPackedTime("Event Time");
            packet.Translator.ReadInt32E<CalendarFlag>("Event Flags");
            packet.Translator.ReadPackedTime("Time2");
            packet.Translator.ReadByteE<CalendarEventType>("Event Type");
            packet.Translator.ReadInt32<LFGDungeonId>("Dungeon ID");
            packet.Translator.ReadCString("Title");
            packet.Translator.ReadCString("Description");
            packet.Translator.ReadByteE<CalendarRepeatType>("Repeat Type");
            packet.Translator.ReadInt32("Max Invites");
            packet.Translator.ReadInt32("Unk int32 (UpdatedAlert)");
        }

        [Parser(Opcode.SMSG_CALENDAR_COMMAND_RESULT)]
        public static void HandleCalendarCommandResult(Packet packet)
        {
            packet.Translator.ReadInt32("UnkInt1");
            packet.Translator.ReadCString("UnkString1");
            packet.Translator.ReadCString("Param 1"); // if %s is used in the error message
            packet.Translator.ReadInt32E<CalendarError>("Error");
        }

        [Parser(Opcode.SMSG_CALENDAR_EVENT_INVITE_REMOVED_ALERT)]
        public static void HandleCalendarEventInviteRemoveAlert(Packet packet)
        {
            packet.Translator.ReadInt64("Event ID");
            packet.Translator.ReadPackedTime("Event Time");
            packet.Translator.ReadInt32E<CalendarFlag>("Event Flags");
            packet.Translator.ReadByteE<CalendarEventStatus>("Status");
        }

        [Parser(Opcode.SMSG_CALENDAR_EVENT_INVITE_STATUS_ALERT)]
        public static void HandleCalendarEventInviteStatusAlert(Packet packet)
        {
            packet.Translator.ReadInt64("Event ID");
            packet.Translator.ReadPackedTime("Event Time");
            packet.Translator.ReadInt32("Unk flag"); // (v38 & 0x440) != 0
            packet.Translator.ReadBool("DeletePendingInvite");
        }

        [Parser(Opcode.CMSG_CALENDAR_EVENT_SIGN_UP)]
        public static void HandleCalendarEventSignup(Packet packet)
        {
            packet.Translator.ReadInt64("Event ID");
            packet.Translator.ReadByteE<CalendarEventStatus>("Status");
        }

        [Parser(Opcode.CMSG_CALENDAR_GET_CALENDAR)]
        [Parser(Opcode.CMSG_CALENDAR_GET_NUM_PENDING)]
        [Parser(Opcode.SMSG_CALENDAR_CLEAR_PENDING_ACTION)]
        public static void HandleCalenderNull(Packet packet)
        {
        }
    }
}
