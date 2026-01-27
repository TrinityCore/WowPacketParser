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
            packet.ReadInt32("MapID");
            packet.ReadInt16<DifficultyId>("DifficultyID");
            packet.ReadInt32("TimeRemaining");
            packet.ReadUInt64("InstanceID");
        }

        [Parser(Opcode.SMSG_CALENDAR_RAID_LOCKOUT_REMOVED)]
        public static void HandleRaidLockoutRemoved(Packet packet)
        {
            packet.ReadInt32("MapID");
            packet.ReadInt16<DifficultyId>("DifficultyID");
            packet.ReadUInt64("InstanceID");
        }

        [Parser(Opcode.SMSG_CALENDAR_RAID_LOCKOUT_UPDATED)]
        public static void HandleCalendarRaidLockoutUpdated(Packet packet)
        {
            packet.ReadPackedTime("ServerTime");
            packet.ReadInt32("MapID");
            packet.ReadInt16<DifficultyId>("DifficultyID");
            packet.ReadInt32("NewTimeRemaining");
            packet.ReadInt32("OldTimeRemaining");
        }
    }
}
