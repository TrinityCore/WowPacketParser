using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V8_0_1_27101.Parsers
{
    public static class LootHandler
    {
        [Parser(Opcode.SMSG_LOOT_LEGACY_RULES_IN_EFFECT)]
        public static void HandleLootLegacyRulesInEffect(Packet packet)
        {
            packet.ReadBit("LegacyRulesActive");
        }
    }
}
