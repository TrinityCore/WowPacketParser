using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V4_4_0_54481.Parsers
{
    public static class LootHandler
    {
        [Parser(Opcode.SMSG_AE_LOOT_TARGETS)]
        public static void HandleClientAELootTargets(Packet packet)
        {
            packet.ReadUInt32("Count");
        }

        [Parser(Opcode.SMSG_LEGACY_LOOT_RULES)]
        public static void HandleLegacyLootRules(Packet packet)
        {
            packet.ReadBit("LegacyRulesActive");
        }

        [Parser(Opcode.SMSG_AE_LOOT_TARGET_ACK)]
        public static void HandleLootZero(Packet packet)
        {
        }
    }
}
