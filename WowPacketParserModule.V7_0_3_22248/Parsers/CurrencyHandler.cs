using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V7_0_3_22248.Parsers
{
    public static class CurrencyHandler
    {
        [Parser(Opcode.SMSG_SETUP_CURRENCY)]
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
                var hasTotalEarned = false;
                var hasNextRechargeTime = false;
                var hasRechargeCyclicStartTime = false;
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V9_0_1_36216))
                    hasTotalEarned = packet.ReadBit("HasTotalEarned");

                if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_0_2_46479))
                    hasNextRechargeTime = packet.ReadBit("HasNextRechargeTime");

                if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_1_0_49318))
                    hasRechargeCyclicStartTime = packet.ReadBit("HasRechargeCyclicStartTime");

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

        [Parser(Opcode.SMSG_SET_CURRENCY)]
        public static void HandleSetCurrency(Packet packet)
        {
            packet.ReadInt32("Type");
            packet.ReadInt32("Quantity");
            packet.ReadUInt32("Flags");

            var hasWeeklyQuantity = packet.ReadBit("HasWeeklyQuantity");
            var hasTrackedQuantity = packet.ReadBit("HasTrackedQuantity");
            var hasMaxQuantity = packet.ReadBit("HasMaxQuantity");
            packet.ReadBit("SuppressChatLog");

            if (hasWeeklyQuantity)
                packet.ReadInt32("WeeklyQuantity");

            if (hasTrackedQuantity)
                packet.ReadInt32("TrackedQuantity");

            if (hasMaxQuantity)
                packet.ReadInt32("MaxQuantity");
        }
    }
}
