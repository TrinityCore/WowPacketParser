using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Parsing.Parsers
{
    public static class EquipmentSetHandler
    {
        private const int NumSlots = 19;

        public static void ReadSetInfo(ref Packet packet)
        {
            packet.ReadPackedGuid("Set ID");
            packet.ReadInt32("Index");
            packet.ReadCString("Set Name");
            packet.ReadCString("Set Icon");

            for (var j = 0; j < NumSlots; j++)
                packet.ReadPackedGuid("Item GUID " + j);
        }

        [Parser(Opcode.SMSG_EQUIPMENT_SET_LIST)]
        public static void HandleEquipmentSetList(Packet packet)
        {
            var count = packet.ReadInt32("Count");

            for (var i = 0; i < count; i++)
                ReadSetInfo(ref packet);
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
            for (var i = 0; i < NumSlots; i++)
            {
                packet.ReadPackedGuid("Item GUID " + i);

                packet.ReadByte("Source Bag");

                packet.ReadByte("Source Slot");
            }
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
