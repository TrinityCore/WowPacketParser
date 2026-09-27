using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V12_0_0_65390.Parsers
{
    public static class ItemHandler
    {
        [Parser(Opcode.CMSG_USE_ITEM, ClientVersionBuild.V12_0_7_68182)]
        public static void HandleUseItem(Packet packet)
        {
            var useItem = packet.Holder.ClientUseItem = new();
            useItem.PackSlot = packet.ReadByte("PackSlot");
            useItem.ItemSlot = packet.ReadByte("Slot");
            useItem.CastItem = packet.ReadPackedGuid128("CastItem");

            useItem.SpellId = SpellHandler.ReadSpellCastRequest(packet, "Cast");
        }

        [Parser(Opcode.CMSG_USE_TOY, ClientVersionBuild.V12_0_7_68182)]
        public static void HandleUseToy(Packet packet)
        {
            SpellHandler.ReadSpellCastRequest(packet, "Cast");
        }

        [Parser(Opcode.SMSG_ITEM_PUSH_RESULT, ClientVersionBuild.V12_1_0_69214)]
        public static void HandleItemPushResult(Packet packet)
        {
            packet.ReadPackedGuid128("PlayerGUID");

            packet.ReadByte("Slot");
            packet.ReadInt32("SlotInBag");
            Substructures.ItemHandler.ReadItemInstance(packet);
            packet.ReadUInt32("QuestLogItemID");
            packet.ReadUInt32("Quantity");
            packet.ReadUInt32("QuantityInInventory");
            packet.ReadUInt32("QuantityInQuestLog");

            packet.ReadInt32("DungeonEncounterID");

            packet.ReadUInt32("BattlePetSpeciesID");
            packet.ReadUInt32("BattlePetBreedID");
            packet.ReadByte("BattlePetBreedQuality");
            packet.ReadUInt32("BattlePetLevel");

            packet.ReadPackedGuid128("ItemGUID");

            var toastCount = packet.ReadUInt32();
            for (var i = 0u; i < toastCount; i++)
                V10_0_0_46181.Parsers.ItemHandler.ReadUIEventToast(packet, "UiEventToast", i);

            packet.ResetBitReader();
            packet.ReadBit("Pushed");
            packet.ReadBit("Created");
            packet.ReadBit("FakeQuestItem");
            packet.ReadBits("ChatNotifyType", 3);
            packet.ReadBit("IsBonusRoll");
            packet.ReadBit("IsPersonalLoot");
            var hasCraftingData = packet.ReadBit();
            var hasFirstCraftOperationID = packet.ReadBit();

            if (hasCraftingData)
                V10_0_0_46181.Parsers.CraftingHandler.ReadCraftingData(packet, "CraftingData");

            if (hasFirstCraftOperationID)
                packet.ReadUInt32("FirstCraftOperationID");
        }
    }
}
