using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class CurrencyHandler
    {
        [Parser(Opcode.SMSG_RESET_WEEKLY_CURRENCY)]
        public static void HandleCurrencyZero(Packet packet)
        {
        }

        [Parser(Opcode.SMSG_SETUP_CURRENCY)]
        public static void HandleInitCurrency(Packet packet)
        {
            var count = packet.Translator.ReadInt32("SetupCurrencyRecord");

            // ClientSetupCurrencyRecord
            for (var i = 0; i < count; ++i)
            {
                packet.Translator.ReadUInt32("Type", i);
                packet.Translator.ReadUInt32("Quantity", i);

                var hasWeeklyQuantity = packet.Translator.ReadBit();
                var hasMaxWeeklyQuantity = packet.Translator.ReadBit();
                var hasTrackedQuantity = packet.Translator.ReadBit();
                packet.Translator.ReadBits("Flags", 5, i);

                if (hasWeeklyQuantity)
                    packet.Translator.ReadUInt32("WeeklyQuantity", i);

                if (hasMaxWeeklyQuantity)
                    packet.Translator.ReadUInt32("MaxWeeklyQuantity", i);

                if (hasTrackedQuantity)
                    packet.Translator.ReadUInt32("TrackedQuantity", i);
            }
        }

        [Parser(Opcode.SMSG_SET_MAX_WEEKLY_QUANTITY)]
        public static void HandleSetMaxWeeklyQuantity(Packet packet)
        {
            packet.Translator.ReadInt32("Type");
            packet.Translator.ReadInt32("MaxWeeklyQuantity");
        }

        [Parser(Opcode.SMSG_SET_CURRENCY)]
        public static void HandleSetCurrency(Packet packet)
        {
            packet.Translator.ReadInt32("Type");
            packet.Translator.ReadInt32("Quantity");
            packet.Translator.ReadInt32("Flags");

            var bit32 = packet.Translator.ReadBit("HasTrackedQuantity");
            var bit40 = packet.Translator.ReadBit("HasWeeklyQuantity");
            packet.Translator.ReadBit("SuppressChatLog");

            if (bit32)
                packet.Translator.ReadInt32("TrackedQuantity");

            if (bit40)
                packet.Translator.ReadInt32("WeeklyQuantity");
        }

        [Parser(Opcode.SMSG_CONQUEST_FORMULA_CONSTANTS)]
        public static void HandleConquestFormulaConstants(Packet packet)
        {
            // Order guessed
            packet.Translator.ReadInt32("PvpMinCPPerWeek");
            packet.Translator.ReadInt32("PvpMaxCPPerWeek");
            packet.Translator.ReadSingle("PvpCPBaseCoefficient");
            packet.Translator.ReadSingle("PvpCPExpCoefficient");
            packet.Translator.ReadSingle("PvpCPNumerator");
        }
    }
}
