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

        [Parser(Opcode.SMSG_LOOT_MONEY_NOTIFY, ClientVersionBuild.V8_1_5_29683)]
        public static void HandleLootMoneyNotify(Packet packet)
        {
            packet.ReadUInt64("Money");
            packet.ReadUInt64("Mod");
            packet.ResetBitReader();
            packet.ReadBit("SoleLooter");
        }
    }
}
