using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_8_18291.Parsers
{
    public static class AchievementHandler
    {
        [Parser(Opcode.SMSG_CRITERIA_UPDATE)]
        public static void HandleCriteriaPlayer(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 4, 6, 2, 3, 7, 1, 5, 0);

            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 2);
            packet.ReadInt32("Criteria ID");
            packet.ReadInt32("Flags");
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 1);
            packet.ReadPackedTime("Time");
            packet.ReadXORByte(guid, 4);
            packet.ReadInt32("Timer 1");
            packet.ReadInt32("Timer 2");
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 0);
            packet.ReadInt64("Counter");

            packet.WriteGuid("Guid", guid);
        }
    }
}
