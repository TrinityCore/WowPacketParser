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
            packet.Translator.ReadInt32("Event Time");
            packet.Translator.ReadInt32("Invite Count");
            packet.Translator.ReadByteE<CalendarEventType>("Event Type");
            packet.Translator.ReadInt32<LFGDungeonId>("Dungeon ID");
            packet.Translator.ReadInt32("Max Invites");

            var bits496 = packet.Translator.ReadBits(8);
            var bits4A4 = packet.Translator.ReadBits(22);

            var guid = new byte[bits4A4][];

            for (var i = 0; i < bits4A4; ++i)
            {
                guid[i] = new byte[8];
                packet.Translator.StartBitStream(guid[i], 3, 2, 5, 4, 6, 0, 1, 7);
            }

            var bits4A8 = (int)packet.Translator.ReadBits(11);

            packet.Translator.ReadWoWString("Description", bits4A8);
            packet.Translator.ReadWoWString("Title", bits496);

            for (var i = 0; i < bits4A4; ++i)
            {
                packet.Translator.ReadByteE<CalendarModerationRank>("Moderation Rank", i);
                packet.Translator.ReadXORByte(guid[i], 6);
                packet.Translator.ReadXORByte(guid[i], 1);
                packet.Translator.ReadXORByte(guid[i], 7);
                packet.Translator.ReadXORByte(guid[i], 5);
                packet.Translator.ReadXORByte(guid[i], 4);
                packet.Translator.ReadXORByte(guid[i], 2);
                packet.Translator.ReadByteE<CalendarEventStatus>("Status", i);
                packet.Translator.ReadXORByte(guid[i], 3);
                packet.Translator.ReadXORByte(guid[i], 0);

                packet.Translator.WriteGuid("Guid", guid[i], i);
            }
        }
    }
}
