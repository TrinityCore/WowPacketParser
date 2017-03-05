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
            var count = packet.Translator.ReadInt32("Count");

            for (var i = 0; i < count; i++)
            {
                packet.Translator.ReadUInt64("Set Guid", i);
                packet.Translator.ReadInt32("Set ID", i);
                int ignoreMask = packet.Translator.ReadInt32("IgnoreMask");

                for (var j = 0; j < NumSlots; j++)
                {
                    bool ignore = (ignoreMask & (1 << j)) != 0;
                    packet.Translator.ReadPackedGuid128("Item Guid" + (ignore ? " (Ignored)" : ""), i, j);
                }

                packet.Translator.ResetBitReader();
                var bits12 = packet.Translator.ReadBits(8);
                var bits141 = packet.Translator.ReadBits(9);

                packet.Translator.ReadWoWString("Set Name", bits12, i);
                packet.Translator.ReadWoWString("Set Icon", bits141, i);
            }
        }

        [Parser(Opcode.CMSG_SAVE_EQUIPMENT_SET)]
        public static void HandleEquipmentSetSave(Packet packet)
        {
            packet.Translator.ReadUInt64("Set Guid");
            packet.Translator.ReadInt32("Set ID");
            int ignoreMask = packet.Translator.ReadInt32("IgnoreMask");

            for (var i = 0; i < NumSlots; i++)
            {
                bool ignore = (ignoreMask & (1 << i)) != 0;
                packet.Translator.ReadPackedGuid128("Item Guid" + (ignore ? " (Ignored)" : ""), i);
            }

            packet.Translator.ResetBitReader();
            var bits12 = packet.Translator.ReadBits(8);
            var bits141 = packet.Translator.ReadBits(9);

            packet.Translator.ReadWoWString("Set Name", bits12);
            packet.Translator.ReadWoWString("Set Icon", bits141);
        }

        [Parser(Opcode.SMSG_EQUIPMENT_SET_ID)]
        public static void HandleEquipmentSetSaved(Packet packet)
        {
            packet.Translator.ReadUInt64("Set Guid");
            packet.Translator.ReadInt32("SetID");
        }

        [Parser(Opcode.CMSG_USE_EQUIPMENT_SET)]
        public static void HandleUseEquipmentSet(Packet packet)
        {
            ItemHandler.ReadInvUpdate(packet, "InvUpdate");
            for (int i = 0; i < NumSlots; i++)
            {
                packet.Translator.ReadPackedGuid128("Item");
                packet.Translator.ReadByte("ContainerSlot");
                packet.Translator.ReadByte("Slot");
            }
        }
    }
}
