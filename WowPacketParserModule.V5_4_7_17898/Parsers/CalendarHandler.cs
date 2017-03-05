using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V5_4_2_17658.Parsers
{
    public static class CalendarHandler
    {

        [Parser(Opcode.CMSG_CALENDAR_ADD_EVENT)]
        public static void HandleAddCalendarEvent(Packet packet)
        {
            packet.ReadInt32("Event Time");
            packet.ReadInt32("Invite Count");
            packet.ReadByteE<CalendarEventType>("Event Type");
            packet.ReadInt32<LFGDungeonId>("Dungeon ID");
            packet.ReadInt32("Max Invites");

            var bits496 = packet.ReadBits(8);
            var bits4A4 = packet.ReadBits(22);

            var guid = new byte[bits4A4][];

            for (var i = 0; i < bits4A4; ++i)
            {
                guid[i] = new byte[8];
                packet.StartBitStream(guid[i], 3, 2, 5, 4, 6, 0, 1, 7);
            }

            var bits4A8 = (int)packet.ReadBits(11);

            packet.ReadWoWString("Description", bits4A8);
            packet.ReadWoWString("Title", bits496);

            for (var i = 0; i < bits4A4; ++i)
            {
                packet.ReadByteE<CalendarModerationRank>("Moderation Rank", i);
                packet.ReadXORByte(guid[i], 6);
                packet.ReadXORByte(guid[i], 1);
                packet.ReadXORByte(guid[i], 7);
                packet.ReadXORByte(guid[i], 5);
                packet.ReadXORByte(guid[i], 4);
                packet.ReadXORByte(guid[i], 2);
                packet.ReadByteE<CalendarEventStatus>("Status", i);
                packet.ReadXORByte(guid[i], 3);
                packet.ReadXORByte(guid[i], 0);

                packet.WriteGuid("Guid", guid[i], i);
            }
        }
    }
}
