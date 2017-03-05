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

            counter[1] = packet.Translator.ReadBit();
            guid2[2] = packet.Translator.ReadBit();
            packet.Translator.ReadBits("Flags", 4); // some flag... & 1 -> delete
            packet.Translator.StartBitStream(guid2, 4, 0, 7);
            counter[6] = packet.Translator.ReadBit();
            guid2[5] = packet.Translator.ReadBit();
            packet.Translator.StartBitStream(counter, 0, 2, 4);
            packet.Translator.StartBitStream(guid2, 3, 6);
            packet.Translator.StartBitStream(counter, 3, 5);
            guid2[1] = packet.Translator.ReadBit();
            counter[7] = packet.Translator.ReadBit();

            packet.Translator.ResetBitReader();

            packet.Translator.ReadPackedTime("Time");
            packet.Translator.ReadXORByte(counter, 1);
            packet.Translator.ReadXORByte(guid2, 2);
            packet.Translator.ReadXORByte(counter, 2);
            packet.Translator.ReadXORByte(guid2, 3);
            packet.Translator.ReadXORByte(counter, 5);
            packet.Translator.ReadXORBytes(guid2, 0, 4);

            packet.Translator.ReadUInt32("Timer 1");
            packet.Translator.ReadXORByte(guid2, 5);
            packet.Translator.ReadInt32("Criteria ID");
            packet.Translator.ReadXORByte(guid2, 1);
            packet.Translator.ReadXORBytes(counter, 7, 3, 6);
            packet.Translator.ReadXORByte(guid2, 7);
            packet.Translator.ReadXORBytes(counter, 0, 4);
            packet.Translator.ReadXORByte(guid2, 6);
            packet.Translator.ReadUInt32("Timer 2");

            packet.AddValue("Counter", BitConverter.ToInt64(counter, 0));
            packet.Translator.WriteGuid("GUID2", guid2);
        }
    }
}
