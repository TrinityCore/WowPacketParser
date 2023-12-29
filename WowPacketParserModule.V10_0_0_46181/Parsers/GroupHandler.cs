
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V10_0_0_46181.Parsers
{
    public static class GroupHandler
    {
        [Parser(Opcode.CMSG_DO_COUNTDOWN, ClientVersionBuild.V10_1_7_51187)]
        public static void HandleDoCountdown(Packet packet)
        {
            var hasPartyIndex = packet.ReadBit("HasPartyIndex");
            packet.ReadInt32("TotalTime");
            if (hasPartyIndex)
                packet.ReadByte("PartyIndex");
        }
    }
}
