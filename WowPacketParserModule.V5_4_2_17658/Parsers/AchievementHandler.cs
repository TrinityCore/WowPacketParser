using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_2_17658.Parsers
{
    public static class AchievementHandler
    {
        [Parser(Opcode.SMSG_CRITERIA_UPDATE)]
        public static void HandleCriteriaPlayer(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 7, 2, 3, 6, 1, 5, 4, 0);

            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 7);

            packet.ReadInt64("Counter");
            packet.ReadInt32("Timer 1");

            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 5);

            packet.ReadInt32("Criteria ID");

            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 3);

            packet.ReadInt32("Flags");
            packet.ReadInt32("Timer 2");

            packet.ReadPackedTime("Time");
            packet.ReadXORByte(guid, 2);

            packet.WriteGuid("Guid", guid);
        }
    }
}
