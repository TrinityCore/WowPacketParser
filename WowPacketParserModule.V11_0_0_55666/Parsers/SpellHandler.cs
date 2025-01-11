using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V11_0_0_55666.Parsers
{
    public static class SpellHandler
    {
        [Parser(Opcode.SMSG_SPELL_VISUAL_LOAD_SCREEN)]
        public static void HandleSpellVisualLoadScreen(Packet packet)
        {
            packet.ReadInt32("SpellVisualKitID");
            packet.ReadInt32("Delay");
        }
    }
}
