using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_7_17898.Parsers
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

                for (var j = 0; j < NumSlots; j++)
                {
                    guid1[i][j] = new byte[8];
                    packet.StartBitStream(guid1[i][j], 0, 2, 3, 1, 6, 7, 4, 5);
                }

                guid2[i][0] = packet.ReadBit();
                guid2[i][6] = packet.ReadBit();
                guid2[i][3] = packet.ReadBit();
                bits0[i] = packet.ReadBits(8);
                guid2[i][1] = packet.ReadBit();
                guid2[i][7] = packet.ReadBit();
                guid2[i][4] = packet.ReadBit();
                bits4[i] = packet.ReadBits(9);
                guid2[i][5] = packet.ReadBit();
                guid2[i][2] = packet.ReadBit();
            }

            for (var i = 0; i < count; i++)
            {
                for (var j = 0; j < NumSlots; j++)
                {
                    packet.ParseBitStream(guid1[i][j], 2, 1, 0, 5, 3, 4, 6, 7);
                    packet.WriteGuid("Item GUID", guid1[i][j], i, j);
                }

                packet.ReadXORByte(guid2[i], 6);
                packet.ReadWoWString("Set Name", bits0[i], i);
                packet.ReadXORByte(guid2[i], 5);
                packet.ReadXORByte(guid2[i], 2);
                packet.ReadWoWString("Set Icon", bits4[i], i);
                packet.ReadXORByte(guid2[i], 3);
                packet.ReadXORByte(guid2[i], 4);
                packet.ReadInt32("Index", i);
                packet.ReadXORByte(guid2[i], 0);
                packet.ReadXORByte(guid2[i], 7);
                packet.ReadXORByte(guid2[i], 1);

                packet.WriteGuid("GUID", guid2[i], i);
            }
        }

        [Parser(Opcode.CMSG_EQUIPMENT_SET_SAVE)]
        public static void HandleEquipmentSetSave(Packet packet)
        {
            var guid1 = new byte[NumSlots][];
            var guid = new byte[8];

            packet.ReadInt32("Index");

            for (var i = 0; i < NumSlots; i++)
            {
                guid1[i] = new byte[8];
                packet.StartBitStream(guid1[i], 1, 0, 4, 3, 7, 5, 6, 2);
            }

            guid[7] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid[3] = packet.ReadBit();

            var bits0 = packet.ReadBits(8);
            guid[0] = packet.ReadBit();
            var bits4 = packet.ReadBits(9);

            guid[2] = packet.ReadBit();

            packet.ReadWoWString("Set Name", bits0);
            packet.ReadXORByte(guid, 4);
            packet.ReadWoWString("Set Icon", bits4);


            for (var i = 0; i < NumSlots; i++)
            {
                packet.ParseBitStream(guid1[i], 1, 5, 6, 2, 4, 7, 3, 0);
                packet.WriteGuid("Item GUID", guid1[i], i);
            }

            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 6);

            packet.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.CMSG_EQUIPMENT_SET_USE)]
        public static void HandleEquipmentSetUse(Packet packet)
        {

            var itemGuids = new byte[NumSlots][];
            var slotsInfo = new byte[NumSlots][];

            for (var i = 0; i < NumSlots; ++i)
            {
                slotsInfo[i] = new byte[2];
                slotsInfo[i][1] = packet.ReadByte();
                slotsInfo[i][0] = packet.ReadByte();
            }

            for (var i = 0; i < NumSlots; ++i)
            {
                itemGuids[i] = new byte[8];
                packet.StartBitStream(itemGuids[i], 2, 3, 1, 7, 4, 6, 0, 5);
            }

            var someCount = packet.ReadBits("Some count", 2);

            var someThings = new byte[someCount][];
            for (var i = 0; i < someCount; ++i)
            {
                someThings[i] = new byte[2];
                packet.StartBitStream(someThings[i], 1, 0);
            }

            for (var i = 0; i < NumSlots; ++i)
            {
                packet.ParseBitStream(itemGuids[i], 7, 3, 0, 4, 1, 2, 5, 6);
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

        [Parser(Opcode.SMSG_USE_EQUIPMENT_SET_RESULT)]
        public static void HandleUseEquipmentSetResult(Packet packet)
        {
            packet.ReadByte("Result");
        }
    }
}