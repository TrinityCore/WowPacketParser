using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_2_17658.Parsers
{
    public static class ReputationHandler
    {
        [Parser(Opcode.CMSG_RESET_FACTION_CHEAT)]
        public static void HandleResetFactionCheat(Packet packet)
        {
            packet.Translator.ReadUInt32("Faction Id");
            packet.Translator.ReadUInt32("Unk Int");
        }

        [Parser(Opcode.SMSG_INITIALIZE_FACTIONS)]
        public static void HandleInitializeFactions(Packet packet)
        {
            for (var i = 0; i < 256; i++)
            {
                packet.Translator.ReadUInt32E<ReputationRank>("Faction Standing", i);
                packet.Translator.ReadByteE<FactionFlag>("Faction Flags", i);
            }

            for (var i = 0; i < 256; i++)
                packet.Translator.ReadBit("Count", i);
        }
    }
}
