using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_8_18291.Parsers
{
    public static class EquipmentSetHandler
    {
        private const int NumSlots = 19;

        [Parser(Opcode.SMSG_LOAD_EQUIPMENT_SET)]
        public static void HandleEquipmentSetList(Packet packet)
        {
            var count = packet.Translator.ReadBits(19);

            var guid1 = new byte[count][][];
            var guid2 = new byte[count][];

            var bits0 = new uint[count];
            var bits4 = new uint[count];

            for (var i = 0; i < count; i++)
            {
                guid1[i] = new byte[NumSlots][];
                guid2[i] = new byte[8];

                guid2[i][4] = packet.Translator.ReadBit();

                for (var j = 0; j < NumSlots; j++)
                {
                    guid1[i][j] = new byte[8];
                    packet.Translator.StartBitStream(guid1[i][j], 3, 5, 7, 2, 6, 0, 4, 1);
                }

                guid2[i][5] = packet.Translator.ReadBit();
                bits4[i] = packet.Translator.ReadBits(9);
                guid2[i][1] = packet.Translator.ReadBit();
                guid2[i][7] = packet.Translator.ReadBit();
                bits0[i] = packet.Translator.ReadBits(8);
                guid2[i][3] = packet.Translator.ReadBit();
                guid2[i][2] = packet.Translator.ReadBit();
                guid2[i][6] = packet.Translator.ReadBit();
                guid2[i][0] = packet.Translator.ReadBit();
            }

            for (var i = 0; i < count; i++)
            {
                for (var j = 0; j < NumSlots; j++)
                {
                    packet.Translator.ParseBitStream(guid1[i][j], 2, 3, 7, 1, 6, 5, 0, 4);
                    packet.Translator.WriteGuid("Item GUID", guid1[i][j], i, j);
                }

                packet.Translator.ReadXORByte(guid2[i], 7);
                packet.Translator.ReadInt32("Index", i);
                packet.Translator.ReadWoWString("Set Name", bits0[i], i);
                packet.Translator.ReadXORByte(guid2[i], 2);
                packet.Translator.ReadXORByte(guid2[i], 6);
                packet.Translator.ReadXORByte(guid2[i], 0);
                packet.Translator.ReadXORByte(guid2[i], 3);
                packet.Translator.ReadXORByte(guid2[i], 1);
                packet.Translator.ReadXORByte(guid2[i], 5);
                packet.Translator.ReadXORByte(guid2[i], 4);
                packet.Translator.ReadWoWString("Set Icon", bits4[i], i);

                packet.Translator.WriteGuid("GUID", guid2[i], i);
            }
        }
    }
}