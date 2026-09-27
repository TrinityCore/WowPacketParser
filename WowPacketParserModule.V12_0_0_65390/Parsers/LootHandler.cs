using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V12_0_0_65390.Parsers
{
    public static class LootHandler
    {
        public static void ReadLootItem(Packet packet, params object[] indexes)
        {
            packet.ResetBitReader();
            packet.ReadBits("ItemType", 2, indexes);
            packet.ReadBits("ItemUiType", 3, indexes);
            packet.ReadBit("CanTradeToTapList", indexes);
            packet.ReadUInt32("Quantity", indexes);
            packet.ReadByte("LootItemType", indexes);
            packet.ReadByte("LootListID", indexes);
            Substructures.ItemHandler.ReadItemInstance(packet, indexes, "Loot");
        }

        [Parser(Opcode.SMSG_LOOT_RESPONSE, ClientVersionBuild.V12_1_0_69214)]
        public static void HandleLootResponse(Packet packet)
        {
            packet.ReadPackedGuid128("Owner");
            packet.ReadPackedGuid128("LootObj");
            packet.ReadByteE<LootError>("FailureReason");
            packet.ReadByteE<LootType>("AcquireReason");
            packet.ReadByteE<LootMethod>("LootMethod");
            packet.ReadByteE<ItemQuality>("Threshold");
            packet.ReadUInt32("Coins");
            var itemCount = packet.ReadUInt32("ItemCount");
            var currencyCount = packet.ReadUInt32("CurrencyCount");

            for (var i = 0; i < itemCount; ++i)
                ReadLootItem(packet, "Items", i);

            for (var i = 0; i < currencyCount; ++i)
                V6_0_2_19033.Parsers.LootHandler.ReadCurrenciesData(packet, "Currencies", i);

            packet.ResetBitReader();
            packet.ReadBit("Acquired");
            packet.ReadBit("AELooting");
            packet.ReadBit("PersonalLooting");
        }
    }
}
