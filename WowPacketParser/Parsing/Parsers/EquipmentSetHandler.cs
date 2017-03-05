using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Parsing.Parsers
{
    public static class EquipmentSetHandler
    {
        private const int NumSlots = 19;

        public static void ReadSetInfo(Packet packet)
        {
            packet.Translator.ReadPackedGuid("Set ID");
            packet.Translator.ReadInt32("Index");
            packet.Translator.ReadCString("Set Name");
            packet.Translator.ReadCString("Set Icon");

            for (var j = 0; j < NumSlots; j++)
                packet.Translator.ReadPackedGuid("Item GUID " + j);
        }

        [Parser(Opcode.SMSG_LOAD_EQUIPMENT_SET)]
        public static void HandleEquipmentSetList(Packet packet)
        {
            var count = packet.Translator.ReadInt32("Count");

            for (var i = 0; i < count; i++)
                ReadSetInfo(packet);
        }

        [Parser(Opcode.CMSG_SAVE_EQUIPMENT_SET)]
        public static void HandleEquipmentSetSave(Packet packet)
        {
            ReadSetInfo(packet);
        }

        [Parser(Opcode.SMSG_EQUIPMENT_SET_ID)]
        public static void HandleEquipmentSetSaved(Packet packet)
        {
            packet.Translator.ReadInt32("Index");

            packet.Translator.ReadPackedGuid("Set ID");
        }

        [Parser(Opcode.CMSG_EQUIPMENT_SET_USE)]
        public static void HandleEquipmentSetUse(Packet packet)
        {
            for (var i = 0; i < NumSlots; i++)
            {
                packet.Translator.ReadPackedGuid("Item GUID ", i);

                packet.Translator.ReadByte("Source Bag", i);

                packet.Translator.ReadByte("Source Slot", i);
            }
        }

        [Parser(Opcode.SMSG_USE_EQUIPMENT_SET_RESULT)]
        public static void HandleUseEquipmentSetResult(Packet packet)
        {
            packet.Translator.ReadByte("Result");
        }

        [Parser(Opcode.CMSG_EQUIPMENT_SET_DELETE)]
        public static void HandleEquipmentSetDelete(Packet packet)
        {
            packet.Translator.ReadPackedGuid("Set ID");
        }
    }
}
