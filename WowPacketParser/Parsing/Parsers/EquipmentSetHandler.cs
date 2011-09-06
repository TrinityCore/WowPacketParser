using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Parsing.Parsers
{
    public static class EquipmentSetHandler
    {
        public static void ReadSetInfo(Packet packet)
        {
            var pguid = packet.ReadPackedGuid();
            Console.WriteLine("Set ID: " + pguid.Full);

            var index = packet.ReadInt32();
            Console.WriteLine("Index: " + index);

            var name = packet.ReadCString();
            Console.WriteLine("Set Name: " + name);

            var iconName = packet.ReadCString();
            Console.WriteLine("Set Icon: " + iconName);

            for (var j = 0; j < 19; j++)
            {
                var itemGuid = packet.ReadPackedGuid();
                Console.WriteLine("Item GUID " + j + ": " + itemGuid);
            }
        }

        [Parser(Opcode.SMSG_EQUIPMENT_SET_LIST)]
        public static void HandleEquipmentSetList(Packet packet)
        {
            var count = packet.ReadInt32();
            Console.WriteLine("Count: " + count);

            for (var i = 0; i < count; i++)
                ReadSetInfo(packet);
        }

        [Parser(Opcode.CMSG_EQUIPMENT_SET_SAVE)]
        public static void HandleEquipmentSetSave(Packet packet)
        {
            ReadSetInfo(packet);
        }

        [Parser(Opcode.SMSG_EQUIPMENT_SET_SAVED)]
        public static void HandleEquipmentSetSaved(Packet packet)
        {
            var index = packet.ReadInt32();
            Console.WriteLine("Index: " + index);

            var id = packet.ReadPackedGuid();
            Console.WriteLine("Set ID: " + id.Full);
        }

        [Parser(Opcode.CMSG_EQUIPMENT_SET_USE)]
        public static void HandleEquipmentSetUse(Packet packet)
        {
            for (var i = 0; i < 19; i++)
            {
                var itemGuid = packet.ReadPackedGuid();
                Console.WriteLine("Item GUID " + i + ": " + itemGuid);

                var srcbag = packet.ReadByte();
                Console.WriteLine("Source Bag: " + srcbag);

                var srcslot = packet.ReadByte();
                Console.WriteLine("Source Slot: " + srcslot);
            }
        }

        [Parser(Opcode.SMSG_EQUIPMENT_SET_USE_RESULT)]
        public static void HandleEquipmentSetUseResult(Packet packet)
        {
            var result = packet.ReadByte();
            Console.WriteLine("Result: " + result);
        }

        [Parser(Opcode.CMSG_DELETEEQUIPMENT_SET)]
        public static void HandleEquipmentSetDelete(Packet packet)
        {
            var pguid = packet.ReadPackedGuid();
            Console.WriteLine("Set ID: " + pguid.Full);
        }
    }
}
