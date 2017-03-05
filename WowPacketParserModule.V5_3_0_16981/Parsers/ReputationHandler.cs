using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_3_0_16981.Parsers
{
    public static class ReputationHandler
    {
        [Parser(Opcode.SMSG_INITIALIZE_FACTIONS)]
        public static void HandleInitializeFactions(Packet packet)
        {
            for (var i = 0; i < 256; i++)
            {
                packet.Translator.ReadUInt32E<ReputationRank>("Faction Standing", i);
                packet.Translator.ReadByteE<FactionFlag>("Faction Flags", i);
            }

            for (var i = 0; i < 256; i++)
            {
                var bit1296 = packet.Translator.ReadBit("Count");
            }
        }
    }
}
