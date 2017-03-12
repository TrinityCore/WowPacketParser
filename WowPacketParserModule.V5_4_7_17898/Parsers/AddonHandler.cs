using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_7_17898.Parsers
{
    public static class AddonHandler
    {
        [Parser(Opcode.CMSG_ADDON_REGISTERED_PREFIXES)]
        public static void MultiplePackets(Packet packet)
        {
            var count = packet.ReadBits("Count", 24);
            var lengths = new int[count];
            for (var i = 0; i < count; ++i)
                lengths[i] = (int)packet.ReadBits(5);

            for (var i = 0; i < count; ++i)
                packet.ReadWoWString("Addon", lengths[i], i);
        }
    }
}
