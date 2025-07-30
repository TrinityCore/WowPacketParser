using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_5_0_61735.Parsers
{
    public static class LfgHandler
    {
        public static void ReadCliRideTicket(Packet packet, params object[] idx)
        {
            packet.ReadPackedGuid128("RequesterGuid", idx);
            packet.ReadInt32("Id", idx);
            packet.ReadInt32("Type", idx);
            packet.ReadTime64("Time", idx);
            packet.ResetBitReader();
            packet.ReadBit("Unknown925", idx);
        }

        public static void ReadLfgPlayerQuestReward(Packet packet, params object[] idx)
        {
            packet.ReadByte("Mask", idx);
            packet.ReadInt32("RewardMoney", idx);
            packet.ReadInt32("RewardXP", idx);

            var itemCount = packet.ReadUInt32("ItemCount", idx);
            var currencyCount = packet.ReadUInt32("CurrencyCount", idx);
            var bonusCurrencyCount = packet.ReadUInt32("BonusCurrency", idx);

            // Item
            for (var k = 0; k < itemCount; ++k)
            {
                packet.ReadInt32<ItemId>("ItemID", idx, k);
                packet.ReadInt32("Quantity", idx, k);
            }

            // Currency
            for (var k = 0; k < currencyCount; ++k)
            {
                packet.ReadInt32("CurrencyID", idx, k);
                packet.ReadInt32("Quantity", idx, k);
            }

            // BonusCurrency
            for (var k = 0; k < bonusCurrencyCount; ++k)
            {
                packet.ReadInt32("CurrencyID", idx, k);
                packet.ReadInt32("Quantity", idx, k);
            }

            packet.ResetBitReader();

            var hasRewardSpellId = packet.ReadBit("HasRewardSpellID", idx);
            var hasUnused1 = packet.ReadBit();
            var hasUnused2 = packet.ReadBit();
            var hasHonor = packet.ReadBit("HasHonor", idx);

            if (hasRewardSpellId)
                packet.ReadInt32("RewardSpellID", idx);

            if (hasUnused1)
                packet.ReadInt32("Unused1", idx);

            if (hasUnused2)
                packet.ReadUInt64("Unused2");

            if (hasHonor)
                packet.ReadInt32("Honor", idx);
        }
    }
}
