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
                packet.ReadInt64("[" + i + "] Event ID");
                packet.ReadInt64("[" + i + "] Invite ID");
                packet.ReadEnum<CalendarEventStatus>("[" + i + "] Status", TypeCode.Byte);
                packet.ReadEnum<CalendarModerationRank>("[" + i + "] Moderation Rank", TypeCode.Byte);
                packet.ReadBoolean("[" + i + "] Guild Event");
                packet.ReadPackedGuid("[" + i + "] Creator GUID");
            }

            var eventCount = packet.ReadInt32("Event Count");

            for (var i = 0; i < eventCount; i++)
            {
                packet.ReadInt64("[" + i + "] Event ID");
                packet.ReadCString("[" + i + "] Event Title ");
                packet.ReadEnum<CalendarEventType>("[" + i + "] Event Type", TypeCode.Int32);
                packet.ReadPackedTime("[" + i + "] Event Time");
                packet.ReadEnum<CalendarFlag>("[" + i + "] Event Flags", TypeCode.Int32);
                Console.WriteLine("[" + i + "] Dungeon ID: " + Extensions.DungeonLine(packet.ReadInt32()));
                packet.ReadPackedGuid("[" + i + "] Creator GUID");
            }

            packet.ReadTime("Current Time");
            packet.ReadPackedTime("Zone Time?");

            var instanceResetCount = packet.ReadInt32("Instance Reset Count");

            for (var i = 0; i < instanceResetCount; i++)
            {
                Console.WriteLine("[" + i + "] Map ID: " + Extensions.MapLine(packet.ReadInt32()));
                packet.ReadEnum<MapDifficulty>("[" + i + "] Difficulty", TypeCode.Int32);
                packet.ReadInt32("[" + i + "] Time left");
                packet.ReadGuid("[" + i + "] Instance ID");
            }

            packet.ReadTime("Constant Date");

            var raidResetCount = packet.ReadInt32("Raid Reset Count");

            for (var i = 0; i < raidResetCount; i++)
            {
                Console.WriteLine("[" + i + "] Map ID: " + Extensions.MapLine(packet.ReadInt32()));
                packet.ReadInt32("[" + i + "] Time left");
                packet.ReadInt32("[" + i + "] Unk Time");
            }

            var holidayCount = packet.ReadInt32("Holiday Count");

            for (var i = 0; i < holidayCount; i++)
            {
                packet.ReadInt32("[" + i + "] Unk Int32 5");
                packet.ReadInt32("[" + i + "] Unk Int32 6");
                packet.ReadInt32("[" + i + "] Unk Int32 7");
                packet.ReadInt32("[" + i + "] Unk Int32 8");
                packet.ReadInt32("[" + i + "] Unk Int32 9");
                for (var j = 0; j < 26; j++)
                    packet.ReadPackedTime("[" + i + ", " + j + "] Unk Int32 10");
                for (var j = 0; j < 10; j++)
                    packet.ReadInt32("[" + i + ", " + j + "] Unk Int32 11");
                for (var j = 0; j < 10; j++)
                    packet.ReadInt32("[" + i + ", " + j + "] Unk Int32 12");
                packet.ReadCString("[" + i + "] Holiday Name");
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
            packet.ReadBoolean("Repeatable");
            packet.ReadInt32("Max Invites");
            Console.WriteLine("Dungeon ID: " + Extensions.DungeonLine(packet.ReadInt32()));
            packet.ReadPackedTime("Unk PackedTime");
            packet.ReadPackedTime("Event Time");
            var flags = packet.ReadEnum<CalendarFlag>("Event Flags", TypeCode.Int32);
            packet.ReadInt32("Guild");

            if ((flags & CalendarFlag.WithoutInvites) != 0)
                return;

            var invCount = packet.ReadInt32("Invite Count");

            for (var i = 0; i < invCount; i++)
            {
                packet.ReadPackedGuid("[" + i + "] Invitee GUID");
                packet.ReadByte("[" + i + "] Player Level");
                packet.ReadEnum<CalendarEventStatus>("[" + i + "] Status", TypeCode.Byte);
                packet.ReadEnum<CalendarModerationRank>("[" + i + "] Moderation Rank", TypeCode.Byte);
                packet.ReadBoolean("[" + i + "] Guild Member");
                packet.ReadInt64("[" + i + "] Invite ID");
                packet.ReadPackedTime("[" + i + "] Status Time");
                packet.ReadCString("[" + i + "] Invite Text");
            }
        }

        [Parser(Opcode.CMSG_CALENDAR_GUILD_FILTER)]
        public static void HandleCalendarGuildFilter(Packet packet)
        {
            packet.ReadInt32("Unk Int32 1");
            packet.ReadInt32("Unk Int32 2");
            packet.ReadInt32("Unk Int32 3");
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
                packet.ReadPackedGuid("[" + i + "] GUID");
                packet.ReadByte("[" + i + "] Unk Byte");
            }
        }

        [Parser(Opcode.CMSG_CALENDAR_ADD_EVENT)]
        public static void HandleAddCalendarEvent(Packet packet)
        {
            packet.ReadCString("Title");
            packet.ReadCString("Description");
            packet.ReadEnum<CalendarEventType>("Event Type", TypeCode.Byte);
            packet.ReadBoolean("Repeatable");
            packet.ReadInt32("Max Invites");
            Console.WriteLine("Dungeon ID: " + Extensions.DungeonLine(packet.ReadInt32()));
            packet.ReadPackedTime("Event Time");
            packet.ReadPackedTime("Unk PackedTime");

            var flags = packet.ReadEnum<CalendarFlag>("Event Flags", TypeCode.Int32);

            if ((flags & CalendarFlag.WithoutInvites) != 0)
                return;

            var count = packet.ReadInt32("Invite Count");

            if (count <= 0)
                return;

            packet.ReadPackedGuid("Creator GUID");
            packet.ReadEnum<CalendarEventStatus>("Status", TypeCode.Byte);
            packet.ReadEnum<CalendarModerationRank>("Moderation Rank", TypeCode.Byte);
        }

        [Parser(Opcode.CMSG_CALENDAR_UPDATE_EVENT)]
        public static void HandleUpdateCalendarEvent(Packet packet)
        {
            packet.ReadInt64("Event ID");
            packet.ReadInt64("Invite ID");
            packet.ReadCString("Title");
            packet.ReadCString("Description");
            packet.ReadEnum<CalendarEventType>("Event Type", TypeCode.Byte);
            packet.ReadBoolean("Repeatable");
            packet.ReadInt32("Max Invites");
            Console.WriteLine("Dungeon ID: " + Extensions.DungeonLine(packet.ReadInt32()));
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
            packet.ReadEnum<CalendarEventStatus>("Status?", TypeCode.Byte);

            var unk3 = packet.ReadBoolean("Confirmed?");

            if (unk3)
                packet.ReadPackedTime("Confirm Time?");

            packet.ReadByte("Event Pending?");
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
            packet.ReadInt32("Unk Int32 1");
            packet.ReadInt32("Unk Int32 2");
            packet.ReadInt32("Unk Int32 3");
            packet.ReadInt32("Unk Int32 4");
            packet.ReadInt32("Unk Int32 5");
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
            packet.ReadInt64("Inviteee ID");
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
            packet.ReadPackedGuid("Invitede GUID");
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
            Console.WriteLine("Map ID: " + Extensions.MapLine(packet.ReadInt32()));
            packet.ReadEnum<MapDifficulty>("Difficulty", TypeCode.Int32);
            packet.ReadInt32("Reset Time");
            packet.ReadGuid("Instance ID");
        }

        [Parser(Opcode.SMSG_CALENDAR_RAID_LOCKOUT_REMOVED)]
        public static void HandleRaidLockoutRemoved(Packet packet)
        {
            Console.WriteLine("Map ID: " + Extensions.MapLine(packet.ReadInt32()));
            packet.ReadEnum<MapDifficulty>("Difficulty", TypeCode.Int32);
            packet.ReadInt32("Reset Time");
            packet.ReadGuid("Instance ID");
        }

        [Parser(Opcode.SMSG_CALENDAR_EVENT_INVITE_ALERT)]
        public static void HandleCalendarEventInviteAlert(Packet packet)
        {
            packet.ReadInt64("Event ID");
            packet.ReadCString("Title");
            packet.ReadPackedTime("Time");
            packet.ReadEnum<CalendarFlag>("Event Flags", TypeCode.Int32);
            packet.ReadEnum<CalendarEventType>("Type", TypeCode.Int32);
            Console.WriteLine("Dungeon ID: " + Extensions.DungeonLine(packet.ReadInt32()));
            packet.ReadInt64("Invite ID");
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
            Console.WriteLine("Dungeon ID: " + Extensions.DungeonLine(packet.ReadInt32()));
            packet.ReadCString("Title");
            packet.ReadCString("Description");
            packet.ReadBoolean("Repeatable");
            packet.ReadInt32("Max Invites");
            packet.ReadInt32("Unk int32 (UpdatedAlert)");
        }

        //[Parser(Opcode.SMSG_CALENDAR_COMMAND_RESULT)]
        public static void HandleCalendarCommandResult(Packet packet)
        {
          /* Find correct order
            packet.ReadInt32("Unk 3");
            packet.ReadByte("Unk 1");
            packet.ReadInt32("Unk 4");
            packet.ReadByte("Unk 2");
          */
        }

        [Parser(Opcode.SMSG_CALENDAR_EVENT_INVITE_REMOVED_ALERT)]
        public static void HandleCalendarEventInviteRemoveAlert(Packet packet)
        {
            packet.ReadInt64("Event ID");
            packet.ReadPackedTime("Event Time");
            packet.ReadEnum<CalendarFlag>("Event Flags", TypeCode.Int32);
            packet.ReadEnum<CalendarEventStatus>("Status", TypeCode.Byte);
        }

        //[Parser(Opcode.SMSG_CALENDAR_EVENT_INVITE_STATUS_ALERT)]
        public static void HandleCalendarEventInviteStatusAlert(Packet packet)
        {
        }

        //[Parser(Opcode.SMSG_CALENDAR_RAID_LOCKOUT_UPDATED)]
        public static void HandleCalendarRaidLockoutUpdated(Packet packet)
        {
        }

        [Parser(Opcode.CMSG_CALENDAR_EVENT_SIGNUP)]
        public static void HandleCalendarEventSignup(Packet packet)
        {
            packet.ReadInt64("Event ID");
            packet.ReadEnum<CalendarEventStatus>("Status", TypeCode.Byte);
        }
    }
}
