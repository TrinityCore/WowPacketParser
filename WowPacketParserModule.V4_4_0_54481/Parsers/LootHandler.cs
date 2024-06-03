using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V4_4_0_54481.Parsers
{
    public static class LootHandler
    {
        [Parser(Opcode.SMSG_LEGACY_LOOT_RULES)]
        public static void HandleLegacyLootRules(Packet packet)
        {
            packet.ReadBit("LegacyRulesActive");
        }
    }
}
