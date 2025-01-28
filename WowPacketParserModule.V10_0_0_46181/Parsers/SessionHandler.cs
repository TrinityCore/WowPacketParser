using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V10_0_0_46181.Parsers
{
    public static class SessionHandler
    {
        [Parser(Opcode.SMSG_SET_TIME_ZONE_INFORMATION)]
        public static void HandleSetTimeZoneInformation(Packet packet)
        {
            var gameTimeTZLength = packet.ReadBits(7);
            var serverTimeTZLength = packet.ReadBits(7);
            var serverRegionalTimeTZLength = packet.ReadBits(7);

            packet.ReadWoWString("GameTimeTZ", gameTimeTZLength);
            packet.ReadWoWString("ServerTimeTZ", serverTimeTZLength);
            packet.ReadWoWString("ServerRegionalTimeTZ", serverRegionalTimeTZLength);
        }
    }
}
