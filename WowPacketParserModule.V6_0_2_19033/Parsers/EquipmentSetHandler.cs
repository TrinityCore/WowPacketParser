using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class EquipmentSetHandler
    {
        private const int NumSlots = 19;

        [Parser(Opcode.SMSG_EQUIPMENT_SET_LIST)]
        public static void HandleEquipmentSetList(Packet packet)
        {
            var count = packet.ReadInt32("Count");

            for (var i = 0; i < count; i++)
            {
                packet.ReadUInt64("Set ID", i);

                packet.ReadInt32("Index", i);
                packet.ReadInt32("Unk Int", i);

                for (var j = 0; j < NumSlots; j++)
                    packet.ReadPackedGuid128("Item Guid", i, j);

                packet.ResetBitReader();
                var bits12 = packet.ReadBits(8);
                var bits141 = packet.ReadBits(9);

                packet.ReadWoWString("Set Name", bits12, i);
                packet.ReadWoWString("Set Icon", bits141, i);
            }
        }

        [Parser(Opcode.CMSG_EQUIPMENT_SET_SAVE)]
        public static void HandleEquipmentSetSave(Packet packet)
        {
            packet.ReadUInt64("Set ID");

            packet.ReadInt32("Index");
            packet.ReadInt32("Unk Int");

            for (var i = 0; i < NumSlots; i++)
                packet.ReadPackedGuid128("Item Guid", i);

            packet.ResetBitReader();
            var bits12 = packet.ReadBits(8);
            var bits141 = packet.ReadBits(9);

            packet.ReadWoWString("Set Name", bits12);
            packet.ReadWoWString("Set Icon", bits141);
        }

        [Parser(Opcode.SMSG_EQUIPMENT_SET_SAVED)]
        public static void HandleEquipmentSetSaved(Packet packet)
        {
            packet.ReadUInt64("Set ID");

            packet.ReadInt32("Index");
        }
    }
}
