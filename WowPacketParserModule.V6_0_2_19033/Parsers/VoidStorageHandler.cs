using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class VoidStorageHandler
    {
        [Parser(Opcode.CMSG_QUERY_VOID_STORAGE)]
        public static void HandleVoidStorageQuery(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Guid");
        }

        [Parser(Opcode.SMSG_VOID_STORAGE_CONTENTS)]
        public static void HandleVoidStorageContents(Packet packet)
        {
            var count = packet.Translator.ReadBits(8);
            for (var i = 0; i < count; ++i)
            {
                packet.Translator.ReadPackedGuid128("Guid", i);
                packet.Translator.ReadPackedGuid128("Creator", i);
                packet.Translator.ReadUInt32("Slot", i);

                ItemHandler.ReadItemInstance(packet, i);
            }
        }

        [Parser(Opcode.CMSG_SWAP_VOID_ITEM)]
        public static void HandleVoidSwapItem(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Npc");
            packet.Translator.ReadPackedGuid128("VoidItem");
            packet.Translator.ReadInt32("DstSlot");
        }

        [Parser(Opcode.SMSG_VOID_ITEM_SWAP_RESPONSE)]
        public static void HandleVoidItemSwapResponse(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("VoidItemA");
            packet.Translator.ReadInt32("VoidItemSlotA");
            packet.Translator.ReadPackedGuid128("VoidItemB");
            packet.Translator.ReadInt32("VoidItemSlotB");
        }

        [Parser(Opcode.CMSG_VOID_STORAGE_TRANSFER)]
        public static void HandleVoidStorageTransfer(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Npc");
            var int32 = packet.Translator.ReadInt32("WithdrawalsCount");
            var int48 = packet.Translator.ReadInt32("DepositsCount");

            for (int i = 0; i < int32; i++)
                packet.Translator.ReadPackedGuid128("Withdrawals", i);

            for (int i = 0; i < int48; i++)
                packet.Translator.ReadPackedGuid128("Deposits", i);
        }

        [Parser(Opcode.SMSG_VOID_STORAGE_TRANSFER_CHANGES)]
        public static void HandleVoidStorageTransferChanges(Packet packet)
        {
            var bits32 = packet.Translator.ReadBits("AddedItemsCount", 4);
            var bits16 = packet.Translator.ReadBits("RemovedItemsCount", 4);

            // AddedItems
            for (int i = 0; i < bits32; i++)
            {
                packet.Translator.ReadPackedGuid128("AddedItemsGuid", i);
                packet.Translator.ReadPackedGuid128("Creator", i);
                packet.Translator.ReadInt32("Slot", i);

                ItemHandler.ReadItemInstance(packet, i);
            }

            // RemovedItems
            for (int i = 0; i < bits16; i++)
                packet.Translator.ReadPackedGuid128("RemovedItemsGuid", i);
        }

        [Parser(Opcode.SMSG_VOID_TRANSFER_RESULT)]
        public static void HandleVoidTransferResults(Packet packet)
        {
            packet.Translator.ReadUInt32E<VoidTransferError>("Error");
        }

        [Parser(Opcode.CMSG_UNLOCK_VOID_STORAGE)]
        public static void HandleVoidStorageUnlock(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Npc");
        }

        [Parser(Opcode.SMSG_VOID_STORAGE_FAILED)]
        public static void HandleVoidStorageFailed(Packet packet)
        {
            packet.Translator.ReadByte("Reason");
        }
    }
}
