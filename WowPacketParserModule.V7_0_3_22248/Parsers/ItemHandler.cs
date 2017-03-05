using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V7_0_3_22248.Parsers
{
    public static class ItemHandler
    {
        public static void ReadItemGemInstanceData(Packet packet, params object[] idx)
        {
            packet.Translator.ReadByte("Slot", idx);
            V6_0_2_19033.Parsers.ItemHandler.ReadItemInstance(packet, "Item", idx);
        }

        [Parser(Opcode.SMSG_ITEM_PUSH_RESULT)]
        public static void HandleItemPushResult(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("PlayerGUID");

            packet.Translator.ReadByte("Slot");

            packet.Translator.ReadInt32("SlotInBag");

            packet.Translator.ReadUInt32("QuestLogItemID");
            packet.Translator.ReadUInt32("Quantity");
            packet.Translator.ReadUInt32("QuantityInInventory");
            packet.Translator.ReadInt32("DungeonEncounterID");

            packet.Translator.ReadUInt32("BattlePetBreedID");
            packet.Translator.ReadUInt32("BattlePetBreedQuality");
            packet.Translator.ReadUInt32("BattlePetSpeciesID");
            packet.Translator.ReadUInt32("BattlePetLevel");

            packet.Translator.ReadPackedGuid128("ItemGUID");

            packet.Translator.ResetBitReader();

            packet.Translator.ReadBit("Pushed");
            packet.Translator.ReadBit("Created");
            packet.Translator.ReadBits("DisplayText", 3);
            packet.Translator.ReadBit("IsBonusRoll");
            packet.Translator.ReadBit("IsEncounterLoot");

            V6_0_2_19033.Parsers.ItemHandler.ReadItemInstance(packet, "ItemInstance");
        }

        [Parser(Opcode.CMSG_USE_ITEM)]
        public static void HandleUseItem(Packet packet)
        {
            packet.Translator.ReadByte("PackSlot");
            packet.Translator.ReadByte("Slot");
            packet.Translator.ReadPackedGuid128("CastItem");

            SpellHandler.ReadSpellCastRequest(packet, "Cast");
        }

        [Parser(Opcode.CMSG_USE_TOY)]
        public static void HandleUseToy(Packet packet)
        {
            packet.Translator.ReadInt32<ItemId>("ItemID");
            SpellHandler.ReadSpellCastRequest(packet, "Cast");
        }
    }
}
