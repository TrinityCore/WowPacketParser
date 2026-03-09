using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V12_0_0_65390.Parsers
{
    public static class CalendarHandler
    {
        [Parser(Opcode.SMSG_CALENDAR_RAID_LOCKOUT_ADDED)]
        public static void HandleRaidLockoutAdded(Packet packet)
        {
            packet.ReadPackedTime("ServerTime");
            packet.ReadInt32<MapId>("MapID");
            packet.ReadInt16<DifficultyId>("DifficultyID");
            packet.ReadInt32("TimeRemaining");
            packet.ReadUInt64("InstanceID");
        }

        [Parser(Opcode.SMSG_CALENDAR_RAID_LOCKOUT_REMOVED)]
        public static void HandleRaidLockoutRemoved(Packet packet)
        {
            packet.ReadInt32<MapId>("MapID");
            packet.ReadInt16<DifficultyId>("DifficultyID");
            packet.ReadUInt64("InstanceID");
        }

        [Parser(Opcode.SMSG_CALENDAR_RAID_LOCKOUT_UPDATED)]
        public static void HandleCalendarRaidLockoutUpdated(Packet packet)
        {
            packet.ReadPackedTime("ServerTime");
            packet.ReadInt32<MapId>("MapID");
            packet.ReadInt16<DifficultyId>("DifficultyID");
            packet.ReadInt32("NewTimeRemaining");
            packet.ReadInt32("OldTimeRemaining");
        }

        public static void ReadCalendarSendCalendarRaidLockoutInfo(Packet packet, params object[] indexes)
        {
            packet.ReadInt32<MapId>("MapID", indexes);
            packet.ReadInt16<DifficultyId>("DifficultyID", indexes);
            packet.ReadInt32("ExpireTime", indexes);
            packet.ReadInt64("InstanceID", indexes);
        }

        [Parser(Opcode.SMSG_CALENDAR_SEND_CALENDAR)]
        public static void HandleCalendarSendCalendar(Packet packet)
        {
            packet.ReadPackedTime("ServerTime");

            var invitesCount = packet.ReadUInt32("InvitesCount");
            var eventsCount = packet.ReadUInt32("EventsCount");
            var raidLockoutsCount = packet.ReadUInt32("RaidLockoutsCount");

            for (var i = 0u; i < raidLockoutsCount; ++i)
                ReadCalendarSendCalendarRaidLockoutInfo(packet, "RaidLockouts", i);

            for (var i = 0u; i < invitesCount; ++i)
                V8_0_1_27101.Parsers.CalendarHandler.ReadCalendarSendCalendarInviteInfo(packet, "Invites", i);

            for (var i = 0u; i < eventsCount; ++i)
                V8_0_1_27101.Parsers.CalendarHandler.ReadCalendarSendCalendarEventInfo(packet, "Events", i);
        }
    }
}
