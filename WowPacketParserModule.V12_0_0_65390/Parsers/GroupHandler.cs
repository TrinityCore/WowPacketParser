using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V12_0_0_65390.Parsers
{
    public static class GroupHandler
    {
        [Parser(Opcode.CMSG_REQUEST_PARTY_MEMBER_STATS)]
        public static void HandleRequestPartyMemberStats(Packet packet)
        {
            var hasPartyIndex = packet.ReadBit("HasPartyIndex");
            var targetCount = packet.ReadUInt32();

            if (hasPartyIndex)
                packet.ReadByte("PartyIndex");

            for (var i = 0u; i < targetCount; ++i)
                packet.ReadPackedGuid128("Target", i);
        }
    }
}
