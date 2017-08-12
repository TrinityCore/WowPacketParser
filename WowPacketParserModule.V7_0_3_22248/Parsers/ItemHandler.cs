using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V7_0_3_22248.Parsers
{
    public static class ItemHandler
    {
        public static void ReadItemGemInstanceData(Packet packet, params object[] idx)
        {
            packet.ReadByte("Slot", idx);
            V6_0_2_19033.Parsers.ItemHandler.ReadItemInstance(packet, "Item", idx);
        }

        [Parser(Opcode.SMSG_ITEM_PUSH_RESULT)]
        public static void HandleItemPushResult(Packet packet)
        {
            packet.ReadPackedGuid128("PlayerGUID");

            packet.ReadByte("Slot");

            packet.ReadInt32("SlotInBag");

            packet.ReadUInt32("QuestLogItemID");
            packet.ReadUInt32("Quantity");
            packet.ReadUInt32("QuantityInInventory");
            packet.ReadInt32("DungeonEncounterID");

            packet.ReadUInt32("BattlePetBreedID");
            packet.ReadUInt32("BattlePetBreedQuality");
            packet.ReadUInt32("BattlePetSpeciesID");
            packet.ReadUInt32("BattlePetLevel");

            packet.ReadPackedGuid128("ItemGUID");

            packet.ResetBitReader();

            packet.ReadBit("Pushed");
            packet.ReadBit("Created");
            packet.ReadBits("DisplayText", 3);
            packet.ReadBit("IsBonusRoll");
            packet.ReadBit("IsEncounterLoot");

            V6_0_2_19033.Parsers.ItemHandler.ReadItemInstance(packet, "ItemInstance");
        }

        [Parser(Opcode.CMSG_USE_ITEM)]
        public static void HandleUseItem(Packet packet)
        {
            packet.ReadByte("PackSlot");
            packet.ReadByte("Slot");
            packet.ReadPackedGuid128("CastItem");

            SpellHandler.ReadSpellCastRequest(packet, "Cast");
        }

        [Parser(Opcode.CMSG_USE_TOY)]
        public static void HandleUseToy(Packet packet)
        {
            packet.ReadInt32<ItemId>("ItemID");
            SpellHandler.ReadSpellCastRequest(packet, "Cast");
        }

        [Parser(Opcode.CMSG_BUY_ITEM)]
        public static void HandleBuyItem(Packet packet)
        {
            packet.ReadPackedGuid128("VendorGUID");
            packet.ReadPackedGuid128("ContainerGUID");

            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V7_2_0_23826))
                V6_0_2_19033.Parsers.ItemHandler.ReadItemInstance(packet, "ItemInstance");

            packet.ReadInt32("Quantity");
            packet.ReadUInt32("Muid");
            packet.ReadUInt32("Slot");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V7_2_0_23826))
                V6_0_2_19033.Parsers.ItemHandler.ReadItemInstance(packet, "ItemInstance");

            packet.ResetBitReader();

            packet.ReadBits("ItemType", 2);
        }

        [Parser(Opcode.SMSG_SOCKET_GEMS)]
        public static void HandleSocketGemsResult(Packet packet)
        {
            packet.ReadPackedGuid128("Item");
        }
    }
}
