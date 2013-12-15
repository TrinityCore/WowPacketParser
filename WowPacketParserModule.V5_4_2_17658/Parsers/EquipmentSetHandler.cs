using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_2_17568.Parsers
{
    public static class EquipmentSetHandler
    {
        private const int NumSlots = 19;

        [Parser(Opcode.SMSG_EQUIPMENT_SET_LIST)]
        public static void HandleEquipmentSetList(Packet packet)
        {
            var count = packet.ReadBits(19);
            
            var guid1 = new byte[count][][];
            var guid2 = new byte[count][];

            var bits0 = new uint[count];
            var bits4 = new uint[count];

            for (var i = 0; i < count; i++)
            {
                guid1[i] = new byte[NumSlots][];
                guid2[i] = new byte[8];

                guid2[i][7] = packet.ReadBit();

                for (var j = 0; j < NumSlots; j++)
                {
                    guid1[i][j] = new byte[8];
                    packet.StartBitStream(guid1[i][j], 6, 7, 5, 1, 0, 4, 2, 3);
                }

                guid2[i][3] = packet.ReadBit();
                guid2[i][1] = packet.ReadBit();
                guid2[i][5] = packet.ReadBit();
                guid2[i][6] = packet.ReadBit();
                bits0[i] = packet.ReadBits(8);
                bits4[i] = packet.ReadBits(9);
                guid2[i][0] = packet.ReadBit();
                guid2[i][2] = packet.ReadBit();
                guid2[i][4] = packet.ReadBit();
            }
            
            for (var i = 0; i < count; i++)
            {
                for (var j = 0; j < NumSlots; j++)
                {
                    packet.ParseBitStream(guid1[i][j], 7, 4, 5, 3, 0, 6, 2, 1);
                    packet.WriteGuid("Item GUID", guid1[i][j], i, j);
                }

                packet.ReadXORByte(guid2[i], 7);
                packet.ReadXORByte(guid2[i], 3);
                packet.ReadXORByte(guid2[i], 0);
                packet.ReadXORByte(guid2[i], 5);
                packet.ReadWoWString("Set Name", bits0[i], i);
                packet.ReadXORByte(guid2[i], 4);
                packet.ReadXORByte(guid2[i], 6);
                packet.ReadXORByte(guid2[i], 2);
                packet.ReadXORByte(guid2[i], 1);
                packet.ReadInt32("Index", i);
                packet.ReadWoWString("Set Icon", bits4[i], i);

                packet.WriteGuid("GUID", guid2[i], i);
            }

        }
    }
}