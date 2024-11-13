
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V11_0_0_55666.Parsers
{
    public static class QueryHandler
    {
        [Parser(Opcode.CMSG_QUERY_TREASURE_PICKER)]
        public static void HandleQueryQuestRewards(Packet packet)
        {
            packet.ReadInt32("QuestID");
            packet.ReadInt32("TreasurePickerID");
        }

        public static void ReadTreasurePickItem(Packet packet, params object[] indexes)
        {
            Substructures.ItemHandler.ReadItemInstance(packet, indexes);
            packet.ReadUInt32("Quantity", indexes);
            var hasContextFlags = packet.ReadBit("HasContextFlags", indexes);
            packet.ResetBitReader();

            if (hasContextFlags)
                packet.ReadInt32("ContextFlags", indexes);
        }

        public static void ReadTreasurePickCurrency(Packet packet, params object[] indexes)
        {
            packet.ReadUInt32("CurrencyID", indexes);
            packet.ReadUInt32("Quantity", indexes);
            var hasContextFlags = packet.ReadBit("HasContextFlags", indexes);
            packet.ResetBitReader();

            if (hasContextFlags)
                packet.ReadInt32("ContextFlags", indexes);
        }

        public static void ReadTreasurePickerBonus(Packet packet, params object[] indexes)
        {
            var bonusItemCount = packet.ReadUInt32("BonusItemCount", indexes);
            var bonusCurrencyCount = packet.ReadUInt32("BonusCurrencyCount", indexes);
            packet.ReadUInt64("BonusMoney", indexes);
            packet.ReadBit("BonusContext", indexes);
            packet.ResetBitReader();

            for (var i = 0; i < bonusItemCount; ++i)
                ReadTreasurePickItem(packet, indexes, i);

            for (var i = 0; i < bonusCurrencyCount; ++i)
                ReadTreasurePickCurrency(packet, indexes, i);
        }

        public static void ReadTreasurePick(Packet packet)
        {
            var itemCount = packet.ReadUInt32("ItemCount");
            var currencyCount = packet.ReadUInt32("CurrencyCount");

            packet.ReadUInt64("Money");

            var bonusCount = packet.ReadUInt32("BonusCount");

            packet.ReadUInt32("Flags");
            packet.ReadBit("IsChoice");
            packet.ResetBitReader();

            for (var i = 0; i < itemCount; ++i)
                ReadTreasurePickItem(packet, i);

            for (int i = 0; i < currencyCount; i++)
                ReadTreasurePickCurrency(packet, i);

            for (var i = 0; i < bonusCount; ++i)
                ReadTreasurePickerBonus(packet, i);
        }

        [Parser(Opcode.SMSG_TREASURE_PICKER_RESPONSE)]
        public static void HandleTreasurePickerResponse(Packet packet)
        {
            packet.ReadUInt32("QuestID");
            packet.ReadUInt32("TreasurePickerID");
            ReadTreasurePick(packet);
        }
    }
}
