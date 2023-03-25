using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

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
            packet.ReadInt32("DungeonEncounterID");

            packet.ReadUInt32("BattlePetSpeciesID");
            packet.ReadUInt32("BattlePetBreedID");
            packet.ReadUInt32("BattlePetBreedQuality");
            packet.ReadUInt32("BattlePetLevel");

            packet.ReadPackedGuid128("ItemGUID");

            var toastCount = packet.ReadUInt32();
            for (var i = 0u; i < toastCount; i++)
                ReadUIEventToast(packet, "UiEventToast", i);

            packet.ReadBit("Pushed");
            packet.ReadBit("Created");
            packet.ReadBits("DisplayText", 3);
            packet.ReadBit("IsBonusRoll");
            packet.ReadBit("IsEncounterLoot");
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
    }
}
