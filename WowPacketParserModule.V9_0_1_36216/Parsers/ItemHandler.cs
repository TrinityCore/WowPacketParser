using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V9_0_1_36216.Parsers
{
    public static class ItemHandler
    {
        [Parser(Opcode.CMSG_USE_TOY)]
        public static void HandleUseToy(Packet packet)
        {
            SpellHandler.ReadSpellCastRequest(packet, "Cast");
        }
    }
}
