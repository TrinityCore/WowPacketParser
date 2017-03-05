using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_0_17359.Parsers
{
    public static class EquipmentSetHandler
    {
        private const int NumSlots = 19;

        [Parser(Opcode.SMSG_LOAD_EQUIPMENT_SET)]
        public static void HandleEquipmentSetList(Packet packet)
        {
            var count = packet.ReadBits("Count", 19);

            var guid1 = new byte[count][][];
            var guid2 = new byte[count][];

            var bits0 = new uint[count];
            var bits4 = new uint[count];

            for (var i = 0; i < count; i++)
            {
                guid1[i] = new byte[NumSlots][];

                for (var j = 0; j < NumSlots; j++)
                {
                    guid1[i][j] = new byte[8];
                    packet.StartBitStream(guid1[i][j], 0, 6, 5, 4, 2, 7, 1, 3);
                }

                guid2[i] = new byte[8];
                packet.StartBitStream(guid2[i], 6, 0, 2, 5);
                bits0[i] = packet.ReadBits(8);
                guid2[i][3] = packet.ReadBit();
                bits4[i] = packet.ReadBits(9);
                packet.StartBitStream(guid2[i], 4, 7, 1);
            }

            for (var i = 0; i < count; i++)
            {
                for (var j = 0; j < NumSlots; j++)
                {
                    packet.ParseBitStream(guid1[i][j], 4, 6, 3, 5, 0, 2, 1, 7);
                    packet.WriteGuid("Item GUID", guid1[i][j], i, j);
                }

                packet.ReadWoWString("Set Name", bits0[i], i);

                packet.ReadXORByte(guid2[i], 5);
                packet.ReadXORByte(guid2[i], 0);
                packet.ReadXORByte(guid2[i], 3);
                packet.ReadXORByte(guid2[i], 6);
                packet.ReadXORByte(guid2[i], 1);

                packet.ReadWoWString("Set Icon", bits4[i], i);

                packet.ReadXORByte(guid2[i], 7);
                packet.ReadXORByte(guid2[i], 4);
                packet.ReadXORByte(guid2[i], 2);

                packet.ReadInt32("Index", i);
                packet.WriteGuid("GUID", guid2[i], i);
            }
        }
    }
}