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
            var count = packet.Translator.ReadBits(19);

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
                    packet.Translator.StartBitStream(guid1[i][j], 0, 2, 3, 1, 6, 7, 4, 5);
                }

                guid2[i][0] = packet.Translator.ReadBit();
                guid2[i][6] = packet.Translator.ReadBit();
                guid2[i][3] = packet.Translator.ReadBit();
                bits0[i] = packet.Translator.ReadBits(8);
                guid2[i][1] = packet.Translator.ReadBit();
                guid2[i][7] = packet.Translator.ReadBit();
                guid2[i][4] = packet.Translator.ReadBit();
                bits4[i] = packet.Translator.ReadBits(9);
                guid2[i][5] = packet.Translator.ReadBit();
                guid2[i][2] = packet.Translator.ReadBit();
            }

            for (var i = 0; i < count; i++)
            {
                for (var j = 0; j < NumSlots; j++)
                {
                    packet.Translator.ParseBitStream(guid1[i][j], 2, 1, 0, 5, 3, 4, 6, 7);
                    packet.Translator.WriteGuid("Item GUID", guid1[i][j], i, j);
                }

                packet.Translator.ReadXORByte(guid2[i], 6);
                packet.Translator.ReadWoWString("Set Name", bits0[i], i);
                packet.Translator.ReadXORByte(guid2[i], 5);
                packet.Translator.ReadXORByte(guid2[i], 2);
                packet.Translator.ReadWoWString("Set Icon", bits4[i], i);
                packet.Translator.ReadXORByte(guid2[i], 3);
                packet.Translator.ReadXORByte(guid2[i], 4);
                packet.Translator.ReadInt32("Index", i);
                packet.Translator.ReadXORByte(guid2[i], 0);
                packet.Translator.ReadXORByte(guid2[i], 7);
                packet.Translator.ReadXORByte(guid2[i], 1);

                packet.Translator.WriteGuid("GUID", guid2[i], i);
            }
        }

        [Parser(Opcode.CMSG_SAVE_EQUIPMENT_SET)]
        public static void HandleEquipmentSetSave(Packet packet)
        {
            var guid1 = new byte[NumSlots][];
            var guid = new byte[8];

            packet.Translator.ReadInt32("Index");

            for (var i = 0; i < NumSlots; i++)
            {
                guid1[i] = new byte[8];
                packet.Translator.StartBitStream(guid1[i], 1, 0, 4, 3, 7, 5, 6, 2);
            }

            guid[7] = packet.Translator.ReadBit();
            guid[1] = packet.Translator.ReadBit();
            guid[4] = packet.Translator.ReadBit();
            guid[5] = packet.Translator.ReadBit();
            guid[6] = packet.Translator.ReadBit();
            guid[3] = packet.Translator.ReadBit();

            var bits0 = packet.Translator.ReadBits(8);
            guid[0] = packet.Translator.ReadBit();
            var bits4 = packet.Translator.ReadBits(9);

            guid[2] = packet.Translator.ReadBit();

            packet.Translator.ReadWoWString("Set Name", bits0);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadWoWString("Set Icon", bits4);


            for (var i = 0; i < NumSlots; i++)
            {
                packet.Translator.ParseBitStream(guid1[i], 1, 5, 6, 2, 4, 7, 3, 0);
                packet.Translator.WriteGuid("Item GUID", guid1[i], i);
            }

            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 6);

            packet.Translator.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.CMSG_EQUIPMENT_SET_USE)]
        public static void HandleEquipmentSetUse(Packet packet)
        {

            var itemGuids = new byte[NumSlots][];
            var slotsInfo = new byte[NumSlots][];

            for (var i = 0; i < NumSlots; ++i)
            {
                slotsInfo[i] = new byte[2];
                slotsInfo[i][1] = packet.Translator.ReadByte();
                slotsInfo[i][0] = packet.Translator.ReadByte();
            }

            for (var i = 0; i < NumSlots; ++i)
            {
                itemGuids[i] = new byte[8];
                packet.Translator.StartBitStream(itemGuids[i], 2, 3, 1, 7, 4, 6, 0, 5);
            }

            var someCount = packet.Translator.ReadBits("Some count", 2);

            var someThings = new byte[someCount][];
            for (var i = 0; i < someCount; ++i)
            {
                someThings[i] = new byte[2];
                packet.Translator.StartBitStream(someThings[i], 1, 0);
            }

            for (var i = 0; i < NumSlots; ++i)
            {
                packet.Translator.ParseBitStream(itemGuids[i], 7, 3, 0, 4, 1, 2, 5, 6);
                packet.Translator.WriteGuid("ItemGUID", itemGuids[i], i);
                packet.AddValue("Source bag", slotsInfo[i][0], i);
                packet.AddValue("Source slot", slotsInfo[i][1], i);
            }

            for (var i = 0; i < someCount; ++i)
            {
                packet.Translator.ParseBitStream(someThings[i], 0, 1);
                packet.AddValue("Unk", "byte 1 " + someThings[0] + " byte 2" + someThings[0], i);
            }
        }

        [Parser(Opcode.SMSG_USE_EQUIPMENT_SET_RESULT)]
        public static void HandleUseEquipmentSetResult(Packet packet)
        {
            packet.Translator.ReadByte("Result");
        }
    }
}