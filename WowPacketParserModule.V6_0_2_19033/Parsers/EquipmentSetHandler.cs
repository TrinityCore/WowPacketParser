using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class EquipmentSetHandler
    {
        private const int NumSlots = 19;

        [Parser(Opcode.SMSG_LOAD_EQUIPMENT_SET)]
        public static void HandleEquipmentSetList(Packet packet)
        {
            var count = packet.ReadInt32("Count");

            for (var i = 0; i < count; i++)
            {
                packet.ReadUInt64("Set Guid", i);
                packet.ReadInt32("Set ID", i);
                int ignoreMask = packet.ReadInt32("IgnoreMask");

                for (var j = 0; j < NumSlots; j++)
                {
                    bool ignore = (ignoreMask & (1 << j)) != 0;
                    packet.ReadPackedGuid128("Item Guid" + (ignore ? " (Ignored)" : ""), i, j);
                }

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
            packet.ReadUInt64("Set Guid");
            packet.ReadInt32("Set ID");
            int ignoreMask = packet.ReadInt32("IgnoreMask");

            for (var i = 0; i < NumSlots; i++)
            {
                bool ignore = (ignoreMask & (1 << i)) != 0;
                packet.ReadPackedGuid128("Item Guid" + (ignore ? " (Ignored)" : ""), i);
            }

            packet.ResetBitReader();
            var bits12 = packet.ReadBits(8);
            var bits141 = packet.ReadBits(9);

            packet.ReadWoWString("Set Name", bits12);
            packet.ReadWoWString("Set Icon", bits141);
        }

        [Parser(Opcode.SMSG_EQUIPMENT_SET_ID)]
        public static void HandleEquipmentSetSaved(Packet packet)
        {
            packet.ReadUInt64("Set Guid");
            packet.ReadInt32("SetID");
        }

        [Parser(Opcode.CMSG_USE_EQUIPMENT_SET)]
        public static void HandleUseEquipmentSet(Packet packet)
        {
            ItemHandler.ReadInvUpdate(packet, "InvUpdate");
            for (int i = 0; i < NumSlots; i++)
            {
                packet.ReadPackedGuid128("Item");
                packet.ReadByte("ContainerSlot");
                packet.ReadByte("Slot");
            }
        }
    }
}
