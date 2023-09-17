using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V10_0_0_46181.Parsers
{
    public static class RecruitAFriendHandler
    {
        [Parser(Opcode.CMSG_GET_RAF_ACCOUNT_INFO)]
        public static void HandleGetRAFAccountInfo(Packet packet)
        {
            packet.ReadUInt32("UnkInt32");
        }
    }
}
