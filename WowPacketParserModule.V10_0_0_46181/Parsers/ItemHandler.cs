using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V10_0_0_46181.Parsers
{
    public static class ItemHandler
    {
        public static void ReadUIEventToast(Packet packet, params object[] args)
        {
            packet.ReadInt32("UiEventToastID", args);
            packet.ReadInt32("Asset", args);
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
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V11_0_0_55666))
                packet.ReadUInt32("QuantityInQuestLog");

            packet.ReadInt32("DungeonEncounterID");

            packet.ReadUInt32("BattlePetSpeciesID");
            packet.ReadUInt32("BattlePetBreedID");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V11_0_0_55666))
                packet.ReadByte("BattlePetBreedQuality");
            else
                packet.ReadUInt32("BattlePetBreedQuality");
            packet.ReadUInt32("BattlePetLevel");

            packet.ReadPackedGuid128("ItemGUID");

            var toastCount = packet.ReadUInt32();
            for (var i = 0u; i < toastCount; i++)
                ReadUIEventToast(packet, "UiEventToast", i);

            packet.ReadBit("Pushed");
            packet.ReadBit("Created");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_1_7_51187))
                packet.ReadBit("FakeQuestItem");

            packet.ReadBits("ChatNotifyType", 3);
            packet.ReadBit("IsBonusRoll");
            packet.ReadBit("IsPersonalLoot");
            var hasCraftingData = packet.ReadBit();
            var hasFirstCraftOperationID = packet.ReadBit();
            packet.ResetBitReader();

            Substructures.ItemHandler.ReadItemInstance(packet);

            if (hasFirstCraftOperationID)
                packet.ReadUInt32("FirstCraftOperationID");

            if (hasCraftingData)
                CraftingHandler.ReadCraftingData(packet, "CraftingData");
        }

        [Parser(Opcode.CMSG_LOOT_MONEY, ClientVersionBuild.V10_0_7_48676)]
        public static void HandleLootMoney(Packet packet)
        {
            packet.ReadBit("IsSoftInteract");
        }

        [Parser(Opcode.SMSG_SELL_RESPONSE, ClientVersionBuild.V10_1_7_51187)]
        public static void HandleSellResponse(Packet packet)
        {
            packet.ReadPackedGuid128("VendorGUID");
            var itemGuidCount = packet.ReadUInt32("ItemGuidCount");

            packet.ReadInt32E<SellResult>("Reason");

            for (var i = 0; i < itemGuidCount; ++i)
                packet.ReadPackedGuid128("ItemGuid", i);
        }

        [Parser(Opcode.CMSG_CHANGE_BAG_SLOT_FLAG)]
        [Parser(Opcode.CMSG_CHANGE_BANK_BAG_SLOT_FLAG)]
        public static void HandleChangeBagSlotFlag(Packet packet)
        {
            packet.ReadUInt32("BagIndex");
            packet.ReadUInt32E<BagSlotFlags>("BagSlotFlag");
            packet.ReadBit("On");
        }

        [Parser(Opcode.CMSG_SET_BACKPACK_AUTOSORT_DISABLED)]
        [Parser(Opcode.CMSG_SET_BACKPACK_SELL_JUNK_DISABLED)]
        [Parser(Opcode.CMSG_SET_BANK_AUTOSORT_DISABLED)]
        public static void HandleBackpackAutosortDisabled(Packet packet)
        {
            packet.ReadBit("Disable");
        }
    }
}
