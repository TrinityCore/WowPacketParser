using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_5_0_61735.Parsers
{
    public static class VoidStorageHandler
    {
        public static void ReadVoidItem(Packet packet, params object[] index)
        {
            packet.ReadPackedGuid128("Guid", index);
            packet.ReadPackedGuid128("Creator", index);
            packet.ReadUInt32("Slot", index);

            Substructures.ItemHandler.ReadItemInstance(packet, index);
        }

        [Parser(Opcode.SMSG_VOID_STORAGE_CONTENTS)]
        public static void HandleVoidStorageContents(Packet packet)
        {
            var count = packet.ReadBits(8);
            for (var i = 0; i < count; ++i)
                ReadVoidItem(packet, i);
        }

        [Parser(Opcode.SMSG_VOID_ITEM_SWAP_RESPONSE)]
        public static void HandleVoidItemSwapResponse(Packet packet)
        {
            packet.ReadPackedGuid128("VoidItemA");
            packet.ReadInt32("VoidItemSlotA");
            packet.ReadPackedGuid128("VoidItemB");
            packet.ReadInt32("VoidItemSlotB");
        }

        [Parser(Opcode.SMSG_VOID_STORAGE_TRANSFER_CHANGES)]
        public static void HandleVoidStorageTransferChanges(Packet packet)
        {
            var bits32 = packet.ReadBits("AddedItemsCount", 4);
            var bits16 = packet.ReadBits("RemovedItemsCount", 4);

            // AddedItems
            for (int i = 0; i < bits32; i++)
                ReadVoidItem(packet, "AddedItems", i);

            // RemovedItems
            for (int i = 0; i < bits16; i++)
                packet.ReadPackedGuid128("RemovedItemsGuid", i);
        }

        [Parser(Opcode.SMSG_VOID_TRANSFER_RESULT)]
        public static void HandleVoidTransferResults(Packet packet)
        {
            packet.ReadUInt32E<VoidTransferError>("Error");
        }

        [Parser(Opcode.SMSG_OPEN_CONTAINER)]
        public static void HandleOpenContainer(Packet packet)
        {
            packet.ReadPackedGuid128("ItemGUID");
        }

        [Parser(Opcode.SMSG_VOID_STORAGE_FAILED)]
        public static void HandleVoidStorageZero(Packet packet)
        {
        }
    }
}
