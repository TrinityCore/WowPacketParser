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
                packet.ReadInt64("Invite ID " + i);

                packet.ReadInt64("Event ID " + i);

                packet.ReadByte("Invite Status " + i);

                packet.ReadByte("Moderation Rank " + i);

                packet.ReadByte("Unk Byte 3 " + i);

                packet.ReadPackedGuid("Creator GUID " + i);
            }

            var eventCount = packet.ReadInt32("Event Count");

            for (var i = 0; i < eventCount; i++)
            {
                packet.ReadInt64("Event ID " + i);

                packet.ReadCString("Event Title " + i);

                packet.ReadInt32("Event Type " + i);

                packet.ReadInt32("Occurrence Time " + i);

                var flags = packet.ReadInt32();
                Console.WriteLine("Event Flags " + i + ": 0x" + flags.ToString("X8"));

                packet.ReadInt32("Unk Int32 1 " + i); // Linked to dungeons?

                packet.ReadPackedGuid("Creator GUID " + i);
            }

            packet.ReadInt32("Unk Int32 2");

            packet.ReadPackedTime("Current Time");

            var instanceResetCount = packet.ReadInt32("Instance Reset Count");

            for (var i = 0; i < instanceResetCount; i++)
            {
                packet.ReadInt32("Map ID " + i);

                packet.ReadEnum<MapDifficulty>("Difficulty " + i, TypeCode.Int32);

                packet.ReadInt32("Reset Time " + i);

                packet.ReadInt64("Instance ID " + i);
            }

            packet.ReadTime("Constant Date");

            var raidResetCount = packet.ReadInt32("Raid Reset Count");

            for (var i = 0; i < raidResetCount; i++)
            {
                packet.ReadInt32("Map ID " + i);

                packet.ReadInt32("Reset Time " + i);

                packet.ReadInt32("Unk Time " + i); // Remaining time?
            }

            var holidayCount = packet.ReadInt32("Holiday Count");

            for (var i = 0; i < holidayCount; i++)
            {
                packet.ReadInt32("Unk Int32 5 " + i);

                packet.ReadInt32("Unk Int32 6 " + i);

                packet.ReadInt32("Unk Int32 7 " + i);

                packet.ReadInt32("Unk Int32 8 " + i);

                packet.ReadInt32("Unk Int32 9 " + i);

                for (var j = 0; j < 26; j++)
                    packet.ReadInt32("Unk Int32 10 " + j);

                for (var j = 0; j < 10; j++)
                    packet.ReadInt32("Unk Int32 11 " + j);

                for (var j = 0; j < 10; j++)
                    packet.ReadInt32("Unk Int32 12 " + j);

                packet.ReadCString("Holiday Name " + i);
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
            packet.ReadByte("Invite Type");

            ReadCalendarEventCreationValues(packet);

            var invCount = packet.ReadInt32();
            Console.WriteLine("Invite Count: " + invCount);

            for (var i = 0; i < invCount; i++)
            {
                packet.ReadPackedGuid("Invited Player GUID " + i);

                packet.ReadByte("Player Level " + i);

                packet.ReadByte("Invite Status " + i);

                packet.ReadByte("Moderation Rank " + i);

                packet.ReadByte("Unk Byte 4 " + i);

                packet.ReadInt64("Invite ID " + i);

                packet.ReadInt32("Unk Int32 " + i);

                packet.ReadCString("Invite Text " + i);
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
            var id = packet.ReadInt32();
            Console.WriteLine("Unk Int32: " + id);
        }

        [Parser(Opcode.SMSG_CALENDAR_ARENA_TEAM)]
        [Parser(Opcode.SMSG_CALENDAR_FILTER_GUILD)]
        public static void HandleCalendarFilters(Packet packet)
        {
            var count = packet.ReadInt32("Count");

            for (var i = 0; i < count; i++)
            {
                packet.ReadPackedGuid("GUID " + i);;
                packet.ReadByte("Unk Byte " + i);
            }
        }

        public static int ReadCalendarEventCreationValues(Packet packet)
        {
            if (packet.GetOpcode() != Opcode.CMSG_CALENDAR_ADD_EVENT)
            {
                packet.ReadInt64("Event ID");
                packet.ReadGuid("Creator GUID");
            }

            packet.ReadCString("Title");

            packet.ReadCString("Description");

            packet.ReadByte("Type");

            packet.ReadBoolean("Repeating");

            packet.ReadInt32("Max Invites");

            packet.ReadInt32("Dungeon ID");

            packet.ReadInt32("Unk Int32 2"); // Creation time?

            packet.ReadInt32("Event Time");

            var flags = packet.ReadInt32();
            Console.WriteLine("Flags: 0x" + flags.ToString("X8"));

            return flags;
        }

        [Parser(Opcode.CMSG_CALENDAR_ADD_EVENT)]
        public static void HandleAddCalendarEvent(Packet packet)
        {
            var flags = ReadCalendarEventCreationValues(packet);

            if (((flags >> 6) & 1) != 0)
                return;

            var count = packet.ReadInt32("Invite Count");

            if (count <= 0)
                return;

            packet.ReadPackedGuid("Creator GUID");
            packet.ReadByte("Invite Status");
            packet.ReadByte("Moderation Rank");
        }

        [Parser(Opcode.CMSG_CALENDAR_UPDATE_EVENT)]
        public static void HandleUpdateCalendarEvent(Packet packet)
        {
            ReadCalendarEventCreationValues(packet);
        }

        [Parser(Opcode.CMSG_CALENDAR_REMOVE_EVENT)]
        public static void HandleRemoveCalendarEvent(Packet packet)
        {
            packet.ReadInt64("Event ID");
            packet.ReadGuid("Creator GUID");
            packet.ReadInt32("Unk Int32");
        }

        [Parser(Opcode.CMSG_CALENDAR_COPY_EVENT)]
        public static void HandleCopyCalendarEvent(Packet packet)
        {
            packet.ReadInt64("Old Event ID");
            packet.ReadInt64("New Event ID");
            packet.ReadInt32("Unk Int32");
        }

        [Parser(Opcode.CMSG_CALENDAR_EVENT_INVITE)]
        public static void HandleAddCalendarEventInvite(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadInt64("Event ID");
            packet.ReadCString("Text");
            packet.ReadByte("Invite Status");
            packet.ReadByte("Moderation Rank");
        }

        [Parser(Opcode.SMSG_CALENDAR_EVENT_INVITE)]
        public static void HandleSendCalendarEventInvite(Packet packet)
        {
            packet.ReadPackedGuid("GUID");
            packet.ReadInt64("Invite ID");
            packet.ReadInt64("Event ID");
            packet.ReadByte("Unk Byte 1");
            packet.ReadByte("Unk Byte 2");

            var unk3 = packet.ReadBoolean("Unk Boolean");

            if (unk3)
                packet.ReadInt32("Unk Int32");

            packet.ReadByte("Unk Byte 3");
        }

        [Parser(Opcode.CMSG_CALENDAR_EVENT_INVITE_NOTES)]
        public static void HandleCalendarUpdateInviteList(Packet packet)
        {
            packet.ReadPackedGuid("GUID");
            packet.ReadInt64("Invite ID");
            packet.ReadCString("Invite Text");
            packet.ReadBoolean("Unk Boolean");
        }

        [Parser(Opcode.SMSG_CALENDAR_EVENT_INVITE_NOTES_ALERT)]
        public static void HandleCalendarUpdateInviteList2(Packet packet)
        {
            packet.ReadInt64("Invite ID");
            packet.ReadCString("Invite Text");
        }

        [Parser(Opcode.SMSG_CALENDAR_EVENT_INVITE_NOTES)]
        public static void HandleCalendarUpdateInviteList3(Packet packet)
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
            var guid = packet.ReadPackedGuid();
            Console.WriteLine("GUID: " + guid);

            var eventId = packet.ReadInt64();
            Console.WriteLine("Event ID: " + eventId);

            var unk1 = packet.ReadInt64();
            Console.WriteLine("Unk Int64 1: " + unk1);

            var unk2 = packet.ReadInt64();
            Console.WriteLine("Unk Int64 2: " + unk2);
        }

        [Parser(Opcode.SMSG_CALENDAR_EVENT_INVITE_REMOVED)]
        public static void HandleSendCalendarEventInviteRemoved(Packet packet)
        {
            packet.ReadPackedGuid("GUID");

            packet.ReadInt64("Invite ID");

            var flags = packet.ReadInt32();
            Console.WriteLine("Flags: 0x" + flags.ToString("X8"));

            packet.ReadByte("Unk Byte");
        }

        [Parser(Opcode.CMSG_CALENDAR_EVENT_STATUS)]
        [Parser(Opcode.CMSG_CALENDAR_EVENT_MODERATOR_STATUS)]
        public static void HandleCalendarEventStatus(Packet packet)
        {
            packet.ReadPackedGuid("GUID");

            packet.ReadInt64("Event ID");

            packet.ReadInt64("Unk Int64 1");

            packet.ReadInt64("Unk Int64 2");

            packet.ReadInt32("Unk Int32");
        }

        [Parser(Opcode.SMSG_CALENDAR_EVENT_STATUS)]
        public static void HandleSendCalendarEventStatus(Packet packet)
        {
            packet.ReadPackedGuid("GUID");

            packet.ReadInt64("Event ID");

            packet.ReadInt32("Unk Int32 1");

            var flags = packet.ReadInt32();
            Console.WriteLine("Flags: 0x" + flags.ToString("X8"));

            packet.ReadByte("Unk Byte 1");

            packet.ReadByte("Unk Byte 2");

            packet.ReadInt32("Unk Int32 2");
        }

        [Parser(Opcode.SMSG_CALENDAR_EVENT_MODERATOR_STATUS_ALERT)]
        public static void HandleSendCalendarEventModeratorStatus(Packet packet)
        {
            packet.ReadPackedGuid("GUID");

            packet.ReadInt64("Event ID");

            packet.ReadByte("Unk Byte");

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
            packet.ReadGuid("GUID");

            packet.ReadInt64("Event ID");

            packet.ReadInt32("Unk Int32");
        }

        [Parser(Opcode.SMSG_CALENDAR_RAID_LOCKOUT_ADDED)]
        public static void HandleRaidLockoutAdded(Packet packet)
        {
            packet.ReadInt32("Map ID");

            packet.ReadInt32("Unk Int32 1");

            packet.ReadInt32("Unk Int32 2");

            packet.ReadInt32("Reset Time");

            packet.ReadInt64("Instance ID");
        }

        [Parser(Opcode.SMSG_CALENDAR_RAID_LOCKOUT_REMOVED)]
        public static void HandleRaidLockoutRemoved(Packet packet)
        {
            var count = packet.ReadInt32("Count");

            for (var i = 0; i < count; i++)
            {
                packet.ReadInt32("Map ID " + i);
                packet.ReadInt32("Reset Time " + i);
                packet.ReadInt32("Instance ID " + i);
            }
        }
    }
}
