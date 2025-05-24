using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V3_4_0_45166.Parsers
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

        [Parser(Opcode.SMSG_VOID_STORAGE_CONTENTS, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleVoidStorageContents(Packet packet)
        {
            var count = packet.ReadBits(8);
            for (var i = 0; i < count; ++i)
                ReadVoidItem(packet, i);
        }

        [Parser(Opcode.SMSG_VOID_ITEM_SWAP_RESPONSE, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleVoidItemSwapResponse(Packet packet)
        {
            packet.ReadPackedGuid128("VoidItemA");
            packet.ReadInt32("VoidItemSlotA");
            packet.ReadPackedGuid128("VoidItemB");
            packet.ReadInt32("VoidItemSlotB");
        }

        [Parser(Opcode.SMSG_VOID_STORAGE_TRANSFER_CHANGES, ClientVersionBuild.V3_4_4_59817)]
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

        [Parser(Opcode.SMSG_VOID_TRANSFER_RESULT, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleVoidTransferResults(Packet packet)
        {
            packet.ReadUInt32E<VoidTransferError>("Error");
        }

        [Parser(Opcode.CMSG_QUERY_VOID_STORAGE, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleVoidStorageQuery(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");
        }

        [Parser(Opcode.CMSG_SWAP_VOID_ITEM, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleVoidSwapItem(Packet packet)
        {
            packet.ReadPackedGuid128("Npc");
            packet.ReadPackedGuid128("VoidItem");
            packet.ReadInt32("DstSlot");
        }

        [Parser(Opcode.CMSG_UNLOCK_VOID_STORAGE, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleVoidStorageUnlock(Packet packet)
        {
            packet.ReadPackedGuid128("Npc");
        }

        [Parser(Opcode.CMSG_VOID_STORAGE_TRANSFER, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleVoidStorageTransfer(Packet packet)
        {
            packet.ReadPackedGuid128("Npc");
            var int48 = packet.ReadInt32("DepositsCount");
            var int32 = packet.ReadInt32("WithdrawalsCount");

            for (int i = 0; i < int48; i++)
                packet.ReadPackedGuid128("Deposits", i);

            for (int i = 0; i < int32; i++)
                packet.ReadPackedGuid128("Withdrawals", i);
        }

        [Parser(Opcode.SMSG_VOID_STORAGE_FAILED, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleVoidStorageZero(Packet packet)
        {
        }
    }
}
