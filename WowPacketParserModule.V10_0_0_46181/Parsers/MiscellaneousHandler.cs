using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V10_0_0_46181.Parsers
{
    public static class MiscellaneousHandler
    {
        [Parser(Opcode.SMSG_SETUP_CURRENCY)]
        public static void HandleSetupCurrency(Packet packet)
        {
            var count = packet.ReadUInt32("SetupCurrencyRecord");

            for (var i = 0; i < count; ++i)
            {
                packet.ReadInt32("Type", i);
                packet.ReadInt32("Quantity", i);

                packet.ResetBitReader();

                var hasWeeklyQuantity = packet.ReadBit();
                var hasMaxWeeklyQuantity = packet.ReadBit();
                var hasTrackedQuantity = packet.ReadBit();
                var hasMaxQuantity = packet.ReadBit();
                var hasTotalEarned = packet.ReadBit();
                var hasHasNextRechargeTime = packet.ReadBit();
                var hasRechargeCycleStartTime = false;
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_1_0_49318))
                    hasRechargeCycleStartTime = packet.ReadBit();

                packet.ReadBits("Flags", 5, i);

                if (hasWeeklyQuantity)
                    packet.ReadUInt32("WeeklyQuantity", i);

                if (hasMaxWeeklyQuantity)
                    packet.ReadUInt32("MaxWeeklyQuantity", i);

                if (hasTrackedQuantity)
                    packet.ReadUInt32("TrackedQuantity", i);

                if (hasMaxQuantity)
                    packet.ReadInt32("MaxQuantity", i);

                if (hasTotalEarned)
                    packet.ReadInt32("TotalEarned", i);

                if (hasHasNextRechargeTime)
                    packet.ReadTime64("NextRechargeTime", i);

                if (hasRechargeCycleStartTime)
                    packet.ReadTime64("RechargeCycleStartTime", i);
            }
        }

        [Parser(Opcode.SMSG_SET_CURRENCY)]
        public static void HandleSetCurrency(Packet packet)
        {
            packet.ReadInt32("Type");
            packet.ReadInt32("Quantity");
            packet.ReadUInt32("Flags");
            uint toastCount = packet.ReadUInt32("UiEventToastCount");
            for (var i = 0; i < toastCount; i++)
                ItemHandler.ReadUIEventToast(packet, "UiEventToast", i);

            var hasWeeklyQuantity = packet.ReadBit("HasWeeklyQuantity");
            var hasTrackedQuantity = packet.ReadBit("HasTrackedQuantity");
            var hasMaxQuantity = packet.ReadBit("HasMaxQuantity");
            var hasTotalEarned = packet.ReadBit("HasTotalEarned");
            packet.ReadBit("SuppressChatLog");
            var hasQuantityChange = packet.ReadBit("HasQuantityChange");
            var hasQuantityLostSource = packet.ReadBit("HasQuantityLostSource");
            var hasQuantityGainSource = packet.ReadBit("HasQuantityGainSource");
            var hasFirstCraftOperationID = packet.ReadBit("HasFirstCraftOperationID");
            var hasHasNextRechargeTime = packet.ReadBit("HasNextRechargeTime");
            var hasRechargeCycleStartTime = false;
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_1_0_49318))
                hasRechargeCycleStartTime = packet.ReadBit("HasRechargeCycleStartTime");

            if (hasWeeklyQuantity)
                packet.ReadInt32("WeeklyQuantity");

            if (hasTrackedQuantity)
                packet.ReadInt32("TrackedQuantity");

            if (hasMaxQuantity)
                packet.ReadInt32("MaxQuantity");

            if (hasTotalEarned)
                packet.ReadInt32("TotalEarned");

            if (hasQuantityChange)
                packet.ReadInt32("QuantityChange");

            if (hasQuantityLostSource)
                packet.ReadInt32("QuantityLostSource");

            if (hasQuantityGainSource)
                packet.ReadInt32("QuantityGainSource");

            if (hasFirstCraftOperationID)
                packet.ReadUInt32("FirstCraftOperationID");

            if (hasHasNextRechargeTime)
                packet.ReadTime64("NextRechargeTime");

            if (hasRechargeCycleStartTime)
                packet.ReadTime64("RechargeCycleStartTime");
        }
    }
}
