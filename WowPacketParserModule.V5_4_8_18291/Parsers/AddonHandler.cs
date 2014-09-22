using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_8_18291.Parsers
{
    public static class AddonHandler
    {
        [Parser(Opcode.SMSG_ADDON_INFO)]
        public static void HandleServerAddonsList(Packet packet)
        {

        }
    }
}
