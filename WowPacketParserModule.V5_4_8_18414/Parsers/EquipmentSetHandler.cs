using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_8_18414.Parsers
{
    public static class EquipmentSetHandler
    {
        private const int NumSlots = 19;

        [Parser(Opcode.SMSG_EQUIPMENT_SET_LIST)]
        public static void HandleEquipmentSetList(Packet packet)
        {
            var count = packet.ReadBits("count", 19);
            var guid = new byte[count][];
            var guid2 = new byte[count][][];
            var len68 = new uint[count];
            var len580 = new uint[count];
            for (var i = 0; i < count; i++)
            {
                guid[i] = new byte[8];

                guid[i][4] = packet.ReadBit("unk", i);
                guid2[i] = new byte[20][];
                for (var j = 0; j < NumSlots; j++)
                {
                    guid2[i][j] = new byte[8];
                    guid2[i][j] = packet.StartBitStream(3, 5, 7, 2, 6, 0, 4, 1);
                }
                guid[i][5] = packet.ReadBit();
                len580[i] = packet.ReadBits("unk580", 9, i);
                guid[i][1] = packet.ReadBit();
                guid[i][7] = packet.ReadBit();
                len68[i] = packet.ReadBits("unk68", 8, i);
                guid[i][3] = packet.ReadBit();
                guid[i][2] = packet.ReadBit();
                guid[i][6] = packet.ReadBit();
                guid[i][0] = packet.ReadBit();
            }
            for (var i = 0; i < count; i++)
            {
                for (var j = 0; j < 19; j++)
                {
                    packet.ParseBitStream(guid2[i][j], 2, 3, 7, 1, 6, 5, 0, 4);
                    packet.WriteGuid("Item GUID", guid2[i][j], i, j);
                }
                packet.ParseBitStream(guid[i], 7);
                packet.ReadInt32("Index", i);
                packet.ReadWoWString("Set Name", len68[i], i);
                packet.ParseBitStream(guid[i], 2, 6, 0, 3, 1, 5, 4);
                packet.ReadWoWString("Set Icon", len580[i], i);
                packet.WriteGuid("Guid", guid[i], i);
            }
        }
    }
}