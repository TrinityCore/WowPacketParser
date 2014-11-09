using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class VoidStorageHandler
    {
        [Parser(Opcode.CMSG_VOID_STORAGE_QUERY)]
        public static void HandleVoidStorageQuery(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");
        }

        [Parser(Opcode.SMSG_VOID_STORAGE_CONTENTS)]
        public static void HandleVoidStorageContents(Packet packet)
        {
            var count = packet.ReadBits(8);
            for (var i = 0; i < count; ++i)
            {
                packet.ReadPackedGuid128("Guid", i);
                packet.ReadPackedGuid128("Creator", i);
                packet.ReadUInt32("Slot", i);

                packet.ReadUInt32("ItemID", i);
                packet.ReadUInt32("RandomPropertiesSeed", i);
                packet.ReadUInt32("RandomPropertiesID", i);
                packet.ResetBitReader();
                var hasBonuses = packet.ReadBit("HasItemBonus", i);
                var hasModifications = packet.ReadBit("HasModifications", i);
                if (hasBonuses)
                {
                    packet.ReadByte("Context", i);

                    var bonusCount = packet.ReadUInt32();
                    for (var j = 0; j < bonusCount; ++j)
                        packet.ReadUInt32("BonusListID", i, j);
                }

                if (hasModifications)
                {
                    var modificationCount = packet.ReadUInt32() / 4;
                    for (var j = 0; j < modificationCount; ++j)
                        packet.ReadUInt32("Modification", i, j);
                }
            }
        }

        [Parser(Opcode.CMSG_VOID_SWAP_ITEM)]
        public static void HandleVoidSwapItem(Packet packet)
        {
            packet.ReadPackedGuid128("Npc");
            packet.ReadPackedGuid128("VoidItem");
            packet.ReadInt32("DstSlot");
        }

        [Parser(Opcode.SMSG_VOID_ITEM_SWAP_RESPONSE)]
        public static void HandleVoidItemSwapResponse(Packet packet)
        {
            packet.ReadPackedGuid128("VoidItemA");
            packet.ReadInt32("VoidItemSlotA");
            packet.ReadPackedGuid128("VoidItemB");
            packet.ReadInt32("VoidItemSlotB");
        }

        [Parser(Opcode.CMSG_VOID_STORAGE_TRANSFER)]
        public static void HandleVoidStorageTransfer(Packet packet)
        {
            packet.ReadPackedGuid128("Npc");
            var int32 = packet.ReadInt32("WithdrawalsCount");
            var int48 = packet.ReadInt32("DepositsCount");

            for (int i = 0; i < int32; i++)
                packet.ReadPackedGuid128("Withdrawals", i);

            for (int i = 0; i < int48; i++)
                packet.ReadPackedGuid128("Deposits", i);
        }

        [Parser(Opcode.SMSG_VOID_STORAGE_TRANSFER_CHANGES)]
        public static void HandleVoidStorageTransferChanges(Packet packet)
        {
            var bits32 = packet.ReadBits("AddedItemsCount", 4);
            var bits16 = packet.ReadBits("RemovedItemsCount", 4);

            // AddedItems
            for (int i = 0; i < bits32; i++)
            {
                packet.ReadPackedGuid128("AddedItemsGuid", i);
                packet.ReadPackedGuid128("Creator", i);
                packet.ReadInt32("Slot", i);

                packet.ReadUInt32("ItemID", i);
                packet.ReadUInt32("RandomPropertiesSeed", i);
                packet.ReadUInt32("RandomPropertiesID", i);
                packet.ResetBitReader();
                var hasBonuses = packet.ReadBit("HasItemBonus", i);
                var hasModifications = packet.ReadBit("HasModifications", i);
                if (hasBonuses)
                {
                    packet.ReadByte("Context", i);

                    var bonusCount = packet.ReadUInt32();
                    for (var j = 0; j < bonusCount; ++j)
                        packet.ReadUInt32("BonusListID", i, j);
                }

                if (hasModifications)
                {
                    var modificationCount = packet.ReadUInt32() / 4;
                    for (var j = 0; j < modificationCount; ++j)
                        packet.ReadUInt32("Modification", i, j);
                }
            }

            // RemovedItems
            for (int i = 0; i < bits16; i++)
                packet.ReadPackedGuid128("RemovedItemsGuid", i);
        }

        [Parser(Opcode.SMSG_VOID_TRANSFER_RESULT)]
        public static void HandleVoidTransferResults(Packet packet)
        {
            packet.ReadEnum<VoidTransferError>("Error", TypeCode.UInt32);
        }

        [Parser(Opcode.CMSG_VOID_STORAGE_UNLOCK)]
        public static void HandleVoidStorageUnlock(Packet packet)
        {
            packet.ReadPackedGuid128("Npc");
        }
    }
}
