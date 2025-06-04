using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V3_4_0_45166.Parsers
{
    public static class CurrencyHandler
    {
        [Parser(Opcode.SMSG_SETUP_CURRENCY, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleSetupCurrency(Packet packet)
        {
            var count = packet.ReadInt32("SetupCurrencyRecord");

            // ClientSetupCurrencyRecord
            for (var i = 0; i < count; ++i)
            {
                packet.ReadUInt32("Type", i);
                packet.ReadUInt32("Quantity", i);

                packet.ResetBitReader();

                var hasWeeklyQuantity = packet.ReadBit();
                var hasMaxWeeklyQuantity = packet.ReadBit();
                var hasTrackedQuantity = packet.ReadBit();
                var hasMaxQuantity = packet.ReadBit();
                var hasTotalEarned = packet.ReadBit("HasTotalEarned");
                var hasNextRechargeTime = packet.ReadBit("HasNextRechargeTime");
                var hasRechargeCyclicStartTime = packet.ReadBit("HasRechargeCyclicStartTime");

                packet.ReadBits("Flags", 5, i);

                if (hasWeeklyQuantity)
                    packet.ReadUInt32("WeeklyQuantity", i);

                if (hasMaxWeeklyQuantity)
                    packet.ReadUInt32("MaxWeeklyQuantity", i);

                if (hasTrackedQuantity)
                    packet.ReadUInt32("TrackedQuantity", i);

                if (hasMaxQuantity)
                    packet.ReadUInt32("MaxQuantity", i);

                if (hasTotalEarned)
                    packet.ReadInt32("TotalEarned", i);

                if (hasNextRechargeTime)
                    packet.ReadTime64("NextRechargeTime", i);

                if (hasRechargeCyclicStartTime)
                    packet.ReadTime64("RechargeCyclicStartTime", i);
            }
        }

        [Parser(Opcode.SMSG_NOTIFY_MONEY)]
        public static void HandleNotifyMoney(Packet packet)
        {
            packet.ReadUInt64("Money");
        }

        [Parser(Opcode.SMSG_RESET_WEEKLY_CURRENCY, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleCurrencyZero(Packet packet)
        {
        }
    }
}
