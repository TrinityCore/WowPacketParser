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
            var count = packet.Translator.ReadInt32("SetupCurrencyRecord");

            // ClientSetupCurrencyRecord
            for (var i = 0; i < count; ++i)
            {
                packet.Translator.ReadUInt32("Type", i);
                packet.Translator.ReadUInt32("Quantity", i);

                packet.Translator.ResetBitReader();

                var hasWeeklyQuantity = packet.Translator.ReadBit();
                var hasMaxWeeklyQuantity = packet.Translator.ReadBit();
                var hasTrackedQuantity = packet.Translator.ReadBit();
                var hasMaxQuantity = packet.Translator.ReadBit();
                packet.Translator.ReadBits("Flags", 5, i);

                if (hasWeeklyQuantity)
                    packet.Translator.ReadUInt32("WeeklyQuantity", i);

                if (hasMaxWeeklyQuantity)
                    packet.Translator.ReadUInt32("MaxWeeklyQuantity", i);

                if (hasTrackedQuantity)
                    packet.Translator.ReadUInt32("TrackedQuantity", i);

                if (hasMaxQuantity)
                    packet.Translator.ReadUInt32("MaxQuantity", i);
            }
        }
    }
}
