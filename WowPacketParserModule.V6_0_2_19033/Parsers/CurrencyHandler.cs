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
            var count = packet.ReadInt32("SetupCurrencyRecord");

            // ClientSetupCurrencyRecord
            for (var i = 0; i < count; ++i)
            {
                packet.ReadUInt32("Type", i);
                packet.ReadUInt32("Quantity", i);

                var hasWeeklyQuantity = packet.ReadBit();
                var hasMaxWeeklyQuantity = packet.ReadBit();
                var hasTrackedQuantity = packet.ReadBit();
                packet.ReadBits("Flags", 5, i);

                if (hasWeeklyQuantity)
                    packet.ReadUInt32("WeeklyQuantity", i);

                if (hasMaxWeeklyQuantity)
                    packet.ReadUInt32("MaxWeeklyQuantity", i);

                if (hasTrackedQuantity)
                    packet.ReadUInt32("TrackedQuantity", i);
            }
        }

        [Parser(Opcode.SMSG_SET_MAX_WEEKLY_QUANTITY)]
        public static void HandleSetMaxWeeklyQuantity(Packet packet)
        {
            packet.ReadInt32("Type");
            packet.ReadInt32("MaxWeeklyQuantity");
        }

        [Parser(Opcode.SMSG_SET_CURRENCY)]
        public static void HandleSetCurrency(Packet packet)
        {
            packet.ReadInt32("Type");
            packet.ReadInt32("Quantity");
            packet.ReadInt32("Flags");

            var bit32 = packet.ReadBit("HasTrackedQuantity");
            var bit40 = packet.ReadBit("HasWeeklyQuantity");
            packet.ReadBit("SuppressChatLog");

            if (bit32)
                packet.ReadInt32("TrackedQuantity");

            if (bit40)
                packet.ReadInt32("WeeklyQuantity");
        }

        [Parser(Opcode.SMSG_CONQUEST_FORMULA_CONSTANTS)]
        public static void HandleConquestFormulaConstants(Packet packet)
        {
            // Order guessed
            packet.ReadInt32("PvpMinCPPerWeek");
            packet.ReadInt32("PvpMaxCPPerWeek");
            packet.ReadSingle("PvpCPBaseCoefficient");
            packet.ReadSingle("PvpCPExpCoefficient");
            packet.ReadSingle("PvpCPNumerator");
        }
    }
}
