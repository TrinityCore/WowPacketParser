using PacketParser.Enums;
using PacketParser.Misc;
using PacketParser.DataStructures;

namespace PacketParser.Parsing.Parsers
{
    public static class EquipmentSetHandler
    {
        private const int NumSlots = 19;

        public static void ReadSetInfo(ref Packet packet, params int[] values)
        {
            packet.StoreBeginObj("ItemSet", values);
            packet.ReadPackedGuid("Set ID");
            packet.ReadInt32("Index");
            packet.ReadCString("Set Name");
            packet.ReadCString("Set Icon");

            packet.StoreBeginList("ItemSlots");
            for (var j = 0; j < NumSlots; j++)
                packet.ReadPackedGuid("Item GUID", j);
            packet.StoreEndList();
            packet.StoreEndObj();
        }

        [Parser(Opcode.SMSG_EQUIPMENT_SET_LIST)]
        public static void HandleEquipmentSetList(Packet packet)
        {
            var count = packet.ReadInt32("Count");

            packet.StoreBeginList("ItemSets");
            for (var i = 0; i < count; i++)
                ReadSetInfo(ref packet, i);
            packet.StoreEndList();
        }

        [Parser(Opcode.CMSG_EQUIPMENT_SET_SAVE)]
        public static void HandleEquipmentSetSave(Packet packet)
        {
            ReadSetInfo(ref packet);
        }

        [Parser(Opcode.SMSG_EQUIPMENT_SET_SAVED)]
        public static void HandleEquipmentSetSaved(Packet packet)
        {
            packet.ReadInt32("Index");

            packet.ReadPackedGuid("Set ID");
        }

        [Parser(Opcode.CMSG_EQUIPMENT_SET_USE)]
        public static void HandleEquipmentSetUse(Packet packet)
        {
            packet.StoreBeginList("ItemSlots");
            for (var i = 0; i < NumSlots; i++)
            {
                packet.ReadPackedGuid("Item GUID " + i);

                packet.ReadByte("Source Bag");

                packet.ReadByte("Source Slot");
            }
            packet.StoreEndList();
        }

        [Parser(Opcode.SMSG_EQUIPMENT_SET_USE_RESULT)]
        public static void HandleEquipmentSetUseResult(Packet packet)
        {
            packet.ReadByte("Result");
        }

        [Parser(Opcode.CMSG_DELETEEQUIPMENT_SET)]
        public static void HandleEquipmentSetDelete(Packet packet)
        {
            packet.ReadPackedGuid("Set ID");
        }
    }
}
