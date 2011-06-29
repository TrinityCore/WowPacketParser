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
            var invCount = packet.ReadInt32();
            Console.WriteLine("Invite Count: " + invCount);

            for (var i = 0; i < invCount; i++)
            {
                var invId = packet.ReadInt64();
                Console.WriteLine("Invite ID " + i + ": " + invId);

                var eventId = packet.ReadInt64();
                Console.WriteLine("Event ID " + i + ": " + eventId);

                var unk1 = packet.ReadByte();
                Console.WriteLine("Unk Byte 1 " + i + ": " + unk1);

                var unk2 = packet.ReadByte();
                Console.WriteLine("Unk Byte 2 " + i + ": " + unk2);

                var unk3 = packet.ReadByte();
                Console.WriteLine("Unk Byte 3 " + i + ": " + unk3);

                var creatorGuid = packet.ReadPackedGuid();
                Console.WriteLine("Creator GUID " + i + ": " + creatorGuid);
            }

            var evtCount = packet.ReadInt32();
            Console.WriteLine("Event Count: " + evtCount);

            for (var i = 0; i < evtCount; i++)
            {
                var evtId = packet.ReadInt64();
                Console.WriteLine("Event ID " + i + ": " + evtId);

                var title = packet.ReadCString();
                Console.WriteLine("Event Title " + i + ": " + title);

                var type = packet.ReadInt32();
                Console.WriteLine("Event Type " + i + ": " + type);

                var occurrence = packet.ReadInt32();
                Console.WriteLine("Occurrence Time " + i + ": " + occurrence);

                var flags = packet.ReadInt32();
                Console.WriteLine("Event Flags " + i + ": 0x" + flags.ToString("X8"));

                var unk = packet.ReadInt32();
                Console.WriteLine("Unk Int32 1 " + i + ": " + unk);

                var creatorGuid = packet.ReadPackedGuid();
                Console.WriteLine("Creator GUID " + i + ": " + creatorGuid);
            }

            var unkInt = packet.ReadInt32();
            Console.WriteLine("Unk Int32 2: " + unkInt);

            var curTime = packet.ReadPackedTime();
            Console.WriteLine("Current Time: " + curTime);

            var instSaveCnt = packet.ReadInt32();
            Console.WriteLine("Instance Save Count: " + instSaveCnt);

            for (var i = 0; i < instSaveCnt; i++)
            {
                var mapId = packet.ReadInt32();
                Console.WriteLine("Map ID " + i + ": " + mapId);

                var difficulty = (MapDifficulty)packet.ReadInt32();
                Console.WriteLine("Difficulty " + i + ": " + difficulty);

                var resetTime = packet.ReadInt32();
                Console.WriteLine("Reset Time " + i + ": " + resetTime);

                var instanceId = packet.ReadInt64();
                Console.WriteLine("Instance ID " + i + ": " + instanceId);
            }

            var unkDate = packet.ReadTime();
            Console.WriteLine("Constant Date: " + unkDate);

            var raidResetCnt = packet.ReadInt32();
            Console.WriteLine("Raid Reset Count: " + raidResetCnt);

            for (var i = 0; i < raidResetCnt; i++)
            {
                var mapId = packet.ReadInt32();
                Console.WriteLine("Map ID " + i + ": " + mapId);

                var resetTime = packet.ReadInt32();
                Console.WriteLine("Reset Time " + i + ": " + resetTime);

                var unkTime = packet.ReadInt32();
                Console.WriteLine("Unk Int32 4 " + i + ": " + unkTime);
            }

            var holidayCnt = packet.ReadInt32();
            Console.WriteLine("Holiday Count: " + holidayCnt);

            for (var i = 0; i < holidayCnt; i++)
            {
                var unk1 = packet.ReadInt32();
                Console.WriteLine("Unk Int32 5 " + i + ": " + unk1);

                var unk2 = packet.ReadInt32();
                Console.WriteLine("Unk Int32 6 " + i + ": " + unk2);

                var unk3 = packet.ReadInt32();
                Console.WriteLine("Unk Int32 7 " + i + ": " + unk3);

                var unk4 = packet.ReadInt32();
                Console.WriteLine("Unk Int32 8 " + i + ": " + unk4);

                var unk5 = packet.ReadInt32();
                Console.WriteLine("Unk Int32 9 " + i + ": " + unk5);

                for (var j = 0; j < 26; j++)
                {
                    var unk = packet.ReadInt32();
                    Console.WriteLine("Unk Int32 10 " + j + ": " + unk);
                }

                for (var j = 0; j < 10; j++)
                {
                    var unk = packet.ReadInt32();
                    Console.WriteLine("Unk Int32 11 " + j + ": " + unk);
                }

                for (var j = 0; j < 10; j++)
                {
                    var unk = packet.ReadInt32();
                    Console.WriteLine("Unk Int32 12 " + j + ": " + unk);
                }

                var name = packet.ReadCString();
                Console.WriteLine("Holiday Name " + i + ": " + name);
            }
        }

        [Parser(Opcode.CMSG_CALENDAR_GET_EVENT)]
        public static void HandleGetCalendarEvent(Packet packet)
        {
            var eventId = packet.ReadInt64();
            Console.WriteLine("Event ID: " + eventId);
        }

        [Parser(Opcode.SMSG_CALENDAR_SEND_EVENT)]
        public static void HandleSendCalendarEvent(Packet packet)
        {
            var unk = packet.ReadByte();
            Console.WriteLine("Unk Byte: " + unk);

            ReadCalendarEventCreationValues(packet);

            var invCount = packet.ReadInt32();
            Console.WriteLine("Invite Count: " + invCount);

            for (var i = 0; i < invCount; i++)
            {
                var guid = packet.ReadPackedGuid();
                Console.WriteLine("GUID " + i + ": " + guid);

                var unk1 = packet.ReadByte();
                Console.WriteLine("Unk Byte 1 " + i + ": " + unk1);

                var unk2 = packet.ReadByte();
                Console.WriteLine("Unk Byte 2 " + i + ": " + unk2);

                var unk3 = packet.ReadByte();
                Console.WriteLine("Unk Byte 3 " + i + ": " + unk3);

                var unk4 = packet.ReadByte();
                Console.WriteLine("Unk Byte 4 " + i + ": " + unk4);

                var unk64 = packet.ReadInt64();
                Console.WriteLine("Invite ID " + i + ": " + unk64);

                var unk32 = packet.ReadInt32();
                Console.WriteLine("Unk Int32 " + ": " + unk32);

                var text = packet.ReadCString();
                Console.WriteLine("Invite Text " + i + ": " + text);
            }
        }

        [Parser(Opcode.CMSG_CALENDAR_GUILD_FILTER)]
        public static void HandleCalendarGuildFilter(Packet packet)
        {
            var unk1 = packet.ReadInt32();
            Console.WriteLine("Unk Int32 1: " + unk1);

            var unk2 = packet.ReadInt32();
            Console.WriteLine("Unk Int32 2: " + unk2);

            var unk3 = packet.ReadInt32();
            Console.WriteLine("Unk Int32 3: " + unk3);
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
            var count = packet.ReadInt32();
            Console.WriteLine("Count: " + count);

            for (var i = 0; i < count; i++)
            {
                var guid = packet.ReadPackedGuid();
                Console.WriteLine("GUID " + i + ": " + guid);

                var unk = packet.ReadByte();
                Console.WriteLine("Unk Byte " + i + ": " + unk);
            }
        }

        public static int ReadCalendarEventCreationValues(Packet packet)
        {
            if (packet.GetOpcode() != Opcode.CMSG_CALENDAR_ADD_EVENT)
            {
                var id = packet.ReadInt64();
                Console.WriteLine("Event ID: " + id);

                var creator = packet.ReadGuid();
                Console.WriteLine("Creator GUID: " + creator);
            }

            var title = packet.ReadCString();
            Console.WriteLine("Title: " + title);

            var description = packet.ReadCString();
            Console.WriteLine("Description: " + description);

            var type = packet.ReadByte();
            Console.WriteLine("Type: " + type);

            var unk1 = packet.ReadByte();
            Console.WriteLine("Unk Byte 1: " + unk1);

            var maxInv = packet.ReadInt32();
            Console.WriteLine("Max Invites: " + maxInv);

            var unk2 = packet.ReadInt32();
            Console.WriteLine("Unk Int32 1: " + unk2);

            var date1 = packet.ReadInt32();
            Console.WriteLine("Unk Int32 2: " + date1);

            var date2 = packet.ReadInt32();
            Console.WriteLine("Unk Int32 3: " + date2);

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

            var count = packet.ReadInt32();
            Console.WriteLine("Invite Count: " + count);

            if (count <= 0)
                return;

            var guid = packet.ReadPackedGuid();
            Console.WriteLine("GUID: " + guid);

            var status = packet.ReadByte();
            Console.WriteLine("Status: " + status);

            var rank = packet.ReadByte();
            Console.WriteLine("Rank: " + rank);
        }

        [Parser(Opcode.CMSG_CALENDAR_UPDATE_EVENT)]
        public static void HandleUpdateCalendarEvent(Packet packet)
        {
            ReadCalendarEventCreationValues(packet);
        }

        [Parser(Opcode.CMSG_CALENDAR_REMOVE_EVENT)]
        public static void HandleRemoveCalendarEvent(Packet packet)
        {
            var eventId = packet.ReadInt64();
            Console.WriteLine("Event ID: " + eventId);

            var creatorGuid = packet.ReadGuid();
            Console.WriteLine("Creator GUID: " + creatorGuid);

            var unk = packet.ReadInt32();
            Console.WriteLine("Unk Int32: " + unk);
        }

        [Parser(Opcode.CMSG_CALENDAR_COPY_EVENT)]
        public static void HandleCopyCalendarEvent(Packet packet)
        {
            var oldId = packet.ReadInt64();
            Console.WriteLine("Old Event ID: " + oldId);

            var newId = packet.ReadInt64();
            Console.WriteLine("New Event ID: " + newId);

            var date = packet.ReadInt32();
            Console.WriteLine("Unk Int32: " + date);
        }

        [Parser(Opcode.CMSG_CALENDAR_EVENT_INVITE)]
        public static void HandleAddCalendarEventInvite(Packet packet)
        {
            var guid = packet.ReadGuid();
            Console.WriteLine("GUID: " + guid);

            var eventId = packet.ReadInt64();
            Console.WriteLine("Event ID: " + eventId);

            var text = packet.ReadCString();
            Console.WriteLine("Text: " + text);

            var unk1 = packet.ReadByte();
            Console.WriteLine("Unk Byte 1: " + unk1);

            var unk2 = packet.ReadByte();
            Console.WriteLine("Unk Byte 2: " + unk2);
        }

        [Parser(Opcode.SMSG_CALENDAR_EVENT_INVITE)]
        public static void HandleSendCalendarEventInvite(Packet packet)
        {
            var guid = packet.ReadPackedGuid();
            Console.WriteLine("GUID: " + guid);

            var inviteId = packet.ReadInt64();
            Console.WriteLine("Invite ID: " + inviteId);

            var eventId = packet.ReadInt64();
            Console.WriteLine("Event ID: " + eventId);

            var unk1 = packet.ReadByte();
            Console.WriteLine("Unk Byte 1: " + unk1);

            var unk2 = packet.ReadByte();
            Console.WriteLine("Unk Byte 2: " + unk2);

            var unk3 = packet.ReadBoolean();
            Console.WriteLine("Unk Boolean: " + unk3);

            if (unk3)
            {
                var unk32 = packet.ReadInt32();
                Console.WriteLine("Unk Int32: " + unk32);
            }

            var unk4 = packet.ReadByte();
            Console.WriteLine("Unk Byte 3: " + unk4);
        }

        [Parser(Opcode.SMSG_CALENDAR_UPDATE_INVITE_LIST)]
        public static void HandleCalendarUpdateInviteList(Packet packet)
        {
            var guid = packet.ReadPackedGuid();
            Console.WriteLine("GUID: " + guid);

            var inviteId = packet.ReadInt64();
            Console.WriteLine("Invite ID: " + inviteId);

            var text = packet.ReadCString();
            Console.WriteLine("Invite Text: " + text);

            var unk = packet.ReadBoolean();
            Console.WriteLine("Unk Boolean: " + unk);
        }

        [Parser(Opcode.SMSG_CALENDAR_UPDATE_INVITE_LIST2)]
        public static void HandleCalendarUpdateInviteList2(Packet packet)
        {
            var invId = packet.ReadInt64();
            Console.WriteLine("Invite ID: " + invId);

            var text = packet.ReadCString();
            Console.WriteLine("Invite Text: " + text);
        }

        [Parser(Opcode.SMSG_CALENDAR_UPDATE_INVITE_LIST3)]
        public static void HandleCalendarUpdateInviteList3(Packet packet)
        {
            var unk1 = packet.ReadInt32();
            Console.WriteLine("Unk Int32 1: " + unk1);

            var unk2 = packet.ReadInt32();
            Console.WriteLine("Unk Int32 2: " + unk2);

            var unk3 = packet.ReadInt32();
            Console.WriteLine("Unk Int32 3: " + unk3);

            var unk4 = packet.ReadInt32();
            Console.WriteLine("Unk Int32 4: " + unk4);

            var unk5 = packet.ReadInt32();
            Console.WriteLine("Unk Int32 5: " + unk5);
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
            var guid = packet.ReadPackedGuid();
            Console.WriteLine("GUID: " + guid);

            var inviteId = packet.ReadInt64();
            Console.WriteLine("Invite ID: " + inviteId);

            var flags = packet.ReadInt32();
            Console.WriteLine("Flags: 0x" + flags.ToString("X8"));

            var unk = packet.ReadByte();
            Console.WriteLine("Unk Byte: " + unk);
        }

        [Parser(Opcode.CMSG_CALENDAR_EVENT_STATUS)]
        [Parser(Opcode.CMSG_CALENDAR_EVENT_MODERATOR_STATUS)]
        public static void HandleCalendarEventStatus(Packet packet)
        {
            var guid = packet.ReadPackedGuid();
            Console.WriteLine("GUID: " + guid);

            var eventId = packet.ReadInt64();
            Console.WriteLine("Event ID: " + eventId);

            var unk1 = packet.ReadInt64();
            Console.WriteLine("Unk Int64 1: " + unk1);

            var unk2 = packet.ReadInt64();
            Console.WriteLine("Unk Int64 2: " + unk2);

            var unk3 = packet.ReadInt32();
            Console.WriteLine("Unk Int32: " + unk3);
        }

        [Parser(Opcode.SMSG_CALENDAR_EVENT_STATUS)]
        public static void HandleSendCalendarEventStatus(Packet packet)
        {
            var guid = packet.ReadPackedGuid();
            Console.WriteLine("GUID: " + guid);

            var eventId = packet.ReadInt64();
            Console.WriteLine("Event ID: " + eventId);

            var unk = packet.ReadInt32();
            Console.WriteLine("Unk Int32 1: " + unk);

            var flags = packet.ReadInt32();
            Console.WriteLine("Flags: 0x" + flags.ToString("X8"));

            var unk1 = packet.ReadByte();
            Console.WriteLine("Unk Byte 1: " + unk1);

            var unk2 = packet.ReadByte();
            Console.WriteLine("Unk Byte 2: " + unk2);

            var unk32 = packet.ReadInt32();
            Console.WriteLine("Unk Int32 2: " + unk32);
        }

        [Parser(Opcode.SMSG_CALENDAR_EVENT_MODERATOR_STATUS_ALERT)]
        public static void HandleSendCalendarEventModeratorStatus(Packet packet)
        {
            var guid = packet.ReadPackedGuid();
            Console.WriteLine("GUID: " + guid);

            var eventId = packet.ReadInt64();
            Console.WriteLine("Event ID: " + eventId);

            var unk1 = packet.ReadByte();
            Console.WriteLine("Unk Byte: " + unk1);

            var unk2 = packet.ReadBoolean();
            Console.WriteLine("Unk Boolean: " + unk2);
        }

        [Parser(Opcode.CMSG_CALENDAR_COMPLAIN)]
        public static void HandleCalendarComplain(Packet packet)
        {
            var eventId = packet.ReadInt64();
            Console.WriteLine("Event ID: " + eventId);

            var guid = packet.ReadGuid();
            Console.WriteLine("GUID: " + guid);
        }

        [Parser(Opcode.SMSG_CALENDAR_SEND_NUM_PENDING)]
        public static void HandleSendCalendarNumPending(Packet packet)
        {
            var count = packet.ReadInt32();
            Console.WriteLine("Pending Invites: " + count);
        }

        [Parser(Opcode.CMSG_CALENDAR_EVENT_RSVP)]
        public static void HandleCalendarRsvp(Packet packet)
        {
            var guid = packet.ReadGuid();
            Console.WriteLine("GUID: " + guid);

            var eventId = packet.ReadInt64();
            Console.WriteLine("Event ID: " + eventId);

            var unk = packet.ReadInt32();
            Console.WriteLine("Unk Int32: " + unk);
        }

        [Parser(Opcode.SMSG_CALENDAR_RAID_LOCKOUT_ADDED)]
        public static void HandleRaidLockoutAdded(Packet packet)
        {
            var mapId = packet.ReadInt32();
            Console.WriteLine("Map ID: " + mapId);

            var unk1 = packet.ReadInt32();
            Console.WriteLine("Unk Int32 1: " + unk1);

            var unk2 = packet.ReadInt32();
            Console.WriteLine("Unk Int32 2: " + unk2);

            var time = packet.ReadInt32();
            Console.WriteLine("Reset Time: " + time);

            var instanceId = packet.ReadInt64();
            Console.WriteLine("Instance ID: " + instanceId);
        }

        [Parser(Opcode.SMSG_CALENDAR_RAID_LOCKOUT_REMOVED)]
        public static void HandleRaidLockoutRemoved(Packet packet)
        {
            var count = packet.ReadInt32();
            Console.WriteLine("Count: " + count);

            if (count < 0)
                return;

            for (var i = 0; i < count; i++)
            {
                var mapId = packet.ReadInt32();
                Console.WriteLine("Map ID " + i + ": " + mapId);

                var resetTime = packet.ReadInt32();
                Console.WriteLine("Reset Time " + i + ": " + resetTime);

                var unk = packet.ReadInt32();
                Console.WriteLine("Instance ID " + i + ": " + unk);
            }
        }
    }
}
