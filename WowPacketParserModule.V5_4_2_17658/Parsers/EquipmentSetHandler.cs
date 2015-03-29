using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_2_17568.Parsers
{
    public static class EquipmentSetHandler
    {
        private const int NumSlots = 19;

        [Parser(Opcode.SMSG_LOAD_EQUIPMENT_SET)]
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

        [Parser(Opcode.CMSG_EQUIPMENT_SET_USE)]
        public static void HandleEquipmentSetUse(Packet packet)
        {
            var slotsInfo = new byte[19][];
            for (var i = 0; i < 19; ++i)
            {
                slotsInfo[i] = new byte[2];
                slotsInfo[i][0] = packet.ReadByte();
                slotsInfo[i][1] = packet.ReadByte();
            }

            var itemGuids = new byte[19][];
            for (var i = 0; i < 19; ++i)
            {
                itemGuids[i] = new byte[8];
                packet.StartBitStream(itemGuids[i], 6, 1, 5, 2, 0, 4, 3, 7);
            }

            var someCount = packet.ReadBits("Some count", 2);
            var someThings = new byte[someCount][];
            for (var i = 0; i < someCount; ++i)
            {
                someThings[i] = new byte[2];
                packet.StartBitStream(someThings[i], 1, 0);
            }

            for (var i = 0; i < 19; ++i)
            {
                packet.ParseBitStream(itemGuids[i], 1, 6, 4, 7, 0, 3, 5, 2);
                packet.WriteGuid("ItemGUID", itemGuids[i], i);
                packet.AddValue("Source bag", slotsInfo[i][0], i);
                packet.AddValue("Source slot", slotsInfo[i][1], i);
            }

            for (var i = 0; i < someCount; ++i)
            {
                packet.ParseBitStream(someThings[i], 0, 1);
                packet.AddValue("Unk", "byte 1 " + someThings[0] + " byte 2" + someThings[0], i);
            }
        }
    }
}
