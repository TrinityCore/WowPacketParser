using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Parsing.Parsers
{
    public static class EquipmentSetHandler
    {
        private const int NumSlots = 19;

        public static void ReadSetInfo(Packet packet)
        {
            packet.ReadPackedGuid("Set ID");
            packet.ReadInt32("Index");
            packet.ReadCString("Set Name");
            packet.ReadCString("Set Icon");

            for (var j = 0; j < NumSlots; j++)
                packet.ReadPackedGuid("Item GUID " + j);
        }

        [Parser(Opcode.SMSG_LOAD_EQUIPMENT_SET)]
        public static void HandleEquipmentSetList(Packet packet)
        {
            var count = packet.ReadInt32("Count");

            for (var i = 0; i < count; i++)
                ReadSetInfo(packet);
        }

        [Parser(Opcode.CMSG_EQUIPMENT_SET_SAVE)]
        public static void HandleEquipmentSetSave(Packet packet)
        {
            ReadSetInfo(packet);
        }

        [Parser(Opcode.SMSG_EQUIPMENT_SET_ID)]
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
                packet.ReadPackedGuid("Item GUID ", i);

                packet.ReadByte("Source Bag", i);

                packet.ReadByte("Source Slot", i);
            }
        }

        [Parser(Opcode.SMSG_USE_EQUIPMENT_SET_RESULT)]
        public static void HandleUseEquipmentSetResult(Packet packet)
        {
            packet.ReadByte("Result");
        }

        [Parser(Opcode.CMSG_EQUIPMENT_SET_DELETE)]
        public static void HandleEquipmentSetDelete(Packet packet)
        {
            packet.ReadPackedGuid("Set ID");
        }
    }
}
