using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_3_0_16981.Parsers
{
    public static class AchievementHandler
    {
        [Parser(Opcode.SMSG_CRITERIA_UPDATE)]
        public static void HandleCriteriaUpdate(Packet packet)
        {
            var counter = new byte[8];
            var guid2 = new byte[8];

            counter[1] = packet.ReadBit();
            guid2[2] = packet.ReadBit();
            packet.ReadBits("Flags", 4); // some flag... & 1 -> delete
            packet.StartBitStream(guid2, 4, 0, 7);
            counter[6] = packet.ReadBit();
            guid2[5] = packet.ReadBit();
            packet.StartBitStream(counter, 0, 2, 4);
            packet.StartBitStream(guid2, 3, 6);
            packet.StartBitStream(counter, 3, 5);
            guid2[1] = packet.ReadBit();
            counter[7] = packet.ReadBit();

            packet.ResetBitReader();

            packet.ReadPackedTime("Time");
            packet.ReadXORByte(counter, 1);
            packet.ReadXORByte(guid2, 2);
            packet.ReadXORByte(counter, 2);
            packet.ReadXORByte(guid2, 3);
            packet.ReadXORByte(counter, 5);
            packet.ReadXORBytes(guid2, 0, 4);

            packet.ReadUInt32("Timer 1");
            packet.ReadXORByte(guid2, 5);
            packet.ReadInt32("Criteria ID");
            packet.ReadXORByte(guid2, 1);
            packet.ReadXORBytes(counter, 7, 3, 6);
            packet.ReadXORByte(guid2, 7);
            packet.ReadXORBytes(counter, 0, 4);
            packet.ReadXORByte(guid2, 6);
            packet.ReadUInt32("Timer 2");

            packet.AddValue("Counter", BitConverter.ToInt64(counter, 0));
            packet.WriteGuid("GUID2", guid2);
        }
    }
}
