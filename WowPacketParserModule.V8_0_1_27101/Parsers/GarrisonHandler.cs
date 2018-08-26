using System.Reflection.Emit;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V8_0_1_27101.Parsers
{
    public static class GarrisonHandler
    {
        [Parser(Opcode.CMSG_GARRISON_CHECK_UPGRADEABLE)]
        [Parser(Opcode.CMSG_GARRISON_REQUEST_CLASS_SPEC_CATEGORY_INFO)]
        public static void HandleGarrisonGarrSiteID(Packet packet)
        {
            packet.ReadInt32("GarrSiteID");
        }
    }
}
